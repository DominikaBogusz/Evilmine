using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    private SpawnState state = SpawnState.COUNTING;

    [System.Serializable]
    public class Wave
    {
        public int count;
        public float timeSpace;
    }

    [SerializeField] private Wave[] waves;
    private int nextWave = 0;
    [SerializeField] private float timeBetweenWaves;
    private float waveCountdown;

    [SerializeField] private Transform[] spawnPoints;  

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            return;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            GetComponent<BattleArea>().StopBattle();
        }
        else
        {
            nextWave++;
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
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(wave.timeSpace);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Enemy enemy = point.GetComponentInParent<EnemySpawner>().Spawn(point.transform);
        GetComponent<BattleArea>().IncreaseEnemyCount(enemy);
    }
}
