using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using LitJson;
using Object = System.Object;


[CreateAssetMenu(fileName = "DataSetup", menuName = "Inventory/List", order = 1)]
public class ScriptableData : ScriptableObject
{
    public List<Color> GameColors = new List<Color>();
    public int TimeGame;

    private void OnDisable()
    {
        if (File.Exists(Application.dataPath + "/Resources/SettingsGame.json"))
        {
            Debug.Log("Файл настроек присутсвует в - "+ Application.dataPath + "/Resources/SettingsGame.json");
            Debug.Log("Начинаю читать настройки из файла");
            ReadFromFileSettings();
        }
      
    }

    /// <summary>
    /// Метод для проверки файла и последующего сохранения
    /// </summary>
    void SaveData()
    {
        if (File.Exists(Application.dataPath + "/Resources/SettingsGame.json"))
        {
            Debug.Log("ФАЙЛ ЕСТЬ");
        }
        else
        {
            File.WriteAllText(Application.dataPath + "/Resources/SettingsGame.json", SaveToString());
        }
    }
    
    /// <summary>
    /// Сохраняет в JSON
    /// </summary>
    /// <returns></returns>
    string SaveToString()
    {
        return JsonUtility.ToJson(this);
    }

    /// <summary>
    /// Читаем Данный из JSON файла и заполняем ScriptableObject
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    void ReadFromFileSettings()
    {
        GameColors.Clear();
        
        string data = File.ReadAllText(Application.dataPath + "/Resources/SettingsGame.json");
        JsonData jsonData = JsonMapper.ToObject(data);
        
        TimeGame = Int32.Parse(jsonData["TimeGame"].ToString());
        Debug.Log("Time - " + TimeGame);
        var a = jsonData["GameColors"];
        
        switch (a.GetJsonType())
        {
            case JsonType.Array:
                for (int i = 0; i < a.Count; i++)
                {
                    if (a[i].IsObject)
                    {
                      GameColors.Add(SetNewColor(a[i]));
                    }
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    /// Парсит обратнов Color из litJSON
    /// </summary>
    /// <param name="jsonData"></param>
    /// <returns></returns>
    Color SetNewColor(JsonData jsonData)
    {
        return new Color(
            float.Parse(jsonData["r"].ToString()),
            float.Parse(jsonData["g"].ToString()),
            float.Parse(jsonData["b"].ToString())
        );
    }
    
}