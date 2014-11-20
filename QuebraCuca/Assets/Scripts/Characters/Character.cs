using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Elements;
using Constants;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        private string _imagePath;
        private int _characterDepth;
        private bool _hited;
        private bool _isShowing;
        private Vector3 _initialPosition;

        void Start()
        {
            //this.gameObject.AddComponent<Button>().SetImagePath(_imagePath);
            //this.gameObject.GetComponent<Button>().OnClick += OnClick;
        }

        public void SetImagePath(string value)
        {
            _imagePath = value;
        }

        public void SetIsHited(bool value)
        {
            _hited = value;
        }

        public bool GetIsHited()
        {
            return _hited;
        }

        public void SetIsShowing(bool value)
        {
            _isShowing = value;
        }

        public bool GetIsShowing()
        {
            return _isShowing;
        }

        public void SetInitialPosition(Vector3 value)
        {
            _initialPosition = value;
        }

        public Vector3 GetInitialPosition()
        {
            return _initialPosition;
        }
    }
}
