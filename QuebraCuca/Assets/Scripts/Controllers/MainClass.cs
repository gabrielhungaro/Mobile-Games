using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Constants;

using States;
using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;

namespace Controllers
{
    public class MainClass : AFEngine 
    {
        ResolutionController _resolutionController;
        PathConstants _paths;
        SoundConstants _sounds;

        public override void Initialize()
        {
            m_stateManager = AFObject.Create<AFStateManager>();

            m_stateManager.Initialize( new GameStateFactory() );

            m_stateManager.GotoState( AState.EGameState.MENU );

            _resolutionController = ResolutionController.Instance();
            _paths = PathConstants.Instance();
            _sounds = SoundConstants.Instance();

            this.gameObject.AddComponent<SoundManager>().Init();
            //SoundManager.PlaySoundByName(SoundConstants.BG_SOUND);

            base.Initialize();
        }
    }
}
