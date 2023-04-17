using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates => startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates => destinationCoordinates;
    Node startNode;
    Node destinationNode;
    Node currentSearchNode;
    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        startNode = gridManager.GetNode(startCoordinates);
        destinationNode = gridManager.GetNode(destinationCoordinates);
    }
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (var direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;
            if (gridManager.Grid.ContainsKey(neighborCoordinates))
            {
                Node neighborNode = gridManager.GetNode(neighborCoordinates);
                neighbors.Add(neighborNode);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if (!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
            }
        }
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;
        frontier.Clear();
        reached.Clear();

        bool isRunning = true;

        frontier.Enqueue(gridManager.GetNode(coordinates));
        reached.Add(coordinates, gridManager.GetNode(coordinates));

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;
        path.Add(currentNode);
        currentNode.isPath = true;

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        path.Reverse();

        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (gridManager.Grid.ContainsKey(coordinates))
        {
            var node = gridManager.GetNode(coordinates);
            bool previousState = node.isWalkable;

            node.isWalkable = false;
            var newPath = GetNewPath();
            node.isWalkable = previousState;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("ReCalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
