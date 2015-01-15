using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    class DestroyerComponent : MonoBehaviour
    {

        private float _screenLimite;

        void Awake()
        {
            //this.gameObject.GetComponent<ClickComponent>().OnClick += DestroyBalloon;
        }

        void Start()
        {
            _screenLimite = (Screen.height / 50f);
        }

        void Update()
        {
            if (this.gameObject.activeInHierarchy)
            {
                if (this.gameObject.transform.localPosition.y >= _screenLimite)
                {
                    if (this.gameObject.GetComponent<Balloon>().GetIsGood())
                        LifeController.RemoveLife();

                    DestroyBalloon(null);
                }
                if (this.gameObject.GetComponent<Balloon>().GetIsTouching() == true)
                {
                    DestroyBalloon(null);
                }
                if (this.gameObject.GetComponent<Balloon>().GetCanBeDestroyed() == true)
                {
                    DestroyBalloon(null);
                }
            }
        }

        public void DestroyBalloon(GameObject g)
        {
            this.gameObject.SetActive(false);
            this.gameObject.GetComponent<Balloon>().SetCanBeDestroyed(false);
            this.gameObject.GetComponent<Balloon>().SetIsTouching(false);
        }
    }
}
