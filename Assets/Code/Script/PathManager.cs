using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathManager : MonoBehaviour
{
    private static PathManager localInstance;
    public static PathManager Instance { get { return localInstance; } }
    [HideInInspector] public Chunk terrain;
    private System.Random random = new System.Random();

    private Voxel currentAStarGoalVoxel;
    private int currentAStarID;

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

    private double CalculateWalkCostBetweenAdjacentVoxelsEightConnected(Voxel initialVoxel, Voxel destinyVoxel)
    {
        if (initialVoxel == destinyVoxel)
        {
            return 0;
        }

        bool diagonal = initialVoxel.x != destinyVoxel.x && initialVoxel.y != destinyVoxel.y;

        if (diagonal)
        {
            return Math.Pow(2, 1 / 2f);
        } else
        {
            return 1;
        }
    }

    private double CalculateHeuristicCost(Voxel currentVoxel)
    {
        return Math.Pow(Math.Pow(currentVoxel.x - currentAStarGoalVoxel.x, 2) 
            + Math.Pow(currentVoxel.y - currentAStarGoalVoxel.y, 2), 1/2f);
    }

    private bool IsVoxelAStarOpen(Voxel voxel)
    {
        if (voxel.AStarID == currentAStarID)
        {
            return !voxel.AStarClose;
        }
        return false;
    }

    private bool IsVoxelAStarClose(Voxel voxel)
    {
        if (voxel.AStarID == currentAStarID)
        {
            return voxel.AStarClose;
        }
        return false;
    }

    private void SetVoxelOpen(Voxel voxel)
    {
        voxel.AStarID = currentAStarID;
        voxel.AStarClose = false;
    }

    private void SetVoxelClose(Voxel voxel)
    {
        voxel.AStarID = currentAStarID;
        voxel.AStarClose = true;
    }

    private List<Voxel> AStar(Voxel startVoxel, Voxel goalVoxel)
    {
        currentAStarID = random.Next(1, 2147483647);
        currentAStarGoalVoxel = goalVoxel;

        KeyValuePair<double, Voxel> currentPair;
        Voxel currentNeighbor;
        double calculatedAStarAcumulatedValue;
        double calculatedAStarValue;

        SortedDictionary<double, Voxel> heapQueue = new SortedDictionary<double, Voxel>();

        Voxel currentVoxel = startVoxel;
        double currentCost = CalculateHeuristicCost(currentVoxel);

        currentVoxel.AStarAcumulatedValue = 0;
        currentVoxel.AStarValue = currentCost;

        currentVoxel.AStarParent = null;
        SetVoxelOpen(currentVoxel);
        heapQueue.Add(currentVoxel.AStarValue, currentVoxel);
        if (currentVoxel == goalVoxel)
        {
            return new List<Voxel>();
        }

        bool found = false;
        while (heapQueue.Count > 0 && !found)
        {
            SortedDictionary<double, Voxel>.Enumerator enumerator = heapQueue.GetEnumerator();
            enumerator.MoveNext();
            
            currentPair = enumerator.Current;

            currentCost = currentPair.Key;
            currentVoxel = currentPair.Value;
            heapQueue.Remove(currentCost);
            
            SetVoxelClose(currentVoxel);

            List<Voxel> neighbors = currentVoxel.GetNeighbors();
            for (int index = 0; index < neighbors.Count; index++)
            {
                currentNeighbor = neighbors[index];

                if (IsVoxelAStarClose(currentNeighbor))
                {
                    continue;
                }

                calculatedAStarAcumulatedValue = currentVoxel.AStarAcumulatedValue 
                    + CalculateWalkCostBetweenAdjacentVoxelsEightConnected(currentVoxel, currentNeighbor);
                calculatedAStarValue = calculatedAStarAcumulatedValue 
                    + CalculateHeuristicCost(currentNeighbor);

                if (IsVoxelAStarOpen(currentNeighbor))
                {
                    if (calculatedAStarAcumulatedValue > currentNeighbor.AStarAcumulatedValue)
                    {
                        continue;
                    } 
                    else
                    {
                        heapQueue.Remove(currentNeighbor.AStarValue);
                    }
                }

                currentNeighbor.AStarParent = currentVoxel;
                SetVoxelOpen(currentNeighbor);

                while (heapQueue.ContainsKey(calculatedAStarValue))
                {
                    calculatedAStarValue += random.Next(1, 1000)/100000f;
                }
                currentNeighbor.AStarAcumulatedValue = calculatedAStarAcumulatedValue;
                currentNeighbor.AStarValue = calculatedAStarValue;
                heapQueue.Add(currentNeighbor.AStarValue, currentNeighbor);
                if (currentNeighbor == goalVoxel)
                {
                    currentVoxel = currentNeighbor;
                    found = true;
                    break;
                }
            }

        }

        List<Voxel> path = new List<Voxel>();
        while (currentVoxel != null)
        {
            path.Add(currentVoxel);
            currentVoxel = currentVoxel.AStarParent;
        }

        path.Reverse();

        return path;
    }

    public Queue<Voxel> CalculatePath(Voxel startVoxel, Voxel goalVoxel)
    {
        List<Voxel> path = AStar(startVoxel, goalVoxel);
        
        Queue<Voxel> queuePath = new Queue<Voxel>();
        for (int i = 0; i < path.Count; i++)
        {
            queuePath.Enqueue(path[i]);
        }

        return queuePath;





        /*
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
        */
    }
}
