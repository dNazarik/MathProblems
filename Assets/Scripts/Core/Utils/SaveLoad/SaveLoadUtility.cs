using System;
using System.IO;
using UnityEngine;

namespace Core.Utils.SaveLoad
{
    public class SaveLoadUtility
    {
        private const string LoadingErrorMessageFormat =
            "Something went wrong during leaderboard loading, gonna create a new one. Error: {0}";

        public static void SaveDataToFile<T>(T data, string filePath)
        {
            var json = JSONUtility.ToJson(data);

            File.WriteAllText(filePath, json);
        }

        public static T LoadDataFromFile<T>(string filePath)
        {
            if (File.Exists(filePath))
            {
                T data;

                try
                {
                    var json = File.ReadAllText(filePath);

                    data = JSONUtility.FromJson<T>(json);
                }
                catch (Exception e)
                {
                    Debug.Log(string.Format(LoadingErrorMessageFormat, e.Message));

                    return default;
                }

                return data;
            }

            return default;
        }
    }
}