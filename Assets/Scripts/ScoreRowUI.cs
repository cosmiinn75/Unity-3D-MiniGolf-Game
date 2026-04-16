using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreRowUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI strokeText;
    public TextMeshProUGUI timeText;
    public Image medalImage;

    public Sprite goldMedal;
    public Sprite silverMedal;
    public Sprite bronzeMedal;

    public void SetScore(int rank, int strokes, string time) {
        rankText.text = rank.ToString();
        strokeText.text = strokes.ToString();
        timeText.text = time;

        if (rank == 1) { medalImage.sprite = goldMedal; medalImage.gameObject.SetActive(true); }
        else if (rank == 2) { medalImage.sprite = silverMedal; medalImage.gameObject.SetActive(true); }
        else if (rank == 3) { medalImage.preserveAspect = bronzeMedal; medalImage.gameObject.SetActive(true); }
        else { medalImage.gameObject.SetActive(false); }
    }

}
