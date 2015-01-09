using UnityEngine;
using System;
using System.Collections;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Factory;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class GameStateFactory : IStateFactory
    {
        private static volatile GameStateFactory _instance;
        private static object _lock = new object();

        public static string START_SCENE = "Balloon";
        public static string CHOOSE_MODE_SCENE = "BalloonChooseModeScene";
        public static string GAME_SCENE = "BalloonGameScene";
        public static string CREDITS_SCENE = "BalloonCreditsScene";
        public static string RESULT_SCENE = "BalloonResultScene";

        public IState CreateStateByID(AState.EGameState newStateID)
        {
            switch (newStateID)
            {
                case AState.EGameState.MENU :
                    return AFObject.Create<StartScene>();
                case AState.EGameState.MENU_1:
                    return AFObject.Create<ChooseModeScene>();
                case AState.EGameState.GAME:
                    return AFObject.Create<GameState>();
                //case AState.EGameState.
            }
            return null;
        }

        // Use this for initialization
        void Start()
        {

        }

        public void GotoGameScene()
        {
            Application.LoadLevel(GAME_SCENE);
        }

        public void GotoStartScene()
        {
            Application.LoadLevel(START_SCENE);
        }

        public void GotoCreditsScene()
        {
            Application.LoadLevel(CREDITS_SCENE);
        }

        public void GotoChooseModeScene()
        {
            Application.LoadLevel(CHOOSE_MODE_SCENE);
        }

        public void GotoResultScene()
        {
            Application.LoadLevel(RESULT_SCENE);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
