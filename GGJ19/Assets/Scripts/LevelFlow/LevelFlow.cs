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
    Interwave,
    Wave1,
    Wave2,
    Wave3,
    Boss,
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
        public float TimeToComplete;

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
    public gameState currentGameState;
    private gameState nextGameState;

    //Lists Enemies & Positions
    public List<InstantiateBehaviour> Enemies = new List<InstantiateBehaviour>();
    public List<Transform> Positions = new List<Transform>();

    // Start is called before the first frame update
    private void Start()
    {
       // currentGameState = gameState.Tutorial;
       // TutorialFlow.Instance.startTutorial();
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

    public void setNextGameState(gameState nextState)
    {
        nextGameState = nextState;
        if (nextGameState == gameState.Wave1)
        {
            fileName = "Level1.json";
            GameEvents.GameState.StartGame();
        }
        else if (nextGameState == gameState.Wave2)
        {
            fileName = "Level2.json";
        }
        else if (nextGameState == gameState.Wave3)
        {
            fileName = "Level3.json";
        }

        SetPath();
        Read();
    }

    private void loseGame()
    {
        currentGameState = gameState.Interwave;
        setNextGameState(gameState.Wave1);
        _timer = 4f;

        foreach (GameObject obj in EnemyPool.Instance.activeEnemies)
        {
            obj.GetComponent<Enemy>().killEnemy();
        }
    }

    public void startWave()
    {
        _timer = 0f;
        EnemyPool.Instance.cleanUpEnemies();
        currentGameState = nextGameState;
    }

    private void winWave()
    {
        GameEvents.GameState.WaveWon.SafeInvoke();
        currentGameState = gameState.Interwave;
        _timer = 4f;

        if(currentGameState == gameState.Wave1) setNextGameState(gameState.Wave2);
        else if (currentGameState == gameState.Wave2) setNextGameState(gameState.Wave3);
        else if(currentGameState == gameState.Wave3) setNextGameState(gameState.Boss);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState == gameState.Interwave)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f) startWave();
        }
        else
        {
            if (LudicController.Instance.ludicMeter <= 0) loseGame();
            else if (Level.WaveInstantiatedEnemies == Level.WaveSize && Level.EnemiesOnScreen == 0) winWave();
            else if (_timer >= (Level.TimeToComplete * 60f)) startWave();
            else
            {
                _timer += Time.deltaTime;
                InstantiateEnemy();
            }
        }
    }
}
