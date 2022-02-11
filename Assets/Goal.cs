using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            WaveManager.instance.LoseLife(1);
            WaveManager.instance.alive.Remove(other.gameObject.transform.parent.gameObject);
            Destroy(other.gameObject.transform.parent.gameObject);
            Destroy(other.gameObject, 0.03f);
        }
    }
}
