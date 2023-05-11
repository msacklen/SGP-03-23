using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;
using Unity.Netcode.Components;

public class puzzleLogic : NetworkBehaviour
{
    [SerializeField] private Vector3 lockedPlace;
    [SerializeField] private Vector3 lockedRotation = new Vector3(-90, 0, 0);
    NetworkVariable<bool> isLocked = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    public GameObject puzzle;


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lockedPlace, transform.position) < 0.33f && IsOwnedByServer && !isLocked.Value)
        {
            Debug.Log("Puzzlepiece in place");
            transform.position = lockedPlace;
            transform.eulerAngles = lockedRotation;

            if (IsHost) isLocked.Value = true;
            if (IsHost) puzzle.GetComponent<universalpuzzle>().total += 1;

        }

        if (isLocked.Value && TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            NetworkManager.Destroy(GetComponent<XRGrabInteractable>());
            NetworkManager.Destroy(GetComponent<NetworkRigidbody>());
            NetworkManager.Destroy(GetComponent<InteractableObject>());
            NetworkManager.Destroy(rb);
        }
    }
}
