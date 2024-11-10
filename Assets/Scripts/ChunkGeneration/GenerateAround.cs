using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAround : MonoBehaviour
{
    public GameObject chunkGameobject;

    public void Generate()
    {
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(!ChunkManager.chunkPosition.Contains(transform.position+new Vector3(ChunkManager.chunkSpawnDistance * x, ChunkManager.chunkSpawnDistance * y, 0)))
                {
                    GameObject chunk= Instantiate(chunkGameobject, transform.position + new Vector3(ChunkManager.chunkSpawnDistance * x, ChunkManager.chunkSpawnDistance * y, 0), Quaternion.identity);
                    ChunkManager.chunkPosition.Add(transform.position + new Vector3(ChunkManager.chunkSpawnDistance * x, ChunkManager.chunkSpawnDistance * y, 0));
                    ChunkManager.chunks.Add(chunk);
                }
            }
        }
    }
}
