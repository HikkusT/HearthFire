using System.Collections;
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

    private GameObject scenarioProp;
    private Chunk _chunk;
    public Chunk chunk
    {
        get { return _chunk; }
        set { _chunk = value; }
    }

    void Awake()
    {
        topOffset = transform.up * 0.5f;
    }

    private void OnMouseDown()
    {
        EventManager.Instance.DispatchEvent(this);
    }

    public void SpawnScenarioProp(GameObject prop)
    {
        scenarioProp = Instantiate(prop, transform);
        scenarioProp.transform.localPosition = topOffset;
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

    public List<Voxel> GetNeighborsEightConnected()
    {
        // TODO: adapt when multiple chunks
        List<Voxel> neighbors = GetNeighborsFourConnected();

        if (chunk.IsPositionInside(x + 1, y + 1))
        {
            neighbors.Add(chunk.chunk[x + 1][y + 1]);
        }
        if (chunk.IsPositionInside(x - 1, y - 1))
        {
            neighbors.Add(chunk.chunk[x - 1][y - 1]);
        }
        if (chunk.IsPositionInside(x + 1, y - 1))
        {
            neighbors.Add(chunk.chunk[x + 1][y - 1]);
        }
        if (chunk.IsPositionInside(x - 1, y + 1))
        {
            neighbors.Add(chunk.chunk[x - 1][y + 1]);
        }

        return neighbors;
    }

    public List<Voxel> GetNeighborsFourConnected()
    {
        // TODO: adapt when multiple chunks
        List<Voxel> neighbors = new List<Voxel>();
        
        if (chunk.IsPositionInside(x, y + 1))
        {
            neighbors.Add(chunk.chunk[x][y + 1]);
        }
        if (chunk.IsPositionInside(x, y - 1))
        {
            neighbors.Add(chunk.chunk[x][y - 1]);
        }
        if (chunk.IsPositionInside(x + 1, y))
        {
            neighbors.Add(chunk.chunk[x + 1][y]);
        }
        if (chunk.IsPositionInside(x - 1, y))
        {
            neighbors.Add(chunk.chunk[x - 1][y]);
        }

        return neighbors;
    }
}
