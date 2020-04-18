using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] int size;
    [SerializeField] GameObject voxel;
    GameObject[][] chunk;

    void Start()
    {
        chunk = new GameObject[size][];
        for (int i = 0; i < size; i++)
        {
            chunk[i] = new GameObject[size];
            for (int j = 0; j < size; j++)
            {
                Vector3 voxelPos = transform.position + i * Vector3.right + j * Vector3.forward;
                chunk[i][j] = Instantiate(voxel, voxelPos, Quaternion.identity, transform);
            }
        }

    }

    void Update()
    {
        
    }
}
