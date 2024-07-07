using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GridManager grdiManager => DI.di.gridManager;
    private List<List<SingleTile>> grid => grdiManager.grid;

    private float moveSpeed = 5f;

    private int currX = 0;
    private int currY = 0;

    private void Awake() => SubscribeEvents();

    private void Start()
    {
        currX = 0;
        currY = 0;
    }

    private void SubscribeEvents()
    {
        EventsModel.TILE_CLICKED += OnTileClicked;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.TILE_CLICKED -= OnTileClicked;
    }

    private void OnTileClicked(int x, int y) // when a tile is clicked the player check if it can rach the target and then movers towars the target
    {
        Debug.Log($"Tile clicked {x} {y}");
        if (x == currX && y == currY)
        {
            Debug.Log($"Same tile clicked");
            return;
        }
        SingleTile start = grid[currX][currY];
        SingleTile end = grid[x][y];
        var path = grdiManager.FindPath(start, end);

        ////////////////////////////////////////////////////////////////
        ///For Debug
        string s = "";
        foreach (var dd in path)
        {
            s += $"({dd.gridX},{dd.gridY})\n";
        }
        Debug.Log($"path is \n {s}");
        ////////////////////////////////////////////////////////////////
        EventsModel.PLAYER_MOVEMNT_STARTED?.Invoke(x, y);
        StartCoroutine(MoveToardsTarget(path));
    }

    private IEnumerator MoveToardsTarget(List<SingleTile> path) // To MoveToards the target and thwrow and Event when reached the target
    {
        foreach (SingleTile tile in path)
        {
            yield return StartCoroutine(MoveToPosition(new Vector3(tile.transform.position.x, transform.position.y, tile.transform.position.z)));
        }
        currX = path[path.Count - 1].gridX;
        currY = path[path.Count - 1].gridY;
        Debug.Log($"Player moved to ({currX},{currY})");
        EventsModel.PLAYER_MOVEMNT_COMPLETED?.Invoke(currX, currY);
    }

    private IEnumerator MoveToPosition(Vector3 target) // move towards the tile
    {
        while ((transform.position - target).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy() => UnsubscribeEvents();

}
