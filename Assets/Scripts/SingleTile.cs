using System.Collections;
using TMPro;
using UnityEngine;

public class SingleTile : MonoBehaviour
{
    [SerializeField] private TextMeshPro coordinateText;
    [SerializeField] private GameObject obstacleSphere;

    public int gridX { get; private set; } = -1;
    public int gridY { get; private set; } = -1;

    public bool isObstacle { get; private set; } = false;

    private bool isPlayerMovementInProgress = false;

    private void Awake()
    {
        coordinateText.gameObject.SetActive(false);
        obstacleSphere.SetActive(false);
        SubscribeEvets();
    }
    private void SubscribeEvets()
    {
        EventsModel.PLAYER_MOVEMNT_STARTED += OnPlayerMovementStarted;
        EventsModel.PLAYER_MOVEMNT_COMPLETED += OnPlayerMovementCompleted;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.PLAYER_MOVEMNT_STARTED -= OnPlayerMovementStarted;
        EventsModel.PLAYER_MOVEMNT_COMPLETED -= OnPlayerMovementCompleted;
    }


    private void OnPlayerMovementStarted(int x, int y) => isPlayerMovementInProgress = true;
    private void OnPlayerMovementCompleted() => StartCoroutine(WaitAndRemovePlayerMovementInProgress());

    private IEnumerator WaitAndRemovePlayerMovementInProgress()
    {
        yield return new WaitForEndOfFrame();
        isPlayerMovementInProgress = false;
    }

    public void SetTileInfo(int x, int y)
    {
        gridX = x;
        gridY = y;
    }

    public void ShowTileInfo()
    {
        coordinateText.text = $"Position\n({gridX},{gridY})";
        StartCoroutine(ShowTextAndHide());
    }

    private IEnumerator ShowTextAndHide()
    {
        coordinateText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        coordinateText.gameObject.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (gridX < 0 || gridY < 0) return;
        ShowTileInfo();
    }

    private void OnMouseDown()
    {
        if (isPlayerMovementInProgress) return;
        if (isObstacle) return; // to prevent clicks on tile that is an obstacle
        EventsModel.TILE_CLICKED?.Invoke(gridX, gridY);
    }

    public void ShowObstacleSpehre()
    {
        obstacleSphere.SetActive(true);
        isObstacle = true;
    }

    private void OnDestroy() => UnsubscribeEvents();
}
