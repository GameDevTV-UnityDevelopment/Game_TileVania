using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private void Awake()
    {
        int numberOfScenePersists = FindObjectsOfType<ScenePersist>().Length;

        if (numberOfScenePersists > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}