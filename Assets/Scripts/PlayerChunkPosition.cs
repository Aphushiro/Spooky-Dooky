using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChunkPosition : MonoBehaviour
{
    
    public GameObject currentChunk;
    public GameObject oldChunk;
    float time = 0;
    private void Start()
    {
        currentChunk = ChunkManager.chunks[0];
        oldChunk = currentChunk;
    }
    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            CheckChunk();
            time = 0;
        }
    }
    void CheckChunk()
    {
        foreach (GameObject chunk in ChunkManager.chunks)
        {
            if (Vector2.Distance(chunk.transform.position, transform.position) < Vector2.Distance(currentChunk.transform.position,transform.position))
            {
                currentChunk = chunk;
            }
            if (Vector2.Distance(chunk.transform.position, transform.position) > ChunkManager.chunkSpawnDistance*5)
            {
                ChunkManager.chunks.Remove(chunk);
                ChunkManager.chunkPosition.Remove(chunk.transform.position);
                Destroy(chunk);
            }
        }
        if(currentChunk != oldChunk)
        {
            currentChunk.GetComponent<GenerateAround>().Generate();
            oldChunk = currentChunk;
        }
    }
}
