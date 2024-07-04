using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private SingleTile cube;

    private float gap = 1f;

    private void Start() => GenerateGrid();


    private void GenerateGrid()
    {
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 10; ++j)
            {
                Vector3 position = new Vector3(i * (1 + gap), 0.5f, j * (1 + gap));
                position = position - new Vector3(8.5f, 0, 8.5f);
                var c = Instantiate(cube, position, Quaternion.identity, transform);
                c.SetTileInfo(i, j);
                c.gameObject.name = $"Tile_{i}_{j}";
            }
        }
    }

}
