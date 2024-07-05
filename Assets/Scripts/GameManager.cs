using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Enemy enemyPrefab;

    private List<List<SingleTile>> grid => DI.di.gridGenerator.grid;

    private void Awake()
    {
        Assert.IsNotNull(playerPrefab, "Please Assign the Player");
        Assert.IsNotNull(enemyPrefab, "Please Assign the Enemy");

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventsModel.GRID_GENERATED += OnGridGnerated;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.GRID_GENERATED -= OnGridGnerated;
    }

    private void OnGridGnerated()
    {
        SpawnPlayerOnTheGrid();
    }

    private void SpawnPlayerOnTheGrid()
    {
        var player = Instantiate(playerPrefab, grid[0][0].transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        var neighbhous = DI.di.gridGenerator.GetNeighbors(grid[0][0]);
        var adj = neighbhous[Random.Range(0, neighbhous.Count)];
        var enemy = Instantiate(enemyPrefab, adj.transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        enemy.SetInitialCoordinates(adj.gridX, adj.gridY);
    }

    private void OnDestroy() => UnsubscribeEvents();
}