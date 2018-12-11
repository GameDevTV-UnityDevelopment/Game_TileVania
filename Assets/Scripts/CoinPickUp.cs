using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickupSFX;

    [SerializeField]
    private float volume = 0.25f;

    [SerializeField]
    private int points = 100;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(points);

        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position, volume);

        Destroy(gameObject);
    }
}