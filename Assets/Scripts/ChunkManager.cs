using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static List<GameObject> chunks = new List<GameObject>();
    public static List<Vector3> chunkPosition = new List<Vector3>();
    public List<GameObject> chunksVisable = new List<GameObject>();
    public List<Vector3> chunkPositionVisable = new List<Vector3>();



    public static int chunkSpawnDistance;
    public static int objectSpawnDistance;
    public int chunkSpawnDistanceEditable;
    public int objectSpawnDistanceEditable;
    public GameObject firstChunk;
    private void Start()
    {
        chunkSpawnDistance = chunkSpawnDistanceEditable;
        objectSpawnDistance = objectSpawnDistanceEditable;

        firstChunk.GetComponent<GenerateAround>().Generate();
        ChunkManager.chunks.Add(firstChunk);
        ChunkManager.chunkPosition.Add(firstChunk.transform.position);
    }

    private void FixedUpdate()
    {
        chunkPositionVisable = chunkPosition;
        chunksVisable = chunks;
    }
}
