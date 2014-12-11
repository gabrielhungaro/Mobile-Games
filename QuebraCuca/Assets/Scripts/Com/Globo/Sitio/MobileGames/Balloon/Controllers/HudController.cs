using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class HudController : MonoBehaviour
    {
        public GameObject _camera;
        public GameObject _scoreHUD;
        public GameObject _scoreShadowHUD;
        private GameObject _recordHUD;
        private GameObject _lifeHUD;
        private List<GameObject> _listOfLifes;
        public GameObject _timeHUD;
        public GameObject _pauseButton;
        private GameObject _gameControllerObj;
        private UIFont _labelFont;
        private Font _scoreFont;
        private Font _finalScoreFont;
        private Font _recordFont;
        private int _fontSize = 100;
        private int _scoreFontSize = 150;
        private int _timeFontSize = 120;
        private bool _canUpdateHudPosition;
        private GameObject _pauseScreen;
        private GameObject _exitButton;
        private GameObject _returnButton;
        private GameObject _soundButton;
        private int _offSetX = 20;
        private Sprite _soundButton_On;
        private Sprite _soundButton_Off;
        private GameObject _timeIcon;
        private GameObject _timeShadow;

        void Start()
        {
            UnityEngine.Debug.Log("[ HUD_CONTROLLER ] - START");

            GameController gameController = FindObjectOfType<GameController>();
            _gameControllerObj = gameController.gameObject;

            LoadFont();

            LayerController.Instance().Start();

            this.gameObject.layer = LayerController.Instance()._layerHUD;
            this.gameObject.AddComponent<UIRoot>();
            this.gameObject.AddComponent<UIPanel>();
            this.gameObject.AddComponent<Rigidbody>();
            this.gameObject.GetComponent<Rigidbody>().useGravity = false;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            this.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
            this.GetComponent<UIRoot>().manualHeight = CameraController.CAMERA_HEIGHT;

            _camera = new GameObject();
            _camera.name = "uiCamera";
            _camera.AddComponent<Camera>();
            _camera.AddComponent<UICamera>();
            _camera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            _camera.GetComponent<Camera>().cullingMask = LayerController.Instance()._maskHud;
            _camera.GetComponent<Camera>().isOrthoGraphic = CameraController.ORTHOGRAPHIC;
            _camera.GetComponent<Camera>().orthographicSize = CameraController.ORTHOGRAPHIC_SIZE;
            _camera.GetComponent<Camera>().nearClipPlane = CameraController.NEAR_CLIP_PLANE;
            _camera.GetComponent<Camera>().farClipPlane = CameraController.FAR_CLIP_PLANE;
            _camera.transform.parent = this.gameObject.transform;

            CreateLifeHud();
            CreateScoreHUD();
            CreateTimeHUD();
            CreatePauseButton();

            _canUpdateHudPosition = true;
        }

        public void LoadFont()
        {
            _labelFont = Resources.Load<UIFont>("Fonts/Arimo21");
            _scoreFont = Resources.Load<Font>("Fonts/TypographyofCoop-Black");
            _finalScoreFont = Resources.Load<Font>("Fonts/Ed-Gothic");
        }

        public void CreateLifeHud()
        {
            if (GameController.GetGameMode() == GameController.SURVIVAL)
            {
                if (_lifeHUD == null)
                {
                    _listOfLifes = new List<GameObject>();
                    _lifeHUD = new GameObject();
                    _lifeHUD.name = "lifeHud";
                    _lifeHUD.transform.parent = this.gameObject.transform;

                    for (int i = 0; i < LifeController.GetInitialLife(); i++)
                    {
                        GameObject _life = new GameObject();
                        //_life.transform.parent = _lifeHUD.transform;
                        _life.name = "life" + i;
                        _life.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "lifeIcon_full");
                        _life.GetComponent<UI2DSprite>().MakePixelPerfect();
                        _listOfLifes.Add(_life);

                        int lifeWidth = _life.GetComponent<UI2DSprite>().width;

                        _life.GetComponent<UI2DSprite>().SetAnchor(this.gameObject);
                        _life.GetComponent<UI2DSprite>().topAnchor.absolute = 817;
                        _life.GetComponent<UI2DSprite>().bottomAnchor.absolute = 528;
                        _life.GetComponent<UI2DSprite>().leftAnchor.absolute = 622 - lifeWidth * i - (_offSetX * i);
                        _life.GetComponent<UI2DSprite>().rightAnchor.absolute = 1320 - lifeWidth * i - (_offSetX * i);
                        _life.GetComponent<UI2DSprite>().UpdateAnchors();
                        _life.GetComponent<UI2DSprite>().MakePixelPerfect();
                    }
                }
                else
                {
                    for (int j = 0; j < LifeController.GetInitialLife(); j++)
                    {
                        _listOfLifes[j].GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "lifeIcon_full");
                    }
                }
            }
        }

        public void CreateScoreHUD()
        {
            _scoreShadowHUD = new GameObject();
            _scoreShadowHUD.name = "scoreShadow";
            _scoreShadowHUD.AddComponent<UILabel>().text = "00";
            _scoreShadowHUD.GetComponent<UILabel>().trueTypeFont = _scoreFont;
            _scoreShadowHUD.GetComponent<UILabel>().fontSize = _scoreFontSize;
            Color _scoreShadowColor = new Color(0f, 0f, 0f, 15 / 255f);
            _scoreShadowHUD.GetComponent<UILabel>().color = _scoreShadowColor;

            _scoreHUD = new GameObject();
            _scoreHUD.name = "scoreLabel";
            _scoreHUD.AddComponent<UILabel>().text = "00";
            _scoreHUD.GetComponent<UILabel>().trueTypeFont = _scoreFont;
            _scoreHUD.GetComponent<UILabel>().fontSize = _scoreFontSize;
            _scoreHUD.GetComponent<UILabel>().alignment = NGUIText.Alignment.Left;
            Color _scoreColor = new Color(254 / 255f, 227 / 255f, 1 / 255f, 255 / 255f);
            _scoreHUD.GetComponent<UILabel>().color = _scoreColor;

            _recordHUD = new GameObject();
            _recordHUD.name = "recordLabel";
            _recordHUD.AddComponent<UILabel>().text = "SEU RECORDE:";
            _recordHUD.GetComponent<UILabel>().trueTypeFont = _finalScoreFont;
            _recordHUD.GetComponent<UILabel>().fontSize = 60;
            Color _recordColor = new Color(18 / 255f, 139 / 255f, 140 / 255f, 255 / 255f);
            _recordHUD.GetComponent<UILabel>().color = _recordColor;

            int scoreWidth = _scoreHUD.GetComponent<UILabel>().width;
            int scoreHeight = _scoreHUD.GetComponent<UILabel>().height;

            _scoreHUD.GetComponent<UILabel>().SetAnchor(this.gameObject);
            _scoreHUD.GetComponent<UILabel>().topAnchor.absolute = 837;
            _scoreHUD.GetComponent<UILabel>().bottomAnchor.absolute = 548;
            _scoreHUD.GetComponent<UILabel>().leftAnchor.absolute = -994;
            _scoreHUD.GetComponent<UILabel>().rightAnchor.absolute = -894;
            _scoreHUD.GetComponent<UILabel>().UpdateAnchors();
            _scoreHUD.GetComponent<UILabel>().MakePixelPerfect();

            _scoreShadowHUD.GetComponent<UILabel>().SetAnchor(this.gameObject);
            _scoreShadowHUD.GetComponent<UILabel>().topAnchor.absolute = 827;
            _scoreShadowHUD.GetComponent<UILabel>().bottomAnchor.absolute = 538;
            _scoreShadowHUD.GetComponent<UILabel>().leftAnchor.absolute = -1004;
            _scoreShadowHUD.GetComponent<UILabel>().rightAnchor.absolute = -904;
            _scoreShadowHUD.GetComponent<UILabel>().UpdateAnchors();
            _scoreShadowHUD.GetComponent<UILabel>().MakePixelPerfect();

            _recordHUD.GetComponent<UILabel>().SetAnchor(this.gameObject);
            _recordHUD.GetComponent<UILabel>().topAnchor.absolute = 727;
            _recordHUD.GetComponent<UILabel>().bottomAnchor.absolute = 438;
            _recordHUD.GetComponent<UILabel>().leftAnchor.absolute = -994;
            _recordHUD.GetComponent<UILabel>().rightAnchor.absolute = -593;
            _recordHUD.GetComponent<UILabel>().UpdateAnchors();
            _recordHUD.GetComponent<UILabel>().MakePixelPerfect();


        }

        public void CreateTimeHUD()
        {
            if (GameController.GetGameMode() == GameController.TIME_TRIAL)
            {

                _timeShadow = new GameObject();
                _timeShadow.name = "timeShadow";
                _timeShadow.AddComponent<UILabel>().text = "Time:";
                _timeShadow.GetComponent<UILabel>().trueTypeFont = _scoreFont;
                _timeShadow.GetComponent<UILabel>().fontSize = _timeFontSize;
                Color _timeShadowColor = new Color(0f, 0f, 0f, 15 / 255f);
                _timeShadow.GetComponent<UILabel>().color = _timeShadowColor;

                _timeHUD = new GameObject();
                _timeHUD.name = "timeLabel";
                _timeHUD.AddComponent<UILabel>().text = "Time:";
                _timeHUD.GetComponent<UILabel>().trueTypeFont = _scoreFont;
                _timeHUD.GetComponent<UILabel>().fontSize = _timeFontSize;
                _timeHUD.GetComponent<UILabel>().MakePixelPerfect();
                Color _timeColor = new Color(254 / 255f, 227 / 255f, 1 / 255f, 255 / 255f);
                _timeHUD.GetComponent<UILabel>().color = _timeColor;

                _timeIcon = new GameObject();
                _timeIcon.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "timeIcon");
                _timeIcon.GetComponent<UI2DSprite>().MakePixelPerfect();

                _timeHUD.GetComponent<UILabel>().SetAnchor(this.gameObject);
                _timeHUD.GetComponent<UILabel>().topAnchor.absolute = 837;
                _timeHUD.GetComponent<UILabel>().bottomAnchor.absolute = 548;
                _timeHUD.GetComponent<UILabel>().leftAnchor.absolute = 430;
                _timeHUD.GetComponent<UILabel>().rightAnchor.absolute = 1120;
                _timeHUD.GetComponent<UILabel>().UpdateAnchors();
                _timeHUD.GetComponent<UILabel>().MakePixelPerfect();

                _timeShadow.GetComponent<UILabel>().SetAnchor(this.gameObject);
                _timeShadow.GetComponent<UILabel>().topAnchor.absolute = 827;
                _timeShadow.GetComponent<UILabel>().bottomAnchor.absolute = 538;
                _timeShadow.GetComponent<UILabel>().leftAnchor.absolute = 420;
                _timeShadow.GetComponent<UILabel>().rightAnchor.absolute = 1110;
                _timeShadow.GetComponent<UILabel>().UpdateAnchors();
                _timeShadow.GetComponent<UILabel>().MakePixelPerfect();

                _timeIcon.GetComponent<UI2DSprite>().SetAnchor(this.gameObject);
                _timeIcon.GetComponent<UI2DSprite>().topAnchor.absolute = 720;
                _timeIcon.GetComponent<UI2DSprite>().bottomAnchor.absolute = 660;
                _timeIcon.GetComponent<UI2DSprite>().leftAnchor.absolute = 480;
                _timeIcon.GetComponent<UI2DSprite>().rightAnchor.absolute = 538;
                _timeIcon.GetComponent<UI2DSprite>().UpdateAnchors();
                _timeIcon.GetComponent<UI2DSprite>().MakePixelPerfect();
            }
        }

        private void CreatePauseButton()
        {
            _pauseButton = new GameObject();
            _pauseButton.name = "pauseButton";
            _pauseButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "btnPause");
            _pauseButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _pauseButton.AddComponent<BoxCollider>().size = new Vector3(_pauseButton.GetComponent<UI2DSprite>().width,
                                                                        _pauseButton.GetComponent<UI2DSprite>().height);
            int buttonWidth = _pauseButton.GetComponent<UI2DSprite>().width;
            int buttonHeight = _pauseButton.GetComponent<UI2DSprite>().height;

            _pauseButton.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _pauseButton.GetComponent<UI2DSprite>().SetAnchor(this.gameObject);
            /*_pauseButton.GetComponent<UI2DSprite>().topAnchor.absolute = -(Screen.height + buttonHeight);
            _pauseButton.GetComponent<UI2DSprite>().bottomAnchor.absolute = -Screen.height;
            _pauseButton.GetComponent<UI2DSprite>().leftAnchor.absolute = Screen.width;
            _pauseButton.GetComponent<UI2DSprite>().rightAnchor.absolute = Screen.width + buttonWidth;*/
            _pauseButton.GetComponent<UI2DSprite>().topAnchor.absolute = -458;
            _pauseButton.GetComponent<UI2DSprite>().bottomAnchor.absolute = -646;
            _pauseButton.GetComponent<UI2DSprite>().leftAnchor.absolute = 610;
            _pauseButton.GetComponent<UI2DSprite>().rightAnchor.absolute = 802;
            _pauseButton.GetComponent<UI2DSprite>().UpdateAnchors();
            _pauseButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            UIEventListener.Get(_pauseButton).onClick += OnClickPause;
        }

        private void OnClickPause(GameObject g)
        {
            if (_pauseScreen == null)
            {
                ShowPauseScreen();
                _gameControllerObj.GetComponent<GameController>().SetIsPaused(!_gameControllerObj.GetComponent<GameController>().GetIsPaused());
            }
        }

        public void RemovePauseButtonListener()
        {
            if (_pauseButton != null)
                _pauseButton.GetComponent<BoxCollider>().enabled = false;
        }

        public void AddPauseButtonListener()
        {
            if (_pauseButton != null)
                _pauseButton.GetComponent<BoxCollider>().enabled = true;
        }

        private void ShowPauseScreen()
        {
            _pauseScreen = new GameObject();
            _pauseScreen.name = "PauseScreen";
            _pauseScreen.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "pauseBackground");
            _pauseScreen.GetComponent<UI2DSprite>().MakePixelPerfect();
            _pauseScreen.GetComponent<UIWidget>().depth = -1;

            _exitButton = new GameObject();
            _exitButton.name = "ExitButton";
            _exitButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "btnExit");
            _exitButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _exitButton.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _exitButton.AddComponent<BoxCollider>().size = new Vector3(_exitButton.GetComponent<UI2DSprite>().width,
                                                                        _exitButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_exitButton).onClick += OnClickExit;
            _exitButton.transform.parent = _pauseScreen.transform;

            _returnButton = new GameObject();
            _returnButton.name = "ReturnButton";
            _returnButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "btnReturn");
            _returnButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _returnButton.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _returnButton.AddComponent<BoxCollider>().size = new Vector3(_returnButton.GetComponent<UI2DSprite>().width,
                                                                               _returnButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_returnButton).onClick += OnClickReturn;
            _returnButton.transform.parent = _pauseScreen.transform;

            _soundButton_On = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "btnSound_On");
            _soundButton_Off = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "btnSound_Off");
            _soundButton = new GameObject();
            _soundButton.name = "SoundButton";
            _soundButton.AddComponent<UI2DSprite>().sprite2D = _soundButton_On;
            _soundButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _soundButton.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _soundButton.AddComponent<BoxCollider>().size = new Vector3(_soundButton.GetComponent<UI2DSprite>().width,
                                                                               _soundButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_soundButton).onClick += OnClickSound;
            _soundButton.transform.parent = _pauseScreen.transform;

            //_retryButton.transform.position = new Vector3(0, -(_retryButton.GetComponent<UI2DSprite>().height));
            //_pointsScreenButton.transform.position = new Vector3(0, (_retryButton.transform.position.y - _pointsScreenButton.GetComponent<UI2DSprite>().height));

        }

        private void OnClickSound(GameObject go)
        {
            if (SoundManager.GetAllSoundsIsMuting() == false)
            {
                _soundButton.GetComponent<UI2DSprite>().sprite2D = _soundButton_Off;
                SoundManager.MuteAllSounds();
            }
            else
            {
                _soundButton.GetComponent<UI2DSprite>().sprite2D = _soundButton_On;
                SoundManager.UnMuteAllSounds();
            }
            _soundButton.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

        private void OnClickReturn(GameObject go)
        {
            _gameControllerObj.GetComponent<GameController>().SetIsPaused(!_gameControllerObj.GetComponent<GameController>().GetIsPaused());
            RemovePauseScreen();
        }

        private void OnClickExit(GameObject go)
        {
            Application.LoadLevel(SceneManager.START_SCENE);
        }

        private void RemovePauseScreen()
        {
            Destroy(_pauseScreen);
        }

        void Update()
        {
            //if (_canUpdateHudPosition == true)
            //{
            if (_pauseScreen != null)
            {
                _returnButton.transform.localPosition = new Vector3(0, -(Screen.height * .2f));
                _exitButton.transform.localPosition = new Vector3(_returnButton.transform.localPosition.x - _exitButton.GetComponent<UI2DSprite>().width - _offSetX * 2, _returnButton.transform.localPosition.y);
                _soundButton.transform.localPosition = new Vector3(_returnButton.transform.localPosition.x + _soundButton.GetComponent<UI2DSprite>().width + _offSetX * 2, _returnButton.transform.localPosition.y);
            }

            if (GameController.GetGameMode() == GameController.TIME_TRIAL)
            {
                _timeHUD.GetComponent<UILabel>().text = TimeController.GetTime();
                _timeHUD.GetComponent<UILabel>().MakePixelPerfect();
                _timeShadow.transform.localPosition = new Vector3(_timeHUD.transform.localPosition.x - 10,
                                                                  _timeHUD.transform.localPosition.y - 10);
                _timeShadow.GetComponent<UILabel>().text = _timeHUD.GetComponent<UILabel>().text;
                _timeShadow.GetComponent<UILabel>().MakePixelPerfect();
            }
            else
            {
                for (int i = 0; i < _listOfLifes.Count(); i++)
                {
                    _listOfLifes[i].transform.localPosition = new Vector3(Screen.width * 2 - (_listOfLifes[i].GetComponent<UI2DSprite>().width * i) - _offSetX * i, (Screen.height * 2 - _listOfLifes[i].GetComponent<UI2DSprite>().height / 2f));
                    //_listOfLifes[i].transform.localPosition = new Vector3(((Screen.width - ((_listOfLifes[i].GetComponent<UI2DSprite>().width / 2f) * i)) / 2f), (Screen.height / 2f));
                    if (LifeController.GetLife() > 0)
                    {
                        for (int k = LifeController.GetLife(); k < LifeController.GetInitialLife(); k++)
                        {
                            _listOfLifes[k].GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetInGamePath() + "lifeIcon_empty");
                            //_listOfLifes[k].SetActive(false);
                        }
                    }
                }

            }

            _scoreHUD.GetComponent<UILabel>().text = ScoreController.GetPoints().ToString();
            _scoreHUD.GetComponent<UILabel>().MakePixelPerfect();
            _scoreShadowHUD.GetComponent<UILabel>().text = _scoreHUD.GetComponent<UILabel>().text;
            _scoreShadowHUD.GetComponent<UILabel>().MakePixelPerfect();
            _recordHUD.GetComponent<UILabel>().text = "SEU RECORDE: " + ScoreController.GetRecord().ToString();
            _recordHUD.GetComponent<UILabel>().MakePixelPerfect();
            _canUpdateHudPosition = false;
            //}

        }

        public UIFont GetFontType()
        {
            return _labelFont;
        }

        public Font GetFinalScoreFont()
        {
            return _finalScoreFont;
        }

        public int GetFontSize()
        {
            return _fontSize;
        }
    }
}
