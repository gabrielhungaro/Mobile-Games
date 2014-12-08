using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;
using AquelaFrameWork.Core.State;

namespace AquelaFrameWork.Core.Factory
{
    public interface IStateFactory
    {
        IState CreateStateByID(AState.EGameState newstateID);
    }
}
