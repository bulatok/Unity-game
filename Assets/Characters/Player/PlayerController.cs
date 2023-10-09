using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 1f;

    public int bulletNum = 30;
    
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    public float maxHp = 100f;

    private float currentHp;

    private Vector3 mousePos;
    public GameObject bulletPrefab;

    // public Transform bulletTransform;
    private Camera cam;


    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;


    private int curDirection;
    // 1 - left
    // 2 - right
    // 3 - down
    // 4 - top


    // [SerializeField] GameObject projectilePrefab;
    // private GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        currentHp = maxHp;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        curDirection = 2;
    }

    private void FixedUpdate() {
        if (movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);
            
            if(!success) {
                success = TryMove(new Vector2(movementInput.x, 0));
            } 

            if(!success) {
                success = TryMove(new Vector2(0, movementInput.y));
            }
            animator.SetBool("isMoving", success);
        } else {
            animator.SetBool("isMoving", false);
        }

        // Set direction of sprite to movement direction
        if(movementInput.x < 0) {
            curDirection = 1;
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0) {
            curDirection = 2;
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction) {
        if(direction != Vector2.zero) {
            int count = rb.Cast(
                direction, 
                movementFilter, 
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if(count == 0){
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    public float GetCurrentHP()
    {
        return currentHp;
    }

    public int GetBulletNum()
    {
        return bulletNum;
    }

    void OnFire() {
        if (bulletNum == 0) {
            return;
        }
        bulletNum -= 1;
        animator.SetTrigger("bulletAttack");

        var screenCenter = new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2);

        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
