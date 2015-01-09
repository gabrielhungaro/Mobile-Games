using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using com.globo.sitio.mobilegames.Balloon.Elements;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class GameState : AState
    {

        private GameController _gameController;
        private GameObject _cameraGameObject;
        private Camera m_camera;
        private GameObject m_interface;
        private GameObject _background;
        private GameObject _pauseButton;
        private GameObject _pointsText;
        private GameObject _recordText;

        private float _timeToSpawnBalloon = 1;
        private bool _canCreateBalloon;
        private int _ticks;
        private MovementSystem _movementSystem;

        protected override void Awake()
        {
            m_stateID = AState.EGameState.GAME;
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

            string path = AFAssetManager.GetPathTargetPlatform() + "Prefabs/Balloon/InGameCanvas";

            GameObject gameStateToInstantiate = AFAssetManager.Instance.Load<GameObject>(path);

            if (!AFObject.IsNull(gameStateToInstantiate))
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
                        AFDebug.LogError("Canvas not found");
                    }

                    _background = GameObject.Find("Background2");
                    _pauseButton = GameObject.Find("PauseButton");
                    _pointsText = GameObject.Find("PointsText");
                    _recordText = GameObject.Find("RecordText");

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "background");
                    AFDebug.Log(path);
                    Sprite L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _background.GetComponent<Image>().sprite = L_sprite;

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnPause");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _pauseButton.GetComponent<Image>().sprite = L_sprite;

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

            CreateBallons();
            CreateSystens();

            _gameController = AFObject.Create<GameController>();
            _gameController.name = "GameController";
            _gameController.Initialize();
            Add(_gameController);

            base.BuildState();
        }

        private void CreateSystens()
        {
            MovementSystem.Instance.Initialize();
        }

        private void CreateBallons()
        {
            _canCreateBalloon = true;
            BalloonFactory.Instance.Initialize();
        }

        public override void AFUpdate(double deltaTime)
        {
            _ticks++;
            if (_ticks * Time.deltaTime > _timeToSpawnBalloon && _canCreateBalloon)
            {
                _ticks = 0;
                BalloonFactory.Instance.CreateBalloon();
            }
            MovementSystem.Instance.AFUpdate(deltaTime);
            base.AFUpdate(deltaTime);
        }

        override public void AFDestroy()
        {
            GameObject.Destroy(_background);
            GameObject.Destroy(_cameraGameObject);
            GameObject.Destroy(_pauseButton);
            GameObject.Destroy(_pointsText);
            GameObject.Destroy(_recordText);
            GameObject.Destroy(_gameController);
            base.Destroy();
        }
    }
}
