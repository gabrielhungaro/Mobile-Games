using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;
using com.globo.sitio.mobilegames.QuebraCuca.States;

using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class HudController : MonoBehaviour
    {
        private GameObject _pointsHud;
        private GameObject _pointsObj;
        private GameObject _lifesObj;
        private GameObject _lifeHUD;
        private GameObject m_pauseBtn;

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

        private GameObject m_hud;

        void Start()
        {
            string path = AFAssetManager.GetDirectoryOwner("Prefabs/CucaGameStateHUD");
            Sprite L_sprite;
            AFDebug.Log(path);

            m_hud = AFAssetManager.Instance.Instantiate<GameObject>( path );

            _listOfLifes = new List<GameObject>(3)
                {
                    GameObject.Find("lifeSprite1"),
                    GameObject.Find("lifeSprite2"),
                    GameObject.Find("lifeSprite3")
                };

            path = PathConstants.GetGameScenePath("lifeIcon_full");
            L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
            if (AFObject.IsNull(L_sprite))
                AFDebug.LogError("Game State was not able to load the life icon");
            else
            { 
                _listOfLifes[0].GetComponent<Image>().sprite = L_sprite;
                _listOfLifes[1].GetComponent<Image>().sprite = L_sprite;
                _listOfLifes[2].GetComponent<Image>().sprite = L_sprite;
            }


            _pointsHud = GameObject.Find("pointsLable");
            _pointsObj = GameObject.Find("pointsText");
            _lifeHUD = GameObject.Find("lifeLable");
            
            path = PathConstants.GetGameScenePath("pauseBtn");
            m_pauseBtn = GameObject.Find("pauseBtn");
            L_sprite = AFAssetManager.Instance.Load<Sprite>(path);

            if (AFObject.IsNull(m_pauseBtn) && AFObject.IsNull(L_sprite) )
                AFDebug.LogError("Game Scene was not able to load the Pause Button Sprite");
            else
                m_pauseBtn.GetComponent<Image>().sprite = L_sprite;
        }

        void Update()
        {
            _points = PointsController.GetPoints();
            if ( !AFObject.IsNull(_pointsObj) )
            {
                _pointsObj.GetComponent<Text>().text = _points.ToString();
            }

            _lifes = LifesController.GetLifes();

            for (int i = 0; i < _listOfLifes.Count(); i++)
            {
                if (LifesController.GetLifes() > 0)
                {
                    for (int k = LifesController.GetLifes(); k < LifesController.GetInitialLifes(); k++)
                    {
                        _listOfLifes[k].GetComponent<Image>().sprite = AFAssetManager.Instance.Load<Sprite>(PathConstants.GetGameScenePath("lifeIcon_empty"));
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
            GameObject.Destroy(m_hud);
        }

        public void SetAnchorTarget(GameObject value)
        {
            _anchorTarget = value;
        }
    }
}
