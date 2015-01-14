using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;

namespace com.globo.sitio.mobilegames.Balloon
{
    public class PauseScreen : AFObject
    {

        private GameObject m_interface;
        private GameObject _background;
        private GameObject _exitButton;
        private GameObject _backButton;
        private GameObject _soundButton;

        public void Initialize()
        {
            string path = AFAssetManager.GetPathTargetPlatform() + "Prefabs/Balloon/PauseCanvas";

            GameObject pauseStateToInstantiate = AFAssetManager.Instance.Load<GameObject>(path);
            Sprite L_sprite;

            if (!AFObject.IsNull(pauseStateToInstantiate))
            {
                m_interface = AFAssetManager.Instance.Instantiate<GameObject>(path);

                if (!AFObject.IsNull(m_interface))
                {
                    Canvas L_canvas = m_interface.GetComponent<Canvas>();

                    if (!AFObject.IsNull(L_canvas))
                    {
                        L_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    }
                    else
                    {
                        AFDebug.LogError("Canvas not found");
                    }

                    _background = GameObject.Find("PauseBG");
                    _exitButton = GameObject.Find("PauseExitButton");
                    _backButton = GameObject.Find("PauseBackButton");
                    _soundButton = GameObject.Find("PauseSoundButton");

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "pauseBackground");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _background.GetComponent<Image>().sprite = L_sprite;

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnExit");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _exitButton.GetComponent<Image>().sprite = L_sprite;
                    _exitButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickExitButton);

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnReturn");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _backButton.GetComponent<Image>().sprite = L_sprite;
                    _backButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickBackButton);

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnSound_On");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _soundButton.GetComponent<Image>().sprite = L_sprite;
                    _soundButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickSoundButton);

                }
                else
                {
                    AFDebug.LogError("Não foi possível encontrar a interface do login");
                }
            }
            else
            {
                AFDebug.LogError("Não foi possível encontrar a interface do login para clonar");
            }
        }

        private void OnClickExitButton()
        {
            HideScreen();
            AFEngine.Instance.GetStateManger().GotoState(AState.EGameState.MENU_1);
        }

        private void OnClickBackButton()
        {
            HideScreen();
            AFEngine.Instance.UnPause();
        }

        private void OnClickSoundButton()
        {
            string path;
            Sprite L_sprite;

            if (SoundManager.GetAllSoundsIsMuting() == false)
            {
                path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnSound_Off");
                L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                _soundButton.GetComponent<Image>().sprite = L_sprite;

                SoundManager.MuteAllSounds();
            }
            else
            {
                path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnSound_On");
                L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                _soundButton.GetComponent<Image>().sprite = L_sprite;

                SoundManager.UnMuteAllSounds();
            }
        }

        public void Destroy()
        {
            GameObject.Destroy(_background);
            GameObject.Destroy(_backButton);
            GameObject.Destroy(_exitButton);
            GameObject.Destroy(_soundButton);
            GameObject.Destroy(m_interface);
        }

        public void ShowScreen()
        {
            m_interface.SetActive(true);
        }

        public void HideScreen()
        {
            m_interface.SetActive(false);
        }
    }
}
