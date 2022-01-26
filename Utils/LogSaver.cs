using System.IO;
using UnityEngine;

namespace USCSL.Utils
{
    public class LogSaver : MonoBehaviour
    {
        [SerializeField] private string filePath = "./Logs/Log";

        private string logContent;
        
        void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }
        
        private void Log(string condition, string stacktrace, LogType type)
        {
            logContent += condition + '\n';
            if (type == LogType.Error || type == LogType.Exception)
                logContent += stacktrace + '\n';

            string fullPath;
            if (filePath[0] == '.')
            {
                fullPath = Application.dataPath + filePath.Substring(1, filePath.Length - 1);
            }
            else
            {
                if (filePath[0] != '/')
                {
                    filePath = "/" + filePath;
                }
                fullPath = Application.dataPath + filePath;
            }
            
            if (fullPath.Contains(".txt"))
            {
                fullPath = fullPath.Substring(0, filePath.LastIndexOf('.') - 1);
            }
            
            fullPath = fullPath + ".txt";
            File.WriteAllText(fullPath, logContent);
        }
        
    }
}