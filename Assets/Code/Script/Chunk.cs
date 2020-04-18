using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] public int size;
    [SerializeField] Voxel voxelPrefab;
    public Voxel[][] chunk;

    void Awake()
    {
        chunk = new Voxel[size][];
        for (int i = 0; i < size; i++)
        {
            chunk[i] = new Voxel[size];
            for (int j = 0; j < size; j++)
            {
                Vector3 voxelPos = transform.position + i * Vector3.right + j * Vector3.forward;
                Voxel voxel = Instantiate(voxelPrefab, voxelPos, Quaternion.identity, transform);
                voxel.x = i;
                voxel.y = j;
                chunk[i][j] = voxel;
            }
        }

    }

    void Update()
    {
        
    }

    public Voxel GetVoxelAt(int x, int y)
    {
        return chunk[x][y];
    }
}
