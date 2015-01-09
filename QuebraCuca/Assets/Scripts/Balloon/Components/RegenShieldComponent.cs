using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    [System.Serializable]
    public class RegenShieldComponent : MonoBehaviour
    {

        private int _shieldLife = 4;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += DestroyShield;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void DestroyShield(GameObject g)
        {
            _shieldLife--;
        }

        public void SetBalloonLife(int value)
        {
            _shieldLife = value;
        }

        public int GetBalloonLife()
        {
            return _shieldLife;
        }
    }
}

