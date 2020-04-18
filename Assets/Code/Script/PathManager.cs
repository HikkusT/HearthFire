using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private static PathManager localInstance;
    public static PathManager Instance { get { return localInstance; } }
    [HideInInspector] public Chunk terrain;

    private void Awake()
    {
        if (localInstance != null && localInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            localInstance = this;
        }
    }

    public Queue<Voxel> CalculatePath(Voxel start, Voxel end)
    {
        Queue<Voxel> path = new Queue<Voxel>();
        int xDelta = (end.x - start.x);
        int xDir = xDelta / Mathf.Abs(xDelta);
        int yDelta = (end.y - start.y);
        int yDir = yDelta / Mathf.Abs(yDelta);

        for (int i = 1; i <= Mathf.Abs(xDelta); i++)
            path.Enqueue(terrain.GetVoxelAt(start.x + i * xDir, start.y));
        for (int i = 1; i <= Mathf.Abs(yDelta); i++)
            path.Enqueue(terrain.GetVoxelAt(end.x, start.y + i * yDir));

        return path;
    }
}
