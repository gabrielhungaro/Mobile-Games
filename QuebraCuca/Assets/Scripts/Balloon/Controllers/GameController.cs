using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class GameController : AFObject
    {

        public static string TIME_TRIAL = "timeTrial";
        public static string SURVIVAL = "survival";
        private static string _gameMode = SURVIVAL;
        private BalloonFactory _balloonFactory;
        private GameObject _pointsController;
        private GameObject _timeController;
        private GameObject _uiRoot;
        private static bool _paused;
        private static bool _endedGame;
        private GameObject _endGameAlert;
        private GameObject _scoreLabel;
        private GameObject _recordLabel;
        private GameObject _retryButton;
        private GameObject _scoreScreen;
        private GameObject _exitButton;
        private GameObject _lifeController;
        private GameObject _soundManager;
        // Use this for initialization

        public void Initialize()
        {
            UnityEngine.Debug.Log("[ GAME_CONTROLLER ] - START");

            InitGame();
        }

        private void InitGame()
        {
            this.gameObject.SetActive(true);
            _paused = false;
            _endedGame = false;

            if (_soundManager == null)
            {
                if (FindObjectOfType<SoundManager>() == null)
                {
                    _soundManager = new GameObject();
                    _soundManager.AddComponent<SoundManager>();
                    _soundManager.GetComponent<SoundManager>().Init();
                }
                else
                {
                    _soundManager = FindObjectOfType<SoundManager>().gameObject;
                }
            }

            if (this.gameObject.GetComponent<BalloonFactory>() == null)
            {
                this.gameObject.AddComponent<BalloonFactory>();
                _balloonFactory = this.gameObject.GetComponent<BalloonFactory>();
            }
            else
            {
                _balloonFactory.enabled = true;
                _balloonFactory.Initialize();
            }

            if (this.gameObject.GetComponent<MovementSystem>() == null)
            {
                this.gameObject.AddComponent<MovementSystem>();
            }
            else
            {
                this.gameObject.GetComponent<MovementSystem>().enabled = true;
            }

            if (_pointsController == null)
            {
                _pointsController = new GameObject();
                _pointsController.name = "PointsController";
                _pointsController.AddComponent<ScoreController>();
            }
            else
            {
                _pointsController.SetActive(true);
                _pointsController.GetComponent<ScoreController>().Start();
            }

            if (_gameMode == SURVIVAL)
            {
                if (_lifeController == null)
                {
                    _lifeController = new GameObject();
                    _lifeController.name = "LifeController";
                    _lifeController.AddComponent<LifeController>();
                }
                else
                {
                    _lifeController.SetActive(true);
                    _lifeController.GetComponent<LifeController>().Start();
                }
            }

            if (_gameMode == TIME_TRIAL)
            {
                if (_timeController == null)
                {
                    _timeController = new GameObject();
                    _timeController.name = "TimeController";
                    _timeController.AddComponent<TimeController>();
                }
                else
                {
                    _timeController.SetActive(true);
                    _timeController.GetComponent<TimeController>().Start();
                }
            }
        }

        // Update is called once per frame
        public override void AFUpdate(double deltaTime)
        {
            if (!_paused)
            {
                if (_endedGame)
                {
                    EndGame();
                }
            }
            else
            {
                this.gameObject.SetActive(false);

                if (_pointsController != null && _pointsController.activeInHierarchy == true)
                    _pointsController.SetActive(false);

                if (_timeController != null && _timeController.activeInHierarchy == true)
                    _timeController.SetActive(false);
            }
            base.AFUpdate(deltaTime);
        }

        public void EndGame()
        {
            ScoreController.SetRecord(ScoreController.GetPoints());

            _paused = true;

            //ShowEndGameScreen();
            this.gameObject.SetActive(false);
        }

        private void OnClickRetry(GameObject g)
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            RemoveEndGameScreen();
            //InitGame();
        }

        private void OnClickExit(GameObject g)
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
            RemoveEndGameScreen();
        }

        private void RemoveEndGameScreen()
        {
            Destroy(_retryButton);
            Destroy(_scoreLabel);
            Destroy(_endGameAlert);
        }

        public static void SetGameMode(string value)
        {
            _gameMode = value;
        }

        public static string GetGameMode()
        {
            return _gameMode;
        }

        public void SetIsPaused(bool value)
        {
            _paused = value;
            this.gameObject.SetActive(!_paused);

            if (_pointsController != null)
                _pointsController.SetActive(!_paused);

            if (_timeController != null)
                _timeController.SetActive(!_paused);
        }

        public bool GetIsPaused()
        {
            return _paused;
        }

        public static void SetEndedGame(bool value)
        {
            _endedGame = value;
        }

        public static bool GetEndedGame()
        {
            return _endedGame;
        }

    }
}
