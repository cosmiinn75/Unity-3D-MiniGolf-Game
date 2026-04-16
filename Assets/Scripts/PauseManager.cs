using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool isPaused = false;

    private void Start()
    {
       
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            TogglePause();
        }
    }


    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {

            ShowPauseMenu();           
        }
        else
        {
            ResumeGame();
        }
        


    }

   private  void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;

    }


    public void ResumeGame()
    {

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

    }
    public void GoBackToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (UnityEngine.EventSystems.EventSystem.current != null)
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
