using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using com.globo.sitio.mobilegames.QuebraCuca.Constants;
using com.globo.sitio.mobilegames.QuebraCuca.States;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;

namespace com.globo.sitio.mobilegames.QuebraCuca.Controllers
{
    public class MainClass : AFEngine 
    {
        ResolutionController _resolutionController;
        PathConstants _paths;
        SoundConstants _sounds;

        public override void Initialize()
        {
            AFAssetManager.SetDirectoryOwner("QuebraCuca");

            m_stateManager = AFObject.Create<AFStateManager>();

            m_stateManager.Initialize(new GameStateFactory());

            _resolutionController = ResolutionController.Instance();
            _paths = PathConstants.Instance();
            _sounds = SoundConstants.Instance();

            GameObject _soundManager = new GameObject();
            _soundManager.name = "SoundManager";
            _soundManager.AddComponent<SoundManager>().Init();
            //SoundManager.PlaySoundByName(SoundConstants.BG_SOUND);

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
