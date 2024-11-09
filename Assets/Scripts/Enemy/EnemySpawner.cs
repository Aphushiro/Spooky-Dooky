using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject pitchFork;

    int forkBase = 8;   // Starting values

    float forkScale = 0.2f;

    int forkQueue = 0;

    int waveCount = 0;

    Vector2 camPos;
    float spawnDist = 8;

    void Start()
    {
        StartCoroutine("LetThemBreathe");
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


        //Debug.Log("Pitch: " + forkQueue + "\n" + "Musketeer: " + "none");
    }

    IEnumerator QueueNextWave()
    {
        waveCount++;
        ScaleEnemies();
        StartCoroutine("SpawnPitch");

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
        normPos.Normalize();

        Vector2 spawnPos = normPos - camPos;

        Instantiate(enemy, spawnPos * ranDist, Quaternion.identity);
    }

    IEnumerator SpawnPitch()
    {
        Debug.Log(forkQueue);
        for (int i = 0; i < forkQueue; i++)
        {
            LosSpawner(pitchFork);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
