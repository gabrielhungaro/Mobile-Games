using System;
using System.Collections.Generic;

using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;

namespace AquelaFrameWork.Sound
{
    public class AFSound : AFObject
    {
        protected float m_volume;
        protected float m_pitch;
        protected string m_name;
        protected string m_path;
        
        protected AFSoundGroup m_group;
        protected AudioClip m_audioClip;
        protected AudioSource m_audioSource;
        protected Transform m_emitter;
        protected Vector3 m_point;
        protected bool m_isPlaying;
        protected bool m_loop;
        protected bool m_oncePertime;

        protected AFSoundManager m_manager;


        public static AFSound Create( 
            string name, 
            AudioClip audio,
            Transform emitter = null,
            float volume = 1.0f,
            float pitch = 1.0f,
            bool loop = false,
            bool playOncePertime = true)
        {
            AFSound L_sound = AFObject.Create<AFSound>(name);
            L_sound.Initialize(name, audio, emitter, volume, pitch, loop, playOncePertime);
            return L_sound;
        }

        public static AFSound Create( 
           string path,
           string name = "",
           Transform emitter = null,
           float volume = 1.0f,
           float pitch = 1.0f,
           bool loop = false,
            bool playOncePertime = true)
        {
            if (name.Equals(""))
                name = path;

            AFSound L_sound = AFObject.Create<AFSound>(name);
            AudioClip L_audioClip = AFAssetManager.Instance.Load<AudioClip>(path);
            L_sound.Initialize(name, L_audioClip ,emitter, volume, pitch, loop, playOncePertime);
            return L_sound;
        }


        public AFSound Initialize( 
            string name, 
            AudioClip audio,
            Transform emitter = null,
            float volume = 1.0f,
            float pitch = 1.0f,
            bool loop = false,
            bool playOncePertime = true)
        {
            m_name = name;
            m_volume = volume;
            m_pitch = pitch;
            m_emitter = emitter;
            m_point = Vector3.zero;
            m_audioClip = audio;
            m_isPlaying = false;
            m_loop = loop;
            m_oncePertime = playOncePertime;
            m_manager = AFSoundManager.Instance;

            return this;
            //Abandon all hope ye who enter beyond this point
            //I'm sorry, but our princess is in another castle.
        }

        public AudioSource Stop()
        {
            if( GetIsPlaying() )
            {
                m_audioSource.Stop();
            }

            return m_audioSource;
        }

        public AudioSource Pause()
        {
            if (GetIsPlaying())
            {
                m_audioSource.Pause();
            }

            return m_audioSource;
        }


        public AudioSource Play()
        {
            if (!GetIsPlaying() || !isOncePerTime() )
            {
                m_audioSource = m_manager.Play(m_audioClip, m_volume, m_pitch, m_emitter, m_loop, m_point);
            }
            else
            {
                UnityEngine.Debug.LogWarning(" The sound already has played ");
            }

            return m_audioSource;
        }

        public bool isOncePerTime()
        {
            return m_oncePertime;
        }

        public AudioSource GetAudioSource()
        {
            return m_audioSource;
        }

        public bool GetIsPlaying()
        {
            if (m_audioSource != null)
                m_isPlaying = m_audioSource.isPlaying;
            else
                m_isPlaying = false;

            return m_isPlaying;
        }

        public string GetName()
        {
            return m_name;
        }

        public AudioClip GetAudioClip()
        {
           return m_audioClip;
        }


        public void Destroy()
        {
            m_group = null;
            m_audioClip = null;
            m_audioSource = null;
            m_emitter = null;
        }

    }
}
