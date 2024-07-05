using System.Collections.Generic;
using UnityEngine;

public partial class GridManager : MonoBehaviour // This class is responsible for Generating the grid 
{
    [SerializeField] private SingleTile cube;
    [SerializeField] private Material blue;
    [SerializeField] private Material green;

    private float gap = 0f;

    public List<List<SingleTile>> grid { get; private set; } = new List<List<SingleTile>>();

    private void Awake()
    {
        if (DI.di.gridManager == null)
            DI.di.SetGridGenerator(this);

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
            grid.Add(new List<SingleTile>());
            for (int j = 0; j < 10; ++j)
            {
                Vector3 position = new Vector3(i * (1 + gap), 0.1f, j * (1 + gap));
                // position = position - new Vector3(8.5f, 0, 8.5f);
                var c = Instantiate(cube, position, Quaternion.identity, transform);
                c.SetTileInfo(i, j);
                c.gameObject.name = $"Tile_{i}_{j}";
                if ((i + j) % 2 == 0)
                    c.GetComponent<Renderer>().material = blue;
                else
                    c.GetComponent<Renderer>().material = green;
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
