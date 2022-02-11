using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ProjectileType { GATLING, ROCKET, SLOW, STRIKE}
public class Projectile : MonoBehaviour
{
    public AudioClip sound;
    
    private Transform target;
    public float projectileSpeed = 50f;

    public int damage = 1;
    private float initialTime;

    private ProjectileType _projectileType;
    public void Seek(Transform _target, ProjectileType type)
    {
        initialTime = Time.time;
        target = _target;
        _projectileType = type;
    }



    private void Start()
    {
        initialTime = Time.time;
    }
    private void FixedUpdate()
    {
        if (target == null && Time.time - initialTime > 0.02f)
        {
            Destroy(gameObject);
            return;
        }

        try
        {
            Vector3 direction = target.position - transform.position;
            //direction.y = direction.y + 1f; //Offset Y so it doesn't just hit the ground
            float distancePerFrame = projectileSpeed * Time.deltaTime;

            if (direction.magnitude <= distancePerFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * distancePerFrame, Space.World);
        }
        catch (Exception e)
        {
            Destroy(gameObject);
        }
        
    }



    private void HitTarget()
    {
        Enemy targetEnemy;
        switch (_projectileType)
        {
            case ProjectileType.GATLING:
                targetEnemy = target.GetComponent<Enemy>();
                targetEnemy.Damage(damage, Color.blue, AudioClipList.instance.gatlingHit);
                break;
            case ProjectileType.ROCKET:
                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 2.5f);
                foreach (var current in targets)
                {
                    if (current.tag.Equals("Enemy"))
                    {
                        Enemy currentEnemy = current.GetComponent<Enemy>();
                        currentEnemy.Damage(damage, new Color(1, 0.5f, 0), AudioClipList.instance.rpgHit);
                        Debug.Log("Damaged enemy");
                    }
                }
                break;
            case ProjectileType.SLOW:
                targetEnemy = target.GetComponent<Enemy>();
                targetEnemy.Damage(damage, Color.cyan, AudioClipList.instance.slowHit);
                targetEnemy.Slow();
                break;
            case ProjectileType.STRIKE:
                targetEnemy = target.GetComponent<Enemy>();
                targetEnemy.Damage(damage, TowerEffects.instance.strikerExplosion, Color.yellow, AudioClipList.instance.strikerHit);
                break;
        }
        
        Destroy(gameObject);
    }
}
