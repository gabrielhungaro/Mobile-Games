using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class ResultScene : MonoBehaviour
    {

        private GameObject _uiRoot;
        private GameObject _camera;
        private GameObject _background;
        private GameObject _retryButton;
        private SceneManager _sceneManager;
        private GameObject _exitButton;
        private GameObject _actualPoints;
        private GameObject _maxPoints;

        void Start()
        {
            _sceneManager = this.gameObject.GetComponent<SceneManager>();
            LayerController.Instance().Start();
            _uiRoot = new GameObject();
            _uiRoot.name = "_uiRoot";
            _uiRoot.layer = LayerController.Instance()._layerHUD;
            _uiRoot.AddComponent<UIRoot>();
            _uiRoot.AddComponent<UIPanel>();
            _uiRoot.AddComponent<Rigidbody>();
            _uiRoot.GetComponent<Rigidbody>().useGravity = false;
            _uiRoot.GetComponent<Rigidbody>().isKinematic = true;
            _uiRoot.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
            _uiRoot.GetComponent<UIRoot>().manualHeight = CameraController.CAMERA_HEIGHT;

            _camera = new GameObject();
            _camera.transform.parent = _uiRoot.transform;
            _camera.name = "uiCamera";
            _camera.AddComponent<Camera>();
            _camera.AddComponent<UICamera>();
            _camera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            _camera.GetComponent<Camera>().cullingMask = LayerController.Instance()._maskHud;
            _camera.GetComponent<Camera>().cullingMask = LayerController.Instance()._maskHud;
            _camera.GetComponent<Camera>().isOrthoGraphic = CameraController.ORTHOGRAPHIC;
            _camera.GetComponent<Camera>().orthographicSize = CameraController.ORTHOGRAPHIC_SIZE;
            _camera.GetComponent<Camera>().nearClipPlane = CameraController.NEAR_CLIP_PLANE;
            _camera.GetComponent<Camera>().farClipPlane = CameraController.FAR_CLIP_PLANE;
            _camera.transform.parent = this.gameObject.transform;

            CreateBackgorund();
            CreateRetryButton();
            CreateExitButton();
            CreatePoints();
        }

        private void CreateBackgorund()
        {
            _background = new GameObject();
            _background.name = "Background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetResultPath() + "backGround");
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
            _background.GetComponent<UIWidget>().depth = -1;
            _background.layer = LayerController.Instance()._layerHUD;
            _background.transform.position = new Vector3(0, 0, -1f);
        }

        private void CreateRetryButton()
        {
            _retryButton = new GameObject();
            _retryButton.name = "retryButton";
            _retryButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetResultPath() + "btnRetry");
            _retryButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _retryButton.AddComponent<UIButton>();
            _retryButton.AddComponent<BoxCollider>().size = new Vector3(_retryButton.GetComponent<UI2DSprite>().width,
                                                                        _retryButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_retryButton).onClick += OnClickRetryButton;
        }

        private void OnClickRetryButton(GameObject go)
        {
            _sceneManager.GotoGameScene();
        }

        private void CreateExitButton()
        {
            _exitButton = new GameObject();
            _exitButton.name = "exitButton";
            _exitButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetResultPath() + "btnExit");
            _exitButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _exitButton.AddComponent<UIButton>();
            _exitButton.AddComponent<BoxCollider>().size = new Vector3(_exitButton.GetComponent<UI2DSprite>().width,
                                                                       _exitButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_exitButton).onClick += OnClickExitButton;
        }

        private void OnClickExitButton(GameObject go)
        {
            _sceneManager.GotoStartScene();
        }

        private void CreatePoints()
        {
            HudController _hudController = new HudController();
            _hudController.LoadFont();

            _actualPoints = new GameObject();
            _actualPoints.name = "actualPoints";
            _actualPoints.AddComponent<UILabel>().bitmapFont = _hudController.GetFontType();
            _actualPoints.GetComponent<UILabel>().fontSize = 100;
            _actualPoints.GetComponent<UILabel>().text = ScoreController.GetPoints().ToString();
            _actualPoints.GetComponent<UILabel>().MakePixelPerfect();

            _maxPoints = new GameObject();
            _maxPoints.name = "maxPoints";
            _maxPoints.AddComponent<UILabel>().bitmapFont = _hudController.GetFontType();
            _maxPoints.GetComponent<UILabel>().fontSize = 100;
            _maxPoints.GetComponent<UILabel>().text = ScoreController.GetRecord().ToString();
            _maxPoints.GetComponent<UILabel>().MakePixelPerfect();
        }

        void Update()
        {
            _actualPoints.transform.position = new Vector3(-(Screen.width / 1000f), (Screen.height / 1000f));
            _maxPoints.transform.position = new Vector3(-(Screen.width / 1000f), (Screen.height / 1500f));
            _exitButton.transform.position = new Vector3(0, -(Screen.height / 1500f));
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

    }
}
