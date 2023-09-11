using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public string sceneName;
    public int sceneIndex;

    public GameData(string name, int index)
    {
        sceneName = name;
        sceneIndex = index;
    }
}
