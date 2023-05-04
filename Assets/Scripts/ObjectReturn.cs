using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReturn : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        InteractableObject otherObject = other.gameObject.GetComponent<InteractableObject>();
        otherObject.ReturnObject(transform.position);
    }
}
