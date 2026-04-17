using TMPro;
using UnityEngine;

public class CongratsDisplay : MonoBehaviour
{
    public TextMeshProUGUI strokeText;
    public TextMeshProUGUI gradeText;
    public TextMeshProUGUI timeText;


    private void Start()
    {
        DisplayFinalStats();
    }


    void DisplayFinalStats()
    {
        if(GameSessionManager.Instance != null)
        {
            int totalStrokes = GameSessionManager.Instance.totalStrokes;
            string totalTime = GameSessionManager.Instance.GetFormattedTime();
            string grade = GradeText(totalStrokes);


            strokeText.text = totalStrokes.ToString();
            gradeText.text = grade;
            timeText.text = totalTime;

        }

      


    }


    string GradeText(int totalStrokes)
    {
        if(totalStrokes <= 45) { 
            gradeText.color = Color.yellow;
            return "S";
        }
        else if(totalStrokes <=50) {
            gradeText.color = Color.white;
            return "A"; 
        }
        else if(totalStrokes <= 55 ) { return "B"; }
        else if(totalStrokes <= 65) { return "C"; }
        else {
            gradeText.color = Color.gray;    
        return "D"; }

    }
}
