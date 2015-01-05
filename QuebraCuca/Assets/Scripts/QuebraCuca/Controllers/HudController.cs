using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;
using com.globo.sitio.mobilegames.QuebraCuca.States;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class HudController : MonoBehaviour
    {
        private GameObject _pointsHud;
        private GameObject _pointsObj;
        private GameObject _lifesObj;
        private GameObject _lifeHUD;
        private Font _hudFont;
        private int _fontSize;
        private PointsController _pointsController;
        private int _points;
        private int _lifes;
        private GameObject _anchorTarget;
        private List<GameObject> _listOfLifes;
        
        private float _offSetX = 20f;
        private GameObject _canvas;
        private Vector2 _anchorMin = new Vector2(0, 1);
        private Vector2 _anchorMax = new Vector2(0, 1);

        void Start()
        {
            _hudFont = Resources.Load<Font>(PathConstants._fontPath + "Ed-Gothic");
            _fontSize = 80;
            Color hudColor = new Color(13f / 255f, 140f / 255f, 7f / 255f, 255 / 255f);
            Color textColor = new Color(24f / 255f, 174f / 255f, 16f / 255f, 255 / 255f);

            _pointsController = PointsController.Instance();
            _points = PointsController.GetPoints();
            _lifes = LifesController.GetLifes();

            _canvas = new GameObject();
            _canvas.name = "Canvas";
            Canvas canvasComponent = _canvas.AddComponent<Canvas>();
            CanvasScaler scalerComponent = _canvas.AddComponent<CanvasScaler>();
            GraphicRaycaster raycasterComponent = _canvas.AddComponent<GraphicRaycaster>();

            canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
            scalerComponent.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scalerComponent.referenceResolution = new Vector2(2048, 1536);
            scalerComponent.matchWidthOrHeight = .5f;

            _pointsHud = new GameObject();
            _pointsHud.transform.parent = _canvas.transform;
            _pointsHud.name = "PointsHud";
            _pointsHud.AddComponent<CanvasRenderer>();
            Text textComponent = _pointsHud.AddComponent<Text>();
            textComponent.text = "PONTOS";
            textComponent.font = _hudFont;
            textComponent.fontSize = _fontSize;
            textComponent.color = hudColor;
            textComponent.rectTransform.anchorMin = _anchorMin;
            textComponent.rectTransform.anchorMax = _anchorMax;
            _pointsHud.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textComponent.preferredWidth);
            _pointsHud.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textComponent.preferredHeight);
            _pointsHud.GetComponent<RectTransform>().anchoredPosition = new Vector2(540f, -60f);

            _pointsObj = new GameObject();
            _pointsObj.transform.parent = _canvas.transform;
            _pointsObj.AddComponent<CanvasRenderer>();
            Text pointsText = _pointsObj.AddComponent<Text>();
            pointsText.text = _points.ToString();
            pointsText.font = _hudFont;
            pointsText.fontSize = _fontSize;
            pointsText.color = textColor;
            pointsText.rectTransform.anchorMin = _anchorMin;
            pointsText.rectTransform.anchorMax = _anchorMax;
            _pointsObj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pointsText.preferredWidth);
            _pointsObj.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, pointsText.preferredHeight);
            _pointsObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(_pointsHud.GetComponent<RectTransform>().anchoredPosition.x + textComponent.preferredWidth, _pointsHud.GetComponent<RectTransform>().anchoredPosition.y);

            _lifeHUD = new GameObject();
            _lifeHUD.transform.parent = _canvas.transform;
            _lifeHUD.AddComponent<CanvasRenderer>();
            Text lifeTextHUD = _lifeHUD.AddComponent<Text>();
            lifeTextHUD.text = "VIDAS";
            lifeTextHUD.font = _hudFont;
            lifeTextHUD.fontSize = _fontSize;
            lifeTextHUD.color = hudColor;
            lifeTextHUD.rectTransform.anchorMin = _anchorMin;
            lifeTextHUD.rectTransform.anchorMax = _anchorMax;
            _lifeHUD.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lifeTextHUD.preferredWidth);
            _lifeHUD.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, lifeTextHUD.preferredHeight);
            _lifeHUD.GetComponent<RectTransform>().anchoredPosition = new Vector2(1300f, _pointsHud.GetComponent<RectTransform>().anchoredPosition.y);
            //_lifeHUD.GetComponent<RectTransform>().anchoredPosition = new Vector2(_lifeHUD.GetComponent<RectTransform>().anchoredPosition.x + lifeTextHUD.preferredWidth, _lifeHUD.GetComponent<RectTransform>().anchoredPosition.y

            CreateLifeHud();
        }

        public void CreateLifeHud()
        {
            _listOfLifes = new List<GameObject>();
            /*_lifeHUD = new GameObject();
            _lifeHUD.name = "lifeHud";
            _lifeHUD.transform.parent = this.gameObject.transform;*/

            for (int i = 0; i < LifesController.GetInitialLifes(); i++)
            {
                GameObject _life = new GameObject();
                _life.transform.parent = _canvas.transform;
                _life.name = "life" + i;
                _life.AddComponent<CanvasRenderer>();
                _life.AddComponent<Image>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "lifeIcon_full");
                _life.GetComponent<Image>().preserveAspect = true;
                _listOfLifes.Add(_life);

                float lifeWidth = _life.GetComponent<Image>().sprite.bounds.size.x * 100f;

                Debug.Log("lifeWidth: " + lifeWidth);

                _life.GetComponent<RectTransform>().anchorMin = _anchorMin;
                _life.GetComponent<RectTransform>().anchorMax = _anchorMax;
                _life.GetComponent<RectTransform>().anchoredPosition = new Vector2(_lifeHUD.GetComponent<RectTransform>().anchoredPosition.x + _lifeHUD.GetComponent<Text>().preferredWidth + lifeWidth * i + (_offSetX * i), _lifeHUD.GetComponent<RectTransform>().anchoredPosition.y);

                /*_life.GetComponent<UI2DSprite>().SetAnchor(this.gameObject);
                _life.GetComponent<UI2DSprite>().topAnchor.absolute = 817;
                _life.GetComponent<UI2DSprite>().bottomAnchor.absolute = 528;
                _life.GetComponent<UI2DSprite>().leftAnchor.absolute = 622 - lifeWidth * i - (_offSetX * i);
                _life.GetComponent<UI2DSprite>().rightAnchor.absolute = 1320 - lifeWidth * i - (_offSetX * i);
                _life.GetComponent<UI2DSprite>().UpdateAnchors();
                _life.GetComponent<UI2DSprite>().MakePixelPerfect();
                FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_life, 4);*/
            }
        }

        void Update()
        {
            _points = PointsController.GetPoints();
            if (_pointsObj != null)
            {
                _pointsObj.GetComponent<Text>().text = _points.ToString();
            }

            _lifes = LifesController.GetLifes();
            /*if (_lifesObj != null)
            {
                _lifesObj.GetComponent<UILabel>().text = _lifes.ToString();
            }*/

            for (int i = 0; i < _listOfLifes.Count(); i++)
            {
                //_listOfLifes[i].transform.localPosition = new Vector3(Screen.width * 2 - (_listOfLifes[i].GetComponent<UI2DSprite>().width * i) - _offSetX * i, (Screen.height * 2 - _listOfLifes[i].GetComponent<UI2DSprite>().height / 2f));
                if (LifesController.GetLifes() > 0)
                {
                    for (int k = LifesController.GetLifes(); k < LifesController.GetInitialLifes(); k++)
                    {
                        _listOfLifes[k].GetComponent<Image>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "lifeIcon_empty");
                    }
                }
            }
        }

        public void Destroy()
        {
            GameObject.Destroy(_pointsHud);
            GameObject.Destroy(_pointsObj);
            GameObject.Destroy(_lifesObj);
            GameObject.Destroy(_lifeHUD);
        }

        public void SetAnchorTarget(GameObject value)
        {
            _anchorTarget = value;
        }
    }
}
