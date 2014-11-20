using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Controllers;
using Constants;
using Characters;

namespace Scenes
{
    public class GameScene : Scene
    {
        //private GameObject _uiRoot;
        private GameObject _camera;
        private GameObject _background;
        private PathConstants _paths;
        private ResolutionController _resolutionController;
        private GameObject _leftWall1;
        private GameObject _leftWall2;
        private GameObject _leftWall3;
        private GameObject _rightWall1;
        private GameObject _rightWall2;
        private GameObject _rightWall3;
        private GameObject _floor1;
        private GameObject _floor2;
        private GameObject _floor3;
        private GameObject _roof;
        private GameObject _pointsBg;
        private int _ticks;
        private GameObject _indexController;
        private Ray _ray;

        void Start()
        {
            _resolutionController = ResolutionController.Instance();
            if(this.gameObject.GetComponent<GameController>() == false)
                this.gameObject.AddComponent<GameController>();

            if(this.gameObject.GetComponent<IndexController>() == false)
                this.gameObject.AddComponent<IndexController>().Start();
            
            _paths = PathConstants.Instance();

            /*if (_uiRoot == null)
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
            }*/
            CreateBackground();
            if(this.gameObject.GetComponent<HudController>() == false)
                this.gameObject.AddComponent<HudController>();
        }

        private void CreateBackground()
        {
            _background = new GameObject();
            _background.name = "background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "background");
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
            _background.AddComponent<Rigidbody>().useGravity = false;
            _background.GetComponent<Rigidbody>().isKinematic = true;
            _background.AddComponent<BoxCollider>().size = new Vector3(_background.GetComponent<UI2DSprite>().width,
                                                                    _background.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_background).onClick += OnClickBG;

            _leftWall3 = new GameObject();
            _leftWall3.name = "_leftWall3";
            _leftWall3.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall3");
            _leftWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            _leftWall3.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _leftWall3.GetComponent<UI2DSprite>().leftAnchor.absolute = 4099;
            _leftWall3.GetComponent<UI2DSprite>().rightAnchor.absolute = -5615;
            _leftWall3.GetComponent<UI2DSprite>().bottomAnchor.absolute = -3072;
            _leftWall3.GetComponent<UI2DSprite>().topAnchor.absolute = 3452;
            _leftWall3.GetComponent<UI2DSprite>().UpdateAnchors();
            _leftWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_leftWall3.AddComponent<Rigidbody>().useGravity = false;
            _leftWall3.GetComponent<Rigidbody>().isKinematic = true;
            _leftWall3.AddComponent<BoxCollider>().size = new Vector3(_leftWall3.GetComponent<UI2DSprite>().width,
                                                                    _leftWall3.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall3, 1);

            _rightWall3 = new GameObject();
            _rightWall3.name = "_rightWall3";
            _rightWall3.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall3");
            _rightWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            _rightWall3.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _rightWall3.GetComponent<UI2DSprite>().leftAnchor.absolute = 1024;
            _rightWall3.GetComponent<UI2DSprite>().rightAnchor.absolute = 492;
            _rightWall3.GetComponent<UI2DSprite>().bottomAnchor.absolute = 768;
            _rightWall3.GetComponent<UI2DSprite>().topAnchor.absolute = -388;
            _rightWall3.GetComponent<UI2DSprite>().UpdateAnchors();
            _rightWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_rightWall3.AddComponent<Rigidbody>().useGravity = false;
            _rightWall3.GetComponent<Rigidbody>().isKinematic = true;
            _rightWall3.AddComponent<BoxCollider>().size = new Vector3(_rightWall3.GetComponent<UI2DSprite>().width,
                                                                    _rightWall3.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall3, 1);

            _floor3 = new GameObject();
            _floor3.name = "_floor3";
            _floor3.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor3");
            _floor3.GetComponent<UI2DSprite>().MakePixelPerfect();
            _floor3.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _floor3.GetComponent<UI2DSprite>().leftAnchor.absolute = 4099;
            _floor3.GetComponent<UI2DSprite>().rightAnchor.absolute = -4099;
            _floor3.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3073;
            _floor3.GetComponent<UI2DSprite>().topAnchor.absolute = -4129;
            _floor3.GetComponent<UI2DSprite>().UpdateAnchors();
            _floor3.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_floor3.AddComponent<Rigidbody>().useGravity = false;
            _floor3.GetComponent<Rigidbody>().isKinematic = true;
            _floor3.AddComponent<BoxCollider>().size = new Vector3(_floor3.GetComponent<UI2DSprite>().width,
                                                                    _floor3.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor3, 1);

            _leftWall2 = new GameObject();
            _leftWall2.name = "_leftWall2";
            _leftWall2.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall2");
            _leftWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            _leftWall2.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _leftWall2.GetComponent<UI2DSprite>().leftAnchor.absolute = 4096;
            _leftWall2.GetComponent<UI2DSprite>().rightAnchor.absolute = -5756;
            _leftWall2.GetComponent<UI2DSprite>().bottomAnchor.absolute = -3074;
            _leftWall2.GetComponent<UI2DSprite>().topAnchor.absolute = 3402;
            _leftWall2.GetComponent<UI2DSprite>().UpdateAnchors();
            _leftWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_leftWall2.AddComponent<Rigidbody>().useGravity = false;
            _leftWall2.GetComponent<Rigidbody>().isKinematic = true;
            _leftWall2.AddComponent<BoxCollider>().size = new Vector3(_leftWall2.GetComponent<UI2DSprite>().width,
                                                                    _leftWall2.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall2, 2);

            _rightWall2 = new GameObject();
            _rightWall2.name = "_rightWall2";
            _rightWall2.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall2");
            _rightWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            _rightWall2.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _rightWall2.GetComponent<UI2DSprite>().leftAnchor.absolute = 5793;
            _rightWall2.GetComponent<UI2DSprite>().rightAnchor.absolute = -4083;
            _rightWall2.GetComponent<UI2DSprite>().bottomAnchor.absolute = -3074;
            _rightWall2.GetComponent<UI2DSprite>().topAnchor.absolute = 3402;
            _rightWall2.GetComponent<UI2DSprite>().UpdateAnchors();
            _rightWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_rightWall2.AddComponent<Rigidbody>().useGravity = false;
            _rightWall2.GetComponent<Rigidbody>().isKinematic = true;
            _rightWall2.AddComponent<BoxCollider>().size = new Vector3(_rightWall2.GetComponent<UI2DSprite>().width,
                                                                    _rightWall2.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall2, 2);

            _floor2 = new GameObject();
            _floor2.name = "_floor2";
            _floor2.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor2");
            _floor2.GetComponent<UI2DSprite>().MakePixelPerfect();
            _floor2.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _floor2.GetComponent<UI2DSprite>().leftAnchor.absolute = 4112;
            _floor2.GetComponent<UI2DSprite>().rightAnchor.absolute = -4086;
            _floor2.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3070;
            _floor2.GetComponent<UI2DSprite>().topAnchor.absolute = -4212;
            _floor2.GetComponent<UI2DSprite>().UpdateAnchors();
            _floor2.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_floor2.AddComponent<Rigidbody>().useGravity = false;
            _floor2.GetComponent<Rigidbody>().isKinematic = true;
            _floor2.AddComponent<BoxCollider>().size = new Vector3(_floor2.GetComponent<UI2DSprite>().width,
                                                                    _floor2.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor2, 2);

            _leftWall1 = new GameObject();
            _leftWall1.name = "_leftWall1";
            _leftWall1.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "leftWall1");
            _leftWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            _leftWall1.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _leftWall1.GetComponent<UI2DSprite>().leftAnchor.absolute = 4096;
            _leftWall1.GetComponent<UI2DSprite>().rightAnchor.absolute = -5960;
            _leftWall1.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3072;
            _leftWall1.GetComponent<UI2DSprite>().topAnchor.absolute = -3076;
            _leftWall1.GetComponent<UI2DSprite>().UpdateAnchors();
            _leftWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_leftWall1.AddComponent<Rigidbody>().useGravity = false;
            _leftWall1.GetComponent<Rigidbody>().isKinematic = true;
            _leftWall1.AddComponent<BoxCollider>().size = new Vector3(_leftWall1.GetComponent<UI2DSprite>().width,
                                                                    _leftWall1.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_leftWall1, 3);

            _rightWall1 = new GameObject();
            _rightWall1.name = "_rightWall1";
            _rightWall1.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "rightWall1");
            _rightWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            _rightWall1.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _rightWall1.GetComponent<UI2DSprite>().leftAnchor.absolute = 5917;
            _rightWall1.GetComponent<UI2DSprite>().rightAnchor.absolute = -4083;
            _rightWall1.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3075;
            _rightWall1.GetComponent<UI2DSprite>().topAnchor.absolute = -3073;
            _rightWall1.GetComponent<UI2DSprite>().UpdateAnchors();
            _rightWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_rightWall1.AddComponent<Rigidbody>().useGravity = false;
            _rightWall1.GetComponent<Rigidbody>().isKinematic = true;
            _rightWall1.AddComponent<BoxCollider>().size = new Vector3(_rightWall1.GetComponent<UI2DSprite>().width,
                                                                    _rightWall1.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_rightWall1, 3);

            _floor1 = new GameObject();
            _floor1.name = "_floor1";
            _floor1.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "floor1");
            _floor1.GetComponent<UI2DSprite>().MakePixelPerfect();
            _floor1.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _floor1.GetComponent<UI2DSprite>().leftAnchor.absolute = 4112;
            _floor1.GetComponent<UI2DSprite>().rightAnchor.absolute = -4086;
            _floor1.GetComponent<UI2DSprite>().bottomAnchor.absolute = 3073;
            _floor1.GetComponent<UI2DSprite>().topAnchor.absolute = -4393;
            _floor1.GetComponent<UI2DSprite>().UpdateAnchors();
            _floor1.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_floor1.AddComponent<Rigidbody>().useGravity = false;
            _floor1.GetComponent<Rigidbody>().isKinematic = true;
            _floor1.AddComponent<BoxCollider>().size = new Vector3(_floor1.GetComponent<UI2DSprite>().width,
                                                                    _floor1.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_floor1, 3);

            _roof = new GameObject();
            _roof.name = "_roof";
            _roof.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "roof1");
            _roof.GetComponent<UI2DSprite>().MakePixelPerfect();
            _roof.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _roof.GetComponent<UI2DSprite>().leftAnchor.absolute = 4237;
            _roof.GetComponent<UI2DSprite>().rightAnchor.absolute = -4237;
            _roof.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4329;
            _roof.GetComponent<UI2DSprite>().topAnchor.absolute = -3073;
            _roof.GetComponent<UI2DSprite>().UpdateAnchors();
            _roof.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_roof.AddComponent<Rigidbody>().useGravity = false;
            _roof.GetComponent<Rigidbody>().isKinematic = true;
            _roof.AddComponent<BoxCollider>().size = new Vector3(_roof.GetComponent<UI2DSprite>().width,
                                                                    _roof.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_roof, 3);

            _pointsBg = new GameObject();
            _pointsBg.name = "_pointsBg";
            _pointsBg.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "pointsBg");
            _pointsBg.GetComponent<UI2DSprite>().MakePixelPerfect();
            _pointsBg.GetComponent<UI2DSprite>().SetAnchor(SceneManager.GetUiRoot());
            _pointsBg.GetComponent<UI2DSprite>().leftAnchor.absolute = 4096;
            _pointsBg.GetComponent<UI2DSprite>().rightAnchor.absolute = -4102;
            _pointsBg.GetComponent<UI2DSprite>().bottomAnchor.absolute = 4327;
            _pointsBg.GetComponent<UI2DSprite>().topAnchor.absolute = -3063;
            _pointsBg.GetComponent<UI2DSprite>().UpdateAnchors();
            _pointsBg.GetComponent<UI2DSprite>().MakePixelPerfect();
            /*_pointsBg.AddComponent<Rigidbody>().useGravity = false;
            _pointsBg.GetComponent<Rigidbody>().isKinematic = true;
            _pointsBg.AddComponent<BoxCollider>().size = new Vector3(_pointsBg.GetComponent<UI2DSprite>().width,
                                                                    _pointsBg.GetComponent<UI2DSprite>().height);*/
            FindObjectOfType<IndexController>().AddObjectToLIstByIndex(_pointsBg, 3);

            /*GameObject place = new GameObject();
            place.name = "background";
            place.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(PathConstants.GetGameScenePath() + "place");
            place.GetComponent<UI2DSprite>().MakePixelPerfect();*/
        }

        private void OnClickBG(GameObject go)
        {
            SoundManager.PlaySoundByName(SoundConstants.SFX_WRONG_HIT);
        }

        void Update()
        {
            if (this.gameObject.GetComponent<GameController>())
            {
                this.gameObject.GetComponent<GameController>().MyUpdate();
            }
            _ticks++;
            if(_ticks >= 10 && _ticks <= 11){
                _leftWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
                _rightWall3.GetComponent<UI2DSprite>().MakePixelPerfect();
                _floor3.GetComponent<UI2DSprite>().MakePixelPerfect();
                _floor2.GetComponent<UI2DSprite>().MakePixelPerfect();
                _floor1.GetComponent<UI2DSprite>().MakePixelPerfect();
                _leftWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
                _rightWall2.GetComponent<UI2DSprite>().MakePixelPerfect();
                _leftWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
                _rightWall1.GetComponent<UI2DSprite>().MakePixelPerfect();
                _roof.GetComponent<UI2DSprite>().MakePixelPerfect();
                _pointsBg.GetComponent<UI2DSprite>().MakePixelPerfect();
            }

            /*if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log(FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition));
                //Debug.Log(Camera.main);
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Ray ray = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
                //Ray ray = _uiRoot.camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("teste de click: " + hit.transform.name);
                    if (this.gameObject.GetComponent<CharacterFactory>())
                    {
                        foreach (GameObject obj in this.gameObject.GetComponent<CharacterFactory>().GetListOfCharacters())
                        {
                            if (obj.GetComponent<Character>().GetIsHited())
                            {
                                Debug.Log("cuca clicada");
                            }
                        }
                    }
                    if (hit.transform.GetComponent<Character>())
                    {
                        if (hit.transform.GetComponent<Character>().GetIsShowing())
                        {
                            Debug.Log("CLICOU NA CUCA");
                        }
                    }
                    else
                    {
                        SoundManager.PlaySoundByName(SoundConstants.SFX_WRONG_HIT);
                    }
                }
            }*/
        }
    }
}
