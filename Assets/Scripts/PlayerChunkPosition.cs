using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChunkPosition : MonoBehaviour
{
    
    public GameObject currentChunk;
    public GameObject oldChunk;
    private void Start()
    {
        currentChunk = ChunkManager.chunks[0];
        oldChunk = currentChunk;
    }
    void FixedUpdate()
    {
        CheckChunk(); 
    }
    void CheckChunk()
    {
        foreach (GameObject chunk in ChunkManager.chunks)
        {
            if (Vector2.Distance(chunk.transform.position, transform.position) < Vector2.Distance(currentChunk.transform.position,transform.position))
            {
                currentChunk = chunk;
            }
            
            
        }
        if(currentChunk != oldChunk)
        {
            foreach (GameObject chunk in ChunkManager.chunks)
            {
                if(Vector2.Distance(chunk.transform.position, currentChunk.transform.position) > ChunkManager.chunkSpawnDistance*1.5f)
                {
                    Debug.Log("Chunk Moved");
                    chunk.transform.position = currentChunk.transform.position + (oldChunk.transform.position - chunk.transform.position);
                }
            }
            oldChunk = currentChunk;
        }
    }
}
