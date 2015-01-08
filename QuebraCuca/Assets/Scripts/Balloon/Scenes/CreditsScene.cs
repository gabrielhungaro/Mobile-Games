using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Com.Globo.Sitio.MobileGames.Balloon
{

    public class CreditsScene : MonoBehaviour
    {

        private GameObject _uiRoot;
        private GameObject _camera;
        private GameObject _background;
        private GameObject _returnButton;

        void Start()
        {
            LayerController.Instance().Start();
            _uiRoot = new GameObject();
            _uiRoot.name = "_uiRoot";
            _uiRoot.layer = LayerController.Instance()._layerHUD;
            _uiRoot.AddComponent<UIRoot>();
            _uiRoot.AddComponent<UIPanel>();
            _uiRoot.AddComponent<Rigidbody>();
            _uiRoot.GetComponent<Rigidbody>().useGravity = false;
            _uiRoot.GetComponent<Rigidbody>().isKinematic = true;
            _uiRoot.GetComponent<UIRoot>().scalingStyle = UIRoot.Scaling.FixedSize;
            _uiRoot.GetComponent<UIRoot>().manualHeight = CameraController.CAMERA_HEIGHT;

            _camera = new GameObject();
            _camera.transform.parent = _uiRoot.transform;
            _camera.name = "uiCamera";
            _camera.AddComponent<Camera>();
            _camera.AddComponent<UICamera>();
            _camera.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
            _camera.GetComponent<Camera>().cullingMask = LayerController.Instance()._maskHud;
            _camera.GetComponent<Camera>().isOrthoGraphic = CameraController.ORTHOGRAPHIC;
            _camera.GetComponent<Camera>().orthographicSize = CameraController.ORTHOGRAPHIC_SIZE;
            _camera.GetComponent<Camera>().nearClipPlane = CameraController.NEAR_CLIP_PLANE;
            _camera.GetComponent<Camera>().farClipPlane = CameraController.FAR_CLIP_PLANE;
            _camera.transform.parent = this.gameObject.transform;

            CreateBackgorund();
            CreateReturnButton();
        }

        private void CreateBackgorund()
        {
            _background = new GameObject();
            _background.name = "Background";
            _background.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetCreditsPath() + "backGround");
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
            _background.GetComponent<UIWidget>().depth = -1;
            _background.layer = LayerController.Instance()._layerHUD;
            _background.transform.position = new Vector3(0, 0, -1f);
        }

        private void CreateReturnButton()
        {
            _returnButton = new GameObject();
            _returnButton.name = "returnButton";
            _returnButton.AddComponent<UI2DSprite>().sprite2D = Resources.Load<Sprite>(ConstantsPaths.GetCreditsPath() + "btnReturn");
            _returnButton.GetComponent<UI2DSprite>().MakePixelPerfect();
            _returnButton.AddComponent<UIButton>().SetState(UIButtonColor.State.Normal, true);
            _returnButton.AddComponent<BoxCollider>().size = new Vector3(_returnButton.GetComponent<UI2DSprite>().width,
                                                                        _returnButton.GetComponent<UI2DSprite>().height);
            UIEventListener.Get(_returnButton).onClick += OnClickReturnButton;
        }

        private void OnClickReturnButton(GameObject go)
        {
            SoundManager.PlaySoundByName(ConstantsSounds.SFX_BUTTON);
        }

        void Update()
        {
            _returnButton.transform.position = new Vector3(0, -(Screen.height / 1500f));
            _background.GetComponent<UI2DSprite>().MakePixelPerfect();
        }

    }
}
