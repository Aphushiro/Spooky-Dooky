using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemySpawner : MonoBehaviour
{
    public GameObject pitchFork;
    public GameObject musketeer;
    public GameObject torcher;

    int forkBase = 8;   // Starting values
    float forkScale = 0.2f;
    int forkQueue = 0;

    int gunBase = 1;   // Starting values
    float gunScale = 0.2f;
    int gunQueue = 0;


    int torchBase = 1;   // Starting values
    float torchScale = 0.1f;
    int torchQueue = 0;

    int waveCount = 0;
    Vector2 camPos;
    float spawnDist = 13;

    void Start()
    {
        StartCoroutine("LetThemBreathe");
    }

    private void Update()
    {
        camPos = Camera.main.transform.position;
    }

    IEnumerator LetThemBreathe()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine("QueueNextWave");
    }

    void ScaleEnemies()
    {
        float t = Time.time / 7f; // +1 every 7 second
        forkQueue = Mathf.FloorToInt(forkBase * forkScale * t);
        gunQueue = Mathf.FloorToInt(gunBase * gunScale * t);
        torchQueue = Mathf.FloorToInt(torchBase * torchScale * t);

    }

    IEnumerator QueueNextWave()
    {
        waveCount++;
        ScaleEnemies();
        StartCoroutine("SpawnPitch");
        StartCoroutine("SpawnGun");
        StartCoroutine("SpawnTorch");

        yield return new WaitForSeconds(15f);
        StartCoroutine("QueueNextWave");
    }

    void LosSpawner(GameObject enemy)
    {
        float rn = Random.Range(0, Mathf.PI * 2);
        float ranDist = spawnDist + Random.Range(0, 5);

        float x = Mathf.Sin(rn);
        float y = Mathf.Cos(rn);

        Vector2 normPos = new Vector3(x, y);

        Vector2 spawnPos = camPos + (normPos * ranDist);

        Instantiate(enemy, spawnPos, Quaternion.identity);
    }

    IEnumerator SpawnPitch()
    {
        for (int i = 0; i < forkQueue; i++)
        {
            LosSpawner(pitchFork);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator SpawnGun()
    {
        for (int i = 0; i < gunQueue; i++)
        {
            LosSpawner(musketeer);
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator SpawnTorch()
    {
        for (int i = 0; i < torchQueue; i++)
        {
            LosSpawner(torcher);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
