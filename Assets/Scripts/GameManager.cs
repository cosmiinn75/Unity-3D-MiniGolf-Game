using System.Collections;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    public GameObject hole;
    public GameObject particle;
    public GameObject player;
    private string difficulty = "easy";
    public int par = 3;
    public TextMeshProUGUI ratingText; // birdie, par or bogey
    private PlayerController playerController;


    private void Start()
    {
        ratingText.text = "";
        playerController = player.GetComponent<PlayerController>();
        particle.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("isHole"))
        {
            GameOver();
        }
    }


    void GameOver()
    {
        CheckScore();
        particle.gameObject.SetActive(true);
       // StartCoroutine(LoadNextScene());  
    }

    void CheckScore()
    {
        int strokeCount = playerController.strokes;
        if(strokeCount == par-1)
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
        else if(strokeCount == 1)
        {
            ratingText.text = "HOLE-IN-ONE!";
        }

    }


    IEnumerator LoadNextScene()
    {

        yield return new WaitForSeconds(2.0f);

        if(difficulty == "easy") // daca dif e easy se incarca primele 3 scene 
        {
            SceneManager.LoadScene(Random.Range(1, 4));

        }
        else if(difficulty == "medium")
        {
            SceneManager.LoadScene(Random.Range(4, 7));
        }
        else if(difficulty == "hard")
        {
            SceneManager.LoadScene(Random.Range(7, 10));
        }
    }
}
