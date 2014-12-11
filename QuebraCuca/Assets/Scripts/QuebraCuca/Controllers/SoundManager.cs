using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class SoundManager : MonoBehaviour
    {

        public bool backgroundSoundState = true;
        public bool sfxSoundState = true;
        public static bool soundsIsMute = false;
        public const int TYPE_MUSIC = 0;
        public const int TYPE_SFX = 1;

        public static Dictionary<int, List<GameObject>> audioDic;
        public List<GameObject> listOfBackgroundSounds;
        public List<GameObject> listOfSfxSounds;

        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public void Init()
        {
            //this.name = "SoundManager";
            Debug.Log("[ SOUND_MANAGER ] - START");
            this.gameObject.AddComponent<AudioListener>();
            //Dictionary of sound's lists and types
            audioDic = new Dictionary<int, List<GameObject>>();
            //list of background's sounds
            listOfBackgroundSounds = new List<GameObject>();
            //list os sfx's sounds
            listOfSfxSounds = new List<GameObject>();

            //add list os audio and your type in dictionary
            audioDic.Add(TYPE_MUSIC, listOfBackgroundSounds);
            audioDic.Add(TYPE_SFX, listOfSfxSounds);

            //Resources.LoadAll ("Audio/Music/");
            //Resources.FindObjectsOfTypeAll<AudioClip> ()[0]
            this.Add(SoundConstants.BG_SOUND, Resources.Load<AudioClip>(SoundConstants._musicPath + SoundConstants.BG_SOUND), TYPE_MUSIC, .8f, true);
            this.Add(SoundConstants.SFX_BUTTON, Resources.Load<AudioClip>(SoundConstants._sfxPath + SoundConstants.SFX_BUTTON), TYPE_MUSIC, .5f, false);
            this.Add(SoundConstants.SFX_CORRECT_HIT, Resources.Load<AudioClip>(SoundConstants._sfxPath + SoundConstants.SFX_CORRECT_HIT), TYPE_MUSIC, .5f, false);
            this.Add(SoundConstants.SFX_HURT, Resources.Load<AudioClip>(SoundConstants._sfxPath + SoundConstants.SFX_HURT), TYPE_MUSIC, .5f, false);
            this.Add(SoundConstants.SFX_WRONG_HIT, Resources.Load<AudioClip>(SoundConstants._sfxPath + SoundConstants.SFX_WRONG_HIT), TYPE_MUSIC, .5f, false);
            
            //this.PlaySoundByName(ConstantsSounds.BG_SOUND);
        }

        // Update is called once per frame
        void Update()
        {

        }
        //function with add a sound in your list by type
        public void Add(string name, AudioClip clip, int type, float volume, bool loop)
        {
            //create GameObject to associate a audio
            GameObject gameObj = new GameObject();
            gameObj.name = name + "_Sound";
            gameObj.AddComponent<AudioSource>().audio.clip = clip;
            gameObj.audio.volume = volume;
            gameObj.audio.loop = loop;
            //this line dont allow the gameObject will be destroyed onChange scene
            GameObject.DontDestroyOnLoad(gameObj);
            //verification type of audio to add to the list
            switch (type)
            {
                case TYPE_MUSIC:
                    listOfBackgroundSounds.Add(gameObj);
                    break;
                case TYPE_SFX:
                    listOfSfxSounds.Add(gameObj);
                    break;
            }
        }

        public List<GameObject> getListOfSoundsByType(int type)
        {
            return audioDic[type];
        }

        public GameObject getSoundByName(string name)
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].name == name + "_Sound")
                    {
                        return list[j];
                    }
                }
            }
            Debug.LogError("[ SOUND_MANAGER ] - Nao existe nenhum som com o nome: " + name + "_Sound");
            return null;
        }

        public static void PlaySoundByName(string name)
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].name == name + "_Sound")
                    {
                        list[j].audio.Play();
                        return;
                    }
                }
            }
            Debug.LogError("[ SOUND_MANAGER ] - Nao existe nenhum som com o nome: " + name + "_Sound");
        }

        public void PauseSoundByName(string name)
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].name == name)
                    {
                        list[j].audio.Pause();
                        return;
                    }
                }
            }
            Debug.LogError("[ SOUND_MANAGER ] - Nao existe nenhum som com o nome: " + name);
        }

        public void StopSoundByName(string name)
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].name == name)
                    {
                        list[j].audio.Stop();
                        return;
                    }
                }
            }
            Debug.LogError("[ SOUND_MANAGER ] - Nao existe nenhum som com o nome: " + name);
        }

        public void StopAllSounds()
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    list[j].audio.Stop();
                }
            }
        }

        public void PauseAllSounds()
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    list[j].audio.Pause();
                }
            }
        }

        public static void MuteAllSounds()
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    list[j].audio.mute = true;
                }
            }
            soundsIsMute = true;
        }

        public static void UnMuteAllSounds()
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    list[j].audio.mute = false;
                }
            }
            soundsIsMute = false;
        }

        public static void MuteSoundByName(string name)
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].name == name)
                    {
                        list[j].audio.mute = true;
                        return;
                    }
                }
            }
            Debug.LogError("[ SOUND_MANAGER ] - Nao existe nenhum som com o nome: " + name);
        }

        public static void UnMuteSoundByName(string name)
        {
            List<GameObject> list;
            for (int i = 0; i < audioDic.Count; i++)
            {
                list = audioDic[i];
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j].name == name)
                    {
                        list[j].audio.mute = false;
                        return;
                    }
                }
            }
            Debug.LogError("[ SOUND_MANAGER ] - Nao existe nenhum som com o nome: " + name);
        }

        public void MuteSoundsByType(int type)
        {
            List<GameObject> list;
            list = audioDic[type];
            for (int i = 0; i < list.Count; i++)
            {
                list[i].audio.mute = true;
                //return;
            }
            //Debug.LogError ("Nao existe nenhum som com o nome: " + name);
        }

        public void UnMuteSoundsByType(int type)
        {
            List<GameObject> list;
            list = audioDic[type];
            for (int i = 0; i < list.Count; i++)
            {
                list[i].audio.mute = false;
            }
        }

        public static bool GetAllSoundsIsMuting()
        {
            return soundsIsMute;
        }

        public void OnClickSoundController()
        {
            backgroundSoundState = !backgroundSoundState;
            if (!backgroundSoundState)
            {
                MuteSoundsByType(TYPE_MUSIC);
            }
            else
            {
                UnMuteSoundsByType(TYPE_MUSIC);
            }
        }

        public void OnClickdSfxController()
        {
            sfxSoundState = !sfxSoundState;
            if (!sfxSoundState)
            {
                MuteSoundsByType(TYPE_SFX);
            }
            else
            {
                UnMuteSoundsByType(TYPE_SFX);
            }
        }
    }
}