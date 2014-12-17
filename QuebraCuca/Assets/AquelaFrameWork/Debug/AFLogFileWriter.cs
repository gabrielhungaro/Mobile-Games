using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AFLogFileWriter
{
    protected static string m_textFilePath = "";
    private static StreamWriter m_file;

    public static void StartLog()
    {
        DateTime L_time = DateTime.Now;
        string L_format = "yyyy_MM_dd";
        m_textFilePath = "AFDebugLog_" + L_time.ToString(L_format) + ".log";

        //m_file = new StreamWriter(m_textFilePath);
    }

    public static void LogInFile(string str)
    {
        if (m_file == null)
            StartLog();

        DateTime L_time = DateTime.Now;
        string L_format = "yyyy-MM-dd hh:mm:ss";

        str = "[" + L_time.ToString(L_format) + "]" + str;

        File.AppendAllText(m_textFilePath, str);
        //m_file.WriteLine(str);
    }

    public static void EndLog()
    {
        m_file.Close();
        m_file = null;
    }

}

