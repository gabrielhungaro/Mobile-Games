using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.View;

namespace Com.Globo.Sitio.MobileGames.Balloon
{

    public class ChooseModeScene : AState
    {

        private GameObject _uiRoot;
        private GameObject _camera;
        private GameObject _background;
        private GameObject _returnButton;
        private GameObject _timeTrialButton;
        private GameObject _surviveButton;
        public int _offSetX = 1;
        private GameObject _soundManager;

        void Start()
        {
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
            CreateSurviveButton();
            CreateTimeTrialButton();
            //CreateReturnButton();
        }

        private void CreateBackgorund()
        {
            _background = new GameObject();
            _background.name = "Background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetChooseModePath() + "backGround");
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
            _background.GetComponent<UIWidget>().depth = -1;
            _background.layer = LayerController.Instance()._layerHUD;
            _background.transform.position = new Vector3(0, 0, -1f);
        }

        private void CreateTimeTrialButton()
        {
            _timeTrialButton = new GameObject();
            _timeTrialButton.name = "timeTrialButton";
            _timeTrialButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetChooseModePath() + "btnTimeTrial");
            _timeTrialButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _timeTrialButton.AddComponent<UIButton>();
            _timeTrialButton.AddComponent<BoxCollider>().size = new Vector3(_timeTrialButton.GetComponent<UI2DSprite>().width,
                                                                        _timeTrialButton.GetComponent<UI2DSprite>().height);
            _timeTrialButton.GetComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            UIEventListener.Get(_timeTrialButton).onClick += OnClickTimeTrialButton;
        }

        private void OnClickTimeTrialButton(GameObject go)
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            GameController.SetGameMode(GameController.TIME_TRIAL);
        }

        private void CreateSurviveButton()
        {
            _surviveButton = new GameObject();
            _surviveButton.name = "surviveButton";
            _surviveButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetChooseModePath() + "btnSurvive");
            _surviveButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _surviveButton.AddComponent<UIButton>();
            _surviveButton.AddComponent<BoxCollider>().size = new Vector3(_surviveButton.GetComponent<UI2DSprite>().width,
                                                                       _surviveButton.GetComponent<UI2DSprite>().height);
            _surviveButton.GetComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            UIEventListener.Get(_surviveButton).onClick += OnClickSurviveButton;
        }

        private void OnClickSurviveButton(GameObject go)
        {
            GameController.SetGameMode(GameController.SURVIVAL);
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
        }

        private void CreateReturnButton()
        {
            _returnButton = new GameObject();
            _returnButton.name = "returnButton";
            _returnButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetChooseModePath() + "btnReturn");
            _returnButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _returnButton.AddComponent<UIButton>();
            _returnButton.AddComponent<BoxCollider>().size = new Vector3(_returnButton.GetComponent<UI2DSprite>().width,
                                                                       _returnButton.GetComponent<UI2DSprite>().height);
            _returnButton.GetComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            UIEventListener.Get(_returnButton).onClick += OnClickReturnButton;
        }

        private void OnClickReturnButton(GameObject go)
        {
        }

        void Update()
        {
            float sizeOfAll = _timeTrialButton.GetComponent<UI2DSprite>().width * 2f;
            float individualPosition = (sizeOfAll / 2) * 0;
            _timeTrialButton.transform.localPosition = new Vector3((individualPosition - (sizeOfAll / 2f - _timeTrialButton.GetComponent<UI2DSprite>().width / 2f)), 0, 0);
            //_timeTrialButton.transform.position = new Vector3(0, -(Screen.height / 1500f));
            _surviveButton.transform.localPosition = new Vector3((_timeTrialButton.transform.localPosition.x + (_timeTrialButton.GetComponent<UI2DSprite>().width)), 0, 0);

            if (_returnButton != null)
                _returnButton.transform.localPosition = new Vector3(0, -(_surviveButton.transform.localPosition.y + _surviveButton.GetComponent<UI2DSprite>().height / 2f + _returnButton.GetComponent<UI2DSprite>().height));
            //_background.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

    }
}