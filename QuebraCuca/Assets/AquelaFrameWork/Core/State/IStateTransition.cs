using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquelaFrameWork.Core.State
{
    public interface IStateTransition
    {
        IStateTransition Show();
        IStateTransition Hide();

        void Initialize( AStateManager stateManager );

        void Begin();
        void End();
        void Remove();
    }
}
