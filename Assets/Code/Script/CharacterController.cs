using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public Voxel currentVoxel;
    Queue<Voxel> path;
    public World world;
    public float velocity = 10.0f;
    private Chunk chunk;

    void Start()
    {
        path = new Queue<Voxel>();
        EventManager.Instance.SubscribeToEvent(PlanMovement);
        chunk = world.terrain;
    }

    void Update()
    {
        currentVoxel = chunk.GetVoxelAt((int)Mathf.Round(transform.position.x), (int)Mathf.Round(transform.position.z));

        if (path.Count > 0)
        {
            if (path.Peek() == currentVoxel) 
            {
                path.Dequeue();
            } else {
                MoveToVoxel(path.Peek());
            }
        } 
    }

    void MoveToVoxel(Voxel voxel)
    {
        transform.position += Vector3.Normalize(voxel.transform.position - transform.position) * (velocity * Time.deltaTime);
    }

    public void PlanMovement(Voxel voxel)
    {
        path = PathManager.Instance.CalculatePath(currentVoxel, voxel);
    }
}
