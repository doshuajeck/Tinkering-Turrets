using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTest : MonoBehaviour
{
    public GameObject spawn;

    public void PlaceTower(Vector3Int coords)
    {
        if (Input.GetMouseButtonUp(0))
        {
            // get mouse click's position in 2d plane
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePoint.z = 0;

            // convert mouse click's position to Grid position
            GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(mousePoint);
            Vector3 gridPosition = cellPosition;
            
            gridPosition.x += 0.5f;
            gridPosition.y += 0.5f;
            
            //Test location
            Debug.Log(cellPosition);
            Instantiate(spawn, gridPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("NA");
        }
    }
    /*private void OnMouseUp()
    {
        // left click - get info from selected tile
        if (Input.GetMouseButtonUp(0))
        {
            // get mouse click's position in 2d plane
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePoint.z = 0;

            // convert mouse click's position to Grid position
            GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(mousePoint);
            Vector3 gridPosition = cellPosition;
            
            gridPosition.x += 0.5f;
            gridPosition.y += 0.5f;
            
            //Test location
            Debug.Log(cellPosition);
            Instantiate(spawn, gridPosition, Quaternion.identity);
        }
        else
        {
            Debug.Log("NA");
        }
    }*/
}
