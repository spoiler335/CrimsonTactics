using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private SingleTile cube;

    private float gap = 1f;

    private List<List<SingleTile>> grid = new List<List<SingleTile>>();

    private void Awake()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventsModel.SET_OBSTACLE += GenerateObstacle;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.SET_OBSTACLE -= GenerateObstacle;
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                Vector3 position = new Vector3(i * (1 + gap), 0.1f, j * (1 + gap));
                position = position - new Vector3(8.5f, 0, 8.5f);
                var c = Instantiate(cube, position, Quaternion.identity, transform);
                c.SetTileInfo(i, j);
                c.gameObject.name = $"Tile_{i}_{j}";
                grid.Add(new List<SingleTile>());
                grid[i].Add(c);
            }
        }
        EventsModel.GRID_GENERATED?.Invoke();
    }

    private void GenerateObstacle(int x, int y)
    {
        grid[x][y].ShowObstacleSpehre();
    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

}
