using UnityEngine;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    
    public List<Vector2Int> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector2Int startTile = WorldToTilePosition(startPos);
        Vector2Int targetTile = WorldToTilePosition(targetPos);

        List<Vector2Int> path = AStar(startTile, targetTile);
        return path;
    }

    List<Vector2Int> AStar(Vector2Int startTile, Vector2Int targetTile)
    {
        List<Vector2Int> openSet = new List<Vector2Int>();
        HashSet<Vector2Int> closedSet = new HashSet<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, float> gScore = new Dictionary<Vector2Int, float>();
        Dictionary<Vector2Int, float> fScore = new Dictionary<Vector2Int, float>();

        openSet.Add(startTile);
        gScore[startTile] = 0;
        fScore[startTile] = Heuristic(startTile, targetTile);

        while (openSet.Count > 0)
        {
            Vector2Int current = GetLowestFScore(openSet, fScore);
            if (current == targetTile)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + 1; // Assuming each move has a cost of 1

                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + Heuristic(neighbor, targetTile);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int current)
    {
        List<Vector2Int> path = new List<Vector2Int> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }

    float Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    Vector2Int GetLowestFScore(List<Vector2Int> openSet, Dictionary<Vector2Int, float> fScore)
    {
        float lowestFScore = float.MaxValue;
        Vector2Int lowestTile = openSet[0];
        foreach (Vector2Int tile in openSet)
        {
            if (fScore.ContainsKey(tile) && fScore[tile] < lowestFScore)
            {
                lowestFScore = fScore[tile];
                lowestTile = tile;
            }
        }
        return lowestTile;
    }

    List<Vector2Int> GetNeighbors(Vector2Int tile)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>
        {
            new Vector2Int(tile.x + 1, tile.y),
            new Vector2Int(tile.x - 1, tile.y),
            new Vector2Int(tile.x, tile.y + 1),
            new Vector2Int(tile.x, tile.y - 1)
        };

        neighbors.RemoveAll(t => t.x < 0 || t.x >= 10 || t.y < 0 || t.y >= 10);

        neighbors.RemoveAll(t => IsObstacle(t));

        return neighbors;
    }

    bool IsObstacle(Vector2Int tile)
    {
        Vector3 worldPosition = TileToWorldPosition(tile);
        Collider[] colliders = Physics.OverlapBox(worldPosition,  new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, obstacleLayer);
        return colliders.Length > 0;
    }

    public Vector2Int WorldToTilePosition(Vector3 worldPosition)
    {
        int x = Mathf.RoundToInt(worldPosition.x / 2);
        int y = Mathf.RoundToInt(worldPosition.z / 2);
        return new Vector2Int(x, y);
    }

    public Vector3 TileToWorldPosition(Vector2Int tile)
    {
        float x = tile.x * 2;
        float y = 0.2f;
        float z = tile.y * 2;
        return new Vector3(x, y, z);
    }
}

