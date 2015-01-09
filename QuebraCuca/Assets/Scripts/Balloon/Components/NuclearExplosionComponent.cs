using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class NuclearExplosionComponent : MonoBehaviour
    {
        private GameObject explosion;

        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += Explode;
        }

        void Start()
        {

        }

        void Explode(GameObject g)
        {
            //meController.SetEndedGame(true);
            explosion = new GameObject();
            explosion.name = "explosion";
            explosion.AddComponent<Explosion>().SetPath(this.gameObject.GetComponent<AnimationComponent>().GetPath());
        }

        void Update()
        {
        }
    }
}
