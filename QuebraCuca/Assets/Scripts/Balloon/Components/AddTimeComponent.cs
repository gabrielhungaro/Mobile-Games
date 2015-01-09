using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{

    [System.Serializable]
    public class AddTimeComponent : MonoBehaviour
    {

        private int _timeToAdd;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += AddTime;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void AddTime(GameObject g)
        {
            TimeController.AddTime(_timeToAdd);
        }

        public void SetTimeToAdd(int value)
        {
            _timeToAdd = value;
        }

        public int GetTimeToAdd()
        {
            return _timeToAdd;
        }
    }
}

