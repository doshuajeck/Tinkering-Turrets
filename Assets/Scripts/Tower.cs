using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //Stats
    [SerializeField] public float cooldown = 1f;
    [SerializeField] public int damage = 1;
    [SerializeField] public float range = 3f;
    public int tier = 1;
    
    //Tower information
    private Animator animator;
    [SerializeField] private GameObject barrel = null; //The part of the tower to aim at the target
    [SerializeField] private GameObject rangeView;
    
    public GameObject target; //Enemy target
    public int towerCost = 20;

    public GameObject[] enemies;
    
    //Internal variables
    private float turnRate = 7f;
    private void Awake()
    {
        //animator = GetComponent<Animator>();
        InvokeRepeating("TargetEnemy", 0f, 0.6f); //So we do not call every frame
    }



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePoint.z = 0;
            GridLayout gridLayout = LevelGrid.instance.GetComponentInParent<GridLayout>();
            Vector3Int cellPosition = gridLayout.WorldToCell(mousePoint);
            Vector3 gridPosition = cellPosition;
        
            gridPosition.x += 0.5f;
            gridPosition.y += 0.5f;
            
            if (Vector3.Distance(transform.position, gridPosition) > 0.9f)
                rangeView.SetActive(false);
        }
        
        if (target == null)
        {
            CancelInvoke("Fire");
            return;
        }

        Vector2 direction = target.transform.position - barrel.transform.position;
        float zRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        /*Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 angleRotation = Quaternion.Lerp(barrel.transform.rotation, lookRotation, 
            turnRate * Time.deltaTime).eulerAngles;*/
        //barrel.transform.rotation = Quaternion.Euler(0f, 0, angleRotation.z);
        barrel.transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90);
        if (!IsInvoking("Fire"))
            InvokeRepeating("Fire", cooldown / 3, cooldown);
    }
    
    
    
    private void TargetEnemy()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closest = 12345678f; //Default to a high number until updated by an enemy
        GameObject nearest = null;
        
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closest && enemy.GetComponent<Enemy>().health > 0)
            {
                closest = distanceToEnemy;
                nearest = enemy;
            }
        }

        if (nearest != null && closest <= range)
        {
            target = nearest;
        }
        else
        {
            target = null;
        }
    }
    
    public virtual void Fire()
    {
        return;
    }

    private void OnMouseDown()
    {
        rangeView.SetActive(!rangeView.activeInHierarchy);
    }
}
