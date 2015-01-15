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

            m_AddPoint = BalloonFactory.Instance.CreatePool(ConstantsBalloons.TYPE_SIMPLE_ADD_POINT, 20);
            m_AddTime = BalloonFactory.Instance.CreatePool(ConstantsBalloons.TYPE_SIMLPE_ADD_TIME,20);
            
            m_RemovePoint = BalloonFactory.Instance.CreatePool(ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT, 20);
            m_RemoveTime = BalloonFactory.Instance.CreatePool(ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME, 20);
            
            m_SlowMotion = BalloonFactory.Instance.CreatePool(ConstantsBalloons.TYPE_SLOW_MOTION, 20);
            m_FastForward = BalloonFactory.Instance.CreatePool(ConstantsBalloons.TYPE_FAST_FOWARD, 20);

            AddBalloonList(m_AddPoint);
            AddBalloonList(m_AddTime);
            AddBalloonList(m_RemovePoint);
            AddBalloonList(m_RemoveTime);
            AddBalloonList(m_SlowMotion);
            AddBalloonList(m_FastForward);


            /*_balloon = BalloonFactory.Instance.CreateBalloon();
            _balloon.Initialize();
            //_balloon.gameObject.GetComponent<AnimationController>().Initialize();
            Add(_balloon);

            int randomXPoint = Random.Range(-Screen.width, Screen.width);
            float yPoint = Screen.height / 100f + _balloon.GetSprite().bounds.size.y;
            _balloon.transform.position = new Vector3(randomXPoint / 100f, -yPoint);*/
        }

        private void AddBalloonList( List<Balloon> list )
        {
            for (int i = 0; i < list.Count; ++i)
                Add(list[i]);
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
            if (_ticks * deltaTime > _timeToSpawnBalloon && _canCreateBalloon)
            {
                _ticks = 0;
                _balloon = GetBallonAvailable();
                int randomXPoint = Random.Range(-Screen.width, Screen.width);
                float yPoint = Screen.height / 100f + _balloon.GetSprite().bounds.size.y;
                _balloon.transform.position = new Vector3(randomXPoint / 100f, -yPoint);
            }

            if (GameController.GetEndedGame())
            {
                m_engine.Pause();
                CreateEndGameScreen();
            }

            MovementSystem.Instance.AFUpdate(deltaTime);
            HudController.Instance.AFUpdate(deltaTime);
            base.AFUpdate(deltaTime);
        }


        private List<Balloon> m_AddPoint = new List<Balloon>();
        private int m_AddPointIndex = 0;
        
        private List<Balloon> m_RemovePoint= new List<Balloon>();
        private int m_RemovePointIndex = 0;

        private List<Balloon> m_AddTime = new List<Balloon>();
        private int m_AddTimeIndex = 0;
        
        private List<Balloon> m_RemoveTime = new List<Balloon>();
        private int m_RemoveTimeIndex = 0;
        
        private List<Balloon> m_FastForward = new List<Balloon>();
        private int m_FastForwardIndex = 0;
        
        private List<Balloon> m_SlowMotion = new List<Balloon>();
        private int m_SlowMotionIndex = 0;


        public Balloon GetBallonAvailable()
        {
            Balloon L_balloon = null;

            switch( BalloonFactory.Instance.GetBalloonPercent() )
            {
                case ConstantsBalloons.TYPE_SIMPLE_REMOVE_POINT :
                    L_balloon = m_RemovePoint[m_AddPointIndex];
                    m_AddPointIndex = AddOrResetPoolIndex(m_AddPointIndex, m_RemovePoint.Count);
                    break;
                case ConstantsBalloons.TYPE_SIMLPE_ADD_TIME:
                    L_balloon = m_AddTime[m_AddTimeIndex];
                    m_AddTimeIndex = AddOrResetPoolIndex(m_AddTimeIndex, m_AddTime.Count);
                    break;
                case ConstantsBalloons.TYPE_SLOW_MOTION:
                    L_balloon = m_SlowMotion[m_SlowMotionIndex];
                    m_SlowMotionIndex = AddOrResetPoolIndex(m_SlowMotionIndex, m_SlowMotion.Count);
                    break;
                case ConstantsBalloons.TYPE_FAST_FOWARD:
                    L_balloon = m_FastForward[m_FastForwardIndex];
                    m_FastForwardIndex = AddOrResetPoolIndex(m_FastForwardIndex, m_FastForward.Count);
                    break;
                case ConstantsBalloons.TYPE_SIMPLE_REMOVE_TIME:
                    L_balloon = m_RemoveTime[m_RemoveTimeIndex];
                    m_RemoveTimeIndex = AddOrResetPoolIndex(m_RemoveTimeIndex, m_RemoveTime.Count);
                    break;
                default :
                    L_balloon = m_AddPoint[m_AddPointIndex];
                    m_AddPointIndex = AddOrResetPoolIndex(m_AddPointIndex, m_AddPoint.Count);
                    break;  
            }
            
//             L_balloon = m_SlowMotion[m_SlowMotionIndex];
//             m_SlowMotionIndex = AddOrResetPoolIndex(m_SlowMotionIndex, m_SlowMotion.Count);
            L_balloon.gameObject.SetActive(true);

            return L_balloon;
        }

        public int AddOrResetPoolIndex(int index, int maxIndex)
        {
            return index >= (maxIndex - 1) ? 0 : ++index;
        }

        override public void AFDestroy()
        {
            GameObject.Destroy(_background);
            GameObject.Destroy(_cameraGameObject);
            GameObject.Destroy(_pauseButton);
            GameObject.Destroy(_pointsText);
            GameObject.Destroy(_recordText);
            GameObject.Destroy(_gameController);
            GameObject.Destroy(_pauseScreen);
            HudController.DestroyInstance();
            BalloonFactory.DestroyInstance();
            base.Destroy();
        }
    }
}
