using UnityEngine;

public class EnemyBullet : MonoBehaviour
{ 
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Unpassable")) {
            Destroy(gameObject);
        }
        if (other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().Heal(-20);
            Destroy(gameObject);
        }
    }
}
