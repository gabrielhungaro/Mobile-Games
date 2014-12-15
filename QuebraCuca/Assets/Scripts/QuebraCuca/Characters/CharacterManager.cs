using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.View;

using com.globo.sitio.mobilegames.QuebraCuca.States;

namespace com.globo.sitio.mobilegames.QuebraCuca.Characters
{
    public class CharacterManager : ASingleton<CharacterManager>
    {

        private int _timeBetweenCharacters = 1;
        private int _ticks;
        private List<Character> _listOfCharacters;
        private int _charsOnScreen;

        public delegate void OnAnimationEvent(UITweener tweener);
        public event OnAnimationEvent OnFinished;

        public GameObject currentCharacterTweening;
        private float _timeInTween = 0.2f;
        private float _distToTween = 100f;
        private float _timeShowing = 2f;
        
        public void Initialize()
        {
            _listOfCharacters = FindObjectOfType<GameState>().GetListOfCharacters();
        }

        public override void AFUpdate(double time)
        {
            _ticks++;
            if(_ticks * Time.deltaTime >= _timeBetweenCharacters){
                _ticks = 0;
                ShowCharacter(RandomCharacterToShow());
                _charsOnScreen++;
            }

            VerifyCharacterStatus();
        }

        private void VerifyCharacterStatus()
        {
            for (int i = 0; i < _listOfCharacters.Count; i++)
            {
                if (_listOfCharacters[i].GetComponent<Character>().GetIsHited() == true && _listOfCharacters[i].GetComponent<Character>().GetIsShowing() == true && _listOfCharacters[i].GetComponent<Character>().GetHitedAnimationIsComplete() == true)
                {
                    HideCharacter(_listOfCharacters[i]);
                }
            }
        }

        private void ShowCharacter(Character obj)
        {
            if (obj != null)
            {
                obj.SetIsShowing(true);
                obj.SetIsHited(false);
                //obj.GetComponent<UI2DSprite>().alpha = 1;

                Vector3 posToTween = new Vector3(0, 0, 0);

                //if (obj.GetComponent<Character>())
                //{
                //Debug.Log("objToShow: " + obj);
                    string[] charPosition = obj.GetComponent<Character>().name.Split(char.Parse("_"));
                    switch (charPosition[0])
                    {
                        case "left":
                            posToTween = new Vector3(obj.gameObject.transform.localPosition.x + _distToTween, obj.transform.localPosition.y, obj.transform.localPosition.z);
                            break;
                        case "center":
                            posToTween = new Vector3(obj.gameObject.transform.localPosition.x, obj.transform.localPosition.y + _distToTween, obj.transform.localPosition.z);
                            break;
                        case "right":
                            posToTween = new Vector3(obj.gameObject.transform.localPosition.x - _distToTween, obj.transform.localPosition.y, obj.transform.localPosition.z);
                            break;
                    }
                //}
                TweenPosition objTween = TweenPosition.Begin(obj.gameObject, _timeInTween, posToTween);

                EventDelegate.Parameter objToApplyTween = new EventDelegate.Parameter();
                objToApplyTween.obj = obj.gameObject;

                EventDelegate del = new EventDelegate(this, "CompleteShowTween");
                del.parameters.SetValue(objToApplyTween, 0);
                EventDelegate.Add(objTween.onFinished, del);
            }
        }

        public void CompleteShowTween(GameObject charObj)
        {
            //HideCharacter(charObj);
        }

        public void HideCharacter(Character obj)
        {
            if (obj != null)
            {
                Debug.Log("[ CHARACTER_MANAGER ] - HIDE_CHARACTER");
                Vector3 posToTween = new Vector3(0, 0, 0);

                obj.SetIsShowing(false);
                obj.SetIsHited(false);

                string[] charPosition = obj.name.Split(char.Parse("_"));
                switch (charPosition[0])
                {
                    case "left":
                        posToTween = new Vector3(obj.gameObject.transform.localPosition.x - _distToTween, obj.gameObject.transform.localPosition.y, obj.gameObject.transform.localPosition.z);
                        break;
                    case "center":
                        posToTween = new Vector3(obj.gameObject.transform.localPosition.x, obj.gameObject.transform.localPosition.y - _distToTween, obj.gameObject.transform.localPosition.z);
                        break;
                    case "right":
                        posToTween = new Vector3(obj.gameObject.transform.localPosition.x + _distToTween, obj.gameObject.transform.localPosition.y, obj.gameObject.transform.localPosition.z);
                        break;
                }


                TweenPosition objTween = TweenPosition.Begin(obj.gameObject, _timeInTween, posToTween);

                EventDelegate.Parameter objToApplyTween = new EventDelegate.Parameter();
                objToApplyTween.obj = obj.gameObject;

                EventDelegate del = new EventDelegate(this, "CompleteHideTween");
                del.parameters.SetValue(objToApplyTween, 0);
                EventDelegate.Add(objTween.onFinished, del);

            }
        }

        private void CompleteHideTween(GameObject charObj)
        {
            if (charObj != null)
            {
                /*charObj.GetComponent<Character>().SetIsShowing(false);
                charObj.GetComponent<Character>().SetIsHited(false);*/
                //charObj.GetComponent<UI2DSprite>().alpha = 0;
                _charsOnScreen--;
            }
            else
            {
                UnityEngine.Debug.LogWarning("[ CHARACTER_MANAGER ] - Tween completado por abjeto nulo!");
            }
        }

        private Character RandomCharacterToShow()
        {
            Character _character = null;
            if (_charsOnScreen < _listOfCharacters.Count)
            {
                int randomChar = Random.Range(0, _listOfCharacters.Count);
                if (_listOfCharacters[randomChar])
                {
                    if (_listOfCharacters[randomChar].GetComponent<Character>().GetIsShowing() == false)
                    {
                        _character = _listOfCharacters[randomChar];
                    }
                    else
                    {
                        _character = this.RandomCharacterToShow();
                    }
                }
            }
            return _character;
        }
    }
}
