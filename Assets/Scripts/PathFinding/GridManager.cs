using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region PropertiesAndVariables
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    [SerializeField] [Tooltip("World grid size should match snap settings")] int unityGridSize = 10;
    public int UnityGridSize { get => unityGridSize; }
    public Dictionary<Vector2Int, Node> Grid { get => grid; }
    #endregion


    private void Awake() => CreateGrid();

    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }

    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinats(Vector2Int cooridnates)
    {
        return new Vector3(cooridnates.x * unityGridSize, 0, cooridnates.y * unityGridSize);
    }

    public Node GetNode(Vector2Int coorinates)
    {
        if (!grid.ContainsKey(coorinates)) return null;
        return grid[coorinates];
    }
}
