using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipList : MonoBehaviour
{
    public static AudioClipList instance;

    public AudioClip gatlingHit;
    public AudioClip rpgHit;
    public AudioClip slowHit;
    public AudioClip strikerHit;

    public AudioClip explosion;
    // Start is called before the first frame update
    void Awake()
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
