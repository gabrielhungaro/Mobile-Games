using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Utils;
using AquelaFrameWork.View;

using UnityEngine;
using States;
using Controllers;
using Constants;

namespace Characters
{
    public class CharacterFactory : ASingleton<CharacterFactory>
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
        private int _numberOfCharacters;
        private int _ticks;
        private List<GameObject> _listOfCharacters;
        GameObject _uiRoot;

        void Start()
        {
            //CreateCharacter();
            _uiRoot = GameStateFactory.GetUiRoot();
            CreatePullOfCharacters();
        }

        private void CreatePullOfCharacters()
        {
            _listOfCharacters = new List<GameObject>();
            _listOfLeftCuca = new List<GameObject>();
            _listOfRightCuca = new List<GameObject>();
            _listOfCenterCuca = new List<GameObject>();

            for (int i = 0; i < _numbersOfCharactersToCreate; i++)
            {
                _listOfCharacters.Add(CreateCharacter());
            }
            UpdateCenterPosition();
            UpdateLeftPosition();
            UpdateRightPosition();
        }

        private GameObject CreateCharacter()
        {
            GameObject character = new GameObject();
            character.name = "Character";
            character.AddComponent<Character>();
            character.AddComponent<TimeController>();
            //character.GetComponent<Character>().SetImagePath(PathConstants.GetGameScenePath() + "cuca");
            character.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "cuca");
            character.AddComponent<Rigidbody>().useGravity = false;
            character.GetComponent<Rigidbody>().isKinematic = false;
            character.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
            character.GetComponent<UI2DSprite>().MakePixelPerfect();
            character.AddComponent<BoxCollider>().size = new Vector3(character.GetComponent<UI2DSprite>().width,
                                                                    character.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(character).onClick += OnClick;
            character.GetComponent<UI2DSprite>().alpha = 0;
            _numberOfCharacters++;
            return character;
        }

        private void OnClick(GameObject go)
        {
            if (go.GetComponent<Character>().GetIsHited() == false)
            {
                go.GetComponent<Character>().SetIsHited(true);
                SoundManager.PlaySoundByName(SoundConstants.SFX_CORRECT_HIT);
                this.gameObject.GetComponent<CharacterManager>().HideCharacter(go);
                PointsController.AddPoints(PointsController.GetPointsToAdd());
            }
        }

        private void UpdateCenterPosition()
        {
            int col = 0;
            int line = 0;
            float characterScale;
            float totalOfcols = 3;
            int offsetX = 0;
            GameObject charObj;
            for (int i = 0; i < _numberOfCharactersInCenter; i++)
            {
                if (i < _listOfCharacters.Count)
                {
                    characterScale = 1 - ((totalOfcols - line) / 10);
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
                    charObj.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
                    charObj.GetComponent<UI2DSprite>().leftAnchor.absolute = -384 + (col * 250) - offsetX;
                    charObj.GetComponent<UI2DSprite>().rightAnchor.absolute = -108 + (col * 250) - offsetX;
                    charObj.GetComponent<UI2DSprite>().bottomAnchor.absolute = -388 - (line * 150);
                    charObj.GetComponent<UI2DSprite>().topAnchor.absolute = -4 - (line * 145);
                    charObj.GetComponent<UI2DSprite>().UpdateAnchors();
                    charObj.GetComponent<UI2DSprite>().MakePixelPerfect();
                    charObj.transform.localScale = new Vector3(characterScale, characterScale, 1);
                    charObj.GetComponent<Character>().SetInitialPosition(_listOfCharacters[i].transform.localPosition);

                    col++;
                    FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_listOfCharacters[i], line + 1);
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
            GameObject charObj;
            for (int i = 0; i < _numberOfCharactersInLeft; i++)
            {
                if (i < _listOfCharacters.Count)
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
                        charObj.name = "left_" + _listOfCharacters[i].name;
                        charObj.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
                        charObj.GetComponent<UI2DSprite>().leftAnchor.absolute = 4400 - (col * 130);
                        charObj.GetComponent<UI2DSprite>().rightAnchor.absolute = -5568 - (col * 130);
                        charObj.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4140 - (line * 350) - offsetY;
                        charObj.GetComponent<UI2DSprite>().topAnchor.absolute = -3156 - (line * 350) - offsetY;
                        charObj.transform.Rotate(new Vector3(0f, 0f, 90f));
                        charObj.GetComponent<UI2DSprite>().UpdateAnchors();
                        charObj.GetComponent<UI2DSprite>().MakePixelPerfect();
                        charObj.transform.localScale = new Vector3(characterScale, characterScale, 1);
                        charObj.GetComponent<Character>().SetInitialPosition(_listOfCharacters[i].transform.localPosition);
                        col++;
                        FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_listOfCharacters[i + _numberOfCharactersInCenter], col);
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
            GameObject charObj;
            for (int i = 0; i < _numberOfCharactersInRight; i++)
            {
                if (i < _listOfCharacters.Count)
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
                        charObj.name = "right_" + _listOfCharacters[i].name;
                        charObj.GetComponent<UI2DSprite>().SetAnchor(_uiRoot);
                        charObj.GetComponent<UI2DSprite>().leftAnchor.absolute = 5520 + (col * 130);
                        charObj.GetComponent<UI2DSprite>().rightAnchor.absolute = -4450 + (col * 130);
                        charObj.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4141 - (line * 350) - offsetY;
                        charObj.GetComponent<UI2DSprite>().topAnchor.absolute = -3155 - (line * 350) - offsetY;
                        charObj.GetComponent<UI2DSprite>().UpdateAnchors();
                        charObj.transform.Rotate(new Vector3(0f, -180f, -90f));
                        charObj.GetComponent<UI2DSprite>().MakePixelPerfect();
                        charObj.transform.localScale = new Vector3(characterScale, characterScale, 1);
                        charObj.GetComponent<Character>().SetInitialPosition(_listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft].transform.localPosition);
                        col++;
                        FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_listOfCharacters[i + _numberOfCharactersInCenter + _numberOfCharactersInLeft], col);
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

        public List<GameObject> GetListOfCharacters()
        {
            return _listOfCharacters;
        }
    }
}
