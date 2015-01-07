using UnityEngine;
using System.Collections;

using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core.State;
using AquelaFrameWork.Sound;

namespace BateRebate
{
    public class BateMenuState : AState
    {
        private string bgAssetUrl = "Scenes/Menu/tela-inicio";
        private string titleAssetUrl = "Scenes/Menu/tela-inicioOver";
        private string btJogarAssetUrl = "Scenes/Menu/telainicioJogar";
        private string btVoltarAssetUrl = "Scenes/Menu/telainicioVoltar";

        private GameObject m_menuScene;
        private GameObject background;
        private GameObject backgroundTitle;
        private GameObject btJogar;
        private GameObject btVoltar;

        private BateController main;

        protected override void Awake()
        {
            m_stateID = EGameState.MENU;
        }
        public override void BuildState()
        {
            main = BateController.Instance;
            main.IsPaused = false;
            main.IsSounding = true;
            main.PlayerNumber = 1;

            bgAssetUrl = main.AddPlatformAndQualityToUrl(bgAssetUrl);
            titleAssetUrl = main.AddPlatformAndQualityToUrl(titleAssetUrl);
            btJogarAssetUrl = main.AddPlatformAndQualityToUrl(btJogarAssetUrl);
            btVoltarAssetUrl = main.AddPlatformAndQualityToUrl(btVoltarAssetUrl);
            
            m_menuScene = AFAssetManager.Instance.Instantiate<GameObject>( AFAssetManager.GetDirectoryOwner("preFabs/PreFabMenuScene") );

            background = GameObject.Find("menuBg");
            backgroundTitle = GameObject.Find("menuTitle");
            btJogar = GameObject.Find("menuBtJogar");
            btVoltar = GameObject.Find("menuBtVoltar");
            
            background.transform.localScale = Vector3.one;
            backgroundTitle.transform.localScale = Vector3.one;
            btJogar.transform.localScale = Vector3.one;
            btVoltar.transform.localScale = Vector3.one;

            background.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(bgAssetUrl);
            background.GetComponent<Image>().preserveAspect = true;

            backgroundTitle.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(titleAssetUrl);
            backgroundTitle.GetComponent<Image>().preserveAspect = true;

            btJogar.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(btJogarAssetUrl);
            btJogar.GetComponent<Image>().preserveAspect = true;

            btVoltar.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(btVoltarAssetUrl);
            btVoltar.GetComponent<Image>().preserveAspect = true;
            
            btJogar.GetComponent<Button>().onClick.AddListener(OnBtJogarClick);
            btVoltar.GetComponent<Button>().onClick.AddListener(OnBtVoltarClick);

            Add(m_menuScene);

            AddSounds();

            base.BuildState();
        }
        private void AddSounds()
        {
            AFSoundManager.Instance.Add(main.somAmbiente, null, 1f, 1f, true);
            AFSoundManager.Instance.Add(main.somBallHitPaddle, null, 1f, 1f, false, false);
            AFSoundManager.Instance.Add(main.somBallHitWall, null, 1f, 1f, false, false);
            AFSoundManager.Instance.Add(main.somPlayerScores, null, 1f, 1f, false, false);
            AFSoundManager.Instance.Add(main.somIAScores, null, 1f, 1f, false, false);

            main.PlaySound();
        }
        private void OnBtJogarClick()
        {
            main.GoToSelection();
        }
        private void OnBtVoltarClick()
        {
            main.QuitGame();
        }
    }
}
