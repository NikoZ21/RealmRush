using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get => startCoordinates; }

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get => destinationCoordinates; }

    Node startNode;
    Node destinationNode;
    Node currentSearchNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reachedNodes = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    #endregion



    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        }

    }

    private void OnEnable()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }


    void BreadthFirstSearch()
    {
        frontier.Clear();
        reachedNodes.Clear();


        bool isRunning = true;

        frontier.Enqueue(startNode);
        reachedNodes.Add(startCoordinates, startNode);

        while (frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbours();
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

    private void ExploreNeighbours()
    {
        List<Node> neighbours = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinats = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighbourCoordinats))
            {
                neighbours.Add(grid[neighbourCoordinats]);
            }
        }

        foreach (Node neighbour in neighbours)
        {
            if (!reachedNodes.ContainsKey(neighbour.coordinates) && neighbour.isWalkable)
            {
                neighbour.connectedTo = currentSearchNode;
                reachedNodes.Add(neighbour.coordinates, neighbour);
                frontier.Enqueue(neighbour);
            }
        }
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            Debug.Log(newPath.Count);
            grid[coordinates].isWalkable = true;

            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

            return false;
        }

        return false;
    }

}
