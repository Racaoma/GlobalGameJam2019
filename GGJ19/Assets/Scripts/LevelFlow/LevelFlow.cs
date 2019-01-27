using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEditor;

/*
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
}*/

public enum gameState
{
    Tutorial,
    Level1,
    Level2,
    Level3,
    Boss,
    Defeat,
    Victory
}

public class LevelFlow : Singleton<LevelFlow>
{
    [Serializable]
    public class LevelParameters
    {
        public int MaxEnemiesOnScreen;
        public int EnemiesOnScreen;
        public int WaveSize;
        public int WaveInstantiatedEnemies;
        public float TimerToInstantiate;

        public LevelParameters() { }

        public void InstantiateEnemiesBehaviour()
        {
            EnemiesOnScreen++;
            WaveInstantiatedEnemies++;
        }
    }

    //Variables
    private string _path;
    [SerializeField]
    private string fileName;
    private string _jsonString;
    public LevelParameters Level;
    private float _timer;
    private gameState currentGameState;

    //Lists Enemies & Positions
    public List<InstantiateBehaviour> Enemies = new List<InstantiateBehaviour>();
    public List<Transform> Positions = new List<Transform>();

    // Start is called before the first frame update
    private void Start()
    {
        SetPath();
        //Save();
        Read();
        currentGameState = gameState.Level1;
    }

    private void SetPath()
    {
        _path = Application.streamingAssetsPath +"/"+ fileName;
    }

    private void Read()
    {
        _jsonString = File.ReadAllText(_path);
        Level = new LevelParameters();
        Level = JsonUtility.FromJson<LevelParameters>(_jsonString);
    }

    public void Save()
    {
        string charc = JsonUtility.ToJson(Level);
        Debug.Log(charc);
        System.IO.File.WriteAllText(_path, charc);
        //Debug.Log(charc);
    }

    public void EnemyDeath()
    {
        Level.EnemiesOnScreen--;
    }

    protected void InstantiateEnemy()
    {
        if(Level.WaveInstantiatedEnemies < Level.WaveSize)
        {
            if (Level.EnemiesOnScreen < Level.MaxEnemiesOnScreen)
            {
                if (_timer >= Level.TimerToInstantiate)
                {
                    int rand = UnityEngine.Random.Range(0, 10);
                    for (int i = 0; i < Enemies.Count; i++)
                    {
                        if (rand >= Enemies[i].Min && rand <= Enemies[i].Max)
                        {
                            EnemyPool.Instance.spawnEnemy(Positions[UnityEngine.Random.Range(0, 2)].transform.position, Enemies[i].Enemy);
                            i = Enemies.Count;
                            Level.InstantiateEnemiesBehaviour();
                        }
                    }

                    _timer = 0;
                }
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
