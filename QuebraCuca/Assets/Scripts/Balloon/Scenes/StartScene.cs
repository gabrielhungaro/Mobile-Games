using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class StartScene : MonoBehaviour
    {

        private GameObject _uiRoot;
        private GameObject _camera;
        private GameObject _background;
        private GameObject _startButton;
        private SceneManager _sceneManager;
        private GameObject _creditsButton;
        private GameObject _soundManager;
        private GameObject _soundButton;
        private ConstantsPaths _constantsPath;

        void Start()
        {
            _constantsPath = new ConstantsPaths();
            _constantsPath.Start();

            if (_soundManager == null)
            {
                if (FindObjectOfType<SoundManager>() == null)
                {
                    _soundManager = new GameObject();
                    _soundManager.AddComponent<SoundManager>();
                    _soundManager.GetComponent<SoundManager>().Init();
                    //SoundManager.PlaySoundByName(ConstantsSounds.BG_SOUND);
                }
                else
                {
                    _soundManager = FindObjectOfType<SoundManager>().gameObject;
                }
            }


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
            CreateStartButton();
            //CreateCreditsButton();
            CreateSoundButton();
        }

        private void CreateBackgorund()
        {
            _background = new GameObject();
            _background.name = "Background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "backGround");
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
            _background.GetComponent<UIWidget>().depth = -1;
            _background.layer = LayerController.Instance()._layerHUD;
            _background.transform.position = new Vector3(0, 0, -1f);
        }

        private void CreateStartButton()
        {
            _startButton = new GameObject();
            _startButton.name = "startButton";
            _startButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "btnStart");
            _startButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _startButton.AddComponent<UIButton>();
            _startButton.AddComponent<BoxCollider>().size = new Vector3(_startButton.GetComponent<UI2DSprite>().width,
                                                                        _startButton.GetComponent<UI2DSprite>().height);
            _startButton.GetComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            UIEventListener.Get(_startButton).onClick += OnClickStartButton;

            _startButton.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
            _startButton.GetComponent<UI2DSprite>().leftAnchor.absolute = -653;
            _startButton.GetComponent<UI2DSprite>().rightAnchor.absolute = -123;
            _startButton.GetComponent<UI2DSprite>().bottomAnchor.absolute = -398;
            _startButton.GetComponent<UI2DSprite>().topAnchor.absolute = -154;
            _startButton.GetComponent<UI2DSprite>().UpdateAnchors();
            _startButton.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

        private void CreateSoundButton()
        {
            _soundButton = new GameObject();
            _soundButton.name = "soundButton";
            _soundButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "btnSound_On");
            _soundButton.AddComponent<UIButton>();
            _soundButton.AddComponent<BoxCollider>().size = new Vector3(_soundButton.GetComponent<UI2DSprite>().width,
                                                                        _soundButton.GetComponent<UI2DSprite>().height);
            _soundButton.GetComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _soundButton.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
            _soundButton.GetComponent<UI2DSprite>().topAnchor.absolute = -606;
            _soundButton.GetComponent<UI2DSprite>().bottomAnchor.absolute = -750;
            _soundButton.GetComponent<UI2DSprite>().leftAnchor.absolute = -1001;
            _soundButton.GetComponent<UI2DSprite>().rightAnchor.absolute = -855;

            _soundButton.GetComponent<UI2DSprite>().UpdateAnchors();
            //_soundButton.GetComponent<UI2DSprite>().bottomAnchor.relative = _uiRoot.GetComponent<UI2DSprite>().bottomAnchor.relative;
            _soundButton.GetComponent<UI2DSprite>().MakePixelPerfect();

            UIEventListener.Get(_soundButton).onClick += OnClickSoundButton;
        }

        private void OnClickStartButton(GameObject go)
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            _sceneManager.GotoChooseModeScene();
        }

        private void OnClickSoundButton(GameObject go)
        {
            if (SoundManager.GetAllSoundsIsMuting() == false)
            {
                _soundButton.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "btnSound_Off");
                SoundManager.MuteAllSounds();
            }
            else
            {
                _soundButton.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "btnSound_On");
                SoundManager.UnMuteAllSounds();
            }
            _soundButton.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

        private void CreateCreditsButton()
        {
            _creditsButton = new GameObject();
            _creditsButton.name = "creditsButton";
            _creditsButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "btnCredits");
            _creditsButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _creditsButton.AddComponent<UIButton>();
            _creditsButton.AddComponent<BoxCollider>().size = new Vector3(_creditsButton.GetComponent<UI2DSprite>().width,
                                                                       _creditsButton.GetComponent<UI2DSprite>().height);
            _creditsButton.GetComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            UIEventListener.Get(_creditsButton).onClick += OnClickCreditsButton;

            _creditsButton.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
            _creditsButton.GetComponent<UI2DSprite>().leftAnchor.absolute = -510;
            _creditsButton.GetComponent<UI2DSprite>().rightAnchor.absolute = -264;
            _creditsButton.GetComponent<UI2DSprite>().bottomAnchor.absolute = -517;
            _creditsButton.GetComponent<UI2DSprite>().topAnchor.absolute = -445;
            _creditsButton.GetComponent<UI2DSprite>().UpdateAnchors();
            _creditsButton.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

        private void OnClickCreditsButton(GameObject go)
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            _sceneManager.GotoCreditsScene();
        }

        void Update()
        {

            GameObject game = GameObject.Find("AnimationBall");


            ///_startButton.transform.localPosition = new Vector3(-(Screen.width), -(Screen.height * .9f));
            //_startButton.transform.localPosition = new Vector3(-(Screen.width * 0.9f), 0);
            //_creditsButton.transform.localPosition = new Vector3(_startButton.transform.localPosition.x, _startButton.transform.localPosition.y - _startButton.GetComponent<UI2DSprite>().height);
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
            //_soundButton.transform.localPosition = new Vector3(-(Screen.width * 2f), -(Screen.height * 2f));
            //_soundButton.transform.localPosition = new Vector3(-(Screen.width ), -(Screen.height));
        }

    }
}
