using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;

    public int money;
    public TextMeshProUGUI text;
    
    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    void FixedUpdate()
    {
        text.text = "$" + money;
    }
}
