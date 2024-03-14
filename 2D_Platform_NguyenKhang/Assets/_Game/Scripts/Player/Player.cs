using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float horizontal;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform kunaiPoint;
    [SerializeField] private GameObject attackArea;

    private bool isGrounded;
    private bool isJumping=false;
    private bool isAttack=false;

    private Vector3 savePoint;
    private int coin=0;


    // Start is called before the first frame update
    public override void Start()
    {
        SavePoint();
        coin=PlayerPrefs.GetInt(KeyConstant.KEY_COIN);
        UIManager.instance.SetCoin(coin);
        base.Start();
        
    }
    public override  void OnInit()
    {
        base.OnInit();
        isAttack = false;
        transform.position = savePoint;
        ChangeAmin(StateAmin.idle.ToString());
        DeActiveAttackArea();
        
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    public override void OnDeath()
    {
        base.OnDeath();
        Invoke(nameof(OnInit), 2f);
    }
    // Update is called once per frame
    void Update()
    {

        isGrounded=  CheckGrounded();
        //horizontal = Input.GetAxisRaw("Horizontal");

        if(IsDead)
        {
            return;
        }
        if(isAttack)
        {
            return;
        }
        if(rb.velocity.y<0 &&!isGrounded)
        {
            ChangeAmin(StateAmin.fall.ToString());
            isJumping = false;
        }
        //moving
        if (isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
            if (!isJumping && Mathf.Abs(horizontal) > 0.5f && horizontal != 0)
            {
                ChangeAmin(StateAmin.run.ToString());
            }
            if(!isJumping && Mathf.Abs(horizontal)< 0.5f)
            {
                ChangeAmin(StateAmin.idle.ToString());
                rb.velocity = Vector2.zero;
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                Throw();
            }
        }
        if (Mathf.Abs(horizontal) > 0.1f && horizontal != 0)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
            //xoay huong nhan vat
            //transform.localScale = new Vector3(horizontal, 1, 1);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
            heathBar.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position,Vector2.down,1.3f,groundLayer);
        return hit.collider != null;
    }
    public void Attack()
    {
        if (isGrounded)
        {
            ChangeAmin(StateAmin.attack.ToString());
            isAttack = true;
            Invoke(nameof(ResetIdle), 0.5f);
            ActiveAttackArea();
            Invoke(nameof(DeActiveAttackArea), 0.5f);
        }
    }
    
    public void Throw()
    {
        if (isGrounded)
        {

            ChangeAmin(StateAmin.throww.ToString());
            isAttack = true;
            Instantiate(kunaiPrefab, kunaiPoint.position, kunaiPoint.rotation);
            Invoke(nameof(ResetIdle), 0.5f);
        }
    }
    public void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            ChangeAmin(StateAmin.jump.ToString());
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
    public void setHorizontal(float horizontal)
    {
        this.horizontal = horizontal;
    }
    private void ResetIdle()
    {
        ChangeAmin(StateAmin.idle.ToString());
        isAttack = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tag.Coin.ToString())
        {
            Destroy(collision.gameObject);
            coin++;
            UIManager.instance.SetCoin(coin);
            PlayerPrefs.SetInt(KeyConstant.KEY_COIN,coin);
        }
        if (collision.tag == Tag.DeadZone.ToString())
        {
            ChangeAmin(StateAmin.die.ToString());
            Invoke(nameof(OnInit), 1f);
        }
    }
    internal void SavePoint()
    {
        savePoint = transform.position;
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
