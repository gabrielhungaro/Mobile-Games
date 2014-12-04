using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Controllers;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Factory;

namespace States
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

        private Scene AddScene(string sceneName, Scene sceneComponent)
        {
            Scene _scene = null;
            if (sceneComponent != null)
            {
                UnityEngine.Debug.Log("[ SCENE_MANAGER ] - Adicionando cena: " + sceneName);
                _scene = sceneComponent;
                _scene.name = sceneName;
                _scene.SetName(sceneName);
                _scene.enabled = false;
            }

            return _scene;
        }

        public static void ChangeScene(string name)
        {
            if(name == _currentSceneName)
            {
                return;
            }

            if (_currentScene)
            {
                GetCurrentScene().SetActive(false);
                GetCurrentScene().enabled = false;
                Debug.Log("[ SCENE_MANAGER ] - desativando a cena [ " + GetCurrentScene().GetName() + " ] ");
            }

            _currentScene = GetSceneByName(name);
            _currentScene.SetActive(true);
            _currentScene.enabled = true;
            Debug.Log("[ SCENE_MANAGER ] - ativando a cena [ " + GetCurrentScene().GetName() + " ] ");
        }

        public static Scene GetSceneByName(string sceneName)
        {
            Scene scene = null;
            foreach (Scene obj in _listOfScenes)
            {
                if (obj.GetName() == sceneName)
                {
                    scene = obj;
                }
            }
            return scene;
        }

        public static string GetCurrentSceneName()
        {
            return _currentSceneName;
        }

        public static Scene GetCurrentScene()
        {
            return _currentScene;
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
