using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Controllers;

namespace Elements
{
    public class MyCamera : MonoBehaviour
    {
        private GameObject _camera;

        void Awake()
        {
            this.gameObject.name = "uiRoot";
            this.gameObject.AddComponent<UIRoot>();
            this.gameObject.AddComponent<UIPanel>();
            this.gameObject.AddComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            //_uiRoot.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
            //_uiRoot.GetComponent<UIRoot>().manualHeight = CameraController.CAMERA_HEIGHT;

            _camera = new GameObject();
            _camera.name = "Camera";
            _camera.gameObject.transform.parent = this.gameObject.transform;
            _camera.AddComponent<UICamera>();

            //_camera.AddComponent<Camera>();
            _camera.AddComponent<CameraSizeHandler>();
            _camera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            _camera.GetComponent<Camera>().isOrthoGraphic = CameraController.ORTHOGRAPHIC;
            _camera.GetComponent<Camera>().orthographicSize = CameraController.ORTHOGRAPHIC_SIZE;
            _camera.GetComponent<Camera>().nearClipPlane = CameraController.NEAR_CLIP_PLANE;
            _camera.GetComponent<Camera>().farClipPlane = CameraController.FAR_CLIP_PLANE;
        }

        void Start()
        {
            
        }

        public GameObject GetUiRoot()
        {
            return this.gameObject;
        }

    }
}
