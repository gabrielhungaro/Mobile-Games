using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {

        public static int CAMERA_HEIGHT = 1536;
        public static bool ORTHOGRAPHIC = true;
        public static float ORTHOGRAPHIC_SIZE = 1f;
        public static float NEAR_CLIP_PLANE = -10f;
        public static float FAR_CLIP_PLANE = 10f;

        void Start()
        {

        }

    }
}
