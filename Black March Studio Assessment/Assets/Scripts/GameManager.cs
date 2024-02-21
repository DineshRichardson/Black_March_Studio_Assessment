using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridBlock cubePrefab;
    [SerializeField] private float spacing;

    [SerializeField] private TMP_Text GridBlockInfoText;
    private Camera mainCamera;
    

    private void Start()
    {
        mainCamera = Camera.main;
        GenerateGrid();
    }

    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            GridBlock gridBlock = hitObject.GetComponent<GridBlock>();

            if (gridBlock != null)
            {
                GridBlockInfoText.text = "Tile Position: " + gridBlock.TilePosition.ToString();
            }
        }
    }

    private void GenerateGrid()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                Vector3 spawnPosition = new Vector3(x * spacing, 0f, z * spacing);
                GridBlock gridBlock = Instantiate(cubePrefab, spawnPosition, Quaternion.identity, transform);
                gridBlock.TilePosition = new Vector2Int(x, z);
            }
        }
    }
}