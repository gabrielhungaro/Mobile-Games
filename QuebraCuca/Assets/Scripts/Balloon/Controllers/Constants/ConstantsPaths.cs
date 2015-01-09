using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class ConstantsPaths
    {
        private static volatile ConstantsPaths _instance;
        private static object _lock = new object();

        private static string _choosedDevice = IOS_PATH;

        public const string IOS_PATH = "IOs/";
        public const string ANDROID_PATH = "Android/";
        public const string WINDOS_PATH = "WindowsPhone/";
        public const string RESOLUTION_PATH = "High/";

        private static string _balloonAnimPath = "Animations/";
        private static string _balloonPath = "Balloons/";
        private static string _inGamePath = "Scenes/InGame/";
        private static string _resultPath = "Scenes/ResultScreen/";
        private static string _startPath = "Scenes/StartScreen/";
        private static string _chooseModePath = "Scenes/ChooseModeScreen/";
        private static string _creditsPath = "Scenes/CreditsScreen/";

        static ConstantsPaths() { }

        public static ConstantsPaths Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new ConstantsPaths();
                }
            }
            return _instance;
        }

        private ConstantsPaths()
        {
        }

        public static string GetBalloonsPath()
        {
            return _inGamePath + _balloonPath;
        }

        public static string GetStartPath()
        {
            return _startPath;
        }

        public static string GetInGamePath()
        {
            return _inGamePath;
        }

        public static string GetResultPath()
        {
            return _resultPath;
        }

        public static string GetChooseModePath()
        {
            return _chooseModePath;
        }

        public static string GetCreditsPath()
        {
            return _creditsPath;
        }

        public static string GetBalloonAnimationsFolder()
        {
            return _inGamePath + _balloonPath;
        }

    }
}

