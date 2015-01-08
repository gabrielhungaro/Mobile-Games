using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core.State;

namespace BateRebate
{
    public class BateStateFactory : IStateFactory
    {
        public IState CreateStateByID(AState.EGameState newstateID)
        {
            switch (newstateID)
            {
                case AState.EGameState.MENU:
                    return AFObject.Create<BateMenuState>();
                case AState.EGameState.SELECTION:
                    return AFObject.Create<BateSelectionState>();
                case AState.EGameState.GAME:
                    return AFObject.Create<BateGameState>();
            }
            return null;
        }
    }
}