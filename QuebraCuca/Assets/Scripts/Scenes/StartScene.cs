using UnityEngine;
using System.Collections;
using Elements;
using Constants;
using Controllers;

namespace Scenes
{
    public class StartScene : Scene
    {
        private GameObject _startButton;
        private GameObject _background;

        void Start()
        {
            /*_uiRoot = new GameObject();
            _uiRoot.name = "uiRoot";
            _uiRoot.AddComponent<UIRoot>();
            _uiRoot.AddComponent<UIPanel>();
            _uiRoot.AddComponent<Rigidbody>().useGravity = false;
            _uiRoot.GetComponent<Rigidbody>().isKinematic = true;
            _uiRoot.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
            _uiRoot.GetComponent<UIRoot>().manualHeight = CameraController.CAMERA_HEIGHT;
            
            _camera = new GameObject();
            _camera.name = "camera";
            _camera.AddComponent<UICamera>();
            //_camera.AddComponent<Camera>();
            _camera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            //_camera.GetComponent<Camera>().cullingMask = 
            _camera.GetComponent<Camera>().isOrthoGraphic = CameraController.ORTHOGRAPHIC;
            _camera.GetComponent<Camera>().orthographicSize = CameraController.ORTHOGRAPHIC_SIZE;
            _camera.GetComponent<Camera>().nearClipPlane = CameraController.NEAR_CLIP_PLANE;
            _camera.GetComponent<Camera>().farClipPlane = CameraController.FAR_CLIP_PLANE;*/

            _background = new GameObject();
            _background.name = "background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetStartScenePath() + "startScene");
            //_background.GetComponent<UI2DSprite>().
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();


            _startButton = new GameObject();
            _startButton.name = "startButton";
            _startButton.AddComponent<Button>().SetImagePath(PathConstants.GetStartScenePath() + "startButton");
            _startButton.GetComponent<Button>().OnClick += OnClick;
            _startButton.GetComponent<Button>().SetWithAnchor(true);
            _startButton.GetComponent<Button>().SetAnchor(SceneManager.GetUiRoot());
            _startButton.GetComponent<Button>().SetLeftAnchorPoint(-57);
            _startButton.GetComponent<Button>().SetRightAnchorPoint(57);
            _startButton.GetComponent<Button>().SetTopAnchorPoint(-720);
            _startButton.GetComponent<Button>().SetBottomAnchorPoint(-588);
        }

        private void OnClick()
        {
            SceneManager.ChangeScene(SceneManager.GAME_SCENE);
            //Application.LoadLevel(SceneManager.GAME_SCENE);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}