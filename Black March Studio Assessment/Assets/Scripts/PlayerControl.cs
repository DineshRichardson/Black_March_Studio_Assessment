using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isMoving = false;

    private Vector2Int targetGridPosition;
    [HideInInspector] public ObstacleData obstacleData;
    [HideInInspector] public PathFinding pathFinding;

    public Action OnPlayerReachedDestination = delegate { };

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GridBlock gridBlock = hit.collider.GetComponent<GridBlock>();
                if (gridBlock != null)
                {
                    targetGridPosition = gridBlock.TilePosition;

                    ObstacleData obstacleData = FindObjectOfType<ObstacleData>();

                    if (obstacleData == null)
                    {
                        List<Vector2Int> newPath = pathFinding.FindPath(transform.position, new Vector3(targetGridPosition.x * 2, 1f, targetGridPosition.y * 2));
                        if (newPath != null)
                        {
                            StartCoroutine(FollowPath(newPath));
                        }
                    }
                    else if (!obstacleData.GetObstacleGrid()[targetGridPosition.x, targetGridPosition.y])
                    {
                        List<Vector2Int> newPath = pathFinding.FindPath(transform.position, new Vector3(targetGridPosition.x * 2, 1f, targetGridPosition.y * 2));
                        if (newPath != null)
                        {
                            StartCoroutine(FollowPath(newPath));
                        }
                    }
                }
            }
        }
    }

    private IEnumerator FollowPath(List<Vector2Int> path)
    {
        isMoving = true;

        foreach (Vector2Int cell in path)
        {
            Vector3 targetPosition = TileToWorldPosition(cell);

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        OnPlayerReachedDestination?.Invoke();
        isMoving = false;
    }

    private Vector3 TileToWorldPosition(Vector2Int tile)
    {
        float x = tile.x * 2;
        float y = 0.2f;
        float z = tile.y * 2;
        return new Vector3(x, y, z);
    }
}
