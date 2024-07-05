using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<List<SingleTile>> grid => DI.di.gridGenerator.grid;

    private float moveSpeed = 5f;

    private int currX = 0;
    private int currY = 0;

    private void Awake() => SubscribeEvents();

    private void Start()
    {
        currX = 0;
        currY = 0;

        Debug.Log($"Grid Dimentions are ({grid.Count},{grid[0].Count})");
    }

    private void SubscribeEvents()
    {
        EventsModel.TILE_CLICKED += OnTileClicked;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.TILE_CLICKED -= OnTileClicked;
    }

    private void OnTileClicked(int x, int y)
    {
        Debug.Log($"Tile clicked {x} {y}");
        if (x == currX && y == currY)
        {
            Debug.Log($"Same tile clicked");
            return;
        }

        var path = FindPath(x, y);
        if (path.Count == 0)
        {
            Debug.Log($"Cannot Reach ({x},{y})");
            return;
        }

        ////////////////////////////////////////////////////////////////
        ///For Debug
        string s = "";
        foreach (var dd in path)
        {
            s += $"({dd.gridX},{dd.gridY})\n";
        }
        Debug.Log($"path is \n {s}");
        ////////////////////////////////////////////////////////////////
        EventsModel.PLAYER_MOVEMNT_STARTED?.Invoke();
        StartCoroutine(MoveToardsTarget(path));
    }

    private IEnumerator MoveToardsTarget(List<SingleTile> path)
    {
        foreach (SingleTile tile in path)
        {
            yield return StartCoroutine(MoveToPosition(new Vector3(tile.transform.position.x, transform.position.y, tile.transform.position.z)));
        }
        currX = path[path.Count - 1].gridX;
        currY = path[path.Count - 1].gridY;
        Debug.Log($"Player moved to ({currX},{currY})");
        EventsModel.PLAYER_MOVEMNT_COMPLETED?.Invoke();
    }

    private List<SingleTile> FindPath(int x, int y)
    {
        SingleTile start = grid[currX][currY];
        SingleTile end = grid[x][y];

        Queue<SingleTile> queue = new Queue<SingleTile>();
        Dictionary<SingleTile, SingleTile> cameFrom = new Dictionary<SingleTile, SingleTile>();
        queue.Enqueue(start);
        cameFrom[start] = null;

        while (queue.Count > 0)
        {
            SingleTile current = queue.Dequeue();

            if (current == end) break;

            foreach (var neighbor in GetNeighbors(current))
            {
                if (!grid[neighbor.gridX][neighbor.gridY].isObstacle && !cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = current;
                }
            }
        }

        List<SingleTile> path = new List<SingleTile>();
        SingleTile currentTile = end;

        while (currentTile != null)
        {
            path.Add(currentTile);
            currentTile = cameFrom[currentTile];
        }

        path.Reverse();
        return path;
    }

    private List<SingleTile> GetNeighbors(SingleTile currentTile)
    {
        List<SingleTile> neighbors = new List<SingleTile>();
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0) continue;
                int nex = currentTile.gridX + i;
                int ney = currentTile.gridY + j;
                if (nex >= 0 && nex < grid[0].Count && ney >= 0 && ney < grid.Count) // check if the corrdinates are within the grid
                {
                    Debug.Log($"Neighbour ({nex}, {ney})");
                    neighbors.Add(grid[nex][ney]);
                }
            }
        }
        return neighbors;
    }

    private float GetDistance(SingleTile x, SingleTile y)
    {
        return Mathf.Sqrt(Mathf.Pow(x.gridX - y.gridX, 2) + Mathf.Pow(x.gridY - y.gridY, 2));
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while ((transform.position - target).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy() => UnsubscribeEvents();

}
