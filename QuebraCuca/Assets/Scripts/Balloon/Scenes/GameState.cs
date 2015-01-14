using System.Collections;
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
        private GameObject _timeText;
        private GameObject _timeIcon;

        private float _timeToSpawnBalloon = 1;
        private bool _canCreateBalloon;
        private int _ticks;
        private MovementSystem _movementSystem;
        private int _numberOfCharacter = 0;
        private Balloon _balloon;
        private GameObject _lifeIcon1;
        private GameObject _lifeIcon2;
        private GameObject _lifeIcon3;
        private PauseScreen _pauseScreen;
        private EndGameScreen _endGame;
        

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

            string path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "background");
            _background = new GameObject();
            _background.name = "Background";
            Sprite L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
            _background.AddComponent<SpriteRenderer>().sprite = L_sprite;
            _background.GetComponent<SpriteRenderer>().sortingOrder = -1;

            path = AFAssetManager.GetPathTargetPlatform() + "Prefabs/Balloon/InGameCanvas";

            GameObject gameStateToInstantiate = AFAssetManager.Instance.Load<GameObject>(path);

            ConstantsBalloons.Instance.Initialize();

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

                    _pauseButton = GameObject.Find("PauseButton");
                    _pointsText = GameObject.Find("PointsText");
                    _recordText = GameObject.Find("RecordText");
                    _timeText = GameObject.Find("TimeText");
                    _timeIcon = GameObject.Find("TimeIcon");
                    _lifeIcon1 = GameObject.Find("LifeIcon1");
                    _lifeIcon2 = GameObject.Find("LifeIcon2");
                    _lifeIcon3 = GameObject.Find("LifeIcon3");

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnPause");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _pauseButton.GetComponent<Image>().sprite = L_sprite;
                    //_pauseButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { OnClickPauseButton(); });
                    _pauseButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickPauseButton);

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "lifeIcon_full");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _lifeIcon1.GetComponent<Image>().sprite = L_sprite;
                    _lifeIcon2.GetComponent<Image>().sprite = L_sprite;
                    _lifeIcon3.GetComponent<Image>().sprite = L_sprite;

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "timeIcon");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _timeIcon.GetComponent<Image>().sprite = L_sprite;

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

            CreateSystens();
            CreateBallons();
            CreatePauseScreen();
            CreateHud();

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

            /*_balloon = BalloonFactory.Instance.CreateBalloon();
            _balloon.Initialize();
            //_balloon.gameObject.GetComponent<AnimationController>().Initialize();
            Add(_balloon);

            int randomXPoint = Random.Range(-Screen.width, Screen.width);
            float yPoint = Screen.height / 100f + _balloon.GetSprite().bounds.size.y;
            _balloon.transform.position = new Vector3(randomXPoint / 100f, -yPoint);*/
        }

        private void CreateHud()
        {
            HudController.Instance.SetCanvas(m_interface.GetComponent<Canvas>());
            HudController.Instance.SetTimeText(_timeText);
            HudController.Instance.SetTimeIcon(_timeIcon);
            HudController.Instance.AddLifeToList(_lifeIcon1);
            HudController.Instance.AddLifeToList(_lifeIcon2);
            HudController.Instance.AddLifeToList(_lifeIcon3);
            HudController.Instance.SetPointsText(_pointsText);
            HudController.Instance.SetRecordText(_recordText);
            HudController.Instance.Initialize();
        }

        private void CreatePauseScreen()
        {
            _pauseScreen = AFObject.Create<PauseScreen>();
            _pauseScreen.Initialize();
            _pauseScreen.HideScreen();
            Add(_pauseScreen);
        }

        private void OnClickPauseButton()
        {
            m_engine.Pause();
            _pauseScreen.ShowScreen();
        }

        private void CreateEndGameScreen()
        {
            _endGame = AFObject.Create<EndGameScreen>();
            _endGame.Initialize();
            Add(_endGame);
        }

        public override void AFUpdate(double deltaTime)
        {
            _ticks++;
            if (_ticks * Time.deltaTime > _timeToSpawnBalloon && _canCreateBalloon)
            {
                _ticks = 0;
                _balloon = BalloonFactory.Instance.CreateBalloon();
                _balloon.Initialize();
                Add(_balloon);

                int randomXPoint = Random.Range(-Screen.width, Screen.width);
                float yPoint = Screen.height / 100f + _balloon.GetSprite().bounds.size.y;
                _balloon.transform.position = new Vector3(randomXPoint / 100f, -yPoint);
            }

            if (GameController.GetEndedGame())
            {
                m_engine.Pause();
                CreateEndGameScreen();
            }

            //MovementSystem.Instance.AFUpdate(deltaTime);
            HudController.Instance.AFUpdate(deltaTime);
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
