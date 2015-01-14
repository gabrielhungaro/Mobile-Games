using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;
using com.globo.sitio.mobilegames.QuebraCuca.Elements;
using com.globo.sitio.mobilegames.QuebraCuca.Controllers;
using com.globo.sitio.mobilegames.QuebraCuca.Characters;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.View;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Components;

namespace com.globo.sitio.mobilegames.QuebraCuca.States
{
    public class GameState : AState
    {

        private float _timeToSpawnCharacter = 5;
        private int _numbersOfCharactersToCreate = 25;
        private int _numberOfCharactersLayers = 3;
        private int _numberOfCharactersInLeft = 8;
        private int _numberOfCharactersInRight = 8;
        private int _numberOfCharactersInCenter = 9;
        private Vector3 _characterRotate;
        private readonly Vector3 _rightCharacterRotation = new Vector3(0f, 180f, 270f);
        private Vector3 _leftCharacterRotation = new Vector3(0f, 0f, 270f);
        
        private int _ticks = 0;

        private GameObject _background;
        private GameObject _leftWall1;
        private GameObject _leftWall2;
        private GameObject _leftWall3;
        private GameObject _rightWall1;
        private GameObject _rightWall2;
        private GameObject _rightWall3;
        private GameObject _floor1;
        private GameObject _floor2;
        private GameObject _floor3;
        private GameObject _roof;
        private GameObject _pointsBg;

        //private int _ticks;
        
        private GameObject _cameraGameObject;
        private Camera m_camera;

        private List<Character> _listOfCharacters;

        private GameController _controller;
        private Character m_char;

        private GameObject m_particleEmiter;

        protected override void Awake()
        {
            m_stateID = AState.EGameState.GAME;
        }

        public override void BuildState()
        {
            if (this.gameObject.GetComponent<IndexController>() == false)
                this.gameObject.AddComponent<IndexController>().Start();

            CreateCamera();
            CreateBackground();
            CreatePullOfCharacters();

            _controller = AFObject.Create<GameController>();
            _controller.gameObject.transform.parent = this.gameObject.transform;
            _controller.Initialize();
            _controller.SetAnchorTarget(_cameraGameObject);

            if (this.gameObject.GetComponent<HudController>() == false)
                this.gameObject.AddComponent<HudController>();
            this.gameObject.GetComponent<HudController>().SetAnchorTarget(_cameraGameObject);

            #if UNITY_EDITOR
            AFAssetManager.SimulatedDPI = AFAssetManager.DPI_IPHONE_4_5;
            AFAssetManager.SimulatePlatform = AFAssetManager.EPlataform.IOS;
            #endif

            CreateParticles();
            base.BuildState();
        }

        private void CreateParticles()
        {
            string path = AFAssetManager.GetPathTargetPlatformWithResolution("CFXM_GroundHit_TAP");
            m_particleEmiter = Resources.Load<GameObject>(path);

            path = AFAssetManager.GetPathTargetPlatformWithResolution("CFXM3_Flying_EmberCFXM_Hit");
            GameObject goEmiter = Instantiate(Resources.Load<GameObject>(path)) as GameObject;
            goEmiter.transform.position = new Vector3(0, 1.8f, 0);
            goEmiter.GetComponent<ParticleSystem>().renderer.sortingLayerName = "Foreground";
            goEmiter.GetComponent<ParticleSystem>().renderer.sortingOrder = 999;
        }

        private void CreatePullOfCharacters()
        {
            _listOfCharacters = new List<Character>();

            Character character;

            for (int i = 0; i < _numbersOfCharactersToCreate; i++)
            {
                m_char = character = CharacterFactory.Instance.CreateCharacter();
                character.name = "Character_" + i;

                _listOfCharacters.Add(character);
                character.Initialize();

                Add(character);
            }

            SetCenterAnchor();
            SetLeftAnchor();
            SetRightAnchor();
            CreatePauseScreen();
        }

        public override void Pause()
        {
            m_pauseScreen.Show();
            base.Pause();
        }

        private CucaPauseScreen m_pauseScreen;

        private void CreatePauseScreen()
        {
            m_pauseScreen = AFObject.Create<CucaPauseScreen>();
            m_pauseScreen.Initialize();
            //m_pauseScreen.Show();
            Add(m_pauseScreen);
        }

        private void CreateCamera()
        {
            //if (FindObjectOfType<MyCamera>() == null)
            //{
                _cameraGameObject = new GameObject();
                _cameraGameObject.name = "StateCam";
                _cameraGameObject.tag = "MainCamera";
                m_camera = _cameraGameObject.AddComponent<MyCamera>().GetCamera();
            /*}
            else
            {
                _camera = FindObjectOfType<MyCamera>().gameObject;
                _camera.name = "StateCam";
            }*/
        }

        private void CreateBackground()
        {
            _background = new GameObject();
            _background.name = "background";
            _background.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "background");
            _background.transform.localPosition = new Vector3(0, 0, 1);
            _background.AddComponent<BoxCollider>().size = new Vector3(_background.GetComponent<SpriteRenderer>().sprite.bounds.size.x,
                                                                    _background.GetComponent<SpriteRenderer>().sprite.bounds.size.y);

            float _backgroundWidth = _background.GetComponent<SpriteRenderer>().bounds.size.x;
            float _backgroundHeight = _background.GetComponent<SpriteRenderer>().bounds.size.y;

            Resolution res = Screen.currentResolution;

            //UIEventListener.Get(_background).onClick += OnClickBG;

            _leftWall3 = new GameObject();
            _leftWall3.name = "_leftWall3";
            _leftWall3.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall3");
            _leftWall3.transform.localPosition = new Vector3( ( _backgroundWidth / -2f ) + _leftWall3.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _leftWall3.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall3, 2);

            _rightWall3 = new GameObject();
            _rightWall3.name = "_rightWall3";
            _rightWall3.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall3");
            _rightWall3.transform.localPosition = new Vector3( ( _backgroundWidth / 2f ) - _rightWall3.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _rightWall3.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall3, 2);

            _floor3 = new GameObject();
            _floor3.name = "_floor3";
            _floor3.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor3");
            _floor3.transform.localPosition = new Vector3( 0, ( _backgroundHeight / -2f ) + _floor3.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor3, 2);

            _leftWall2 = new GameObject();
            _leftWall2.name = "_leftWall2";
            _leftWall2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall2");
            _leftWall2.transform.localPosition = new Vector3( ( _backgroundWidth / -2f ) + _leftWall2.GetComponent<SpriteRenderer>().bounds.size.x / 2f, (_backgroundHeight / 2f) - _leftWall2.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall2, 4);

            _rightWall2 = new GameObject();
            _rightWall2.name = "_rightWall2";
            _rightWall2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall2");
            _rightWall2.transform.localPosition = new Vector3( ( _backgroundWidth / 2f ) - _rightWall2.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _rightWall2.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall2, 4);

            _floor2 = new GameObject();
            _floor2.name = "_floor2";
            _floor2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor2");
            _floor2.transform.localPosition = new Vector3(0, (_backgroundHeight / -2f) + _floor2.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor2, 4);

            _leftWall1 = new GameObject();
            _leftWall1.name = "_leftWall1";
            _leftWall1.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall1");
            _leftWall1.transform.localPosition = new Vector3( ( _backgroundWidth / -2f ) + _leftWall1.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _leftWall1.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall1, 6);

            _rightWall1 = new GameObject();
            _rightWall1.name = "_rightWall1";
            _rightWall1.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall1");
            _rightWall1.transform.localPosition = new Vector3( ( _backgroundWidth / 2f ) - _rightWall1.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _rightWall1.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall1, 6);

            _floor1 = new GameObject();
            _floor1.name = "_floor1";
            _floor1.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor1");
            _floor1.transform.localPosition = new Vector3(0, (_backgroundHeight / -2f) + _floor1.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor1, 6);

            _roof = new GameObject();
            _roof.name = "_roof";
            _roof.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "roof1");
            _roof.transform.localPosition = new Vector3(0, (_backgroundHeight / 2f) - _roof.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_roof, 6);

            Resources.UnloadUnusedAssets();

            /*
            _pointsBg = new GameObject();
            _pointsBg.name = "_pointsBg";
            _pointsBg.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "pointsBg");
            _pointsBg.GetComponent<UI2DSprite>().MakePixelPerfect();
            _pointsBg.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _pointsBg.GetComponent<UI2DSprite>().leftAnchor.absolute = 4096;
            _pointsBg.GetComponent<UI2DSprite>().rightAnchor.absolute = -4102;
            _pointsBg.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4327;
            _pointsBg.GetComponent<UI2DSprite>().topAnchor.absolute = -3063;
            _pointsBg.GetComponent<UI2DSprite>().UpdateAnchors();
            _pointsBg.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_pointsBg, 3);
             
             */
        }

        void OnMouseDown()
        {
            //UnityEngine.Debug.Log("clicou mesmo");
        }

        private void OnClickBG(GameObject go)
        {
            SoundManager.PlaySoundByName(SoundConstants.SFX_WRONG_HIT);
        }

        public override void AFUpdate(double deltaTime)
        {
            InputUpdate();

            _controller.AFUpdate(deltaTime);

            if (_controller != null)
            {
                if (_controller.GetIsEndGame())
                {
                    m_engine.GetStateManger().GotoState(AState.EGameState.MENU);
                }
            }

            base.AFUpdate(deltaTime);
        }

        private void InputUpdate()
        {
            Vector3 screenPoint;

            if (Input.GetMouseButtonDown( 0 ))
            {
                
                screenPoint = Input.mousePosition;
                screenPoint.z = 0;
                CreateTapParticle(m_camera.ScreenToWorldPoint(screenPoint));
            }

            foreach (Touch t in Input.touches)
            {
                if (t.phase == TouchPhase.Began)
                {
                    Vector3 point = new Vector3(t.position.x, t.position.y, 0);
                    Ray ray = m_camera.ViewportPointToRay(point);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        hit.collider.SendMessage("OnMouseDown", null, SendMessageOptions.DontRequireReceiver);
                    }

                    //CreateTapParticle(m_camera.ScreenToWorldPoint(point));
                }
            }
        }


        private void CreateTapParticle(Vector3 screenPoint)
        {
            GameObject go = Instantiate(m_particleEmiter) as GameObject;
            go.layer = LayerMask.NameToLayer("UI");
            go.transform.position = screenPoint;
            go.transform.localScale = new Vector3(3, 3, 3);

            ParticleSystem ps = go.GetComponent<ParticleSystem>();
            ps.renderer.sortingOrder = 999;
        }

        private void SetCenterAnchor()
        {
            const float characterScaleFactor = 1.3f;
            //const float characterScaleFactor = 1;

            int col = 0;
            int line = 0;
            float characterScale;
            float totalOfcols = 3;
            float offsetX = 0;
            float offsetY = 1.8f;

            Character charObj;

            for (int i = 0; i < _numberOfCharactersInCenter; i++)
            {
                if (i < _listOfCharacters.Count)
                {
                    characterScale = characterScaleFactor - ((totalOfcols - line) / 10);
                    if (line == 1 && col != 1)
                    {
                        //offsetX = 80;
                        offsetX = 1.2f;
                        if (col == 2)
                            offsetX *= -characterScaleFactor;
                    }
                    else if (line == 2 && col != 1)
                    {
                        //offsetX = 190;
                        offsetX = 2.5f;
                        if (col == 2)
                            offsetX *= -1;
                    }
                    else
                    {
                        offsetX = 0;
                    }

                    charObj = _listOfCharacters[i];
                    
                    string[] charPosition = charObj.name.Split(char.Parse("_"));

                    if (charPosition[0] != "center")
                    {
                        charObj.name = "center_" + _listOfCharacters[i].name;
                    }
                    Sprite charSpr = charObj.GetCharacterAnimation().GetCurrentState().GetSprite();
                    charObj.transform.localPosition = new Vector3((-charSpr.bounds.size.x) + (col * charSpr.bounds.size.x) - offsetX, (-charSpr.bounds.size.y * offsetY) - (line * (charSpr.bounds.size.y / 2)), 0);
                    charObj.gameObject.transform.localScale = new Vector3(characterScale, characterScale, 1);
                    charObj.SetInitialPosition(charObj.transform.localPosition);
                    charObj.SetScale(characterScale);

                    //charObj.gameObject.GetComponent<BoxCollider>().transform.position = new Vector2(sp.transform.position.x, sp.transform.position.y);

                    col++;
                    charObj.SetIndex((line * 2) + 1);
                    FindObjectOfType<IndexController>().AddObjectToLIstByIndex(charObj.GetCharacterAnimation().GetCurrentState().gameObject, charObj.GetIndex());
                    if (col == 3)
                    {
                        col = 0;
                        line++;
                    }
                }
            }
        }

        private void SetLeftAnchor()
        {
            int col = 0;
            int line = 0;
            const float characterScaleFactor = 1.3f;
            float characterScale;
            float totalOfcols = 3;
            float offsetX = 0;
            float offsetY = 0;

            Vector2 initialPosition = new Vector2(3.8f, 1.5f);

            Character charObj;

            for (int i = 0; i < _numberOfCharactersInLeft; i++)
            {
                if ((i + _numberOfCharactersInCenter) < _listOfCharacters.Count)
                {
                    characterScale = characterScaleFactor - ((totalOfcols - col) / 10);
                    if (col == 1)
                    {
                        offsetY = 2f;
                        offsetX = .5f;
                    }
                    else if (col == 2)
                    {
                        offsetY = .8f;
                        offsetX = .6f;
                    }
                    else
                    {
                        offsetY = 0;
                    }

                    if (_listOfCharacters.Count > i + _numberOfCharactersInCenter)
                    {
                        charObj = _listOfCharacters[i + _numberOfCharactersInCenter];

                        string[] charPosition = charObj.name.Split(char.Parse("_"));

                        if (charPosition[0] != "left")
                        {
                            charObj.name = "left_" + _listOfCharacters[i + _numberOfCharactersInCenter].name;
                        }

                        Sprite charSpr = charObj.GetCharacterAnimation().GetCurrentState().GetSprite();
                        charObj.transform.localPosition = new Vector3((-charSpr.bounds.size.x * initialPosition.x) - (col * ((charSpr.bounds.size.x * 0.5f) + offsetX)), (charSpr.bounds.size.y * initialPosition.y) - (line * (charSpr.bounds.size.y)) - offsetY, 0);

                        if (!charObj.GetIsRotated())
                        {
                            charObj.transform.rotation = Quaternion.Euler(_leftCharacterRotation);
                        }

                        charObj.transform.localScale = new Vector3(characterScale, characterScale, 1);

                        charObj.SetIsRotated(true);
                        charObj.SetInitialPosition(charObj.transform.localPosition);
                        charObj.SetScale(characterScale);

                        charObj.gameObject.GetComponent<BoxCollider>().transform.position = new Vector2(charObj.transform.position.x, charObj.transform.position.y);
                        
                        charObj.SetIndex((col * 2) + 1);
                        col++;
                        FindObjectOfType<IndexController>().AddObjectToLIstByIndex(charObj.GetCharacterAnimation().GetCurrentState().gameObject, charObj.GetIndex());
                        if (col == 3)
                        {
                            col = 0;
                            line++;
                        }
                        else if (col == 1 && line == 2)
                        {
                            col++;
                        }
                    }
                }
            }
        }

        private void SetRightAnchor()
        {
            int col = 0;
            int line = 0;
            const float characterScaleFactor = 1.3f;
            float characterScale;
            float totalOfcols = 3;
            float offsetX = 0;
            float offsetY = 4.0f;

            Vector2 initialPosition = new Vector2(3.8f, 1.5f);

            Character charObj;
            for (int i = 0; i < _numberOfCharactersInRight; i++)
            {
                if ((i + _numberOfCharactersInCenter + _numberOfCharactersInLeft) < _listOfCharacters.Count)
                {
                    characterScale = characterScaleFactor - ((totalOfcols - col) / 10);
                    if (col == 1)
                    {
                        offsetY = 2f;
                        offsetX = .5f;
                    }
                    else if (col == 2)
                    {
                        offsetY = .8f;
                        offsetX = .7f;
                    }
                    else
                    {
                        offsetY = 0;
                    }

                    if (_listOfCharacters.Count > i + _numberOfCharactersInCenter + _numberOfCharactersInLeft)
                    {
                        charObj = _listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft];

                        string[] charPosition = charObj.name.Split(char.Parse("_"));

                        if (charPosition[0] != "right")
                        {
                            charObj.name = "right_" + _listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft].name;
                        }

                        Sprite charSpr = charObj.GetCharacterAnimation().GetCurrentState().GetSprite();
                        charObj.transform.localPosition = new Vector3((charSpr.bounds.size.x * initialPosition.x) + (col * ((charSpr.bounds.size.x * 0.5f) + offsetX)), (charSpr.bounds.size.y * initialPosition.y) - (line * (charSpr.bounds.size.y)) - offsetY, 0);

                        if (!charObj.GetIsRotated())
                        {
                            charObj.transform.rotation = Quaternion.Euler(_rightCharacterRotation);
                        }

                        charObj.transform.localScale = new Vector3(characterScale, characterScale, 1);
                        charObj.SetScale(characterScale);
                        charObj.SetIsRotated(true);
                        charObj.GetComponent<Character>().SetInitialPosition(charObj.transform.localPosition);
                        charObj.gameObject.GetComponent<BoxCollider>().transform.position = new Vector2(charObj.transform.position.x, charObj.transform.position.y);

                        charObj.SetIndex((col * 2) + 1);
                        col++;
                        FindObjectOfType<IndexController>().AddObjectToLIstByIndex(charObj.GetCharacterAnimation().GetCurrentState().gameObject, charObj.GetIndex());
                        if (col == 3)
                        {
                            col = 0;
                            line++;
                        }
                        else if (col == 1 && line == 2)
                        {
                            col++;
                        }
                    }
                }
            }
        }

        private void UpdateCharacterRotation()
        {
            for (int i = 0; i < _listOfCharacters.Count; i++)
            {
                string[] charPosition = _listOfCharacters[i].name.Split(char.Parse("_"));
//                 switch (charPosition[0])
//                 {
//                     case "left":
//                         _characterRotate = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.eulerAngles;
//                         if (_characterRotate != _leftCharacterRotation)
//                         {
//                             (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation = Quaternion.Euler(_leftCharacterRotation);
//                             UnityEngine.Debug.Log("right rotate its: " + (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation);
//                         }
//                         break;
//                     case "right":
//                         _characterRotate = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.eulerAngles;
//                         if ( ! _characterRotate.Equals( _rightCharacterRotation ) )
//                         //if ( ! _characterRight.Equals(_rightCharacterRotation))
//                         {
//                             (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation = Quaternion.Euler(_rightCharacterRotation);
//                             //UnityEngine.Debug.Log("right rotate its: " + (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation);
//                         }
//                     break;
//                 }
            }
        }

        private void UpdateCharacterScale()
        {
            for (int i = 0; i < _listOfCharacters.Count; i++)
            {
//                 UI2DSprite sp = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer;
//                 sp.MakePixelPerfect();
//                 sp.transform.localScale = new Vector3(_listOfCharacters[i].GetScale(), _listOfCharacters[i].GetScale(), 1);
            }
        }

        private void UpdateSpritesPosition()
        {
            for (int i = 0; i < _listOfCharacters.Count; i++)
            {
//                 UI2DSprite sp = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer;
//                 sp.transform.localPosition = _listOfCharacters[i].transform.position;
//                 //UnityEngine.Debug.Log("sp.position: " + sp.transform.position + " || go.position: " + _listOfCharacters[i].gameObject.transform.localPosition);
            }
        }

        override public void AFDestroy()
        {
            AFEngine.Instance.gameObject.transform.parent = null;

            GameObject.Destroy(_floor3);
            GameObject.Destroy(_floor2);
            GameObject.Destroy(_floor1);

            GameObject.Destroy(_leftWall1);
            GameObject.Destroy(_leftWall3);
            GameObject.Destroy(_leftWall2);

            GameObject.Destroy(_rightWall2);
            GameObject.Destroy(_rightWall3);
            GameObject.Destroy(_rightWall1);

            GameObject.Destroy(_roof);
            GameObject.Destroy(_cameraGameObject);

            this.gameObject.GetComponent<HudController>().Destroy();

            base.AFDestroy();
        }

        public List<Character> GetListOfCharacters()
        {
            return _listOfCharacters;
        }
    }
}
