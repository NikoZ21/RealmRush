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
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    WayPoint wayPoint;
    #endregion


    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        wayPoint = GetComponentInParent<WayPoint>();
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
        GetComponent<TextMeshPro>().color = wayPoint.IsPlaceable ? defaultColor : blockedColor;
    }

    private void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }

    private void DisplayCoordinatsss()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + " , " + coordinates.y;
    }
}
