using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float moveSpeed;
    public static CameraFollow instance;
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if(target!=null)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.fixedDeltaTime);
    }
}
