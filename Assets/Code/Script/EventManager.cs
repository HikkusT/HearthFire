using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager localInstance;
    public static EventManager Instance { get { return localInstance; } }

    public delegate void OnClickVoxelEvent(Voxel voxel);
    private event OnClickVoxelEvent onClickVoxelEvent;

    public delegate void UpdateInteractEvent(object obj, bool newState);
    private event UpdateInteractEvent updateInteractEvent;


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

    public void DispatchEvent(Voxel voxel)
    {
        foreach (OnClickVoxelEvent runEvent in onClickVoxelEvent.GetInvocationList())
        { 
            runEvent.Invoke(voxel);
        }
    }

    public void DispatchEvent(object obj, bool newState)
    {
        foreach (UpdateInteractEvent runEvent in updateInteractEvent.GetInvocationList())
        {
            runEvent.Invoke(obj, newState);
        }
    }

    public void SubscribeToEvent(OnClickVoxelEvent callBack)
    {
        onClickVoxelEvent += callBack;
    }

    public void SubscribeToEvent(UpdateInteractEvent callBack)
    {
        updateInteractEvent += callBack;
    }
}
