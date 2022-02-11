using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int bestWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            return;

        DontDestroyOnLoad(gameObject);
        instance = this;

        bestWave = PlayerPrefs.GetInt("BestWave");
    }

    public void EndGame(int waveReached)
    {
        if (waveReached < bestWave)
            return;

        bestWave = waveReached;
        PlayerPrefs.SetInt("BestWave", bestWave);
    }

}
