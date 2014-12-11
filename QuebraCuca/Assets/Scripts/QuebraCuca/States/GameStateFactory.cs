using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

using com.globo.sitio.mobilegames.QuebraCuca.Controllers;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Factory;

namespace com.globo.sitio.mobilegames.QuebraCuca.States
{
    public class GameStateFactory : IStateFactory
    {
        private static volatile GameStateFactory _instance;
        private static object _lock = new object();

        public static string START_SCENE = "StartScene";
        public static string GAME_SCENE = "GameScene";

        private static string _currentSceneName;
        private static Scene _currentScene;
        private static List<Scene> _listOfScenes;
        private static bool _sceneIsActive;
        private static GameObject _uiRoot;
        private static GameObject _camera;

        public IState CreateStateByID(AState.EGameState newStateID)
        {
            switch( newStateID )
            {
                case AState.EGameState.MENU :
                    return AFObject.Create<MenuState>();
                case AState.EGameState.GAME :
                    return AFObject.Create<GameState>();
            }
            return null;
        }

        public static GameObject GetUiRoot()
        {
            return _uiRoot;
        }

        public static GameObject GetCamera()
        {
            return _camera;
        }
    }
}
