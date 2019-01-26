using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class LevelFlow : MonoBehaviour
{
    public static LevelFlow Instance;
 

    [Serializable]
    public class LevelParameters
    {
        public int MaxEnemiesOnScreen;
        public int InstantiatedEnemies;
        public int WaveSize;
        public float TimerToInstantiate;

        public LevelParameters(int _maxEnemiesInstantiated, int _enemiesInstantiated, int _waveEnemies, float _enemyTimer) {
            MaxEnemiesOnScreen = _maxEnemiesInstantiated;
            InstantiatedEnemies = _enemiesInstantiated;
            WaveSize = _waveEnemies;
            TimerToInstantiate = _enemyTimer;
        }
        public LevelParameters() { }

    }

   [SerializeField]
   private string path;
    private string jsonString;
    public LevelParameters Level; 
        // Start is called before the first frame update
        void Start()
    {
        Instance = this;
        SetPath();
        Save();
    }

    void SetPath() {

        path = Application.streamingAssetsPath + "/Level.json";
        jsonString = File.ReadAllText(path);
        Level = new LevelParameters();
        Level = JsonUtility.FromJson<LevelParameters>(jsonString);
    }

    public void Save()
    {
        string charc = JsonUtility.ToJson(Level);
        Debug.Log(charc);
        System.IO.File.WriteAllText(path, charc);

        // //Debug.Log(charc);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
