using UnityEngine;
using System.Collections.Generic;

public class BestScoresDisplay : MonoBehaviour
{
    public GameObject scoreRowPrefab;
    public Transform container;

    void OnEnable() 
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
       
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }

     
        List<ScoreData> highScores = BestScoreManager.LoadScores();

        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject newRow = Instantiate(scoreRowPrefab, container);
            ScoreRowUI ui = newRow.GetComponent<ScoreRowUI>();


            ui.SetScore(i + 1, highScores[i].strokes, highScores[i].timeDisplay);
        }
    }
}