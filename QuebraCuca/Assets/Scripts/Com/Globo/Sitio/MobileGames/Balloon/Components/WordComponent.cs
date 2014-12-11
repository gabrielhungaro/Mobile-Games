using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    [System.Serializable]
    public class WordComponent : MonoBehaviour
    {
        private string _wordToCollect = "emilia";
        private List<char> _listOfChars;
        private char _actualCharCollected;
        private int _numberOfCharsCollected;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += OnClick;
        }

        void Start()
        {
            Debug.Log("START WORD");
            _numberOfCharsCollected = 0;
            _listOfChars = new List<char>();
            for (int i = 0; i < _wordToCollect.ToString().Length; i++)
            {
                _listOfChars.Add(_wordToCollect[i]);
            }
            _actualCharCollected = _listOfChars[_numberOfCharsCollected];
            this.gameObject.GetComponent<AnimationComponent>().SetPath(this.gameObject.GetComponent<AnimationComponent>().GetPath() + "_" + _actualCharCollected);
            string path = this.gameObject.GetComponent<Balloon>().GetSpritePath();
            //this.gameObject.GetComponent<Balloon>().SetSpritePath(path + "Word_" + _actualCharCollected);
        }

        void Update()
        {

        }

        private void OnClick(GameObject g)
        {
            _numberOfCharsCollected++;
            if (_numberOfCharsCollected > _listOfChars.Count)
            {

            }
            else
            {
                _actualCharCollected = _listOfChars[_numberOfCharsCollected];
                updateLetter();
            }
            //this.gameObject.GetComponent<Animator>().Play(ConstantsAnimations.EXPLODE);
            //ScoreController.AddPoints(_pointsToAdd);
        }

        private void updateLetter()
        {
            this.gameObject.GetComponent<Balloon>().SetSpriteName("Word" + _actualCharCollected);
            this.gameObject.GetComponent<Balloon>().LoadSprite();
        }

        public void SetWordToCollect(string value)
        {
            _wordToCollect = value;
        }

        public string GetWordToCollect()
        {
            return _wordToCollect;
        }
    }
}

