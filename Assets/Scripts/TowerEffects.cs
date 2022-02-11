using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerEffects : MonoBehaviour
{
    public static TowerEffects instance;
    
    [Header("Death Effects")] 
    public Effect strikerExplosion;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            return;

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
