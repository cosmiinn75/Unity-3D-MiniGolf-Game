using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hole;
    public GameObject particle;
    public GameObject player;
   [SerializeField] private int par = 3;
    public TextMeshProUGUI ratingText; // birdie, par or bogey
    private PlayerController playerController;
    private bool isGameOver = false;
    public AudioClip holeSound;


    private void Start()
    {
        ratingText.text = "";
        playerController = player.GetComponent<PlayerController>();
        particle.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("isHole") && !isGameOver)
            
        {
            AudioSource.PlayClipAtPoint(holeSound, transform.position);
            isGameOver = true;
            GameOver();
        }
    }


    void GameOver()
    {
        CheckScore();
        particle.gameObject.SetActive(true);

        if(GameSessionManager.Instance != null)
        {
            GameSessionManager.Instance.totalStrokes += playerController.strokes; //Se aduna doar la final stroke urile

            GameSessionManager.Instance.StopSession(); // Oprim cronometrul
        }

        StartCoroutine(LoadNextScene());


    }

    void CheckScore()
    {
        int strokeCount = playerController.strokes;
        if(strokeCount == 1)
        {
            ratingText.text = "HOLE-IN-ONE!";
        }
        else if(strokeCount == par - 2)
        {
            ratingText.text = "EAGLE!";
        }
        else if(strokeCount == par-1)
        {
            ratingText.text = "BIRDIE!";
        }
        else if(strokeCount  == par)
        {
            ratingText.text = "PAR!";
        }
        else if(strokeCount == par + 1)
        {
            ratingText.text = "BOGEY!";
        }
        else if(strokeCount == par + 2)
        {
            ratingText.text = "DOUBLE BOGEY!";
        }
        else if(strokeCount == par + 3)
        {
            ratingText.text = "TRIPLE BOGEY!";
        }
        else if(strokeCount > par+3)
        {
            ratingText.text = "FOCUS!";
        }
        else
        {
            ratingText.text = "NICE!";
        }

    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3.2f);
        LevelManager.Instance.LoadNextLevel();
    }

}
