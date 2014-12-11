using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class RemovePointComponent : MonoBehaviour
    {

        private int _pointsToRemove;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += RemovePoints;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void RemovePoints(GameObject g)
        {
            ScoreController.RemovePoints(_pointsToRemove);
        }

        public void SetPointsToRemove(int value)
        {
            _pointsToRemove = value;
        }

        public int GetPointsToRemove()
        {
            return _pointsToRemove;
        }
    }
}

