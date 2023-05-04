using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ObjectReturn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        InteractableObject otherObject = other.gameObject.GetComponentInParent<InteractableObject>();
        if(otherObject != null) otherObject.ReturnObject(transform.position);
    }
}
