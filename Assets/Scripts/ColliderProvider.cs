using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderProvider : MonoBehaviour
{
    private Dictionary<Collider, ObstacleController> obstaclesPairs;
    private void Awake()
    {
        obstaclesPairs = new Dictionary<Collider, ObstacleController>();
    }

    public void RegisterObstacle(Collider col, ObstacleController obstacle)
    {
        obstaclesPairs.Add(col, obstacle);
    }

    internal bool TryGetObstacle(Collider collider, out ObstacleController obstacle)
    {
        if (obstaclesPairs.ContainsKey(collider))
        {
            obstacle = obstaclesPairs[collider];
            return true;
        }

        obstacle = null;
        return false;
    }
}
