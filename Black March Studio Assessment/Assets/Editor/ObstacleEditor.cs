using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ObstacleData))]
public class ObstacleEditor : Editor
{
    private bool[,] obstacleGrid = new bool[10, 10];
    private const string EditorPrefsKey = "ObstacleDataGrid";
    private const string ObstaclePrefsKey = "ObstacleData";

    void OnEnable()
    {
        ObstacleData obstacleData = (ObstacleData)target;
        obstacleGrid = obstacleData.GetObstacleGrid();

        LoadGrid();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        GUILayout.Label("Obstacle Editor");

        for (int i = 0; i < 10; i++)
        {
            EditorGUILayout.BeginHorizontal();

            for (int j = 0; j < 10; j++)
            {
                obstacleGrid[i, j] = EditorGUILayout.Toggle(obstacleGrid[i, j], GUILayout.Width(20), GUILayout.Height(20));
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Apply Obstacles"))
        {
            ApplyObstacles();
        }
    }

    void ApplyObstacles()
    {
        ObstacleData obstacleData = (ObstacleData)target;
        obstacleData.SetObstacleGrid(obstacleGrid);
        SaveGrid();
    }

    void SaveGrid()
    {
        string gridData = "";

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                gridData += obstacleGrid[i, j] ? "1" : "0";
            }
        }

        EditorPrefs.SetString(EditorPrefsKey, gridData);
        EditorPrefs.SetString(ObstaclePrefsKey, gridData);
    }

    void LoadGrid()
    {
        string gridData = EditorPrefs.GetString(EditorPrefsKey, "");

        if (gridData.Length == 100)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    obstacleGrid[i, j] = gridData[i * 10 + j] == '1';
                }
            }
        }
    }
}
