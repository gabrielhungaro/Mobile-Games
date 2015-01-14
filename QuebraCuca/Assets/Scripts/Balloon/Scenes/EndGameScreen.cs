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
    public class EndGameScreen : AFObject
    {
        private GameObject m_interface;
        private GameObject _background;
        private GameObject _pointsText;
        private GameObject _recordText;
        private GameObject _tryAgainButton;
        private GameObject _returnButton;

        public void Initialize()
        {
            string path = AFAssetManager.GetPathTargetPlatform() + "Prefabs/Balloon/EndGameCanvas";

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

                    _background = GameObject.Find("EndGameBG");
                    _pointsText = GameObject.Find("EndGamePointsText");
                    _recordText = GameObject.Find("EndGameRecordText");
                    _tryAgainButton = GameObject.Find("EndGameTryAgainButton");
                    _returnButton = GameObject.Find("EndGameReturnButton");

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "gameOverBackground");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _background.GetComponent<Image>().sprite = L_sprite;

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnRetry_GameOver");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _tryAgainButton.GetComponent<Image>().sprite = L_sprite;
                    _tryAgainButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickTryAgainButton);

                    path = AFAssetManager.GetPathTargetPlatformWithResolution(ConstantsPaths.GetInGamePath() + "btnExit_GameOver");
                    L_sprite = AFAssetManager.Instance.Load<Sprite>(path);
                    _returnButton.GetComponent<Image>().sprite = L_sprite;
                    _returnButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnClickReturnButton);
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

        private void OnClickTryAgainButton()
        {
            AFEngine.Instance.GetStateManger().GotoState(AState.EGameState.GAME);
        }

        private void OnClickReturnButton()
        {
            Debug.Log("vou voltar");
            AFEngine.Instance.GetStateManger().GotoState(AState.EGameState.MENU);
        }
    }
}
