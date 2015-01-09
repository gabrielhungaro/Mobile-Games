using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{

    [System.Serializable]
    public class AddPointComponent : MonoBehaviour
    {

        private int _pointsToAdd;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += AddPoints;
        }

        void Start()
        {

        }

        void Update()
        {

        }

        private void AddPoints(GameObject g)
        {
            //this.gameObject.GetComponent<Animator>().Play(ConstantsAnimations.EXPLODE);
            ScoreController.AddPoints(_pointsToAdd);
        }

        public void SetPointsToAdd(int value)
        {
            _pointsToAdd = value;
        }

        public int GetPointsToAdd()
        {
            return _pointsToAdd;
        }
    }
}

