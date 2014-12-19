using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class MoveComponent : MonoBehaviour
    {

        private float _velocity;

        void Start()
        {

        }

        public void SetVelocity(float value)
        {
            _velocity = value;
        }

        public float GetVelocity()
        {
            return _velocity;
        }
    }
}

