using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed;
    [SerializeField] float damage;
    [SerializeField] GameObject hitVFX;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    private void OnInit()
    {
        rb.velocity = transform.right * moveSpeed;
        Invoke(nameof(OnDespawn), 3f);
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Enemy")
        {
            collision.GetComponent<Character>().OnHit(damage);
            GameObject hit= Instantiate(hitVFX, transform.position, transform.rotation);
            Destroy(hit, 0.5f);
            Destroy(gameObject);
        }
    }
}
