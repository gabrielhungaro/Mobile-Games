using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    [System.Serializable]
    public class SlowMotionComponent : MonoBehaviour
    {

        private int _slowMotionTime;
        private int _ticks;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += ApplySlowMotion;
        }

        void Start()
        {

        }

        private void ApplySlowMotion(GameObject g)
        {
            TimeController.ActiveSlowMotion(2f);
        }

        public void SetSlowMotionTime(int value)
        {
            _slowMotionTime = value;
        }

        public int GetSlowMotionTime()
        {
            return _slowMotionTime;
        }
    }
}

