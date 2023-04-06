using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Networking;

public class NetworkGrabbableObject : NetworkBehaviour
{
    NetworkVariable<Vector3> velocity = new NetworkVariable<Vector3>();
    [SerializeField] Rigidbody rb;

    private void Update()
    {
        if (IsOwner)
        {
            RequestVelocityUpdateServerRpc(rb.velocity);
        }
    }

    [ServerRpc]
    void RequestVelocityUpdateServerRpc(Vector3 _velocity)
    {
        velocity.Value = _velocity;
    }

    public void OnServerOwnershipClaim()
    {
        if (IsOwnedByServer)
        {
            rb.isKinematic = false;
            rb.velocity = velocity.Value;
        }
    }
}
