using UnityEngine;

public class ObstacleManager : MonoBehaviour // This class reads the data for the obstacle from the Scriptable object and sets the obstacles accordingly
{

    [SerializeField] private ObstacleData obstacleData;

    private void Awake()
    {
        EventsModel.GRID_GENERATED += GenerateObstacles;
    }

    private void GenerateObstacles()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                int corrIdx = i * 10 + j;
                if (obstacleData.obstables[corrIdx])
                {
                    Debug.Log($"Placing obstacle at ({i},{j})");
                    EventsModel.SET_OBSTACLE?.Invoke(i, j);
                }
            }
        }
    }

    private void OnDestroy()
    {
        EventsModel.GRID_GENERATED -= GenerateObstacles;
    }

}