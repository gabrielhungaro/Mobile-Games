using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core;

namespace BateRebate
{
    public class BateRebateMain : AFEngine
    {
        public override void Initialize()
        {
            AFAssetManager.SetDirectoryOwner("BateRebate");
            //AFAssetManager.SimulatedDPI = AFAssetManager.DPI_IPAD_RETINA;
            //AFAssetManager.SimulatePlatform = AFAssetManager.EPlataform.IOS;

#if UNITY_EDITOR
            AFAssetManager.SimulatedDPI = AFAssetManager.DPI_IPHONE_4_5;
            AFAssetManager.SimulatePlatform = AFAssetManager.EPlataform.IOS;
#endif

            m_stateManager = AFObject.Create<AFStateManager>();
            m_stateManager.Initialize(new BateStateFactory());

            //AFSingleTransition tras = AFObject.Create<AFSingleTransition>();
            //SpriteRenderer sr = tras.gameObject.AddComponent<SpriteRenderer>();
            //sr.sprite = AFAssetManager.Instance.CreateSpriteFromTexture("Common/High/loadingscreen");
            //sr.transform.localScale = new Vector3(1, 1, 1);
            //m_stateManager.AddTransition(tras);
            //m_stateManager.GotoState(AState.EGameState.MENU);

            BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.MENU);
            //BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.SELECTION);
            //BateRebateMain.Instance.GetStateManger().GotoState(AState.EGameState.GAME);

            base.Initialize();
        }
    }
}