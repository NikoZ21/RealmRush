using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] Tower tower;
    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }

    PathFinder pathFinder;
    GridManager gridManager;
    Bank bank;
    Vector2Int coordinates = new Vector2Int();
    #endregion

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
        bank = FindObjectOfType<Bank>();
    }

    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!IsPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    private void OnMouseDown()
    {
        if (bank.CurrentBalance < tower.Cost) return;
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessfull = tower.CreateTower(tower, transform.position);
            gridManager.BlockNode(coordinates);
        }
    }
}
