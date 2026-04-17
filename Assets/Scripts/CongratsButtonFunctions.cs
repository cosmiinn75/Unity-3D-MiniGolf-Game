using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratsButtonFunctions: MonoBehaviour
{
    public void BackToMainMenuFunction() {

        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        Debug.Log("Butonul Play Again a fost apasat!"); // Asta ar trebui sa apara in consola
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.StartNewGame();
        }
        else
        {
            Debug.LogError("LevelManager.Instance este NULL! Ai pornit jocul din MainMenu?");
        }
    }
}
