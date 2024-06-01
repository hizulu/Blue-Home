using System.IO;
using UnityEngine;
public static class JsonUtils
{
    public static void SaveToJson<T>(T objectToSave, string filePath){
        string json = JsonUtility.ToJson(objectToSave, true);
        File.WriteAllText(filePath, json);
    }
    public static T LoadFromJson<T>(string filePath){
        if(File.Exists(filePath)){
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }
        else{
            Debug.LogError("No existe el archivo");
            return default;
        }
    }
}
