using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkGrabbableObject : NetworkBehaviour
{
    NetworkVariable<Vector3> velocity = new NetworkVariable<Vector3>();
    [SerializeField] Rigidbody rb;

    private void Update()
    {
        velocity.Value = rb.velocity;
    }

    public void OnServerOwnershipClaim()
    {
        if (IsOwnedByServer && IsHost)
        {
            rb.velocity = velocity.Value;
        }
    }
}
