using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "SaveSystemSO", menuName = "SO/SaveSystem/Manager")]
public class SaveSystemSO : ScriptableObject
{
    private string directery = "/SaveData";
    private string fileName = "MyData.txt";

    public SaveData GameData;

    [ContextMenu("Save")]
    public void Save()
    {
        string dir = Application.persistentDataPath + directery;
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        string json = JsonUtility.ToJson(GameData);
        File.WriteAllText(dir + fileName, json);
        Debug.Log("------------------Saving------------------");
    }

    [ContextMenu("Load")]
    public void Load()
    {
        string fullPath = Application.persistentDataPath + directery + fileName;

        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            GameData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("------------------Loading------------------");
        }
    }

    void DeleteAllSaveData()
    {
        GameData = new SaveData();
        Save();
    }
}