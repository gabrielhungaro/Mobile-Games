using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controllers;
using UnityEngine;

namespace Constants
{
    public class PathConstants
    {
        private static volatile PathConstants _instance;
        private static object _lock = new object();

        public static string _remotePath = "";
        private static string _choosedDevice;
        private static string IOs = "IOs";
        private static string ANDROID = "Android";
        private static string _deviceResolution;
        private static string _scenesPath = "Scenes/";
        public static string _startScenePath = "StartScene/";
        public static string _gameScenePath = "GameScene/";
        public static string _fontPath = "Fonts/";

        static PathConstants() { }

        public static PathConstants Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new PathConstants();
                }
            }
            return _instance;
        }

        private PathConstants()
        {
            MountPaths();
        }

        public void MountPaths()
        {
            _choosedDevice = IOs;
            _deviceResolution = ResolutionController.GetResolution();
            _remotePath = _choosedDevice + "/" + _deviceResolution + "/";
        }

        public static string GetStartScenePath()
        {
            return _remotePath + _scenesPath + _startScenePath;
        }

        public static string GetGameScenePath()
        {
            return _remotePath + _scenesPath + _gameScenePath;
        }
    }
}
