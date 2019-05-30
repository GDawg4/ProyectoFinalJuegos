using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject youWin;

    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    public Wave[] waves;
    public Transform[] spawnPoints;


    public float timeBetweenWaves = 5f;
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    public SpawnState State
    {
        get { return state; }
    }

    private float waveCountdown;
    public float WaveCountdown
    {
        get { return waveCountdown; }
    }

    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

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
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            youWin.SetActive(true);
        } else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if ( searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);

            yield return new WaitForSeconds(1f/ wave.rate);
        }

        state = SpawnState.WAITING;
        yield break; 
    }

    void SpawnEnemy( Transform _enemy)
    {
        Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, spawn.position, spawn.rotation);
    }
}  

[System.Serializable]
public class Wave
{
    public string name;
    public Transform enemy;
    public int count;
    public float rate;
}
