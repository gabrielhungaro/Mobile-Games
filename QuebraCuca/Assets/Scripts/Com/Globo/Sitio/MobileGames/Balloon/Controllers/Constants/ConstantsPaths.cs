using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class ConstantsPaths
    {
        private static string _choosedDevice = IOS_PATH;

        public const string IOS_PATH = "IOs/";
        public const string ANDROID_PATH = "Android/";
        public const string WINDOS_PATH = "WindowsPhone/";

        private static string _folderPath = "Images/";
        private static string _balloonAnimPath = "Animations/";
        private static string _balloonPath = "Balloons/Padrao/";
        private static string _inGamePath = "Screens/InGame/";
        private static string _resultPath = "Screens/ResultScreen/";
        private static string _startPath = "Screens/StartScreen/";
        private static string _chooseModePath = "Screens/ChooseModeScreen/";
        private static string _creditsPath = "Screens/CreditsScreen/";

        public void Start()
        {
            //_choosedDevice = IOS_PATH;
            if (Screen.width < 800 && Screen.height < 480)
            {
                Debug.Log("LOW RESOLUTION DETECTED");
            }
            else if (Screen.width == 800 && Screen.height == 480)
            {
                Debug.Log("MEDIUM RESOLUTION DETECTED");
            }
            else
            {
                Debug.Log("HIGH RESOLUTION DETECTED");
            }
            Debug.Log("WIDTH: " + Screen.width + " HEIGHT: " + Screen.height);
        }

        public static string GetBalloonsPath()
        {
            return _folderPath + _choosedDevice + _balloonPath;
        }

        public static string GetStartPath()
        {
            return _folderPath + _choosedDevice + _startPath;
        }

        public static string GetInGamePath()
        {
            return _folderPath + _choosedDevice + _inGamePath;
        }

        public static string GetResultPath()
        {
            return _folderPath + _choosedDevice + _resultPath;
        }

        public static string GetChooseModePath()
        {
            return _folderPath + _choosedDevice + _chooseModePath;
        }

        public static string GetCreditsPath()
        {
            return _folderPath + _choosedDevice + _creditsPath;
        }

        public static string GetBalloonAnimationsFolder()
        {
            return _folderPath + _choosedDevice + _balloonAnimPath;
        }

    }
}

