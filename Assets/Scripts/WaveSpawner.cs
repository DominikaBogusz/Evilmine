using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    public SpawnState State { get;  private set; }

    [System.Serializable]
    public class Wave
    {
        public int count;
        public float timeSpace;
    }

    [SerializeField] private Wave[] waves;
    public int NextWave { get; private set; }
    [SerializeField] private float timeBetweenWaves;
    public float WaveCountdown { get; private set; }

    [SerializeField] private Transform[] spawnPoints;  

    [SerializeField] WaveUI waveUI;

    void Start()
    {
        State = SpawnState.COUNTING;
        NextWave = 1;
        WaveCountdown = timeBetweenWaves;
        waveUI.waveSpawner = GetComponent<WaveSpawner>();
    }

    void Update()
    {
        if (State == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            return;
        }

        if (WaveCountdown <= 0)
        {
            if (State != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[NextWave - 1]));
            }
        }
        else
        {
            WaveCountdown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        State = SpawnState.COUNTING;
        WaveCountdown = timeBetweenWaves;

        if (NextWave > waves.Length - 1)
        {
            GetComponent<BattleArea>().StopBattle();
        }
        else
        {
            NextWave++;
        }
    }

    private bool EnemyIsAlive()
    {
        if (GetComponent<BattleArea>().EnemyCount == 0)
        {
            return false;
        }
        return true;
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        State = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(wave.timeSpace);
        }

        State = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy enemy = point.GetComponentInParent<EnemySpawner>().Spawn(point.transform);
        GetComponent<BattleArea>().IncreaseEnemyCount(enemy);
    }
}
