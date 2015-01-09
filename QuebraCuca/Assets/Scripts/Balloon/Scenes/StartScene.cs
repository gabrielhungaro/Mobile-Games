using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using com.globo.sitio.mobilegames.Balloon.Elements;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.View;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class StartScene : AState
    {
        private GameObject _background;
        private GameObject _startButton;
        private GameObject _creditsButton;
        private GameObject _soundButton;

        private GameObject _cameraGameObject;
        private Camera m_camera;

        private GameObject m_interface;

        private GameObject _soundManager;

        protected override void Awake()
        {
            m_stateID = AState.EGameState.MENU;
        }

        public override void BuildState()
        {
#if UNITY_EDITOR
            AFAssetManager.SimulatedDPI = AFAssetManager.DPI_IPHONE_4_5;
            AFAssetManager.SimulatePlatform = AFAssetManager.EPlataform.IOS;
#endif

            _cameraGameObject = new GameObject();
            _cameraGameObject.name = "StateCam";
            m_camera = _cameraGameObject.AddComponent<MyCamera>().GetCamera();

            string path = AFAssetManager.GetPathTargetPlatform() + "Prefabs/Balloon/StartSceneCanvas";

            GameObject startSceneToInstantiate = AFAssetManager.Instance.Load<GameObject>(path);

            if ( !AFObject.IsNull(startSceneToInstantiate) )
            {
                m_interface = AFAssetManager.Instance.Instantiate<GameObject>(path);

                if ( !AFObject.IsNull(m_interface) )
                {
                    Canvas L_canvas = m_interface.GetComponent<Canvas>();

                    if( !AFObject.IsNull(L_canvas) )
                    {
                        L_canvas.renderMode = RenderMode.ScreenSpaceCamera;
                        L_canvas.worldCamera = m_camera;
                    }
                    else
                    {
                        AFDebug.Log("Canvas not found");
                    }

                    _background = GameObject.Find("Background");
                    _startButton = GameObject.Find("StartButton");
                    _creditsButton = GameObject.Find("CreditsButton");
                    _soundButton = GameObject.Find("SoundButton");

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetStartPath() + "background");
                    Sprite L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _background.GetComponent<Image>().sprite = L_sprite;

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetStartPath() + "startButton");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _startButton.GetComponent<Image>().sprite = L_sprite;
                    _startButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClickStart(); });

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetStartPath() + "soundButton_On");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _soundButton.GetComponent<Image>().sprite = L_sprite;
                    _soundButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClickSoundButton(); });

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetStartPath() + "creditsButton");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _creditsButton.GetComponent<Image>().sprite = L_sprite;
                    _creditsButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClickCreditsButton(); });
                }
                else
                {
                    AFDebug.LogError("Não foi possível encontrar a interface do login");
                }
            }
            else
            {
                AFDebug.LogError("Não foi possível encontar a interface do login para clonar");
            }

            Add(m_interface);

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

            base.BuildState();
        }

        private void OnClickStart()
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            m_engine.GetStateManger().GotoState(AState.EGameState.GAME);
        }

        private void OnClickSoundButton()
        {
            string path;
            Sprite L_sprite;

            if (SoundManager.GetAllSoundsIsMuting() == false)
            {
                path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetStartPath() + "soundButton_Off");
                L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                _soundButton.GetComponent<Image>().sprite = L_sprite;

                //_soundButton.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "btnSound_Off");
                SoundManager.MuteAllSounds();
            }
            else
            {
                path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetStartPath() + "soundButton_On");
                L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                _soundButton.GetComponent<Image>().sprite = L_sprite;

                //_soundButton.GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetStartPath() + "btnSound_On");
                SoundManager.UnMuteAllSounds();
            }
            _soundButton.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

        private void OnClickCreditsButton()
        {
            m_engine.GetStateManger().GotoState(AState.EGameState.MENU_1);
        }

        public override void AFDestroy()
        {
            GameObject.Destroy(_startButton);
            GameObject.Destroy(_background);
            GameObject.Destroy(_cameraGameObject);
            base.AFDestroy();
        }
    }
}
