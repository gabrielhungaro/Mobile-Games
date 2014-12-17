using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Sound;

using Signals;


namespace AquelaFrameWork.View
{
    public abstract class AMovieClip : AFObject, IAnimatable
    {
        [SerializeField]
        protected bool m_loop;

        [SerializeField]
        protected bool m_playing;

        [SerializeField]
        protected int m_currentFrame;

        [SerializeField]
        protected float m_defaultFrameDuration;

        [SerializeField]
        protected double m_currentTime;

        protected List<Sprite> m_sprites;
        protected List<AFSound> m_sounds;
        protected List<double> m_durations;
        protected List<double> m_startTimes;

        public Signal<bool> OnComplete = new Signal<bool>();

        protected ISpriteContainer m_spriteRender;

        protected string m_name = "";

        virtual public void SetSpriteCotnainer( ISpriteContainer spriteContainer )
        {
            m_spriteRender = spriteContainer;
        }

        virtual public void Init(UnityEngine.Sprite[] sprites, float fps = 12, string name = "")
        {
            //m_spriteRender = this.gameObject.AddComponent<SpriteRenderer>() as ISpriteContainer;

            if (sprites.Length == 0)
            {
                throw new Exception("Empty texture array");
            }
            else if (fps <= 0)
            {
                throw new Exception("Invalid fps: " + fps);
            }

            int numFrames = sprites.Length;

            m_defaultFrameDuration = 1.0f / fps;
            m_loop = true;
            m_playing = true;
            m_currentTime = 0.0f;
            m_currentFrame = 0;
            m_sprites = new List<Sprite>(numFrames);
            m_sounds = new List<AFSound>(numFrames);
            m_startTimes = new List<double>(numFrames);
            m_durations = new List<double>(numFrames);

            for (int i = 0; i < numFrames; ++i)
            {
                AddFrame(sprites[i]);
            }

            m_spriteRender.sprite = m_sprites[0];

            SetName(name);
        }

        public string GetName()
        {
            return m_name;
        }

        public void SetName( string name )
        {
            m_name = name;
        }

        public void AdvanceTime(double time)
        {
            if (!m_playing || !this.gameObject.activeSelf || time <= 0.0f) return;


            int finalFrame = 0;
            int previousFrame = m_currentFrame;
            double restTime = 0.0f;

            bool breakAfterFrame = false;
            bool dispatchCompleteEvent = false;
            double totalTime = GetTotalTime();


            if (m_loop && m_currentTime >= totalTime)
            {
                m_currentTime = 0.0f;
                m_currentFrame = 0;
            }


            if (m_currentTime < totalTime)
            {
                m_currentTime += time;
                finalFrame = m_sprites.Count - 1;

                while (m_currentTime > m_startTimes[m_currentFrame] + m_durations[m_currentFrame])
                {
                    if (m_currentFrame == finalFrame)
                    {
                        if (m_loop && OnComplete.NumListeners == 0)
                        {
                            m_currentTime -= totalTime;
                            m_currentFrame = 0;
                        }
                        else
                        {
                            breakAfterFrame = true;
                            restTime = m_currentTime - totalTime;
                            dispatchCompleteEvent = true;
                            m_currentFrame = finalFrame;
                            m_currentTime = totalTime;

                        }
                    }
                    else
                    {
                        m_currentFrame++;
                    }

                    AFSound sound = m_sounds[m_currentFrame];
                    if (sound) sound.Play();

                    if (breakAfterFrame) break;
                }

                if (m_currentFrame == finalFrame && m_currentTime == totalTime)
                    dispatchCompleteEvent = true;
            }


            if (m_currentFrame != previousFrame)
                m_spriteRender.sprite = m_sprites[m_currentFrame];

            if (dispatchCompleteEvent)
                OnComplete.Dispatch(true);

            if (m_loop && restTime > 0.0f)
            {
                AdvanceTime(restTime);
            }
        }

        public void AddFrame(Sprite sprite, AFSound sound = null, double duration = -1.0f)
        {
            AddFrameAt(GetTotalFrames(), sprite, sound, duration);
        }

        public void AddFrameAt(int frameID, Sprite sprite, AFSound sound = null, double duration = -1.0f)
        {
            if (frameID < 0 || frameID > GetTotalFrames()) throw new ArgumentException("Invalid frame id");
            if (duration < 0) duration = m_defaultFrameDuration;

            m_sprites.Insert(frameID, sprite);
            m_sounds.Insert(frameID, sound);
            m_durations.Insert(frameID, duration);
            m_startTimes.Insert(frameID, 0);

            if (frameID > 0 && frameID == GetTotalFrames() - 1)
            {
                m_startTimes[frameID] = m_startTimes[(int)(frameID - 1)] + m_durations[(int)(frameID - 1)];
            }
            else
            {
                UpdateStartTimes();
            }
        }

        public void GotoAndPlay(int frameID)
        {
            if (frameID < 0 || frameID > GetTotalFrames()) throw new ArgumentException("Invalid frame id");
            m_currentFrame = frameID;
            m_spriteRender.sprite = m_sprites[m_currentFrame];
            Play();
        }
        public void GotoAndStop(int frameID)
        {
            if (frameID < 0 || frameID > GetTotalFrames()) throw new ArgumentException("Invalid frame id");
            m_currentFrame = frameID;
            m_spriteRender.sprite = m_sprites[m_currentFrame];
            Stop();
        }

        public void Play()
        {
            m_playing = true;
        }

        public void Stop()
        {
            m_playing = false;
            m_currentFrame = 0;
            m_currentTime = 0;
            m_spriteRender.sprite = m_sprites[m_currentFrame];
        }

        public void Pause()
        {
            m_playing = false;
        }

        public override void AFUpdate(double time)
        {
            AdvanceTime(time);           
        }

        virtual public void UpdateSpriteContainer()
        {
            throw new InvalidOperationException("This function must be overridden");
        }

        private void UpdateStartTimes()
        {
            int numFrames = GetTotalFrames();

            m_startTimes[0] = 0;

            if (numFrames > 1)
            {
                for (int i = 1; i < numFrames; ++i)
                {
                    m_startTimes[i] = m_startTimes[i - 1] + m_durations[i - 1];
                }
            }
            else
            {
                m_startTimes[0] += m_durations[0];
            }
        }


        public Sprite GetSprite()
        {
            return m_sprites[m_currentFrame];
        }

        public Texture2D GetTexture()
        {
            return m_sprites[m_currentFrame].texture;
        }

        public double GetTotalTime()
        {
            int numFrames = GetTotalFrames() - 1;
            return m_startTimes[numFrames] + m_durations[numFrames];
        }

        public int GetTotalFrames()
        {
            return m_sprites.Count;
        }

        public Sprite GetFrameTexture(int frameID)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");

            return m_sprites[frameID];
        }

        public void SetFrameTexture(int frameID, Sprite texture)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");
            m_sprites[frameID] = texture;
        }

        public AFSound GetFrameSound(int frameID)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");

            return m_sounds[frameID];
        }

        public void SetFrameSound(int frameID, AFSound sound)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");
            m_sounds[frameID] = sound;
        }

        public double GetFrameDuration(int frameID)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");

            return m_durations[frameID];
        }

        public void SetFrameDuration(int frameID, float duration)
        {
            if (frameID < 0 || frameID >= GetTotalFrames()) throw new ArgumentException("Invalid frame number");
            m_durations[frameID] = duration;
        }


        public bool GetIsLoop()
        {
            return m_loop;
        }

        public void SetIsLoop(bool value)
        {
            m_loop = value;
        }

        public Sprite GetCurrentFrameSprite()
        {
            return m_spriteRender.sprite;
        }

        public int GetCurrentFrameNumber()
        {
            return m_currentFrame;
        }

        public bool isPlaying()
        {
            if (m_playing)
                return m_loop || m_currentTime < GetTotalTime();

            return false;
        }

        public bool IsComplete()
        {
            return !m_loop && m_currentTime >= GetTotalTime();
        }
    }
}
