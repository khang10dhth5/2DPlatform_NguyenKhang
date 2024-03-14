using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform kunaiPoint;
    [SerializeField] private GameObject attackArea;

    private IState currentState;

    private bool isRight = true;
    private Character target;
    public Character Target => target;


    public override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    void Update()
    {
        if(currentState!=null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAttackArea();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(gameObject);
        
    }
    public override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
        
    }
    public void ChangeState(IState newState)
    {
        if(currentState!=null)
        {
            currentState.OnExit(this);

        }
        currentState = newState;
        if (currentState != null)
        {
            currentState.OnEnter(this);

        }
        
    }

    internal void SetTarget(Character character)
    {
        this.target = character;
        if(IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if(target!=null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }
    public void Moving()
    {
        ChangeAmin(StateAmin.run.ToString());
        rb.velocity = transform.right * moveSpeed;
    }
    public void StopMoving()
    {
        ChangeAmin(StateAmin.idle.ToString());
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChangeAmin(StateAmin.attack.ToString());
        ActiveAttackArea();
        Invoke(nameof(DeActiveAttackArea), 0.5f);
    }
    public bool IsTargetInRange()
    {
        if(target==null)
        {
            return false;
        }
        return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
    }
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
        heathBar.transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tag.EnemyWall.ToString())
        {
            ChangeDirection(!isRight);
        }
    }
    private void ActiveAttackArea()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttackArea()
    {
        attackArea.SetActive(false);
    }
}
