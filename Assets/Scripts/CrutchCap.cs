using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;

public class CrutchCap : NetworkBehaviour
{
    [SerializeField] GameObject vihjelappu;
    NetworkVariable<bool> isActivedNetwork = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    bool isActivated = false;
    bool noteSpawned = false;

    private void Update()
    {
        if(IsOwner && isActivated)
        {
            isActivedNetwork.Value = true;
        }

        if (!noteSpawned && isActivedNetwork.Value)
        {
            noteSpawned = true;
            GameObject _lappu = Instantiate(vihjelappu, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
            NetworkObject _lappuNO = _lappu.GetComponent<NetworkObject>();
            if (IsHost) _lappuNO.Spawn();

            NetworkManager.Destroy(GetComponent<FixedJoint>());
        }
    }

    public void OnSelectCap(SelectEnterEvent eventarg)
    {
        isActivated = true;
    }
}
