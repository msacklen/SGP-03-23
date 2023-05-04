using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class pihditt : NetworkBehaviour
{
    public Transform pihdit;
    public Transform paikkaavaimelle;
    private bool isActivated = false;
    public Transform kansi;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, paikkaavaimelle.position) < 0.1f && isActivated == true)
        {
            GetComponent<XRGrabInteractable>().enabled = true;
            transform.parent = pihdit;
            transform.position = paikkaavaimelle.position;
            rb.isKinematic = true;
            if (!IsOwner)
            {
                NetworkObject _pihditNO = pihdit.GetComponent<NetworkObject>();
                RequestGrabbableOwnershipServerRpc(_pihditNO.OwnerClientId, GetComponent<NetworkObject>());
            }
        }
        else
        {
            rb.isKinematic = false;
            if (IsOwner)
            {
                RequestGrabbableOwnershipRemoveServerRpc(GetComponent<NetworkObject>());
            }
        }
    }

    public void activated()
    {
        isActivated = true;
    }

    public void deactivate()
    {
        isActivated = false;
    }

    [ServerRpc]
    void RequestGrabbableOwnershipServerRpc(ulong newOwnerId, NetworkObjectReference networkObjectReference)
    {
        if (networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.ChangeOwnership(newOwnerId);
        }
        else
        {
            Debug.LogError("Unable to change grabbable ownership");
        }
    }

    [ServerRpc]
    void RequestGrabbableOwnershipRemoveServerRpc(NetworkObjectReference networkObjectReference)
    {
        if (networkObjectReference.TryGet(out NetworkObject networkObject))
        {
            networkObject.RemoveOwnership();
        }
        else
        {
            Debug.LogError("Unable to change grabbable ownership");
        }
    }
}
