using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAssetsInChunk : MonoBehaviour
{
    public List<GameObject> chunkAssets = new List<GameObject>();
    public List<int> weights = new List<int>();
    public int ObjectSpawnDistance;
    public int[,] chunkMatrix = new int[9, 9];

    private void Start()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                int randomValue = Random.Range(0, 100);
                chunkMatrix[i, j] = 0;
                foreach (int weight in weights)
                {
                    if (randomValue > weight)
                    {
                        chunkMatrix[i, j] = weights.IndexOf(weight);
                    }
                }
            }
        }

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (chunkMatrix[i, j] != 6)
                {
                    Vector3 jittervector = new Vector3(Random.Range(-ChunkManager.objectSpawnDistance, ChunkManager.objectSpawnDistance), Random.Range(-ChunkManager.objectSpawnDistance, ChunkManager.objectSpawnDistance), 0);
                    GameObject asset = Instantiate(chunkAssets[chunkMatrix[i, j]], transform.position + new Vector3((i - 4) * ObjectSpawnDistance, (j - 4) * ObjectSpawnDistance, 0) + jittervector, Quaternion.identity);//
                    asset.transform.parent = this.transform;
                }
            }
        }
    }
}
