using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Characters;
using Scenes;

namespace Controllers
{
    public class GameController : MonoBehaviour
    {
        private bool gameEnded;

        void Start()
        {
            gameEnded = false;
            this.gameObject.AddComponent<CharacterFactory>();
            this.gameObject.AddComponent<CharacterManager>();
            LifesController lifesController = LifesController.Instance();
        }

        public void MyUpdate()
        {
            if (this.gameObject.GetComponent<CharacterFactory>())
            {
                this.gameObject.GetComponent<CharacterFactory>().MyUpdate();
            }
            if (this.gameObject.GetComponent<CharacterManager>())
            {
                this.gameObject.GetComponent<CharacterManager>().MyUpdate();
            }

            if (LifesController.GetLifes() == 0 && gameEnded == false)
            {
                gameEnded = true;
                EndGame();
            }
        }

        private void EndGame()
        {
            Debug.Log("acabando do jogo");
            SceneManager.ChangeScene(SceneManager.START_SCENE);
            //Application.LoadLevel(SceneManager.START_SCENE);
        }
    }
}
