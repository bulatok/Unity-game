using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    private enum State
    {
        Idle,
        Moving,
        Attacking,
        Damaged,
        Dead,
        Dashed
    }

    [SerializeField] public PlayerController player;
    private Animator animator;
    public Rigidbody2D rb;
    public float dashCoolDown = 3f; // 3 sec
    public float dashDistance = 5f;
    public float dashSpeed = 10f;
    private bool _canDash = true;
    
    private float _maxHp = 200;
    private float _hp;
    [SerializeField] EnemyInfoManager _enemyInfoManager;

    private State _state;
    public float speed = 3;

    void Start()
    {
        animator = GetComponent<Animator>();
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
                if (dir.magnitude < dashDistance && _canDash) // Dash
                {
                    rb.velocity = dir.normalized * dashSpeed;
                    _canDash = false;
                    _state = State.Dashed;
                    Invoke(nameof(FinishDash), dir.magnitude / dashSpeed);
                    Invoke(nameof(UnsetDashFlag), dashCoolDown);
                }
                else // Normal moving
                {
                    _state = State.Moving;
                    rb.velocity = dir.normalized * speed;
                }
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
        // Debug.Log(_hp + _state.ToString());
    }

    private void UnsetDashFlag()
    {
        _canDash = true;
    }

    private void FinishDash()
    {
        _state = State.Idle;
    }
    public void Attack()
    {
        if (_state != State.Dead && _state != State.Attacking)
        {
            animator.ResetTrigger("IsMoving");
            _state = State.Attacking;
            animator.SetTrigger("IsAttacking");
            player.Heal(-20);
            Invoke(nameof(UnsetAttackFlag), 0.5f);
        }
    }

    public void Damage(float delta)
    {
        if (_state == State.Dead)
            return;
        _state = State.Damaged;
        animator.SetTrigger("IsDamaged");
        _hp -= delta;
        _enemyInfoManager.Change(_hp / _maxHp);
        if (_hp <= 0)
        {
            rb.velocity = new Vector2(0, 0);
            animator.SetTrigger("IsDead");
            animator.ResetTrigger("IsDamaged");
            _state = State.Dead;
            Invoke(nameof(Die), 2);
            return;
        }

        Debug.Log(_state.ToString() + _hp);
        Invoke(nameof(UnsetDamageFlag), 1);
    }

    private void UnsetDamageFlag()
    {
        animator.ResetTrigger("IsDamaged");
        if (_state != State.Dead)
            _state = State.Idle;
    }

    private void UnsetAttackFlag()
    {
        animator.ResetTrigger("IsAttacking");
        _state = State.Idle;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}