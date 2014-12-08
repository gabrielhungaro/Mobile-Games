using System.Collections.Generic;
using System.Linq;

using System;

using AquelaFrameWork.Core;

using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class AFDebug
{
     protected static bool m_initialize = false;
    protected static string m_log = "";
    protected static uint m_bufferIndex = 0;

    protected static AFDebugSettings m_settings = new AFDebugSettings() 
    {
        LogFilePath = "Assets/Logs",
        Configs = AFDebugSettings.OUTPUT_FILE | AFDebugSettings.OUTPUT_UNITY | AFDebugSettings.OUTPUT_SCREEN | AFDebugSettings.LOG_LOCAL_FILE,
        TextColor = 0xFF0000,
        MaxCharacters = 1000,
        DebugCanvas = AFObject.Create<AFDebugCanvas>("AFDebugCanvas")//Resources.Load<GameObject>("Common/AFDebugCanvas")
    };
   

    public static void SetSettings(AFDebugSettings settings)
    {
        m_settings = settings;
    }


    public static void LogError( string message )
    {
        Log(message, "[ERROR]");

        if ((m_settings.Configs & AFDebugSettings.OUTPUT_UNITY) == AFDebugSettings.OUTPUT_UNITY)
        {
            UnityEngine.Debug.Log(message);
        }
    }

    public static void LogWarning(string message)
    {
        Log(message, "[WARNING]");

        if ((m_settings.Configs & AFDebugSettings.OUTPUT_UNITY) == AFDebugSettings.OUTPUT_UNITY)
        {
            UnityEngine.Debug.Log(message);
        }
    }

    public static void Log(string message , string type = "[INFO]")
    {
        LogMessage(message, type);

        if ((m_settings.Configs & AFDebugSettings.OUTPUT_UNITY) == AFDebugSettings.OUTPUT_UNITY)
        {
            UnityEngine.Debug.Log(message);
        }
    }

    
    private static void LogMessage(string message , string type)
    {
        message = type + " " + message + Environment.NewLine;
        m_log += message;

        if ((m_settings.Configs & AFDebugSettings.OUTPUT_SCREEN) == AFDebugSettings.OUTPUT_SCREEN)
        {
            GameObject go = GameObject.Find("AFDebugText");

            if (m_log.Length > m_settings.MaxCharacters)
                go.GetComponent<Text>().text = m_log.Substring(m_log.Length - m_settings.MaxCharacters);
            else
                go.GetComponent<Text>().text = m_log;
        }

        if ((m_settings.Configs & AFDebugSettings.LOG_LOCAL_FILE) == AFDebugSettings.LOG_LOCAL_FILE)
        {
            AFLogFileWriter.LogInFile(message);
        }

        m_bufferIndex++;
    }
}
