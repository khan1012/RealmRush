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
    void Awake()
    {
        label = GetComponent<TMP_Text>();
        DisplayCoordinates();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
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
