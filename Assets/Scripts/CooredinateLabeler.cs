using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SceneManagement;

[ExecuteAlways]
public class CooredinateLabeler : MonoBehaviour
{

    TMP_Text label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;
    void Awake()
    {
        waypoint = GetComponentInParent<Waypoint>();
        label = GetComponent<TMP_Text>();
        label.enabled = false;
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ColorCoordinates();
        ToggleCoordinateVisibility();
    }

    void ToggleCoordinateVisibility()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void ColorCoordinates()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = Color.white;
        }
        else
        {
            label.color = Color.gray;
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y;
    }
}
