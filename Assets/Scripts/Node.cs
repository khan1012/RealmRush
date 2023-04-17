using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isPath;
    public bool isExplored;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.isWalkable = isWalkable;
        this.coordinates = coordinates;
    }
}
