using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;

namespace com.globo.sitio.mobilegames.Balloon.Controllers
{
    public class MainClass : AFEngine
    {
        ConstantsPaths _paths;
        ConstantsSounds _soundsPaths;

        public override void Initialize()
        {
            AFAssetManager.SetDirectoryOwner("Balloon");

            m_stateManager = AFObject.Create<AFStateManager>();

            m_stateManager.Initialize(new GameStateFactory());

            _paths = ConstantsPaths.Instance();
            _soundsPaths = ConstantsSounds.Instance();

            GameObject _soundManager = new GameObject();
            _soundManager.name = "SoundManager";
            _soundManager.AddComponent<SoundManager>().Init();

            AFSingleTransition tras = AFObject.Create<AFSingleTransition>();
            SpriteRenderer sr = tras.gameObject.AddComponent<SpriteRenderer>();

            sr.transform.position = new Vector3(0, 0, 6);
            sr.sprite = AFAssetManager.Instance.CreateSpriteFromTexture("Common/High/loadingscreen");
            sr.transform.localScale = new Vector3(1, 1, 1);
            m_stateManager.AddTransition(tras);

            m_stateManager.GotoState(AState.EGameState.MENU);

            base.Initialize();
        }
    }
}
