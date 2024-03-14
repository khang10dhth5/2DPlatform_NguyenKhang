using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator amin;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] private TextCombat textCombat;
    private string currentAmin=null;
    private float hp;

    public bool IsDead => hp <= 0;//khi hp<=0 thì isDead là true
    


    // Start is called before the first frame update
    public virtual void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void OnInit()
    {
        hp = 100;
        heathBar.OnInit(100);
    }
    public virtual void OnDespawn()
    {

    }
    public virtual void OnDeath()
    {
        ChangeAmin(StateAmin.die.ToString());
        Invoke(nameof(OnDespawn), 1.5f);
    }
    protected void ChangeAmin(string aminName)
    {
        if(amin==null)
        {
            amin = GetComponent<Animator>();
        }
        if(currentAmin==null)
        {
            currentAmin = StateAmin.idle.ToString();
        }
        if (currentAmin != aminName)
        {
            amin.ResetTrigger(currentAmin);
            currentAmin = aminName;
            amin.SetTrigger(currentAmin);
        }
    }
    public void OnHit(float damage)
    {
        if(!IsDead)
        {
            hp -= damage;
            if(IsDead)
            {
                hp = 0;
                OnDeath();
            }
            heathBar.SetHp(hp);
            Instantiate(textCombat, transform.position+Vector3.up, Quaternion.identity).OnInit(damage);
        }
        
    }



}
