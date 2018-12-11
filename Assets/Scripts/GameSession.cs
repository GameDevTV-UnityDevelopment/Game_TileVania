using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private int playerLives = 3;

    [SerializeField]
    private Text uiLives;

    [SerializeField]
    private Text uiScore;

    private int score = 0;


    public void AddToScore(int points)
    {
        score += points;
        uiScore.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void Awake()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numberOfGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void ResetGameSession()
    {
        Destroy(gameObject);

        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        uiLives.text = playerLives.ToString();
        uiScore.text = score.ToString();
    }

    private void TakeLife()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        playerLives--;
        uiLives.text = playerLives.ToString();

        SceneManager.LoadScene(currentSceneIndex);
    }
}