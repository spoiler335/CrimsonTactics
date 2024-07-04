using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleTile : MonoBehaviour
{
    [SerializeField] private TextMeshPro coordinateText;

    public int gridX { get; private set; } = -1;
    public int gridY { get; private set; } = -1;


    private void Awake() => coordinateText.gameObject.SetActive(false);
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
}
