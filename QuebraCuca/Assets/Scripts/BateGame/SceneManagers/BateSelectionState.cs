using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;

namespace BateRebate
{
    public class BateSelectionState : AState
    {
        private string bgAssetUrl = "Scenes/Selection/bg";
        private string p1AssetUrl = "Scenes/Selection/p1";
        private string p2AssetUrl = "Scenes/Selection/p2";

        private GameObject m_selectionScene;
        private BateController main;
        private GameObject background;
        private GameObject btP1;
        private GameObject btP2;

        protected override void Awake()
        {
            m_stateID = EGameState.SELECTION;
        }

        public override void BuildState()
        {
            main = BateController.Instance;

            m_selectionScene = AFAssetManager.Instance.Instantiate<GameObject>(AFAssetManager.GetDirectoryOwner("preFabs/PreFabSelectionScene"));

            background = GameObject.Find("BG");
            bgAssetUrl = main.AddPlatformAndQualityToUrl(bgAssetUrl);
            background.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(bgAssetUrl);
            background.GetComponent<Image>().preserveAspect = true;

            btP1 = GameObject.Find("onePBtn");
            p1AssetUrl = main.AddPlatformAndQualityToUrl(p1AssetUrl);
            btP1.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(p1AssetUrl);
            btP1.GetComponent<Button>().onClick.AddListener(OnClickP1);
            btP1.GetComponent<Image>().preserveAspect = true;

            btP2 = GameObject.Find("twoPBtn");
            p2AssetUrl = main.AddPlatformAndQualityToUrl(p2AssetUrl);
            btP2.GetComponent<Image>().sprite = AFAssetManager.Instance.Instantiate<Sprite>(p2AssetUrl);
            btP2.GetComponent<Button>().onClick.AddListener(OnClickP2);
            btP2.GetComponent<Image>().preserveAspect = true;

            Add(m_selectionScene);

            base.BuildState();
        }
        private void OnClickP1()
        {
            main.PlayerNumber = 1;
            main.GoToGame();
        }
        private void OnClickP2()
        {
            main.PlayerNumber = 2;
            main.GoToGame();
        }
    }
}