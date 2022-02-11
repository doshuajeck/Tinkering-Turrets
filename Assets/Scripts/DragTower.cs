using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine;

public class DragTower : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject tower;
    [SerializeField] private GameObject dragTower;
    
    public int cost;
    
    private int pathLayer;
    private int cameraLayer;
    private bool dragging;
    
    private GameObject dragInstance;
    
    private void Start()
    {
        pathLayer = LayerMask.NameToLayer("Ground");
        cameraLayer = LayerMask.NameToLayer("Camera");
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Destroy(dragInstance);
            dragging = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging)
            return;
        
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0;
        dragInstance.transform.position = mousePoint;
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0;
        dragInstance = Instantiate(dragTower, mousePoint, Quaternion.identity);
        dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!dragging)
            return;
        
        Destroy(dragInstance);
        
        //If money > cost
        // get mouse click's position in 2d plane
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePoint.z = 0;
        
        // convert mouse click's position to Grid position
        GridLayout gridLayout = LevelGrid.instance.GetComponentInParent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(mousePoint);
        Vector3 gridPosition = cellPosition;
        
        gridPosition.x += 0.5f;
        gridPosition.y += 0.5f;

        try
        {
            bool valid = true;
            Collider2D[] locObjects = Physics2D.OverlapAreaAll(new Vector2(cellPosition.x + .03f, cellPosition.y + .03f),
                new Vector2(cellPosition.x + .97f, cellPosition.y + .97f));
            foreach (Collider2D col in locObjects)
            {
                //if (Physics2D.OverlapCircle(gridPosition, 0.01f).gameObject.layer != pathLayer)
                //   valid = false;
                if (col.gameObject.layer != pathLayer && col.gameObject.layer != cameraLayer)
                    valid = false;
            }

            if (valid)
            {
                StartCoroutine(NavigationManager.instance.Check(tower, gridPosition, cost));
                /*if (EconomyManager.instance.money >= cost)
                {
                    
                    GameObject newTower = Instantiate(tower, gridPosition, Quaternion.identity);
                    if (!NavigationManager.instance.Check())
                    {
                        Destroy(newTower);
                        Debug.Log("Blocked Path");
                    }
                    else
                    {
                        EconomyManager.instance.money -= cost;
                        Debug.Log("Purchased");
                    }
                }*/
            }
            else
            {
                Debug.Log("Invalid Spot");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Not Ground");
        }

        dragging = false;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Click");
    }
}
