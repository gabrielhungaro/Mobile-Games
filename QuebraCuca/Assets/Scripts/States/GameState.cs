using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Controllers;
using UnityEngine;
using Constants;
using Elements;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;

using Characters;

namespace States
{
    public class GameState : AState
    {

        private float _timeToSpawnCharacter = 5;
        private int _numbersOfCharactersToCreate = 10;
        private int _numberOfCharactersLayers = 3;
        private List<GameObject> _listOfLeftCuca;
        private List<GameObject> _listOfRightCuca;
        private List<GameObject> _listOfCenterCuca;
        private int _numberOfCharactersInLeft = 8;
        private int _numberOfCharactersInRight = 8;
        private int _numberOfCharactersInCenter = 9;
       
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
        
        private GameObject _camera;

        private List<Character> _listOfCharacters;

        GameObject _uiRoot;

        private GameController _controller;
        private Character m_char;
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
            _controller.SetAnchorTarget(_camera);

            if (this.gameObject.GetComponent<HudController>() == false)
                this.gameObject.AddComponent<HudController>();
            this.gameObject.GetComponent<HudController>().SetAnchorTarget(_camera);

            base.BuildState();
        }

        private void CreatePullOfCharacters()
        {
            _listOfCharacters = new List<Character>();

            Character character;

            for (int i = 0; i < _numbersOfCharactersToCreate; i++)
            {
                m_char = character = CharacterFactory.Instance.CreateCharacter();
                _listOfCharacters.Add(character);
                Add(character);
                character.Initialize();
            }
            
            UpdateCenterPosition();
            UpdateLeftPosition();
            UpdateRightPosition();
        }

        private void CreateCamera()
        {
            _camera = new GameObject();
            _camera.name = "StateCam";
            _camera.AddComponent<MyCamera>();
        }

        private void CreateBackground()
        {
            _background = new GameObject();
            _background.name = "background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "background");
            _background.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
            _background.AddComponent<Rigidbody>().useGravity = false;
            _background.GetComponent<Rigidbody>().isKinematic = true;
            _background.AddComponent<BoxCollider>().size = new Vector3(_background.GetComponent<UI2DSprite>().width,
                                                                    _background.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_background).onClick += OnClickBG;

            _leftWall3 = new GameObject();
            _leftWall3.name = "_leftWall3";
            _leftWall3.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall3");
            _leftWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            _leftWall3.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _leftWall3.GetComponent<UI2DSprite>().leftAnchor.absolute = 4099;
            _leftWall3.GetComponent<UI2DSprite>().rightAnchor.absolute = -5615;
            _leftWall3.GetComponent<UI2DSprite>().bottomAnchor.absolute = -3072;
            _leftWall3.GetComponent<UI2DSprite>().topAnchor.absolute = 3452;
            _leftWall3.GetComponent<UI2DSprite>().UpdateAnchors();
            _leftWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall3, 1);

            _rightWall3 = new GameObject();
            _rightWall3.name = "_rightWall3";
            _rightWall3.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall3");
            _rightWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            _rightWall3.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _rightWall3.GetComponent<UI2DSprite>().leftAnchor.absolute = 1024;
            _rightWall3.GetComponent<UI2DSprite>().rightAnchor.absolute = 492;
            _rightWall3.GetComponent<UI2DSprite>().bottomAnchor.absolute = 768;
            _rightWall3.GetComponent<UI2DSprite>().topAnchor.absolute = -388;
            _rightWall3.GetComponent<UI2DSprite>().UpdateAnchors();
            _rightWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall3, 1);

            _floor3 = new GameObject();
            _floor3.name = "_floor3";
            _floor3.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor3");
            _floor3.GetComponent<UI2DSprite>().MakePixelPerfect();
            _floor3.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _floor3.GetComponent<UI2DSprite>().leftAnchor.absolute = 4099;
            _floor3.GetComponent<UI2DSprite>().rightAnchor.absolute = -4099;
            _floor3.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3073;
            _floor3.GetComponent<UI2DSprite>().topAnchor.absolute = -4129;
            _floor3.GetComponent<UI2DSprite>().UpdateAnchors();
            _floor3.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor3, 1);

            _leftWall2 = new GameObject();
            _leftWall2.name = "_leftWall2";
            _leftWall2.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall2");
            _leftWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            _leftWall2.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _leftWall2.GetComponent<UI2DSprite>().leftAnchor.absolute = 4096;
            _leftWall2.GetComponent<UI2DSprite>().rightAnchor.absolute = -5756;
            _leftWall2.GetComponent<UI2DSprite>().bottomAnchor.absolute = -3074;
            _leftWall2.GetComponent<UI2DSprite>().topAnchor.absolute = 3402;
            _leftWall2.GetComponent<UI2DSprite>().UpdateAnchors();
            _leftWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall2, 2);

            _rightWall2 = new GameObject();
            _rightWall2.name = "_rightWall2";
            _rightWall2.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall2");
            _rightWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            _rightWall2.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _rightWall2.GetComponent<UI2DSprite>().leftAnchor.absolute = 5793;
            _rightWall2.GetComponent<UI2DSprite>().rightAnchor.absolute = -4083;
            _rightWall2.GetComponent<UI2DSprite>().bottomAnchor.absolute = -3074;
            _rightWall2.GetComponent<UI2DSprite>().topAnchor.absolute = 3402;
            _rightWall2.GetComponent<UI2DSprite>().UpdateAnchors();
            _rightWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall2, 2);

            _floor2 = new GameObject();
            _floor2.name = "_floor2";
            _floor2.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor2");
            _floor2.GetComponent<UI2DSprite>().MakePixelPerfect();
            _floor2.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _floor2.GetComponent<UI2DSprite>().leftAnchor.absolute = 4112;
            _floor2.GetComponent<UI2DSprite>().rightAnchor.absolute = -4086;
            _floor2.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3070;
            _floor2.GetComponent<UI2DSprite>().topAnchor.absolute = -4212;
            _floor2.GetComponent<UI2DSprite>().UpdateAnchors();
            _floor2.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor2, 2);

            _leftWall1 = new GameObject();
            _leftWall1.name = "_leftWall1";
            _leftWall1.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall1");
            _leftWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            _leftWall1.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _leftWall1.GetComponent<UI2DSprite>().leftAnchor.absolute = 4096;
            _leftWall1.GetComponent<UI2DSprite>().rightAnchor.absolute = -5960;
            _leftWall1.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3072;
            _leftWall1.GetComponent<UI2DSprite>().topAnchor.absolute = -3076;
            _leftWall1.GetComponent<UI2DSprite>().UpdateAnchors();
            _leftWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall1, 3);

            _rightWall1 = new GameObject();
            _rightWall1.name = "_rightWall1";
            _rightWall1.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall1");
            _rightWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            _rightWall1.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _rightWall1.GetComponent<UI2DSprite>().leftAnchor.absolute = 5917;
            _rightWall1.GetComponent<UI2DSprite>().rightAnchor.absolute = -4083;
            _rightWall1.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3075;
            _rightWall1.GetComponent<UI2DSprite>().topAnchor.absolute = -3073;
            _rightWall1.GetComponent<UI2DSprite>().UpdateAnchors();
            _rightWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall1, 3);

            _floor1 = new GameObject();
            _floor1.name = "_floor1";
            _floor1.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor1");
            _floor1.GetComponent<UI2DSprite>().MakePixelPerfect();
            _floor1.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _floor1.GetComponent<UI2DSprite>().leftAnchor.absolute = 4112;
            _floor1.GetComponent<UI2DSprite>().rightAnchor.absolute = -4086;
            _floor1.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3073;
            _floor1.GetComponent<UI2DSprite>().topAnchor.absolute = -4393;
            _floor1.GetComponent<UI2DSprite>().UpdateAnchors();
            _floor1.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor1, 3);

            _roof = new GameObject();
            _roof.name = "_roof";
            _roof.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "roof1");
            _roof.GetComponent<UI2DSprite>().MakePixelPerfect();
            _roof.GetComponent<UI2DSprite>().SetAnchor(_camera);
            _roof.GetComponent<UI2DSprite>().leftAnchor.absolute = 4237;
            _roof.GetComponent<UI2DSprite>().rightAnchor.absolute = -4237;
            _roof.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4329;
            _roof.GetComponent<UI2DSprite>().topAnchor.absolute = -3073;
            _roof.GetComponent<UI2DSprite>().UpdateAnchors();
            _roof.GetComponent<UI2DSprite>().MakePixelPerfect();
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_roof, 3);

            /*_pointsBg = new GameObject();
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
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_pointsBg, 3);*/
        }

        private void OnClickBG(GameObject go)
        {
            SoundManager.PlaySoundByName(SoundConstants.SFX_WRONG_HIT);
        }

        public override void AFUpdate(double deltaTime)
        {
            _controller.AFUpdate(deltaTime);

            _ticks++;
            if (_ticks >= 10 && _ticks <= 11)
            {
                _leftWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
                _rightWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
                _floor3.GetComponent<UI2DSprite>().MakePixelPerfect();
                _floor2.GetComponent<UI2DSprite>().MakePixelPerfect();
                _floor1.GetComponent<UI2DSprite>().MakePixelPerfect();
                _leftWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
                _rightWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
                _leftWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
                _rightWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
                _roof.GetComponent<UI2DSprite>().MakePixelPerfect();

                UpdateCenterPosition();
                UpdateLeftPosition();
                UpdateRightPosition();
            }

            if (Input.GetKey("right"))
            {
                m_char.GetCharacterAnimation().GoTo("idle");
            }

            if (Input.GetKey("left"))
            {
                m_char.GetCharacterAnimation().GoTo("angry");
            }

            base.AFUpdate(deltaTime);
        }

        private void UpdateCenterPosition()
        {
            int col = 0;
            int line = 0;
            float characterScale;
            float totalOfcols = 3;
            int offsetX = 0;
            Character charObj;
            for (int i = 0; i < _numberOfCharactersInCenter; i++)
            {
                if (i < _listOfCharacters.Count)
                {
                    characterScale = 1  - ((totalOfcols - line) / 10);
                    if (line == 1 && col != 1)
                    {
                        offsetX = 60;
                        if (col == 2)
                            offsetX *= -1;
                    }
                    else if (line == 2 && col != 1)
                    {
                        offsetX = 170;
                        if (col == 2)
                            offsetX *= -1;
                    }
                    else
                    {
                        offsetX = 0;
                    }

                    charObj = _listOfCharacters[i];
                    charObj.name = "center_" + _listOfCharacters[i].name;
//                     charObj.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
//                     charObj.GetComponent<UI2DSprite>().leftAnchor.absolute = -384 + (col * 250) - offsetX;
//                     charObj.GetComponent<UI2DSprite>().rightAnchor.absolute = -108 + (col * 250) - offsetX;
//                     charObj.GetComponent<UI2DSprite>().bottomAnchor.absolute = -388 - (line * 150);
//                     charObj.GetComponent<UI2DSprite>().topAnchor.absolute = -4 - (line * 145);
//                     charObj.GetComponent<UI2DSprite>().UpdateAnchors();
//                     charObj.GetComponent<UI2DSprite>().MakePixelPerfect();
                    charObj.gameObject.transform.localScale = new Vector3(characterScale, characterScale, 1);
                    charObj.gameObject.GetComponent<Character>().SetInitialPosition(_listOfCharacters[i].transform.localPosition);

                    col++;
                    //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_listOfCharacters[i].gameObject, line + 1);
                    if (col == 3)
                    {
                        col = 0;
                        line++;
                    }

                }
            }
        }

        private void UpdateLeftPosition()
        {
            int col = 0;
            int line = 0;
            float characterScale;
            float totalOfcols = 3;
            int offsetY = 0;
            Character charObj;
            for (int i = 0; i < _numberOfCharactersInLeft; i++)
            {
                if ((i + _numberOfCharactersInCenter) < _listOfCharacters.Count)
                {
                    characterScale = 1 - ((totalOfcols - col) / 10);
                    if (col == 1)
                    {
                        offsetY = 200;
                    }
                    else if (col == 2)
                    {
                        offsetY = 100;
                    }
                    else
                    {
                        offsetY = 0;
                    }

                    if (_listOfCharacters.Count > i + _numberOfCharactersInCenter)
                    {
                        charObj = _listOfCharacters[i + _numberOfCharactersInCenter];
                        charObj.name = "left_" + _listOfCharacters[i + _numberOfCharactersInCenter].name;
//                         charObj.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
//                         charObj.GetComponent<UI2DSprite>().leftAnchor.absolute = 4400 - (col * 130);
//                         charObj.GetComponent<UI2DSprite>().rightAnchor.absolute = -5568 - (col * 130);
//                         charObj.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4140 - (line * 350) - offsetY;
//                         charObj.GetComponent<UI2DSprite>().topAnchor.absolute = -3156 - (line * 350) - offsetY;
                        charObj.transform.Rotate(new Vector3(0f, 0f, 90f));
//                         charObj.GetComponent<UI2DSprite>().UpdateAnchors();
//                         charObj.GetComponent<UI2DSprite>().MakePixelPerfect();
                        charObj.transform.localScale = new Vector3(characterScale, characterScale, 1);
                        charObj.GetComponent<Character>().SetInitialPosition(_listOfCharacters[i + _numberOfCharactersInCenter].transform.localPosition);
                        col++;
                        //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_listOfCharacters[i + _numberOfCharactersInCenter].gameObject, col);
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

        private void UpdateRightPosition()
        {
            int col = 0;
            int line = 0;
            float characterScale;
            float totalOfcols = 3;
            int offsetY = 0;
            Character charObj;
            for (int i = 0; i < _numberOfCharactersInRight; i++)
            {
                if ((i + _numberOfCharactersInCenter + _numberOfCharactersInLeft) < _listOfCharacters.Count)
                {
                    characterScale = 1 - ((totalOfcols - col) / 10);
                    if (col == 1)
                    {
                        offsetY = 200;
                    }
                    else if (col == 2)
                    {
                        offsetY = 100;
                    }
                    else
                    {
                        offsetY = 0;
                    }

                    if (_listOfCharacters.Count > i + _numberOfCharactersInCenter + _numberOfCharactersInLeft)
                    {
                        charObj = _listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft];
                        charObj.name = "right_" + _listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft].name;
//                         charObj.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
//                         charObj.GetComponent<UI2DSprite>().leftAnchor.absolute = 5520 + (col * 130);
//                         charObj.GetComponent<UI2DSprite>().rightAnchor.absolute = -4450 + (col * 130);
//                         charObj.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4141 - (line * 350) - offsetY;
//                         charObj.GetComponent<UI2DSprite>().topAnchor.absolute = -3155 - (line * 350) - offsetY;
//                         charObj.GetComponent<UI2DSprite>().UpdateAnchors();
                         charObj.transform.Rotate(new Vector3(0f, -180f, -90f));
//                         charObj.GetComponent<UI2DSprite>().MakePixelPerfect();
                        charObj.transform.localScale = new Vector3(characterScale, characterScale, 1);
                        charObj.GetComponent<Character>().SetInitialPosition(_listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft].transform.localPosition);
                        col++;
                        FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft].gameObject, col);
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

        public void MyUpdate()
        {
            _ticks++;
            if (_ticks >= 10 && _ticks <= 11)
            {
                //UpdatePixelPerfect();
                UpdateCenterPosition();
                UpdateLeftPosition();
                UpdateRightPosition();
                /*for (int i = 0; i < _numberOfCharacters; i++)
                {
                    _listOfCharacters[i].GetComponent<UI2DSprite>().MakePixelPerfect();
                }*/
            }
        }

        public List<Character> GetListOfCharacters()
        {
            return _listOfCharacters;
        }

        internal void SetAnchorTarget(GameObject value)
        {
            _uiRoot = value;
        }

    }
}
