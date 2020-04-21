using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public Voxel currentVoxel;
    Queue<Voxel> path;
    public World world;
    public float velocity = 4.0f;
    public float fixVelocity = 2.0f;
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
                MoveToVoxel(path.Peek(), velocity);
            }
        } 
        else
        {
            MoveToVoxel(currentVoxel, fixVelocity);
        }
    }

    void MoveToVoxel(Voxel voxel, float velocity)
    {
        if (Vector3.Distance(voxel.transform.position, transform.position) > 0.05f)
        {
            transform.position += Vector3.Normalize(voxel.transform.position - transform.position) * (velocity * Time.deltaTime);
        }
        else
        {
            transform.position = voxel.transform.position;
        }
    }

    public void PlanMovement(Voxel voxel)
    {
        if (!voxel.hasProp)
        {
            path = PathManager.Instance.CalculatePath(currentVoxel, voxel);
        }
    }
}
