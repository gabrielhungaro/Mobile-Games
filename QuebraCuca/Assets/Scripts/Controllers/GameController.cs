﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Characters;
using States;

using AquelaFrameWork.Core;

namespace Controllers
{
    public class GameController : AFObject
    {
        private bool gameEnded;
        private GameObject _uiRoot;

        private CharacterManager _characterManger;
        private CharacterFactory _characterFactory;

        public void Initialize()
        {
            gameEnded = false;
            
            _characterFactory = CharacterFactory.Instance;
            _characterManger =  CharacterManager.Instance;

            _characterManger.Initialize();

            LifesController lifesController = LifesController.Instance();
        }

        public override void AFUpdate(double time)
        {
            _characterFactory.AFUpdate(time);
            _characterManger.AFUpdate(time);
            
            if (LifesController.GetLifes() == 0 && gameEnded == false)
            {
                gameEnded = true;
                EndGame();
            }
        }

        private void EndGame()
        {
            Debug.Log("acabando do jogo");
            //GameStateFactory.ChangeScene(GameStateFactory.START_SCENE);
            //Application.LoadLevel(SceneManager.START_SCENE);
        }

        internal void SetAnchorTarget(GameObject value)
        {
            _uiRoot = value;
        }
    }
}
