using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ObjectReturn : NetworkBehaviour
{
    [SerializeField] GameObject objectReturn;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider other)
    {
        Collider _returnCollider = objectReturn.GetComponent<BoxCollider>();
        if(other == _returnCollider && IsOwner)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = objectReturn.transform.position;
        }
    }
}
