using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public static GameSessionManager Instance;

    public int totalStrokes = 0;
    public float totalTime = 0f;
    private bool isTimerRunning = false;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isTimerRunning)
        {
            totalTime += Time.deltaTime;
        }
    }

    public void StartSession() { isTimerRunning = true; }
    public void StopSession() { isTimerRunning = false; }

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        return string.Format("{0}:{1:00}", minutes, seconds);
    }
}