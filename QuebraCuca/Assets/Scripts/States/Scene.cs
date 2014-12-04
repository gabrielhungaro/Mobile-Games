using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace States
{
    public class Scene : MonoBehaviour
    {

        private string _name;
        private bool _isActive;

        void Start()
        {

        }

        void Update()
        {

        }

        public void SetName(string value)
        {
            _name = value;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetActive(bool value)
        {
            _isActive = value;
        }

        public bool GetActive()
        {
            return _isActive;
        }
    }
}
