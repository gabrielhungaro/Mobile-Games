using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    [System.Serializable]
    public class FastFowardComponent : MonoBehaviour
    {

        private int _fastFowardTime;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += ApplyFastFoward;
        }

        void Start()
        {

        }

        private void ApplyFastFoward(GameObject g)
        {
            TimeController.ActiveFastFoward(2f);
        }

        public void SetFastFowardTime(int value)
        {
            _fastFowardTime = value;
        }

        public int GetFastFowardTime()
        {
            return _fastFowardTime;
        }
    }
}

