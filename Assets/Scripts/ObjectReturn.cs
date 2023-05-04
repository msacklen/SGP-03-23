using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ObjectReturn : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InteractableObject otherObject = other.gameObject.GetComponentInParent<InteractableObject>();
        otherObject.ReturnObject(transform.position);
    }
}
