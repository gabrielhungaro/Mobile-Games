using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquelaFrameWork.Core.State
{
    public class AFDefaultStateTransition : IStateTransition
    {
        public void Initialize( AStateManager stateManager )
        {

        }

        public IStateTransition Show()
        {
            return null;
        }

        public IStateTransition Hide()
        {
            return null;
        }

        public void Begin()
        {

        }

        public void End()
        {

        }
        public void Remove()
        {

        }
    }
}
