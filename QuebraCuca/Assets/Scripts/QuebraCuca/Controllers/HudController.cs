using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;
using com.globo.sitio.mobilegames.QuebraCuca.States;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class HudController : MonoBehaviour
    {
        private GameObject _pointsHud;
        private GameObject _pointsObj;
        private GameObject _lifeHud;
        private GameObject _lifesObj;
        private Font _hudFont;
        private int _fontSize;
        private PointsController _pointsController;
        private int _points;
        private int _lifes;
        private GameObject _anchorTarget;
        private List<GameObject> _listOfLifes;
        private GameObject _lifeHUD;
        private int _offSetX = 20;

        void Start()
        {
            _hudFont = Resources.Load<Font>(PathConstants._fontPath + "Ed-Gothic");
            _fontSize = 80;
            _pointsController = PointsController.Instance();
            _points = PointsController.GetPoints();
            _lifes = LifesController.GetLifes();

            Color hudColor = new Color(13f / 255f, 140f / 255f, 7f / 255f, 255 / 255f);
            _pointsHud = new GameObject();
            _pointsHud.name = "_pointsHud";
            _pointsHud.AddComponent<UILabel>().text = "PONTOS";
            _pointsHud.GetComponent<UILabel>().trueTypeFont = _hudFont;
            _pointsHud.GetComponent<UILabel>().fontSize = _fontSize;
            _pointsHud.GetComponent<UILabel>().color = hudColor;
            _pointsHud.GetComponent<UILabel>().MakePixelPerfect();
            _pointsHud.GetComponent<UILabel>().SetAnchor(_anchorTarget);
            _pointsHud.GetComponent<UILabel>().leftAnchor.absolute = -595;
            _pointsHud.GetComponent<UILabel>().rightAnchor.absolute = -337;
            _pointsHud.GetComponent<UILabel>().bottomAnchor.absolute = 666;
            _pointsHud.GetComponent<UILabel>().topAnchor.absolute = 766;
            _pointsHud.GetComponent<UILabel>().UpdateAnchors();
            _pointsHud.GetComponent<UILabel>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_pointsHud, 4);

            /*_lifeHud = new GameObject();
            _lifeHud.name = "_lifeHud";
            _lifeHud.AddComponent<UILabel>().text = "VIDAS";
            _lifeHud.GetComponent<UILabel>().trueTypeFont = _hudFont;
            _lifeHud.GetComponent<UILabel>().fontSize = _fontSize;
            _lifeHud.GetComponent<UILabel>().color = hudColor;
            _lifeHud.GetComponent<UILabel>().MakePixelPerfect();
            _lifeHud.GetComponent<UILabel>().SetAnchor(_anchorTarget);
            _lifeHud.GetComponent<UILabel>().leftAnchor.absolute = 158;
            _lifeHud.GetComponent<UILabel>().rightAnchor.absolute = 384;
            _lifeHud.GetComponent<UILabel>().bottomAnchor.absolute = 666;
            _lifeHud.GetComponent<UILabel>().topAnchor.absolute = 766;
            _lifeHud.GetComponent<UILabel>().UpdateAnchors();
            _lifeHud.GetComponent<UILabel>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_lifeHud, 4);*/

            Color textColor = new Color(24f / 255f, 174f / 255f, 16f / 255f, 255 / 255f);
            _pointsObj = new GameObject();
            _pointsObj.name = "_points";
            _pointsObj.AddComponent<UILabel>().text = _points.ToString();
            _pointsObj.GetComponent<UILabel>().trueTypeFont = _hudFont;
            _pointsObj.GetComponent<UILabel>().fontSize = _fontSize;
            _pointsObj.GetComponent<UILabel>().color = textColor;
            _pointsObj.GetComponent<UILabel>().MakePixelPerfect();
            _pointsObj.GetComponent<UILabel>().SetAnchor(_anchorTarget);
            _pointsObj.GetComponent<UILabel>().leftAnchor.absolute = -299;
            _pointsObj.GetComponent<UILabel>().rightAnchor.absolute = -219;
            _pointsObj.GetComponent<UILabel>().bottomAnchor.absolute = 666;
            _pointsObj.GetComponent<UILabel>().topAnchor.absolute = 766;
            _pointsObj.GetComponent<UILabel>().UpdateAnchors();
            _pointsObj.GetComponent<UILabel>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_pointsObj, 4);

            /*_lifesObj = new GameObject();
            _lifesObj.name = "_lifes";
            _lifesObj.AddComponent<UILabel>().text = _lifes.ToString();
            _lifesObj.GetComponent<UILabel>().trueTypeFont = _hudFont;
            _lifesObj.GetComponent<UILabel>().fontSize = _fontSize;
            _lifesObj.GetComponent<UILabel>().color = textColor;
            _lifesObj.GetComponent<UILabel>().MakePixelPerfect();
            _lifesObj.GetComponent<UILabel>().SetAnchor(_anchorTarget);
            _lifesObj.GetComponent<UILabel>().leftAnchor.absolute = 345;
            _lifesObj.GetComponent<UILabel>().rightAnchor.absolute = 445;
            _lifesObj.GetComponent<UILabel>().bottomAnchor.absolute = 666;
            _lifesObj.GetComponent<UILabel>().topAnchor.absolute = 766;
            _lifesObj.GetComponent<UILabel>().UpdateAnchors();
            _lifesObj.GetComponent<UILabel>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_lifesObj, 4);*/

            CreateLifeHud();
        }

        public void CreateLifeHud()
        {
            _listOfLifes = new List<GameObject>();
            _lifeHUD = new GameObject();
            _lifeHUD.name = "lifeHud";
            _lifeHUD.transform.parent = this.gameObject.transform;

            for (int i = 0; i < LifesController.GetInitialLifes(); i++)
            {
                GameObject _life = new GameObject();
                //_life.transform.parent = _lifeHUD.transform;
                _life.name = "life" + i;
                _life.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "lifeIcon_full");
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
                FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_life, 4);
            }
        }

        void Update()
        {
            _points = PointsController.GetPoints();
            if (_pointsObj != null)
            {
                _pointsObj.GetComponent<UILabel>().text = _points.ToString();
            }

            _lifes = LifesController.GetLifes();
            if (_lifesObj != null)
            {
                _lifesObj.GetComponent<UILabel>().text = _lifes.ToString();
            }

            for (int i = 0; i < _listOfLifes.Count(); i++)
            {
                _listOfLifes[i].transform.localPosition = new Vector3(Screen.width * 2 - (_listOfLifes[i].GetComponent<UI2DSprite>().width * i) - _offSetX * i, (Screen.height * 2 - _listOfLifes[i].GetComponent<UI2DSprite>().height / 2f));
                if (LifesController.GetLifes() > 0)
                {
                    for (int k = LifesController.GetLifes(); k < LifesController.GetInitialLifes(); k++)
                    {
                        _listOfLifes[k].GetComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "lifeIcon_empty");
                    }
                }
            }
        }

        public void SetAnchorTarget(GameObject value)
        {
            _anchorTarget = value;
        }
    }
}
