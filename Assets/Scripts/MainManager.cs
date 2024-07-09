using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MainManager Instance {  get; private set; }

    public int[] Resources = new int[3];
    public List<float[]> Locations;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;

        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Locations = new List<float[]>();
    }

    [System.Serializable]
    class SaveData
    {
        public List<float[]> Locations;
        public int[] Resources = new int[3];
    }

    public void SaveFiles()
    {
        SaveData data = new SaveData();
        data.Locations = Locations;
        data.Resources = Resources;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/SaveFile.json", json);
       
    }

    public void LoadFiles()
    {
        string path = Application.persistentDataPath + "/SaveFile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Locations = data.Locations;
            Resources = data.Resources;
        }
    }


    
}
