using UnityEngine;

[System.Serializable]
public class ObstacleData : ScriptableObject
{
    [SerializeField]
    private bool[,] obstacleGrid = new bool[10, 10];

    public bool[,] GetObstacleGrid()
    {
        return obstacleGrid;
    }

    public void SetObstacleGrid(bool[,] newGrid)
    {
        obstacleGrid = newGrid;
    }
}
