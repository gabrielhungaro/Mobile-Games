using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class ResolutionController
    {
        private static string LOW_RESOLUTION = "Low";
        private static string MEDIUM_RESOLUTION = "Medium";
        private static string HIGH_RESOLUTION = "High";
        private static string RESOLUTION;

        private static volatile ResolutionController _instance;
        private static object _lock = new object();

        static ResolutionController() { }

        public static ResolutionController Instance()
        {
            if(_instance == null){
                lock (_lock)
                {
                    if (_instance == null) _instance = new ResolutionController();
                }
            }
            return _instance;
        }

        private ResolutionController()
        {
            CheckResolution();
        }

        public void CheckResolution()
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            if (screenWidth < 800 && screenHeight < 480)
            {
                RESOLUTION = LOW_RESOLUTION;
            }
            else if (screenWidth == 800 && screenHeight == 480)
            {
                RESOLUTION = MEDIUM_RESOLUTION;
            }
            else
            {
                RESOLUTION = HIGH_RESOLUTION;
            }

            RESOLUTION = HIGH_RESOLUTION;
        }

        public static string GetResolution()
        {
            return RESOLUTION;
        }

    }
}
