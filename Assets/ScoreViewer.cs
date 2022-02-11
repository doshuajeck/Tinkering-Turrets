using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<TextMeshProUGUI>().text = "Best: " + ScoreManager.instance.bestWave;
    }
}
