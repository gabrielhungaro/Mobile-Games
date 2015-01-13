using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Utils;
using AquelaFrameWork.View;
using AquelaFrameWork.Components;


using com.globo.sitio.mobilegames.QuebraCuca.Controllers;
using com.globo.sitio.mobilegames.QuebraCuca.Constants;

namespace com.globo.sitio.mobilegames.QuebraCuca.States
{
    public class CucaPauseScreen : AFObject
    {
        private GameObject m_background;
        private GameObject m_closeBtn;
        private GameObject m_backToGameBtn;
        private GameObject m_soundBtn;
        private GameObject m_pauseInterface;

        private bool m_isSoundOn = true;
        private bool m_isVisible = false;

        public void Initialize()
        {
            string path = AFAssetManager.GetDirectoryOwner("Prefabs/CucaPauseScreen");
            m_pauseInterface = AFAssetManager.Instance.Instantiate<GameObject>(path);

            if (AFObject.IsNull(m_pauseInterface))
                AFDebug.LogError("CucaPauseScreen was not able to load PauseScene");
            else
            {
                m_background = GameObject.Find("backgroundPauseScreen");
                m_closeBtn = GameObject.Find("closeBtn");
                m_backToGameBtn = GameObject.Find("backToGameBtn");
                m_soundBtn = GameObject.Find("soundBtn");

                LoadAssetAndAddBackgroundSrpite(m_background, PathConstants.GetGameScenePath("jogocuca_pause_screen"));
                LoadAssetAndAddSrpite(m_closeBtn, PathConstants.GetGameScenePath("closeBtn"));
                LoadAssetAndAddSrpite(m_backToGameBtn, PathConstants.GetGameScenePath("backBtn"));
                LoadAssetAndAddSrpite(m_soundBtn, PathConstants.GetGameScenePath("soundOnBtn"));

                m_closeBtn.GetComponent<Button>().onClick.AddListener(OnCloseButtonClickHandler);
                m_backToGameBtn.GetComponent<Button>().onClick.AddListener(OnBackButtonClickHandler);
                m_soundBtn.GetComponent<Button>().onClick.AddListener(OnSoundButtonClickHandler);
            }

            Hide();
        }

        internal void LoadAssetAndAddSrpite( GameObject go , string path )
        {

            if( !AFObject.IsNull( go ) )
            {
                Sprite L_sprite = AFAssetManager.Instance.Load<Sprite>(path);

                if (AFObject.IsNull(L_sprite))
                    AFDebug.LogError("CucaPauseScreen was not able to load : " + path);
                else
                    go.GetComponent<Image>().sprite = L_sprite;
            }
        }

        internal void LoadAssetAndAddBackgroundSrpite(GameObject go, string path)
        {
            if (!AFObject.IsNull(go))
            {
                Sprite L_sprite = AFAssetManager.Instance.Load<Sprite>(path);

                if (AFObject.IsNull(L_sprite))
                    AFDebug.LogError("CucaPauseScreen was not able to load : " + path);
                else
                    go.GetComponent<Image>().sprite = L_sprite;
            }
        }

        public void Show()
        {
            m_isVisible = true;
            this.m_pauseInterface.gameObject.SetActive(true);
        }

        public void Hide()
        {
            m_isVisible = false;
            AFEngine.Instance.UnPause();
            this.m_pauseInterface.gameObject.SetActive(false);
        }

        public bool IsVisible()
        {
            return m_isVisible;
        }

        private void OnCloseButtonClickHandler()
        {
            Hide();
            AFEngine.Instance.GetStateManger().GotoState(AState.EGameState.MENU);
        }

        private void OnBackButtonClickHandler()
        {
            Hide();
        }

        private void OnSoundButtonClickHandler()
        {
            if (m_isSoundOn)
            {
                LoadAssetAndAddSrpite(m_soundBtn, PathConstants.GetGameScenePath("soundOffBtn"));
                m_isSoundOn = false;
            }
            else
            {
                LoadAssetAndAddSrpite(m_soundBtn, PathConstants.GetGameScenePath("soundOnBtn"));
                m_isSoundOn = true;
            }
            
        }

        public override void AFDestroy()
        {
            base.AFDestroy();
        }
    }
}
