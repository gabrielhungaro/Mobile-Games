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
    protected static bool isDebug = true;

    private static AFDebugCanvas m_debugCanvas = AFObject.Create<AFDebugCanvas>("AFDebugCanvas");
    public static AFDebugCanvas DebugCanvas
    {
        get
        {
            return m_debugCanvas;
        }
    }

    private static AFDebugSettings m_settings = new AFDebugSettings()
        {
            LogFilePath = "Assets/Logs",
            Configs = AFDebugSettings.OUTPUT_UNITY | AFDebugSettings.OUTPUT_SCREEN,
            TextColor = Color.red,
            MaxCharacters = 1000,
        };



    public static void SetLogPath( string path )
    {

    }

    public static void SetScreenTextColor(Color color)
    {
        m_settings.TextColor = color;

        UpdateCanvas();
    }

    private static void UpdateCanvas()
    {
        m_debugCanvas.GettextFild().color = m_settings.TextColor;
    }

    public static void SetConfigs( uint flags )
    {
        m_settings.Configs = flags;
    }


    public static void LogError( string message )
    {
        Log(message, "[ERROR]");

        if ((m_settings.Configs & AFDebugSettings.OUTPUT_UNITY) == AFDebugSettings.OUTPUT_UNITY)
        {
            UnityEngine.Debug.LogError(message);
        }
    }

    public static void LogWarning(string message)
    {
        Log(message, "[WARNING]");

        if ((m_settings.Configs & AFDebugSettings.OUTPUT_UNITY) == AFDebugSettings.OUTPUT_UNITY)
        {
            UnityEngine.Debug.LogWarning(message);
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
        if (isDebug)
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
}
