#if UNITY_EDITOR
using System;
using System.Diagnostics;
using System.IO;
 
using UnityEngine;
using UnityEditor;

namespace AquelaFrameWork.Core.Window
{
    public class AFSmartFoxServerWindow : EditorWindow
    {
        static public AFSmartFoxServerWindow window;

        private string host = "localhost";
        private int port = 9933;
        private bool debugMode = true;
        private string zone = "Matheus";

        // Add menu named "My Window" to the Window menu
        [MenuItem("Window/SmartFox")]
        public static void Init()
        {
            StreamWriter stdOut = new StreamWriter(Console.OpenStandardOutput());
            stdOut.AutoFlush = true;
            Console.SetOut(stdOut);

            StreamWriter stdErr = new StreamWriter(Console.OpenStandardError());
            stdErr.AutoFlush = true;
            Console.SetError(stdErr);

            StreamReader stdIn = new StreamReader(Console.OpenStandardInput());
            Console.SetIn(stdIn);
            // Get existing open window or if none, make a new one:
            window = (AFSmartFoxServerWindow)EditorWindow.GetWindow(typeof(AFSmartFoxServerWindow));
        }

        void OnGUI()
        {
            GUILayout.Label("Host Name ");
            host = EditorGUILayout.TextField(host);

            GUILayout.Label("Port Number ");
            port = EditorGUILayout.IntField(port);

            GUILayout.Label("Zone ");
            zone = EditorGUILayout.TextField(zone);

            GUILayout.Label("Debug Mode ");
            debugMode = EditorGUILayout.Toggle( debugMode );

        }


    }
}

#endif // UNITY_EDITOR