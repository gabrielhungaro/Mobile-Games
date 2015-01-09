using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using com.globo.sitio.mobilegames.Balloon;
using com.globo.sitio.mobilegames.Commom;

namespace com.globo.sitio.mobilegames.Balloon.Elements
{
    public class MyCamera : MonoBehaviour
    {
        private GameObject _camera;

        void Awake()
        {
            _camera = new GameObject();
            _camera.name = "Camera";
            _camera.gameObject.transform.parent = this.gameObject.transform;
            _camera.AddComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            _camera.GetComponent<Camera>().isOrthoGraphic = CameraController.ORTHOGRAPHIC;
            _camera.GetComponent<Camera>().orthographicSize = CameraController.ORTHOGRAPHIC_SIZE;
            _camera.GetComponent<Camera>().nearClipPlane = CameraController.NEAR_CLIP_PLANE;
            _camera.GetComponent<Camera>().farClipPlane = CameraController.FAR_CLIP_PLANE;
            _camera.AddComponent<CameraSizeHandler>();
        }

        void Start()
        {
            
        }

        public GameObject GetUiRoot()
        {
            return this.gameObject;
        }

        public Camera GetCamera()
        {
            return _camera.GetComponent<Camera>();
        }

    }
}
