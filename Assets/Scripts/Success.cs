using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Success : MonoBehaviour
{
    [SerializeField]
    private float levelLoadDelay = 1f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        FindObjectOfType<GameSession>().ResetGameSession();
    }
}