using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Controllers;

namespace com.globo.sitio.mobilegames.QuebraCuca.Constants
{
    public class SoundConstants
    {
        private static volatile SoundConstants _instance;
        private static object _lock = new object();

        public static string _remotePath = "QuebraCuca/";
        private static string soundPath = "Sounds/";
        public static string _musicPath;
        public static string _sfxPath;

        public static string BG_SOUND = "bgSound";
        public static string SFX_BUTTON = "sfxButton";
        public static string SFX_CORRECT_HIT = "sfxCorrectHit";
        public static string SFX_WRONG_HIT = "sfxWrongHit";
        public static string SFX_HURT = "sfxHurt";

        static SoundConstants() { }

        public static SoundConstants Instance()
        {
            if(_instance == null){
                lock (_lock)
                {
                    if (_instance == null) _instance = new SoundConstants();
                }
            }
            return _instance;
        }

        private SoundConstants()
        {
            MountPaths();
        }

        public void MountPaths()
        {
            _remotePath += soundPath;
            _musicPath = _remotePath + "Music/";
            _sfxPath = _remotePath + "Sfx/";
        }
    }
}
