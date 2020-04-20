using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [HideInInspector] public Voxel currentVoxel;
    Queue<Voxel> path;
    // Voxel nextVoxel;

    // Start is called before the first frame update
    void Start()
    {
        path = new Queue<Voxel>();
        EventManager.Instance.SubscribeToEvent(PlanMovement);
    }

    // Update is called once per frame
    void Update()
    {
        if (path.Count > 0)
        {
            if (path.Peek() == currentVoxel) 
            {
                path.Dequeue();
            } else {
                MoveToVoxel(path.Peek());
            }
            /*
            if (nextVoxel && nextVoxel != currentVoxel)
            {
                MoveToVoxel(nextVoxel);

                if (currentVoxel == nextVoxel && path.Count != 0)
                    nextVoxel = path.Dequeue();
            }
            */
        }
    }

    void MoveToVoxel(Voxel voxel)
    {
        transform.position = Vector3.Lerp(transform.position, voxel.transform.position, 0.5f);

        if (Vector2.Distance(transform.position, voxel.transform.position) < 0.05f)
        {
            currentVoxel = voxel;
        }
    }

    public void PlanMovement(Voxel voxel)
    {
        path = PathManager.Instance.CalculatePath(currentVoxel, voxel);
        // nextVoxel = path.Dequeue();
    }
}
