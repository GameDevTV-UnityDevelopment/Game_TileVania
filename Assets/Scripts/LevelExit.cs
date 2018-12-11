using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private float levelLoadDelay = 1f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    private IEnumerator LoadNextLevel()
    {
        DestroyScenePersist();

        yield return new WaitForSecondsRealtime(levelLoadDelay);

        int nextLevelBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextLevelBuildIndex);
    }

    private static void DestroyScenePersist()
    {
        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();

        if (scenePersist)
        {
            Destroy(scenePersist.gameObject);
        }
    }
}