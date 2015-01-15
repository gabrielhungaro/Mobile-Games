using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using com.globo.sitio.mobilegames.QuebraCuca.Controllers;
using com.globo.sitio.mobilegames.QuebraCuca.Elements;
using com.globo.sitio.mobilegames.QuebraCuca.Constants;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;
using AquelaFrameWork.Core.Asset;

namespace com.globo.sitio.mobilegames.QuebraCuca.States
{
    public class MenuState : AState
    {
        private GameObject _startButton;
        private GameObject _background;

        private GameObject _cameraGameObject;

        private GameObject m_interface;

        private Camera m_camera;

        protected override void Awake()
        {
            m_stateID = AState.EGameState.MENU;
        }

        public override void BuildState()
        {
            _cameraGameObject = new GameObject();
            _cameraGameObject.name = "StateCam";
            m_camera = _cameraGameObject.AddComponent<MyCamera>().GetCamera();

            string path = AFAssetManager.GetPathTargetPlatform() + "Prefabs/StartSceneCanvas";

            AFDebug.Log(AFAssetManager.GetPathTargetPlatform() + "Prefabs/StartScene" );

            GameObject startSceneToIntantiate = AFAssetManager.Instance.Load<GameObject>(path);

            if (!AFObject.IsNull(startSceneToIntantiate))
            {
                m_interface = AFAssetManager.Instance.Instantiate<GameObject>(path);

                if (!AFObject.IsNull(m_interface))
                {
                    Canvas L_canvas = m_interface.GetComponent<Canvas>();

                    if ( !AFObject.IsNull(L_canvas) )
                    {
                        L_canvas.renderMode = RenderMode.ScreenSpaceCamera;
                        L_canvas.worldCamera = m_camera;
                    }
                    else
                    {
                        AFDebug.LogError("Canvas not found!");
                    }
                   
                    _background = GameObject.Find("Background");
                    _startButton = GameObject.Find("StartButton");

                    path = AFAssetManager.GetPathTargetPlatformWithResolution("Scenes/StartScene/Background");
                    AFDebug.Log(path);
                    Sprite L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _background.GetComponent<Image>().sprite = L_sprite;


                    path = AFAssetManager.GetPathTargetPlatformWithResolution("Scenes/StartScene/StartButton");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    AFDebug.Log(path);
                    _startButton.GetComponent<Image>().sprite = L_sprite;

                    _startButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClick();});
                }
                else
                {
                    AFDebug.LogError("Nao foi possivel encontrar a interface do login");
                }
            }
            else
            {
                AFDebug.LogError("Nao foi possivel encontrar a interface do login para clonar");
            }


            Add(m_interface);

            base.BuildState();
        }

        private void OnClick()
        {
            m_engine.GetStateManger().GotoState(AState.EGameState.GAME);
        }

        override public void AFDestroy()
        {
            GameObject.Destroy(_startButton);
            GameObject.Destroy(_cameraGameObject);
            base.Destroy();
        }
    }
}
