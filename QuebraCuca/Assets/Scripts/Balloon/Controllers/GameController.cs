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
        void Update()
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
                if (_balloonFactory.enabled == true)
                    _balloonFactory.enabled = false;

                if (_pointsController != null && _pointsController.activeInHierarchy == true)
                    _pointsController.SetActive(false);

                if (_timeController != null && _timeController.activeInHierarchy == true)
                    _timeController.SetActive(false);
            }
        }

        public void EndGame()
        {
            ScoreController.SetRecord(ScoreController.GetPoints());

            _paused = true;

            ShowEndGameScreen();
            this.gameObject.SetActive(false);
        }

        private void ShowEndGameScreen()
        {
            HudController _hudController;
            _hudController = FindObjectOfType<HudController>();
            _hudController.LoadFont();
            _hudController.RemovePauseButtonListener();

            /*_endGameAlert = new GameObject();
            _endGameAlert.name = "EndGameAlert";
            _endGameAlert.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "gameOverBackground");
            _endGameAlert.GetComponent<UI2DSprite>().MakePixelPerfect();
            _endGameAlert.GetComponent<UIWidget>().depth = 0;

            Color shadowColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            GameObject _shadowScore = new GameObject();
            _shadowScore.name = "ShadowScoreLabel";
            _shadowScore.AddComponent<UILabel>().trueTypeFont = _hudController.GetFinalScoreFont();
            _shadowScore.GetComponent<UILabel>().fontSize = 150;
            _shadowScore.GetComponent<UILabel>().text = ScoreController.GetPoints().ToString();
            _shadowScore.transform.parent = _endGameAlert.transform;
            _shadowScore.GetComponent<UILabel>().color = shadowColor;
            _shadowScore.GetComponent<UIWidget>().depth = 1;

            GameObject _shadowRecord = new GameObject();
            _shadowRecord.name = "ShadowRecordLabel";
            _shadowRecord.AddComponent<UILabel>().trueTypeFont = _hudController.GetFinalScoreFont();
            _shadowRecord.GetComponent<UILabel>().fontSize = 150;
            _shadowRecord.GetComponent<UILabel>().text = ScoreController.GetPoints().ToString();
            _shadowRecord.transform.parent = _endGameAlert.transform;
            _shadowRecord.GetComponent<UILabel>().color = shadowColor;
            _shadowRecord.GetComponent<UIWidget>().depth = 1;

            Color fontColor = new Color(14 / 255f, 143 / 255f, 144 / 255f, 255 / 255f);

            _scoreLabel = new GameObject();
            _scoreLabel.name = "ScoreLabel";
            _scoreLabel.AddComponent<UILabel>().trueTypeFont = _hudController.GetFinalScoreFont();
            _scoreLabel.GetComponent<UILabel>().fontSize = 150;
            _scoreLabel.GetComponent<UILabel>().text = "SUA PONTUAÇÃO: " + ScoreController.GetPoints().ToString();
            _scoreLabel.GetComponent<UILabel>().MakePixelPerfect();
            _scoreLabel.transform.parent = _endGameAlert.transform;
            _scoreLabel.GetComponent<UILabel>().color = fontColor;
            _scoreLabel.GetComponent<UIWidget>().depth = 1;

            _recordLabel = new GameObject();
            _recordLabel.name = "RecordLabel";
            _recordLabel.AddComponent<UILabel>().trueTypeFont = _hudController.GetFinalScoreFont();
            _recordLabel.GetComponent<UILabel>().fontSize = 150;
            _recordLabel.GetComponent<UILabel>().text = "SEU RECORDE: " + ScoreController.GetRecord().ToString();
            _recordLabel.GetComponent<UILabel>().MakePixelPerfect();
            _recordLabel.transform.parent = _endGameAlert.transform;
            _recordLabel.GetComponent<UILabel>().color = fontColor;
            _recordLabel.GetComponent<UIWidget>().depth = 1;

            _shadowScore.GetComponent<UILabel>().text = _scoreLabel.GetComponent<UILabel>().text;
            _shadowScore.GetComponent<UILabel>().MakePixelPerfect();
            _shadowRecord.GetComponent<UILabel>().text = _recordLabel.GetComponent<UILabel>().text;
            _shadowRecord.GetComponent<UILabel>().MakePixelPerfect();

            _retryButton = new GameObject();
            _retryButton.name = "RetryButton";
            _retryButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "btnRetry_GameOver");
            _retryButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _retryButton.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _retryButton.AddComponent<BoxCollider>().size = new Vector3(_retryButton.GetComponent<UI2DSprite>().width,
                                                                        _retryButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_retryButton).onClick += OnClickRetry;
            _retryButton.transform.parent = _endGameAlert.transform;
            _retryButton.GetComponent<UIWidget>().depth = 1;

            _exitButton = new GameObject();
            _exitButton.name = "ExitButton";
            _exitButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "btnExit_GameOver");
            _exitButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _exitButton.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _exitButton.AddComponent<BoxCollider>().size = new Vector3(_exitButton.GetComponent<UI2DSprite>().width,
                                                                               _exitButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_exitButton).onClick += OnClickExit;
            _exitButton.transform.parent = _endGameAlert.transform;
            _exitButton.GetComponent<UIWidget>().depth = 1;


            float sizeOfAll = _retryButton.GetComponent<UI2DSprite>().width * 2f;
            float individualPosition = (sizeOfAll / 2) * 0;
            _retryButton.transform.localPosition = new Vector3((individualPosition - (sizeOfAll / 2f - _retryButton.GetComponent<UI2DSprite>().width / 2f)), -(_retryButton.GetComponent<UI2DSprite>().height), 0);
            //_timeTrialButton.transform.position = new Vector3(0, -(Screen.height / 1500f));
            _exitButton.transform.localPosition = new Vector3((_retryButton.transform.localPosition.x + (_retryButton.GetComponent<UI2DSprite>().width)), _retryButton.transform.localPosition.y, 0);

            _scoreLabel.transform.localPosition = new Vector3(0, Screen.height * .6f);
            _shadowScore.transform.localPosition = new Vector3(_scoreLabel.transform.localPosition.x - 3, _scoreLabel.transform.localPosition.y - 3);
            _recordLabel.transform.localPosition = new Vector3(0, _scoreLabel.transform.position.y - _scoreLabel.GetComponent<UILabel>().height);
            _shadowRecord.transform.localPosition = new Vector3(_recordLabel.transform.localPosition.x - 3, _recordLabel.transform.localPosition.y - 3);*/
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
            HudController _hudController;
            _hudController = FindObjectOfType<HudController>();
            _hudController.LoadFont();
            _hudController.AddPauseButtonListener();
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
            if (_balloonFactory != null)
                _balloonFactory.enabled = !_paused;

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
