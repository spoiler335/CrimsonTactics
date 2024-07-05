using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;

    private void Awake()
    {
        displayText.gameObject.SetActive(false);

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EventsModel.PLAYER_MOVEMNT_STARTED += OnPlayerMovementStarted;
        EventsModel.SAME_TILE_CLICKED += OnSameTileClicked;
        EventsModel.PLAYER_HAS_NO_PATH += OnPlayerHasNoPath;
    }

    private void UnsubscribeEvents()
    {
        EventsModel.PLAYER_MOVEMNT_STARTED -= OnPlayerMovementStarted;
        EventsModel.SAME_TILE_CLICKED -= OnSameTileClicked;
        EventsModel.PLAYER_HAS_NO_PATH -= OnPlayerHasNoPath;
    }

    private void OnPlayerHasNoPath()
    {
        displayText.text = "Player Has No Path";
        StartCoroutine(DisplayTextAndHide());
    }

    private void OnSameTileClicked()
    {
        displayText.text = "Same Tile Clicked";
        StartCoroutine(DisplayTextAndHide());
    }

    private void OnPlayerMovementStarted(int x, int y)
    {
        displayText.text = $"Player Moving Towards : ({x},{y})";
        StartCoroutine(DisplayTextAndHide());
    }

    private IEnumerator DisplayTextAndHide()
    {
        displayText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        displayText.gameObject.SetActive(false);
    }

}
