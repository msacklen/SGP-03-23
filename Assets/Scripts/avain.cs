using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class avain : NetworkBehaviour
{
    [SerializeField] private GameObject lid;
    [SerializeField] GameObject highlight;
    private void Start()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Key")
        {
            GameObject _lappu = Instantiate(highlight, highlight.transform.position, Quaternion.identity);
            NetworkObject _lappuNO = _lappu.GetComponent<NetworkObject>();
            if (IsHost) _lappuNO.Spawn();

            if (IsHost) NetworkManager.Destroy(lid);


            Debug.Log("boxi aukee");
        }
    }
}
