using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    PathFinder pathFinder;
    public bool IsPlaceable => isPlaceable;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        if (!isPlaceable)
        {
            gridManager.BlockNode(coordinates);
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isTowerCreated = towerPrefab.CreateTower(towerPrefab, transform.position);
            if (isTowerCreated)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
        }
    }
}
