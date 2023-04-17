using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class CooredinateLabeler : MonoBehaviour
{

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    Color blockedColor = Color.grey;
    Color pathColor = new Color(1f, 0.5f, 0f);
    Color exploredColor = Color.yellow;
    Color defaultColor = Color.white;
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleCoordinateVisibility();
    }

    void ToggleCoordinateVisibility()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        Node node = gridManager.GetNode(coordinates);
        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }

    }

    void UpdateObjectName()
    {
        if (PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            // Only update the object name if we are not editing the prefab
            transform.parent.name = coordinates.ToString();
        }

    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
    }
}
