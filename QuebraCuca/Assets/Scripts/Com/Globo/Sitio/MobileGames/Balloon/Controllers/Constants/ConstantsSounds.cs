using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class ConstantsSounds
    {
        private static string _folderPath = "Audios/";
        private static string _musicFolder = "Musics/";
        private static string _sfxsFolder = "Sfxs/";

        public static string BG_SOUND = "bgSound";
        public static string SFX_BUTTON = "Button";
        public static string SFX_ADD_POINT = "AddPoint";
        public static string SFX_REMOVE_POINT = "RemovePoint";
        public static string SFX_ADD_TIME = "AddTime";
        public static string SFX_REMOVE_TIME = "RemoveTime";
        public static string SFX_NUCLEAR_EXPLOSIVE = "NuclearExplosive";
        public static string SFX_SLOW_MOTION = "SlowMotion";
        public static string SFX_FAST_FOWARD = "FastFoward";
        public static string SFX_EXPLOSIVE = "Explosive";

        public void Start()
        {
            //_choosedDevice = IOS_PATH;
        }

        public static string GetMusicPath()
        {
            return _folderPath + _musicFolder;
        }

        public static string GetSfxsPath()
        {
            return _folderPath + _sfxsFolder;
        }

    }
}

