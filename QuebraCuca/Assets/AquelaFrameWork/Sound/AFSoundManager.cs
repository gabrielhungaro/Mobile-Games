// /////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Sound Manager.
//
// /////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using AquelaFrameWork.Core;

using Signals;

namespace AquelaFrameWork.Sound
{
    public class AFSoundManager : ASingleton<AFSoundManager>
    {
        private Dictionary<string, AFSoundGroup> m_groups = new Dictionary<string, AFSoundGroup>();
        private Dictionary<string, AFSound> m_audios = new Dictionary<string, AFSound>();

        public readonly Signal<bool> OnMute = new Signal<bool>();
        public readonly Signal<AFSound> OnAudioPlay = new Signal<AFSound>();
        public readonly Signal<AFSound> OnAudioAdd = new Signal<AFSound>();
        public readonly Signal<string> OnAudioRemove = new Signal<string>();
        public readonly Signal<AFSound> OnAudioPause = new Signal<AFSound>();
        public readonly Signal<AFSound> OnAudioStop = new Signal<AFSound>();

        public int audiosInList = 0;
        protected bool m_mute = false;


        void Awake()
        {
            gameObject.transform.parent = AFEngine.Instance.gameObject.transform;
        }

        public AFSound Add( string name )
        {
            //TODO: GetFrom AssetManager
            UnityEngine.Debug.LogWarning("ADD SOUND FROM ASSET MANAGER >>>>> NOT IMPLEMENTED" );
            return null;
        }

        public AFSound Add(string name, AudioClip audioClip, Transform emitter = null, float volume = 1.0f , float pitch = 1.0f , bool loop = false)
        {
            AFSound audio;

            if( name != null && name.Length > 0 )
            {
                if (m_audios.ContainsKey(name))
                {
                    audio = m_audios[name];
                    UnityEngine.Debug.LogWarning("The audio was not added, because the name already in list of audios");
                }
                else
                {
                    audio = new AFSound(name, audioClip, emitter, volume, pitch, loop);
                    m_audios[name] = audio;

                    OnAudioAdd.Dispatch(audio);
                }
            }
            else
            {
                throw new Exception("The name of sound was not valid");
            }

            audiosInList++;

            return audio;
        }

        public void Remove(string name)
        {
            if (m_audios.ContainsKey(name))
            {
                Remove(m_audios[name]);
                OnAudioRemove.Dispatch(name);
            }
            else
            {
                UnityEngine.Debug.LogWarning("The name was not found in the audio list");
            }
        }

        public void Remove(AFSound audio)
        {
            audio.Destroy();
            m_audios.Remove(audio.GetName());
            audiosInList--;
        }

        public void RemoveAll(string name)
        {
            foreach( KeyValuePair<string, AFSound> ent in m_audios )
            {
                Remove(ent.Value);
            }

            audiosInList = 0;
        }
        

        public AudioSource Stop(string name)
        {
            if (!m_audios.ContainsKey(name))
                throw new Exception("Audio not exists");


            return Stop(m_audios[name]);
        }

        public AudioSource Stop(AFSound sound)
        {
            OnAudioStop.Dispatch(sound);
            return sound.Stop();
        }


        public void StopAll()
        {
            foreach( KeyValuePair<string, AFSound> keypair in m_audios )
            {
                Stop(keypair.Value);
            }
        }

        public AFSound GetAudio( string name )
        {
            if( m_audios.ContainsKey(name) )
                return m_audios[name];

            return null;
        }

        public AudioSource Pause(string name)
        {
            if (!m_audios.ContainsKey(name))
                throw new Exception("Audio not exists");

            return Pause(m_audios[name]);
        }

        private AudioSource Pause(AFSound sound)
        {
            if (!m_audios.ContainsKey(name))
                throw new Exception("Audio not exists");

            OnAudioPause.Dispatch(sound);
            return sound.Pause();
        }

        public void PauseAll()
        {
            foreach (KeyValuePair<string, AFSound> entry in m_audios)
            {
                Pause(entry.Value);
            }
        }

        public AudioSource Play( string name )
        {
            if (!m_audios.ContainsKey(name))
                throw new Exception("Audio not exists");

            AFSound audio = m_audios[name];
            OnAudioPlay.Dispatch(audio);

            return audio.Play();
        }

        public AudioSource Play(AudioClip clip)
        {
            return Play(clip, 1f, 1f, null, false);
        }

        public AudioSource Play(AudioClip clip, Transform emitter)
        {
            return Play(clip, 1f, 1f, emitter, false);
        }

        public AudioSource Play(AudioClip clip, float volume, Transform emitter, bool loop = false)
        {
            return Play(clip, volume, 1f, emitter, loop);
        }

        public AudioSource Play(AudioClip clip, Vector3 point)
        {
            return Play(clip, point, 1f, 1f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume)
        {
            return Play(clip, point, volume, 1f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch, bool loop = false)
        {
            return Play(clip, volume, pitch, null, loop, point);
        }

        public AudioSource Play(AudioClip clip, float volume, float pitch, Transform emitter, bool loop)
        {
            return Play( clip, volume, pitch, emitter, loop, Vector3.zero );
        }

        public AudioSource Play(AudioClip clip, float volume, float pitch, Transform emitter, bool loop, Vector3 point)
        {
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = point;

            if (emitter != null)
            {
                go.transform.position = emitter.position;
                go.transform.parent = emitter;
            }


            AudioSource source = go.AddComponent<AudioSource>();

            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;

            source.Play();

            Destroy(go, clip.length);

            return source;
        }

        public bool GetMute()
        {
            return m_mute;
        }

        public void SetMute(bool value)
        {

            if (value != m_mute)
            {
                foreach (KeyValuePair<string, AFSound> keypair in m_audios)
                {
                    keypair.Value.GetAudioSource().mute = value;
                }
            }

            OnMute.Dispatch(m_mute);

            m_mute = value;
        }


    }
}