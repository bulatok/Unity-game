using UnityEngine;

public class AttractPlayerToPoint : MonoBehaviour
{
    [SerializeField] private Transform attractPoint; // Serialized field for the point to attract towards

    // private Vector3 attractPoint = new Vector3(-3.23f, -1.2f, 0f);
        
    [SerializeField] private float attractionSpeed = 5f; // Speed of attraction towards the point

    private bool playerInRange = false;
    
    public Transform playerTransform;
    
    public AudioSource attrackSound;

    private void Start()
    {
        attrackSound = GetComponent<AudioSource>();
    }

    private void Update()
    {   
        if (playerInRange)
        {
            AttractPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attrackSound.Play(0);
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void AttractPlayer()
    {
        if (attractPoint != null && playerTransform != null)
        {
            playerTransform.position = Vector3.MoveTowards(
                playerTransform.position,
                attractPoint.position,
                attractionSpeed * Time.deltaTime
            );
        }
        else
        {
            Debug.LogWarning("Attract Point or Player not assigned!");
        }
    }
}