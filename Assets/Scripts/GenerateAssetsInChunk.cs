using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAssetsInChunk : MonoBehaviour
{
    public List<GameObject> chunkAssets = new List<GameObject>();
    public List<int> weights = new List<int>();


    //chunkMatrix

    public int[,] chunkMatrix = new int[10, 10];
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                chunkMatrix[i, j] = 0;
            }
        }
        Debug.Log(chunkMatrix[0,0]);
    }
}
