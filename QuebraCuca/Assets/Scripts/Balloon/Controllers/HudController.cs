using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class HudController : ASingleton<HudController>
    {
        public GameObject _scoreHUD;
        private GameObject _recordHUD;
        private GameObject _lifeHUD;
        private List<GameObject> _listOfLifes;
        public GameObject _pauseButton;
        private UIFont _labelFont;
        private Font _scoreFont;
        private Font _finalScoreFont;
        private Font _recordFont;
        private int _fontSize = 100;
        private int _scoreFontSize = 150;
        private int _timeFontSize = 120;
        private GameObject _pauseScreen;
        private GameObject _exitButton;
        private GameObject _returnButton;
        private GameObject _soundButton;
        private int _offSetX = 20;
        private Sprite _soundButton_On;
        private Sprite _soundButton_Off;

        private GameObject _timeIcon;
        private GameObject _timeText;
        private GameObject _lifeIcon;
        private GameObject _pointsText;
        private GameObject _recordText;
        private Canvas _canvas;

        public void Initialize()
        {
            UnityEngine.Debug.Log("[ HUD_CONTROLLER ] - START");

            LoadFont();

            if (GameController.GetGameMode() == GameController.SURVIVAL)
            {
                _timeText.SetActive(false);
                _timeIcon.SetActive(false);
                InitLife();
            }
            else
            {
                _timeText.SetActive(true);
                _timeIcon.SetActive(true);
                HideLifes();
            }

        }

        private void InitLife()
        {
            for (int j = 0; j < LifeController.GetInitialLife(); j++)
            {
                _listOfLifes[j].GetComponent<Image>().sprite = AFAssetManager.Instance.Load<Sprite>(AFAssetManager.GetPathTargetPlatformWithResolution() + ConstantsPaths.GetInGamePath() + "lifeIcon_full");
            }
        }

        private void HideLifes()
        {
            for (int j = 0; j < LifeController.GetInitialLife(); j++)
            {
                _listOfLifes[j].SetActive(false);
            }
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
                        _life.AddComponent<Image>().sprite = AFAssetManager.Instance.Load<Sprite>(AFAssetManager.GetPathTargetPlatformWithResolution() + ConstantsPaths.GetInGamePath() + "lifeIcon_full");
                        _life.transform.parent = _canvas.transform;
                        _listOfLifes.Add(_life);

                        float lifeWidth = _life.GetComponent<Image>().sprite.bounds.size.x;

                        _listOfLifes[i].transform.localPosition = new Vector3(Screen.width - (_life.GetComponent<Image>().sprite.bounds.size.x * i) - _offSetX * i, (Screen.height * 2 - _life.GetComponent<Image>().sprite.bounds.size.y / 2f));
                    }
                }
                else
                {
                    for (int j = 0; j < LifeController.GetInitialLife(); j++)
                    {
                        _listOfLifes[j].GetComponent<Image>().sprite = AFAssetManager.Instance.Load<Sprite>(AFAssetManager.GetPathTargetPlatformWithResolution() + ConstantsPaths.GetInGamePath() + "lifeIcon_full");
                    }
                }
            }
        }

        public override void AFUpdate(double deltaTime)
        {
            if (GameController.GetGameMode() == GameController.TIME_TRIAL)
            {
                _timeText.GetComponent<Text>().text = TimeController.GetTime();
            }
            else
            {
                for (int i = 0; i < _listOfLifes.Count(); i++)
                {
                    if (LifeController.GetLife() > 0)
                    {
                        for (int k = LifeController.GetLife(); k < LifeController.GetInitialLife(); k++)
                        {
                            _listOfLifes[k].GetComponent<Image>().sprite = AFAssetManager.Instance.Load<Sprite>(AFAssetManager.GetPathTargetPlatformWithResolution() + ConstantsPaths.GetInGamePath() + "lifeIcon_empty");
                        }
                    }
                }
            }

            _pointsText.GetComponent<Text>().text = ScoreController.GetPoints().ToString();
            _recordText.GetComponent<Text>().text = "SEU RECORDE: " + ScoreController.GetRecord().ToString();
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

        internal void SetTimeText(GameObject value)
        {
            _timeText = value;
        }

        internal void SetTimeIcon(GameObject value)
        {
            _timeIcon = value;
        }

        internal void SetLifeIcon(GameObject value)
        {
            _lifeIcon = value;
        }

        internal void SetPointsText(GameObject value)
        {
            _pointsText = value;
        }

        internal void SetRecordText(GameObject value)
        {
            _recordText = value;
        }

        internal void SetCanvas(Canvas value)
        {
            _canvas = value;
        }

        internal void AddLifeToList(GameObject value)
        {
            if (_listOfLifes == null)
            {
                _listOfLifes = new List<GameObject>();
            }
            _listOfLifes.Add(value);
        }
    }
}
