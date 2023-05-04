using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class avain : MonoBehaviour
{
    [SerializeField] private GameObject lid;
    [SerializeField] private GameObject puzzle;
    [SerializeField] GameObject highlight;
    private void Start()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Key")
        {
            GameObject _lappu = Instantiate(highlight, new Vector3(1.85f, 1.012f, -1.092f), Quaternion.identity);
            NetworkObject _lappuNO = _lappu.GetComponent<NetworkObject>();
            _lappuNO.Spawn();

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
