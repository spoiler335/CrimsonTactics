using System.Collections.Generic;
using UnityEngine;

public partial class GridManager
{
    public List<SingleTile> GetNeighbors(SingleTile currentTile) // returns a list of tiles that are adjacent to the current tile and are not obstacles
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
                    if (!grid[nex][ney].isObstacle)
                        neighbors.Add(grid[nex][ney]);
                }
            }
        }
        return neighbors;
    }

    /*
      - returns the list of tiles whihc froms a path between the current tile and the target tile
      - We store the tiles whcih the player was previously on in the dictionary
      - We add the tile to the Queue which are non obstacle neighbors and not present in the dictionary
      - Then we add the tiles form dictionary to a list whcih would be the reverse order so before we return the path we reverse it Again.
    */
    public List<SingleTile> FindPath(SingleTile start, SingleTile end)
    {

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



}