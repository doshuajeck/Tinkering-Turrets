using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid instance;
    
    
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
