using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class DisplayCoordinats : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.blue;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    #endregion


    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = true;
        gridManager = FindObjectOfType<GridManager>();
        DisplayCoordinatsss();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinatsss();
            UpdateObjectName();
        }
        SetLabelColors();
        ToggleLabel();
    }

    void ToggleLabel()
    {
        if (Input.GetKeyDown(KeyCode.C)) label.enabled = !label.IsActive();
    }

    private void SetLabelColors()
    {
        if (gridManager == null) return;

        Node node = gridManager.GetNode(coordinates);

        if (node == null) return;

        if (!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            label.color = Color.blue;
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

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void DisplayCoordinatsss()
    {
        if (gridManager == null) return;

        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + " , " + coordinates.y;
    }
}
