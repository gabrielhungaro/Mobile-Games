using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    [System.Serializable]
    public class RemoveTimeComponent : MonoBehaviour
    {

        private int _timeToRemove;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += RemoveTime;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void RemoveTime(GameObject g)
        {
            TimeController.RemoveTime(_timeToRemove);
        }

        public void SetTimeToRemove(int value)
        {
            _timeToRemove = value;
        }

        public int GetTimeToRemove()
        {
            return _timeToRemove;
        }
    }
}

