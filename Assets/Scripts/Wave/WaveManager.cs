using UnityEngine;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int firstWaveEnemyCount = 3;
    [SerializeField] private int enemyIncreasePerWave = 2;
    [SerializeField] private int maxWave = 5;
    [SerializeField] private float spawnInterval = 1.2f;
    [SerializeField] private float restTime = 3.0f;

    private WaveState currentState = WaveState.Ready;
    private int currentWave = 0;
    private int spawnedCount = 0;
    private int enemyCountForThisWave = 0;
    private float spawnTimer = 0.0f;
    private float restTimer = 0.0f;

    private List<EnemyHealth> aliveEnemies = new List<EnemyHealth>();

    public WaveState CurrentState
    {
        get { return currentState; }
    }

    public int CurrentWave
    {
        get { return currentWave; }
    }

    public int MaxWave
    {
        get { return maxWave; }
    }

    public int EnemyCountForThisWave
    {
        get { return enemyCountForThisWave; }
    }

    public int SpawnedCount
    {
        get { return spawnedCount; }
    }

    public int AliveCount
    {
        get { return aliveEnemies.Count; }
    }

    public float SpawnTimer
    {
        get { return spawnTimer; }
    }

    public float RestTimer
    {
        get { return restTimer; }
    }

    void ChangeState(WaveState nextState)
    {
        currentState = nextState;
    }

    void StartResting()
    {
        restTimer = restTime;
        ChangeState(WaveState.Resting);
    }

    void UpdateResting()
    {
        restTimer -= Time.deltaTime;
        if(restTimer <= 0.0f)
        {
            StartNextWave();
        }
    }

    void SpawnOneEnemy()
    {
        GameObject enemyObject = enemySpawner.SpawnEnemy();
        if(enemyObject == null)
        {
            return;
        }

        EnemyHealth enemyHealth = enemyObject.GetComponent<EnemyHealth>();
        if(enemyHealth != null)
        {
            aliveEnemies.Add(enemyHealth);
        }

        spawnedCount++;
    }

    void UpdateAliveEnemies()
    {
        for(int i=aliveEnemies.Count-1; i>=0; --i)
        {
            if (aliveEnemies[i] == null || aliveEnemies[i].IsDead() == true)
            {
                aliveEnemies.RemoveAt(i);
            }
        }

        if(aliveEnemies.Count == 0)
        {
            StartResting();
        }
    }

    void UpdateSpawning()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer > 0.0f)
        {
            return;
        }

        spawnTimer = spawnInterval;
        SpawnOneEnemy();

        if(spawnedCount >= enemyCountForThisWave)
        {
            ChangeState(WaveState.WaitingClear);
        }
    }

    void StartNextWave()
    {
        currentWave++;

        if(currentWave > maxWave)
        {
            ChangeState(WaveState.Finished);
            return;
        }

        enemyCountForThisWave = firstWaveEnemyCount + ((currentWave - 1) * enemyIncreasePerWave);

        spawnedCount = 0;
        spawnTimer = 0.0f;
        aliveEnemies.Clear();
        ChangeState(WaveState.Spawning);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == WaveState.Spawning)
        {
            UpdateSpawning();
        }
        else if(currentState == WaveState.WaitingClear)
        {
            UpdateAliveEnemies();
        }
        else if(currentState == WaveState.Resting)
        {
            UpdateResting();
        }
    }
}
