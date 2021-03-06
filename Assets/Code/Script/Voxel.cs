﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Voxel : MonoBehaviour
{
    public int x, y;
    public Vector3 topOffset;

    public int AStarID;
    public double AStarAcumulatedValue;
    public double AStarValue;
    public bool AStarClose;
    public Voxel AStarParent;

    public GameObject scenarioProp;
    private Chunk _chunk;

    public bool hasProp = false;
    public bool hasAFourConnectecNeighborProp = false;

    public Chunk chunk
    {
        get { return _chunk; }
        set { _chunk = value; }
    }

    void Awake()
    {

    }

    private void OnMouseDown()
    {
        EventManager.Instance.DispatchEvent(this);
    }

    private void TellAllNeighborsAboutProp()
    {
        TellNeighborAboutProp(x + 1, y);
        TellNeighborAboutProp(x - 1, y);
        TellNeighborAboutProp(x, y + 1);
        TellNeighborAboutProp(x, y - 1);
    }

    private void TellNeighborAboutProp(int x, int y)
    {
        if (chunk.IsPositionInside(x, y))
        {
            chunk.GetVoxelAt(x, y).hasAFourConnectecNeighborProp = true;
        }
    }

    public void SpawnScenarioProp(GameObject prop)
    {
        scenarioProp = Instantiate(prop, transform);
        scenarioProp.transform.localPosition = topOffset;
        hasProp = true;
        if (scenarioProp.GetComponent<TreeCutScript>() != null)
            scenarioProp.GetComponent<TreeCutScript>().voxel = _chunk.GetVoxelAt(x - 1, y);
        TellAllNeighborsAboutProp();
    }

    public void DestroyScenarioProp()
    {
        Destroy(scenarioProp);
    }

    public void SpawnAt(GameObject obj) 
    {
        Instantiate(obj, transform.position + topOffset, Quaternion.identity);
    }

    public void SnapTo(GameObject obj)
    {
        obj.transform.position = transform.position + topOffset;
    }

    private void AddNeighbor(int x, int y, List<Voxel> neighbors)
    {
        if (chunk.IsPositionInside(x, y))
        {
            if (!chunk.chunk[x][y].hasProp)
            {
                neighbors.Add(chunk.chunk[x][y]);
            }
        }
    }

    private void AddNeighborWithProp(int x, int y, List<Voxel> neighbors)
    {
        if (chunk.IsPositionInside(x, y))
        {
            if (chunk.chunk[x][y].hasProp)
            {
                neighbors.Add(chunk.chunk[x][y]);
            }
        }
    }

    public List<Voxel> GetNeighbors()
    {
        if (hasAFourConnectecNeighborProp)
        {
            return GetNeighborsFourConnected();
        }
        else
        {
            return GetNeighborsEightConnected();
        }
    }

    private List<Voxel> GetNeighborsEightConnected()
    {
        // TODO: adapt when multiple chunks
        List<Voxel> neighbors = GetNeighborsFourConnected();

        AddNeighbor(x + 1, y + 1, neighbors);
        AddNeighbor(x - 1, y - 1, neighbors);
        AddNeighbor(x + 1, y - 1, neighbors);
        AddNeighbor(x - 1, y + 1, neighbors);

        return neighbors;
    }

    private List<Voxel> GetNeighborsFourConnected()
    {
        // TODO: adapt when multiple chunks
        List<Voxel> neighbors = new List<Voxel>();
        
        AddNeighbor(x, y + 1, neighbors);
        AddNeighbor(x, y - 1, neighbors);
        AddNeighbor(x + 1, y, neighbors);
        AddNeighbor(x - 1, y, neighbors);

        return neighbors;
    }

    private List<Voxel> GetNeighborsFourConnectedWithProp()
    {
        // TODO: adapt when multiple chunks
        List<Voxel> neighbors = new List<Voxel>();

        AddNeighborWithProp(x, y + 1, neighbors);
        AddNeighborWithProp(x, y - 1, neighbors);
        AddNeighborWithProp(x + 1, y, neighbors);
        AddNeighborWithProp(x - 1, y, neighbors);

        return neighbors;
    }

    public void Interact()
    {
        if (scenarioProp && scenarioProp.GetComponent<TreeCutScript>() != null)
            scenarioProp.GetComponent<TreeCutScript>().Interact();
    }

    public void InteractWithNeighbors()
    {
        foreach (Voxel neighbor in GetNeighborsFourConnectedWithProp())
            neighbor.Interact();
    }
}
