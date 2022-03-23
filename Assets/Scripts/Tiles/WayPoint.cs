using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    #region VariablesAndProperties
    [SerializeField] Tower tower;
    [SerializeField] bool isPlaceable = false;
    public bool IsPlaceable { get { return isPlaceable; } }
    #endregion


    private void OnMouseDown()
    {
        if (!isPlaceable) return;
        bool isPlaced = tower.CreateTower(tower, transform.position);
        isPlaceable = !isPlaced;
    }
}
