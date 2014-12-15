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
        private int _numbersOfCharactersToCreate = 1;
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
        
        private GameObject _camera;

        private List<Character> _listOfCharacters;

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

            /*if (this.gameObject.GetComponent<HudController>() == false)
                this.gameObject.AddComponent<HudController>();
            this.gameObject.GetComponent<HudController>().SetAnchorTarget(_camera);*/

            base.BuildState();
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
                Add(character);
                character.Initialize();
            }

            /*SetCenterAnchor();
            SetLeftAnchor();
            SetRightAnchor();*/
        }

        private void CreateCamera()
        {
            //if (FindObjectOfType<MyCamera>() == null)
            //{
                _camera = new GameObject();
                _camera.name = "StateCam";
                _camera.AddComponent<MyCamera>();
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

            //UIEventListener.Get(_background).onClick += OnClickBG;

            _leftWall3 = new GameObject();
            _leftWall3.name = "_leftWall3";
            _leftWall3.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall3");
            _leftWall3.transform.localPosition = new Vector3( ( _backgroundWidth / -2f ) + _leftWall3.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _leftWall3.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall3, 1);

            _rightWall3 = new GameObject();
            _rightWall3.name = "_rightWall3";
            _rightWall3.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall3");
            _rightWall3.transform.localPosition = new Vector3( ( _backgroundWidth / 2f ) - _rightWall3.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _rightWall3.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall3, 1);

            _floor3 = new GameObject();
            _floor3.name = "_floor3";
            _floor3.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor3");
            _floor3.transform.localPosition = new Vector3(0, (_backgroundHeight / -2f) + _floor3.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor3, 1);

            _leftWall2 = new GameObject();
            _leftWall2.name = "_leftWall2";
            _leftWall2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall2");
            _leftWall2.transform.localPosition = new Vector3((_backgroundWidth / -2f) + _leftWall2.GetComponent<SpriteRenderer>().bounds.size.x / 2f, (_backgroundHeight / 2f) - _leftWall2.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall2, 2);

            _rightWall2 = new GameObject();
            _rightWall2.name = "_rightWall2";
            _rightWall2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall2");
            _rightWall2.transform.localPosition = new Vector3( ( _backgroundWidth / 2f ) - _rightWall2.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _rightWall2.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall2, 2);

            _floor2 = new GameObject();
            _floor2.name = "_floor2";
            _floor2.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor2");
            _floor2.transform.localPosition = new Vector3(0, (_backgroundHeight / -2f) + _floor2.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor2, 2);

            _leftWall1 = new GameObject();
            _leftWall1.name = "_leftWall1";
            _leftWall1.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall1");
            _leftWall1.transform.localPosition = new Vector3( ( _backgroundWidth / -2f ) + _leftWall1.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _leftWall1.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall1, 3);

            _rightWall1 = new GameObject();
            _rightWall1.name = "_rightWall1";
            _rightWall1.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall1");
            _rightWall1.transform.localPosition = new Vector3( ( _backgroundWidth / 2f ) - _rightWall1.GetComponent<SpriteRenderer>().bounds.size.x / 2f, ( _backgroundHeight / 2f ) - _rightWall1.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0 );
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall1, 3);

            _floor1 = new GameObject();
            _floor1.name = "_floor1";
            _floor1.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor1");
            _floor1.transform.localPosition = new Vector3(0, (_backgroundHeight / -2f) + _floor1.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0);
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor1, 3);

            /*_roof = new GameObject();
            _roof.name = "_roof";
            _roof.AddComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "roof1");
            //FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_roof, 3);*/

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

            /*_ticks++;
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

                SetCenterAnchor();
                SetLeftAnchor();
                SetRightAnchor();
            }

            UpdateCharacterRotation();
            UpdateCharacterScale();
            UpdateSpritesPosition();*/

            if (_controller != null)
            {
                if (_controller.GetIsEndGame())
                {
                    m_engine.GetStateManger().GotoState(AState.EGameState.MENU);
                }
            }

            base.AFUpdate(deltaTime);
        }

        private void SetCenterAnchor()
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
                        offsetX = 80;
                        if (col == 2)
                            offsetX *= -1;
                    }
                    else if (line == 2 && col != 1)
                    {
                        offsetX = 190;
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

                    UI2DSprite sp = (charObj.GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer;

                    sp.SetAnchor(_camera);
                    sp.leftAnchor.absolute = -422 + (col * 350) - offsetX;
                    sp.rightAnchor.absolute = -222 + (col * 350) - offsetX;
                    sp.bottomAnchor.absolute = -649 - (line * 50);
                    sp.topAnchor.absolute = -389 - (line * 45);
                    sp.UpdateAnchors();
                    sp.MakePixelPerfect();
                    GameObject nullGo = null;
                    sp.SetAnchor(nullGo);

                    charObj.gameObject.transform.localScale = new Vector3(characterScale, characterScale, 1);
                    charObj.SetInitialPosition(_listOfCharacters[i].transform.localPosition);
                    charObj.SetScale(characterScale);

                    charObj.gameObject.GetComponent<BoxCollider>().transform.position = new Vector2(sp.transform.position.x, sp.transform.position.y);

                    col++;
                    FindObjectOfType<IndexController>().AddObjectToLIstByIndex(sp.gameObject, line + 1);
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

                        string[] charPosition = charObj.name.Split(char.Parse("_"));

                        if (charPosition[0] != "left")
                        {
                            charObj.name = "left_" + _listOfCharacters[i + _numberOfCharactersInCenter].name;
                        }

                        UI2DSprite sp = (charObj.GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer;

                        sp.SetAnchor(_camera);
                        sp.leftAnchor.absolute = 4400 - (col * 130);
                        sp.rightAnchor.absolute = -5568 - (col * 130);
                        sp.bottomAnchor.absolute = 4140 - (line * 350) - offsetY;
                        sp.topAnchor.absolute = -3156 - (line * 350) - offsetY;
                        if (!charObj.GetIsRotated())
                        {
                            sp.transform.rotation = Quaternion.Euler(_leftCharacterRotation);
                        }
                        sp.UpdateAnchors();
                        sp.MakePixelPerfect();

                        GameObject nullGo = null;
                        sp.SetAnchor(nullGo);

                        sp.transform.localScale = new Vector3(characterScale, characterScale, 1);

                        charObj.SetIsRotated(true);
                        charObj.SetInitialPosition(_listOfCharacters[i + _numberOfCharactersInCenter].transform.localPosition);
                        charObj.SetScale(characterScale);

                        charObj.gameObject.GetComponent<BoxCollider>().transform.position = new Vector2(sp.transform.position.x, sp.transform.position.y);


                        col++;
                        FindObjectOfType<IndexController>().AddObjectToLIstByIndex(sp.gameObject, col);
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


                        string[] charPosition = charObj.name.Split(char.Parse("_"));

                        if (charPosition[0] != "right")
                        {
                            charObj.name = "right_" + _listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft].name;
                        }

                        UI2DSprite sp = (charObj.GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer;

                        sp.SetAnchor(_camera);
                        sp.leftAnchor.absolute = 5520 + (col * 130);
                        sp.rightAnchor.absolute = -4450 + (col * 130);
                        sp.bottomAnchor.absolute = 4141 - (line * 350) - offsetY;
                        sp.topAnchor.absolute = -3155 - (line * 350) - offsetY;
                        if (!charObj.GetIsRotated())
                        {
                            sp.transform.rotation = Quaternion.Euler(_rightCharacterRotation);
                        }
                        sp.UpdateAnchors();
                        sp.MakePixelPerfect();

                        GameObject nullGo = null;
                        sp.SetAnchor(nullGo);

                        sp.transform.localScale = new Vector3(characterScale, characterScale, 1);
                        charObj.SetScale(characterScale);
                        charObj.SetIsRotated(true);
                        charObj.GetComponent<Character>().SetInitialPosition(_listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft].transform.localPosition);
                        charObj.gameObject.GetComponent<BoxCollider>().transform.position = new Vector2(sp.transform.position.x, sp.transform.position.y);
                        
                        col++;
                        FindObjectOfType<IndexController>().AddObjectToLIstByIndex(sp.gameObject, col);
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

                switch (charPosition[0])
                {
                    case "left":
                        _characterRotate = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.eulerAngles;
                        if (_characterRotate != _leftCharacterRotation)
                        {
                            (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation = Quaternion.Euler(_leftCharacterRotation);
                            UnityEngine.Debug.Log("right rotate its: " + (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation);
                        }
                        break;
                    case "right":
                        _characterRotate = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.eulerAngles;
                        if ( ! _characterRotate.Equals( _rightCharacterRotation ) )
                        //if ( ! _characterRight.Equals(_rightCharacterRotation))
                        {
                            (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation = Quaternion.Euler(_rightCharacterRotation);
                            //UnityEngine.Debug.Log("right rotate its: " + (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer.transform.rotation);
                        }
                    break;
                }
            }
        }

        private void UpdateCharacterScale()
        {
            for (int i = 0; i < _listOfCharacters.Count; i++)
            {
                UI2DSprite sp = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer;
                sp.MakePixelPerfect();
                sp.transform.localScale = new Vector3(_listOfCharacters[i].GetScale(), _listOfCharacters[i].GetScale(), 1);
            }
        }

        private void UpdateSpritesPosition()
        {
            for (int i = 0; i < _listOfCharacters.Count; i++)
            {
                UI2DSprite sp = (_listOfCharacters[i].GetCharacterAnimation().GetCurrentState() as AFMovieCLipNGUI).UI2DSpriteRenderer.SpriteContainer;
                sp.transform.localPosition = _listOfCharacters[i].transform.position;
                //UnityEngine.Debug.Log("sp.position: " + sp.transform.position + " || go.position: " + _listOfCharacters[i].gameObject.transform.localPosition);
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
            GameObject.Destroy(_camera);

            this.gameObject.GetComponent<HudController>().Destroy();

            base.AFDestroy();
        }

        public List<Character> GetListOfCharacters()
        {
            return _listOfCharacters;
        }
    }
}
