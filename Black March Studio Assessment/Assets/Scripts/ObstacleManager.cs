using UnityEditor;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float SpaceBetweenObstacles;
    [SerializeField] private ObstacleData obstacleData;
    [SerializeField] private PathFinding pathfinding;

    [SerializeField] private PlayerControl playerControl;
    private Vector3 playerSpawnPosition = new Vector3(0f, 0.2f, 0f);
    private bool GotSpawnPosition = false;
    private PlayerControl player;

    [SerializeField] private EnemyAI enemyUnit;
    private Vector3 enemySpawnPosition = new Vector3(1f, 0.2f, 1f);
    private bool GotEnemySpawnPosition = false;

    private const string ObstaclePrefsKey = "ObstacleData";
    private bool IsNoObstacleInMap = true;

    public string gridData;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        bool[,] obstacleGrid = obstacleData.GetObstacleGrid();

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                if (obstacleGrid[x, z])
                {
                    IsNoObstacleInMap = false;
                    break;
                }
            }

            if (!IsNoObstacleInMap)
            {
                break;
            }
        }


        if (IsNoObstacleInMap)
        {
             gridData = EditorPrefs.GetString(ObstaclePrefsKey, "");

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    obstacleGrid[i, j] = gridData[i * 10 + j] == '1';
                }
            }
        }

        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                if (obstacleGrid[x, z])
                {
                    Vector3 spawnPosition = new Vector3(x * 2, 0f, z * 2);
                    Instantiate(obstaclePrefab, spawnPosition, Quaternion.Euler(0f, 230f, 0f), transform);
                }
                else if (!GotSpawnPosition)
                {
                    playerSpawnPosition = new Vector3(x * 2, 0.2f, z * 2);
                    GotSpawnPosition = true;
                }
                else if (!GotEnemySpawnPosition)
                {
                    enemySpawnPosition = new Vector3(x * 2, 0.2f, z * 2);
                    GotEnemySpawnPosition = true;
                }
            }
        }

        InstantiatePlayer();
        InstantiateEnemy();
    }


    void InstantiatePlayer()
    {
        player = Instantiate(playerControl, playerSpawnPosition, Quaternion.Euler(0f, -90f, 0f));
        player.obstacleData = obstacleData;
        player.pathFinding = pathfinding;
    }

    void InstantiateEnemy()
    {
        EnemyAI enemy = Instantiate(enemyUnit, enemySpawnPosition, Quaternion.Euler(0f, -90f, 0f));
        enemy.pathFinding = pathfinding;
        enemy.player = player;
    }
}
