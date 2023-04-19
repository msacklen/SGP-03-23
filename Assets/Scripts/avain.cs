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
            puzzle.SetActive(true);
            NetworkObject puzzleNO = GetComponent<NetworkObject>();
            puzzleNO.Spawn();
            NetworkObject[] puzzlepieces = GetComponentsInChildren<NetworkObject>();
            foreach (NetworkObject piece in puzzlepieces)
            {
                if (piece.tag == "Piece") piece.Spawn();
            }
        }
    }
}
