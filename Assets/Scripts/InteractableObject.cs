using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class InteractableObject : NetworkBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void ReturnObject(Vector3 returnPos)
    {
        if (IsOwner)
        {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = returnPos;
        }
    }
}
