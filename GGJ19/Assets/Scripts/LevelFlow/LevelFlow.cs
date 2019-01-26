using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEditor;


[CustomEditor(typeof(LevelFlow))]
public class LevelFlowEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelFlow myScript = (LevelFlow)target;
        if (GUILayout.Button("Save Object"))
        {
            LevelFlow.Instance.Save();
        }
    }
}

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

        public void InstantiateEnemy() { }

    }

    private string _path;
    private string _jsonString;
    public LevelParameters Level;

    private float _timer;

    public List<InstantiateBehaviour> Enemies = new List<InstantiateBehaviour>();

    public List<Transform> Positions = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SetPath();
       // Save();
        Read();
    }

    void SetPath() {

        _path = Application.streamingAssetsPath + "/Level.json";
       
    }

     void Read() {
        _jsonString = File.ReadAllText(_path);
        Level = new LevelParameters();
        Level = JsonUtility.FromJson<LevelParameters>(_jsonString);
    }


    public void Save()
    {
        string charc = JsonUtility.ToJson(Level);
        Debug.Log(charc);
        System.IO.File.WriteAllText(_path, charc);

        // //Debug.Log(charc);
    }


    public void InstantiateEnemy() {
        if (Level.InstantiatedEnemies <= Level.MaxEnemiesOnScreen)
        {
            if (_timer >= Level.TimerToInstantiate)
            {
                int rand = UnityEngine.Random.Range(0, 10);
                Debug.Log("rand " + rand);

                for (int i = 0; i < Enemies.Count; i++)
                {
                    if (rand >= Enemies[i].Min && rand <=Enemies[i].Max)
                    {
                        int pos = UnityEngine.Random.Range(0, 2);

                        Instantiate(Enemies[i].Enemy, Positions[pos].transform.position, Positions[pos].transform.rotation);
                        i = Enemies.Count;
                    }
                }

                _timer = 0;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        InstantiateEnemy();       
    }
}
