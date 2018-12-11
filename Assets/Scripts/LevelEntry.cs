using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{
    [SerializeField]
    private float levelLoadDelay = 1f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadPreviousLevel());
    }

    private IEnumerator LoadPreviousLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        int previousLevelBuildIndex = SceneManager.GetActiveScene().buildIndex - 1;

        SceneManager.LoadScene(previousLevelBuildIndex);
    }
}