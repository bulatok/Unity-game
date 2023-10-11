using UnityEngine;

public class HP : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.Heal(20);
            Destroy(gameObject);
        }
    }
}