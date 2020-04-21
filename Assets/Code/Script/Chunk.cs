using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // TODO: Create initial i and initial j, to adapt all the positional logic when there we have many chuncks
    // [SerializeField] public int initialI;
    // [SerializeField] public int initialJ;

    [SerializeField] public int size;
    [SerializeField] Voxel voxelPrefab;
    public Voxel[][] chunk;

    public bool IsPositionInside(int i, int j)
    {
        // TODO: need to be updated once there are many chuncks
        return i >= 0 && i < size && j >= 0 && j < size;
    }

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
                voxel.chunk = this;
            }
        }

    }

    void Update()
    {
        
    }

    public Voxel GetVoxelAt(int x, int y)
    {
        if (x >= 0 && x < size && y >= 0 && y < size)
            return chunk[x][y];
        else
            return null;
    }
}
