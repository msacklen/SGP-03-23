using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class avain : MonoBehaviour
{
    [SerializeField] private GameObject lid;
    [SerializeField] private GameObject puzzle;

    private void Start()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Key")
        {
            Destroy(lid);
            puzzle.transform.position = new Vector3(0, 0, 0);
            Rigidbody[] pieces = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody piece in pieces)
            {
                piece.isKinematic = false;
            }
        }
    }
}
