using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public string fileName;

    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    public void SaveSceneData(GameData data)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public GameData LoadSceneData()
    {
        string path = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.LogWarning("File not found.");
            return null;
        }
    }
}
