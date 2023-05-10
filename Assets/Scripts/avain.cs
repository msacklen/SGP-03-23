using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class avain : NetworkBehaviour
{
    [SerializeField] GameObject puzzleBox;
    [SerializeField] GameObject puzzleAvain;
    [SerializeField] GameObject highlight;

    private void Start()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == puzzleAvain)
        {
            if (IsHost)
            {
                GameObject _hilight = Instantiate(highlight, highlight.transform.position, Quaternion.identity);
                NetworkObject _hilightNO = _hilight.GetComponent<NetworkObject>();
                _hilightNO.Spawn();
            }

            NetworkManager.Destroy(puzzleBox);

            GameObject[] pieces = GameObject.FindGameObjectsWithTag("Piece");
            foreach(GameObject piece in pieces)
            {
                if (IsHost) piece.transform.position = new Vector3(0, piece.transform.position.y, piece.transform.position.z);
            }

            Debug.Log("boxi aukee");
        }
    }
}
