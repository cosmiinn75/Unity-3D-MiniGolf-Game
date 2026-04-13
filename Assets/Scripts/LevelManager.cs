using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private List<string> easyLevels = new List<string> { "Easy1", "Easy2", "Easy3" };
    private List<string> mediumLevels = new List<string> { "Medium1", "Medium2" };
    private List<string> hardLevels = new List<string> { "Hard1", "Hard2" };

    private List<string> fullPlaylist = new List<string>();
    private int currentLevelIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            GenerateRandomPlaylist();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void GenerateRandomPlaylist()
    {
        // Facem copii ale listelor pentru a nu amesteca listele originale la infinit
        List<string> e = new List<string>(easyLevels);
        List<string> m = new List<string>(mediumLevels);
        List<string> h = new List<string>(hardLevels);

        Shuffle(e);
        Shuffle(m);
        Shuffle(h);

        fullPlaylist.Clear();
        fullPlaylist.AddRange(e);
        fullPlaylist.AddRange(m);
        fullPlaylist.AddRange(h);
        currentLevelIndex = 0;
    }

    void Shuffle(List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            string temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < fullPlaylist.Count)
        {
            string sceneToLoad = fullPlaylist[currentLevelIndex];
            currentLevelIndex++;
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Resetăm playlist-ul pentru o tură nouă dacă se întoarce în MainMenu
            SceneManager.LoadScene("MainMenu");
            GenerateRandomPlaylist();
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit executat");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}