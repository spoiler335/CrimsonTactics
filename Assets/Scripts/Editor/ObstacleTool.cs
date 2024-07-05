using UnityEditor;
using UnityEngine;

public class ObstacleTool : EditorWindow
{
    private ObstacleData obstacleData;

    [MenuItem("Tools/Obstacle Tool")]
    public static void ShowWindow()
    {
        GetWindow<ObstacleTool>("Obstacle Tool");
    }

    private void OnGUI()
    {
        obstacleData = (ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", obstacleData, typeof(ObstacleData), false);

        if (obstacleData == null) return;

        if (obstacleData.obstables.Count == 0)
        {
            for (int i = 0; i < 100; i++)
            {
                obstacleData.obstables.Add(false);
            }
        }

        for (int x = 0; x < 10; x++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int y = 0; y < 10; y++)
            {
                int corrIdx = x * 10 + y;
                obstacleData.obstables[corrIdx] = EditorGUILayout.Toggle(obstacleData.obstables[corrIdx]);
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(obstacleData);
            AssetDatabase.SaveAssets();
        }
    }
}