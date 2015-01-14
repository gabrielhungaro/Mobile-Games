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

    public class ChooseModeScene : AState
    {

        private GameObject _background;
        private GameObject _returnButton;
        private GameObject _timeTrialButton;
        private GameObject _surviveButton;
        public int _offSetX = 1;
        private GameObject _soundManager;
        private GameObject _cameraGameObject;
        private Camera m_camera;
        private GameObject m_interface;

        protected override void Awake()
        {
            m_stateID = AState.EGameState.MENU_1;
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

            string path = AFAssetManager.GetPathTargetPlatform() + "Prefabs/Balloon/ChooseModeSceneCanvas";

            GameObject chooseSceneToInstantiate = AFAssetManager.Instance.Load<GameObject>(path);

            if(!AFObject.IsNull(chooseSceneToInstantiate))
            {
                m_interface = AFAssetManager.Instance.Instantiate<GameObject>(path);

                if (!AFObject.IsNull(m_interface))
                {
                    Canvas L_canvas = m_interface.GetComponent<Canvas>();

                    if (!AFObject.IsNull(L_canvas))
                    {
                        L_canvas.renderMode = RenderMode.ScreenSpaceCamera;
                        L_canvas.worldCamera = m_camera;
                    }
                    else
                    {
                        AFDebug.LogError("Canvas not found!");
                    }

                    _background = GameObject.Find("ChooseModeBG");
                    _timeTrialButton = GameObject.Find("ChooseModeTimeTrialButton");
                    _surviveButton = GameObject.Find("ChooseModeSurviveButton");

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetChooseModePath() + "background");
                    Sprite L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _background.GetComponent<Image>().sprite = L_sprite;

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetChooseModePath() + "surviveButton");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _surviveButton.GetComponent<Image>().sprite = L_sprite;
                    _surviveButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClickSurviveButton(); });

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetChooseModePath() + "timeTrialButton");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _timeTrialButton.GetComponent<Image>().sprite = L_sprite;
                    _timeTrialButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClickTimeTrialButton(); });

                }
                else
                {
                    AFDebug.LogError("Não foi possível encontrar a interface do login");
                }
            }
            else
            {
                AFDebug.LogError("Não foi possível encontrar a interface do login para clonar");
            }

            Add(m_interface);

            base.BuildState();
        }

        private void OnClickTimeTrialButton()
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            GameController.SetGameMode(GameController.TIME_TRIAL);
            m_engine.GetStateManger().GotoState(AState.EGameState.GAME);
        }

        private void OnClickSurviveButton()
        {
            GameController.SetGameMode(GameController.SURVIVAL);
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            m_engine.GetStateManger().GotoState(AState.EGameState.GAME);
        }

        public override void AFDestroy()
        {
            GameObject.Destroy(_background);
            GameObject.Destroy(_cameraGameObject);
            GameObject.Destroy(_returnButton);
            GameObject.Destroy(_soundManager);
            GameObject.Destroy(_surviveButton);
            GameObject.Destroy(_timeTrialButton);
            GameObject.Destroy(m_camera);
            base.AFDestroy();
        }
    }
}