using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core.State;
using AquelaFrameWork.Core.Factory;
using AquelaFrameWork.Core;

public class StartStateFactory : IStateFactory 
{
    public IState CreateStateByID(AState.EGameState newStateID)
    {
        switch (newStateID)
        {
            case AState.EGameState.MENU:
                StartState loginS = AFObject.Create<StartState>();
                loginS.SetStateID(AState.EGameState.MENU);
                return loginS;
        }

        return null;
    }	
}
