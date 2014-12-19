using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class ConstantsAnimations
    {
        private static string _folderPath = "Audios/";
        private static string _musicFolder = "Musics/";
        private static string _sfxsFolder = "Sfxs/";

        public static string IDLE = "idle";
        public static string EXPLODE = "explode";


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

