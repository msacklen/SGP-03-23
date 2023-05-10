using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class puzzleLogic : NetworkBehaviour
{
    [SerializeField] private Vector3 lockedPlace;
    [SerializeField] private Vector3 lockedRotation = new Vector3(-90, 0, 0);

    public GameObject puzzle;


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lockedPlace, transform.position) < 0.33f && IsOwnedByServer)
        {
            Debug.Log("Puzzlepiece in place");
            transform.position = lockedPlace;
            transform.eulerAngles = lockedRotation;
            GetComponent<Rigidbody>().isKinematic = true;

            
            GetComponent<XRGrabInteractable>().enabled = false;
            GetComponent<puzzleLogic>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;

            //Debug.Log(total);
            if (IsHost) puzzle.GetComponent<universalpuzzle>().total += 1;

            Debug.Log(puzzle.GetComponent<universalpuzzle>().total);
        }
    }
}
