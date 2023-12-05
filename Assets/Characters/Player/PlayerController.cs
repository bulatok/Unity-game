using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float attackCoolDown = 0.5f;

    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    public float maxHp = 100f;
    public float maxStamina = 3f;

    private float currentStamina;
    private float currentHp;

    private Vector3 mousePos;

    public InventoryManager inventory;
    
    private Camera cam;
    
    [SerializeField] RectTransform fader;
    

    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Animator animator;
    SpriteRenderer spriteRenderer;

	
	[SerializeField]
    private GameObject enemyZoneObject;

    private int curDirection;
    // 1 - left
    // 2 - right
    // 3 - down
    // 4 - top
    
    void Start() {
		enemyZoneObject.SetActive(false);
        cam = Camera.main;
        currentHp = maxHp;
        currentStamina = maxStamina / 2;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        curDirection = 2;
    }

    private void FixedUpdate() {
        if (currentHp <= 0) {
            Invoke(nameof(Die), 2);
            return;
        }
        
        RegenStamina();
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
			inventory.flipWeapon(true);
        } else if (movementInput.x > 0) {
            curDirection = 2;
            spriteRenderer.flipX = false;
			inventory.flipWeapon(false);
        }
    }

    private bool TryMove(Vector2 direction)
    {
        float distance = moveSpeed * Time.fixedDeltaTime * inventory.getPlayerSpeedCoef();
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0) // dash
            distance *= 2;
        if(direction != Vector2.zero) {
            int count = rb.Cast(
                direction, 
                movementFilter, 
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                distance + collisionOffset); // The amount to cast equal to the movement plus an offset
            if(count == 0){
                rb.MovePosition(rb.position + direction * distance);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
    }

    void RegenStamina() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            currentStamina -= 1 * Time.fixedDeltaTime;
        } else {
            currentStamina += 0.5f * Time.fixedDeltaTime;
        }
    }
    
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }

    public float GetCurrentHP() {
        return currentHp;
    }

    private bool _canFire = true;
    void OnFire() {
        if (currentHp <= 0 || !_canFire) {
            return;
        }

        _canFire = false;

        Invoke(nameof(UnsetFireFlag), attackCoolDown);
        // Weapon
        inventory.Shoot(transform.position);
    }

    private void UnsetFireFlag() {
        _canFire = true;
    }

    public void Heal(float delta) {
        currentHp = Mathf.Min(currentHp + delta, maxHp);
        if (delta < 0)
            animator.SetTrigger("bulletAttack");
    }
    
    private void Die() {
        Destroy(gameObject);

        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene("Level1");
        });
        
        // SceneManager.LoadScene("Level1");
    }
	
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyZone"))
        {
			enemyZoneObject.SetActive(true);
		}
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }
}
