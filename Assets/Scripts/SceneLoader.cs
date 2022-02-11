using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject towerPanel;
    
    public void LevelOne()
    {
        SceneManager.LoadScene("LevelOne");
    }

    public void ToggleTowerPanel()
    {
        towerPanel.SetActive(!towerPanel.activeInHierarchy);
    }
}
