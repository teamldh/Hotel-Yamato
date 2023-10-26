using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public string fileName;
    public string currentAccountID;
    private GameData gameData;

    public bool HasSaveData { get; private set;}

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
        string path;
        if(string.IsNullOrEmpty(currentAccountID))
        {
            path = Application.persistentDataPath + "/" + fileName;
        }
        else
        {
            path = Application.persistentDataPath + "/" + currentAccountID + ".data";
        }

        //if the file exists, delete it
        if(File.Exists(path))
        {
            File.Delete(path);
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
        HasSaveData = true;
    }

    /// <summary>
    /// Read the save data from the local file, this is used when the player is not logged in
    /// </summary>
    /// <returns></returns>
    public bool LoadSceneDataFromLocal()
    {
        //gameData = null;
        string path = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(json);
            HasSaveData = true;
            return true;
        }
        else
        {
            Debug.LogWarning("File not found.");
            HasSaveData = false;
            return false;
        }
    }

    /// <summary>
    /// Read the save data from the local file based on the account id, this is used when the player is logged in
    /// </summary>
    /// <param name="id"></param>
    public bool LoadSceneDataFromAccount(string id)
    {
        //gameData = null;
        string path = Application.persistentDataPath + "/" + id + ".data";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            gameData = JsonUtility.FromJson<GameData>(json);
            HasSaveData = true;
            return true;
        }
        else
        {
            Debug.LogWarning("File not found.");
            HasSaveData = false;
            return false;
        }
    }

    /// <summary>
    /// Delete the save data from the local file when new game is started
    /// </summary>
    public void DeleteSceneData()
    {
        string path;
        if(string.IsNullOrEmpty(currentAccountID))
        {
            path = Application.persistentDataPath + "/" + fileName;
        }
        else
        {
            path = Application.persistentDataPath + "/" + currentAccountID + ".data";
        }

        //if the file exists, delete it
        if(File.Exists(path))
        {
            File.Delete(path);
            HasSaveData = false;
        }
    }


    public GameData LoadSceneData()
    {
        string path;
        if(string.IsNullOrEmpty(currentAccountID))
        {
            path = Application.persistentDataPath + "/" + fileName;
        }
        else
        {
            path = Application.persistentDataPath + "/" + currentAccountID + ".data";
        }

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
