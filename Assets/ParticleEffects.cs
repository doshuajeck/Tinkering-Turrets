using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public static ParticleEffects instance;

    public GameObject strikerEffect;

    void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
}
