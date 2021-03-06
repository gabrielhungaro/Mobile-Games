﻿using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;


public class StartSceneMain : AFEngine 
{

    public override void Initialize()
    {
        //AFDebug.SetConfigs( AFDebugSettings.OUTPUT_SCREEN | AFDebugSettings.OUTPUT_UNITY );

        AFAssetManager.SetDirectoryOwner("");

        m_stateManager = AFObject.Create<AFStateManager>();
        m_stateManager.Initialize(new StartStateFactory());
        m_stateManager.GotoState(AState.EGameState.MENU);

        base.Initialize();

        AFSingleTransition tras = AFObject.Create<AFSingleTransition>();
        SpriteRenderer sr = tras.gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = AFAssetManager.Instance.CreateSpriteFromTexture("Common/High/loadingscreen");
        sr.transform.localScale = new Vector3(1, 1, 1);
        m_stateManager.AddTransition(tras);
    }
}
