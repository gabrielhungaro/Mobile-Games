using UnityEngine;
using System.Collections;

using AquelaFrameWork.Core;

public class AFDebugSettings
{
    public const uint OUTPUT_SCREEN = 0x1;
    public const uint OUTPUT_UNITY = 0x2;
    public const uint OUTPUT_FILE = 0x4;

    public const uint LOG_LOCAL_FILE = 0x8;


    public bool HasSeparator { get; set; }
    public int MaxCharacters { get; set; }
    public uint TextColor { get; set; }
    public uint Configs { get; set; }
    public string LogFilePath { get; set; }
    public AFObject DebugCanvas { get; set; }

}
