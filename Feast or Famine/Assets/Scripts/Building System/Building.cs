using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;
    #region Build Method

    public void Update()
    {
        if(Placed == false)
        { 
            GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        }
    }


    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        if (GridBuildingSystem.instance.CanTakeArea(areaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        Placed = true;

        GridBuildingSystem.instance.TakeArea(areaTemp);
    }

    #endregion
}
