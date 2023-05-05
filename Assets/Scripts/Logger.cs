using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Logger : MonoBehaviour
{
    public static void Log(string text, [CallerFilePath] string file = "")
    {
        Debug.Log($"[{Path.GetFileNameWithoutExtension(file)}]: {text}");
    }
}
