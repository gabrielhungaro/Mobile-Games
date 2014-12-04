#if UNITY_EDITOR

using System;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

using AquelaFrameWork.Core.Window;

namespace AquelaFrameWork.Core
{
    public class AquelaFrameWork : Editor
    {
        
        [MenuItem("AquelaFrameWork/SmartFoxServer")]
        static void ConfigSmartFoxServer()
        {
            AFSmartFoxServerWindow.Init();
        }

        [MenuItem("AquelaFrameWork/Settings")]
        static void ProjectSettings()
        {
            AFFoulderStructWindow.Init();
        }
    }
}

#endif