using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [HideInInspector] public PathFinding pathFinding;
    [HideInInspector] public PlayerControl player;
    private List<Vector2Int> currentPath;
    private int currentPathIndex;
    [SerializeField] public float moveSpeed;

    private void Start()
    {
        player.OnPlayerReachedDestination += UpdateEnemyAI;
    }

    private void OnDisable()
    {
        player.OnPlayerReachedDestination -= UpdateEnemyAI;
    }

    public void MoveTo(Vector2Int targetTile)
    {
        currentPath = pathFinding.FindPath(transform.position, pathFinding.TileToWorldPosition(targetTile));
        currentPathIndex = 0;

        if (currentPath != null && currentPath.Count > 1)
        {
            StartCoroutine(MoveToNextTile());
        }
    }

    private IEnumerator MoveToNextTile()
    {
        while (currentPathIndex < currentPath.Count - 2)
        {
            Vector3 nextTileWorldPos = pathFinding.TileToWorldPosition(currentPath[currentPathIndex + 1]);

            transform.position = Vector3.MoveTowards(transform.position, nextTileWorldPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, nextTileWorldPos) < 0.01f)
            {
                currentPathIndex++;
            }

            yield return null;
        }
    }

    private void UpdateEnemyAI()
    {
        MoveTo(pathFinding.WorldToTilePosition(player.transform.position));
    }
}