using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 mousePos;

    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    private Camera cam;

    void Start() {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Unpassable")) {
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy")) {
            other.GetComponent<IDamagable>().Damage(20);
            Destroy(gameObject);
        }
    }
}
