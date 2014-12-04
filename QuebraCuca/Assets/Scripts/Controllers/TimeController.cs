using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Characters;
using States;

namespace Controllers
{
    public class TimeController : MonoBehaviour
    {
        private int _ticks;
        private float _timeShowingCharacter = 5;

        void Start()
        {
        }

        void Update()
        {
            if (FindObjectOfType<GameScene>())
            {
                if (this.gameObject.GetComponent<Character>().GetIsShowing() == true && this.gameObject.GetComponent<Character>().GetIsHited() == false)
                {
                    _ticks++;
                    if (_ticks * Time.deltaTime >= _timeShowingCharacter)
                    {
                        _ticks = 0;
                        FindObjectOfType<CharacterManager>().HideCharacter(this.gameObject);
                        LifesController.RemoveLife();
                    }
                }
            }
        }
    }
}
