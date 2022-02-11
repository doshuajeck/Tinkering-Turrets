using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTower : Tower
{
    [SerializeField] private GameObject firePoint = null;
    //private AudioSource source;

    private void Start()
    {
        //source = GetComponent<AudioSource>();
    }
    
    
    
    public override void Fire()
    {
        try
        {
            Vector2 position = firePoint.transform.position;
            Vector3 forward = Vector3.forward;
            float x = position.x;
            GameObject projectile = Instantiate(ProjectileManager.instance.rocket,
                new Vector2(position.x, position.y), firePoint.transform.rotation);
            //source.Play();
            Projectile ball = projectile.GetComponent<Projectile>();
            //GetComponent<Animator>().SetTrigger("Shoot");
            ball.damage = damage;
            if (ball != null)
                ball.Seek(target.transform, ProjectileType.ROCKET);
        }
        catch (Exception e)
        {
            return;
        }
        
    }
}