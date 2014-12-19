using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Com.Globo.Sitio.MobileGames.Balloon
{
    public class ExplosionComponent : MonoBehaviour
    {
        void Awake()
        {
            this.gameObject.GetComponent<ClickComponent>().OnClick += Explode;
        }

        void Start()
        {

        }

        void Explode(GameObject g)
        {
            List<GameObject> listOfBalloons = GameObject.FindObjectOfType<BalloonFactory>().GetListOfBalloons();

            for (int i = 0; i < listOfBalloons.Count; i++)
            {
                if (listOfBalloons[i].gameObject.activeInHierarchy == true)
                {
                    if (listOfBalloons[i].GetComponent<ClickComponent>() != null)
                    {
                        if (listOfBalloons[i].gameObject != this.gameObject)
                        {
                            Debug.Log("Bãlão diferente: " + listOfBalloons[i].gameObject.name);
                            listOfBalloons[i].GetComponent<ClickComponent>().SetExplodeByClick(false);
                            //listOfBalloons[i].GetComponent<ClickComponent>().OnMouseDown();
                        }
                        else
                        {
                            Debug.Log("Bãlão igual: " + listOfBalloons[i].gameObject.name);

                        }
                    }
                }
            }
        }

        void Update()
        {

        }
    }
}
