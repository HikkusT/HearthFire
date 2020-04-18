using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] GameObject tree;
    [SerializeField] float tolerance;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopulateTrees(Chunk chunk)
    {
        foreach (Voxel[] voxelRow in chunk.chunk)
        {
            foreach (Voxel voxel in voxelRow)
            {
                float random = Random.Range(0, 1f);
                if (random > tolerance)
                {
                    voxel.SpawnScenarioProp(tree);
                }
            }
        }
    }
}
