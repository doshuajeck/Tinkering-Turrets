using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    
    public ArrayList alive = new ArrayList();
    private GameObject[] spawnPoints;
    private ArrayList spawns = new ArrayList();

    [Header("Game Info")] 
    public int lives = 20;
    
    [Header("Wave Details")]
    public int wave = 1;
    [SerializeField] int baseWeight = 5;
    private int scaledWeight;
    public int weight;
    
    [Header("Enemy Info")]
    public float scale = 1.1f;
    public GameObject[] enemies;

    [Header("Misc Elements")] 
    public TextMeshProUGUI text;
    public TextMeshProUGUI liveText;
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            return;
        
        instance = this;
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn");
        foreach (GameObject spawn in spawnPoints)
        {
            spawns.Add(spawn.GetComponent<Spawn>());
        }

        scaledWeight = 0;
        weight = baseWeight;
        
        Invoke("StartWave", 5f);
    }

    public void StartWave()
    {
        wave++;
        text.text = "Wave " + wave;
        liveText.text = lives + " Lives";
        int nextEnemy = 0;
        scaledWeight += 5;
        weight = scaledWeight;

        scale = Mathf.Pow(1.11f, wave - 1);
        
        foreach (Spawn current in spawns)
        {
            current.StartWave(enemies[0], scale);
        }

        InvokeRepeating("WaveCheck", 5f, 1.5f);
    }

    private void WaveCheck()
    {
        if (alive.Count == 0)
        {
            CancelInvoke("WaveCheck");
            StartWave();
        }
    }

    public void LoseLife(int count)
    {
        lives -= count;
        liveText.text = lives + " Lives";
        if (lives <= 0)
        {
            ScoreManager.instance.EndGame(wave);
            SceneManager.LoadScene("Menu");
        }
    }
}
