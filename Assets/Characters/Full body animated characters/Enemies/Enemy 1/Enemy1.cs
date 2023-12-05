using UnityEngine;

public class Enemy1 : MonoBehaviour, IDamagable
{
    private enum State {
        Idle, Moving, Damaged, Dead
    }

    public GameObject bulletPrefab;
    [SerializeField] public PlayerController player;
    private Animator animator;
    private Rigidbody2D rb;

    private float _maxHp = 20;
    private float _hp;

    private State _state;
    public float speed = 2;

    public float attackCD = 2;
    private bool _canAttack = true;
    
    private AudioSource attackSound;

    void Start() {
        attackSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        _state = State.Idle;
        _hp = _maxHp;
        rb.freezeRotation = true;
    }


    void Update()
    {
        if (_state == State.Dead)
            return;

        if (_state == State.Idle || _state == State.Moving)
        {
            var position = player.transform.position;
            var position1 = rb.position;
            Vector2 dir = new Vector2(position.x - position1.x, position.y - position1.y);
            if (dir.magnitude > 0.2)
            {
                animator.SetTrigger("IsMoving");
                _state = State.Moving;
                rb.velocity = dir.normalized * speed;
                if (Mathf.Abs(Vector2.Angle(transform.right, dir)) > 90f)
                {
                    transform.Rotate(0, 180, 0);
                }
            }
            else
            {
                animator.ResetTrigger("IsMoving");
                _state = State.Idle;
            }
        }

        if (_state != State.Damaged && _state != State.Dead) {
            Attack(player.transform.position - transform.position);
        }
        // Debug.Log(_hp + _state.ToString());
    }

    void UnsetAttackFlag() {
        _canAttack = true;
    }

    public void Attack(Vector2 dir)
    {
        if (_state != State.Dead && _canAttack) {
            attackSound.Play(0);
            _canAttack = false;
            animator.ResetTrigger("IsMoving");
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * 7f;
            Invoke(nameof(UnsetAttackFlag), attackCD);
        }
    }

    public void Damage(float delta)
    {
        if (_state == State.Dead)
            return;
        _state = State.Damaged;
        animator.SetTrigger("Hit");
        _hp -= delta;
        if (_hp <= 0)
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetTrigger("Die");
            animator.ResetTrigger("Hit");
            _state = State.Dead;
            Invoke(nameof(Die), 0.6f);
            return;
        }
        Invoke(nameof(UnsetDamageFlag), 0.3f);
    }

    private void UnsetDamageFlag() {
        animator.ResetTrigger("Hit");
        if (_state != State.Dead)
            _state = State.Idle;
    }
    private void Die() {
        Destroy(gameObject);
    }
}