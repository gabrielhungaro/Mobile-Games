using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Controllers;

namespace Scenes
{
    public class SceneManager
    {
        private static volatile SceneManager _instance;
        private static object _lock = new object();

        public static string START_SCENE = "StartScene";
        public static string GAME_SCENE = "GameScene";

        private static string _currentSceneName;
        private static Scene _currentScene;
        private static List<Scene> _listOfScenes;
        private static bool _sceneIsActive;
        private static GameObject _uiRoot;
        private static GameObject _camera;

        static SceneManager() { }

        public static SceneManager Instance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new SceneManager();
                }
            }
            return _instance;
        }

        private SceneManager()
        {
            CreateCamera();

            _listOfScenes = new List<Scene>();
            _listOfScenes.Add(AddScene(START_SCENE, new GameObject().AddComponent<StartScene>()));
            _listOfScenes.Add(AddScene(GAME_SCENE, new GameObject().AddComponent<GameScene>()));
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

        private void CreateCamera()
        {
            if (_uiRoot == null)
            {
                _uiRoot = new GameObject();
                _uiRoot.name = "uiRoot";
                _uiRoot.AddComponent<UIRoot>();
                _uiRoot.AddComponent<UIPanel>();
                _uiRoot.AddComponent<Rigidbody>().useGravity = false;
                _uiRoot.GetComponent<Rigidbody>().isKinematic = true;
                _uiRoot.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
                _uiRoot.GetComponent<UIRoot>().manualHeight = CameraController.CAMERA_HEIGHT;
            }

            if (_camera == null)
            {
                _camera = new GameObject();
                _camera.name = "camera";
                _camera.AddComponent<UICamera>();
                //_camera.AddComponent<Camera>();
                _camera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
                //_camera.GetComponent<Camera>().cullingMask = 
                _camera.GetComponent<Camera>().isOrthoGraphic = CameraController.ORTHOGRAPHIC;
                _camera.GetComponent<Camera>().orthographicSize = CameraController.ORTHOGRAPHIC_SIZE;
                _camera.GetComponent<Camera>().nearClipPlane = CameraController.NEAR_CLIP_PLANE;
                _camera.GetComponent<Camera>().farClipPlane = CameraController.FAR_CLIP_PLANE;
            }
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
