using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private AudioSource source;
    
    public int health;
    [SerializeField] int maxHealth;
    public int weight;
    public int gold;
    public int lives;
    private SpriteRenderer sprite;
    
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        source = GetComponentInParent<AudioSource>();
        sprite.color = Color.white;
        health = maxHealth;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
    private IEnumerator HitColor(Color hitColor)
    {
        sprite.color = hitColor;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;

    }

    private IEnumerator HitFlash()
    {
        transform.localScale = new Vector3(0.65f, 0.65f, 1f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
    }
    public void Damage(int damage, Color hitColor, AudioClip clip)
    {
        StartCoroutine(HitColor(hitColor));
        StartCoroutine(HitFlash());
        health -= damage;
        source.clip = clip;
        source.Play();
        if (health <= 0)
            Death();
    }

    public void Damage(int damage, Effect effect, Color hitColor, AudioClip clip)
    {
        StartCoroutine(HitColor(hitColor));
        StartCoroutine(HitFlash());
        health -= damage;
        source.clip = clip;
        source.Play();
        //Route based on type of effect
        if (effect.isOnHit)
        {
            Debug.Log("On Hit");
        }

        if (health <= 0 && effect.isOnDeath)
        {
            effect.Activate(transform.position);
            Death(ParticleEffects.instance.strikerEffect);
        } 
        else if (health <= 0)
        {
            Death();
        }
    }
    
    public void Damage(int damage, Color hitColor, GameObject deathEffect, AudioClip clip)
    {
        StartCoroutine(HitColor(hitColor));
        StartCoroutine(HitFlash());
        health -= damage;
        source.clip = clip;
        source.Play();
        
        if (health <= 0)
        {
            Death(deathEffect);
        }
    }
    public void Scale(float scalar)
    {
        health = (int) (health * scalar);
        maxHealth = (int) (maxHealth * scalar);
        gold = (int) (gold * (scalar * .8f));
    }

    public void Slow()
    {
        StopCoroutine(GetComponent<Pathfinder>().SlowEffect());
        StartCoroutine(GetComponent<Pathfinder>().SlowEffect());
    }
    
    private void Death()
    {
        EconomyManager.instance.money += gold;
        WaveManager.instance.alive.Remove(transform.parent.gameObject);
        Destroy(gameObject, 0.05f);
        Destroy(transform.parent.gameObject, 1.5f);
    }
    
    private void Death(GameObject deathParticles)
    {
        EconomyManager.instance.money += gold;
        WaveManager.instance.alive.Remove(transform.parent.gameObject);
     
        //Spawn particles :D
        GameObject particles = Instantiate(deathParticles, transform.position, Quaternion.identity);
        
        //Kill particles :(
        Destroy(particles, 0.7f);
        Destroy(gameObject, 0.05f);
        Destroy(transform.parent.gameObject, 1.5f);
    }
}
