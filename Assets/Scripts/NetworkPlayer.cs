using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

public class NetworkPlayer : NetworkBehaviour
{
    GameObject[] floors;
    [SerializeField] Mesh[] bodyMeshes;
    [SerializeField] Mesh[] headMeshes;
    [SerializeField] MeshFilter bodyMeshFilter;
    [SerializeField] MeshFilter headMeshFilter;

    public override void OnNetworkSpawn()
    {
        DisableClientInput();
        floors = GameObject.FindGameObjectsWithTag("Floor");
        if (IsClient && IsOwner)
        {
            foreach (GameObject floor in floors)
            {
                floor.GetComponent<TeleportationArea>().teleportationProvider = GetComponent<TeleportationProvider>();
            }
        }

        //if (IsHost)
        //{
        //    bodyMeshFilter.mesh = bodyMeshes[1];
        //    headMeshFilter.mesh = headMeshes[0];
        //}

        NetworkManager.SceneManager.OnSceneEvent += SceneEvent;
    }

    void SceneEvent(SceneEvent sceneEvent)
    {
        if(sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted && IsClient && IsOwner)
        {
            Debug.Log("New scene loaded");
            floors = GameObject.FindGameObjectsWithTag("Floor");
            foreach (GameObject floor in floors)
            {
                floor.GetComponent<TeleportationArea>().teleportationProvider = GetComponent<TeleportationProvider>();
            }
        }
    }

    public void DisableClientInput()
    {
        //Disable XR rig movement if client is not owner of rig
        if (IsClient && !IsOwner)
        {
            NetworkTeleportProvider clientTeleportProvider = GetComponent<NetworkTeleportProvider>();
            ActionBasedController[] clientControllers = GetComponentsInChildren<ActionBasedController>();
            ActionBasedSnapTurnProvider clientTurnProvider = GetComponent<ActionBasedSnapTurnProvider>();
            TrackedPoseDriver clientHead = GetComponentInChildren<TrackedPoseDriver>();
            Camera clientCamera = GetComponentInChildren<Camera>();
            AudioListener clientListeners = GetComponentInChildren<AudioListener>();

            clientCamera.enabled = false;
            clientListeners.enabled = false;
            clientTeleportProvider.enableTeleportation = false;
            clientTurnProvider.enableTurnLeftRight = false;
            clientTurnProvider.enableTurnAround = false;
            clientHead.enabled = false;

            foreach (ActionBasedController controller in clientControllers)
            {
                controller.enableInputActions = false;
                controller.enableInputTracking = false;
            }
        }
    }

    public void OnSelectGrabbable(SelectEnterEventArgs eventArgs)
    {
        if (IsClient && IsOwner)
        {
            NetworkObject grabbable = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
            if (grabbable != null)
            {
                RequestGrabbableOwnershipServerRpc(OwnerClientId, grabbable);
            }
        }
    }

    public void OnDeselectGrabbable(SelectExitEventArgs eventArgs)
    {
        if (IsClient && IsOwner)
        {
            NetworkObject grabbable = eventArgs.interactableObject.transform.GetComponent<NetworkObject>();
            if (grabbable != null)
            {
                RequestGrabbableOwnershipRemoveServerRpc(grabbable);
            }
        }
    }

    [ServerRpc]
    public void RequestGrabbableOwnershipServerRpc(ulong newOwnerId, NetworkObjectReference networkObjectReference)
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
