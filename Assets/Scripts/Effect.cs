using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Effect", menuName = "Tower Effect", order = 1)]
public class Effect : ScriptableObject
{
    [Header("Trigger")] 
    public bool isOnDeath = false;
    public bool isOnHit = false;
    //Other triggers if needed

    [Header("Properties")] 
    [SerializeField] bool explosive = false;
    [SerializeField] float radius = 0f;
    [SerializeField] int boomDamage = 0;
    
    //To be implemented on full release through customization
    [Space(10)] 
    private bool damageOverTime = false;
    private int damagePerTick = 0;
    private float damageTime = 0f;

    public void Activate(Vector2 enemy)
    {
        if (explosive)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(enemy, radius);
            foreach (Collider2D current in enemies)
            {
                if (current.tag.Equals("Enemy"))
                {
                    current.GetComponent<Enemy>().Damage(boomDamage, Color.yellow, ParticleEffects.instance.strikerEffect, AudioClipList.instance.strikerHit);
                }
            }
        }
    }
}
