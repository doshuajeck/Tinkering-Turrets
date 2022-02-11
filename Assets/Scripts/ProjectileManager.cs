using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;

    public GameObject pellet; //Gatling
    public GameObject rocket;
    public GameObject slowBall;
    public GameObject strikerRound;
    
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
