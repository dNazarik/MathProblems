using System;
using UnityEngine;

namespace Core.Utils
{
    public class JSONUtility
    {
        public static string ToJson<T>(T data)
        {
            var json = JsonUtility.ToJson(data);
            return json;
        }

        public static T FromJson<T>(string json)
        {
            T data;

            try
            {
                data = JsonUtility.FromJson<T>(json);
            }
            catch (Exception e)
            {
                Debug.Log(e);

                return default;
            }

            return data;
        }
    }
}