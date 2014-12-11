using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Characters;
using com.globo.sitio.mobilegames.QuebraCuca.States;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
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
            //if (FindObjectOfType<Ga>())
            //{
                if (this.gameObject.GetComponent<Character>().GetIsShowing() == true && this.gameObject.GetComponent<Character>().GetIsHited() == false)
                {
                    _ticks++;
                    if (_ticks * Time.deltaTime >= _timeShowingCharacter)
                    {
                        _ticks = 0;
                        FindObjectOfType<CharacterManager>().HideCharacter(this.gameObject.GetComponent<Character>());
                        LifesController.RemoveLife();
                    }
                }
            //}
        }
    }
}
