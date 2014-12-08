using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AquelaFrameWork.Core;

namespace AquelaFrameWork.Input
{
    public class AFInput : ASingleton<AFInput>
    {
        void Awake()
        {
            gameObject.transform.parent = AFEngine.Instance.gameObject.transform;
        }
    }
}
