using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

public class NetworkPlayer : NetworkBehaviour
{
    GameObject[] floors;
    GameObject[] gameObjects;
    [SerializeField] GameObject body;
    [SerializeField] GameObject head;

    public override void OnNetworkSpawn()
    {
        DisableClientInput();
        floors = GameObject.FindGameObjectsWithTag("Floor");
        if (IsClient && IsOwner && floors.Length > 0)
        {
            foreach (GameObject floor in floors)
            {
                floor.GetComponent<TeleportationArea>().teleportationProvider = GetComponent<TeleportationProvider>();
            }
        }

        NetworkManager.SceneManager.OnSceneEvent += SceneEvent;
    }

    void SceneEvent(SceneEvent sceneEvent)
    {
        if(sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted && IsClient && IsOwner)
        {
            Debug.Log("New scene loaded");
            floors = GameObject.FindGameObjectsWithTag("Floor");
            if (floors.Length > 0)
            {
                foreach (GameObject floor in floors)
                {
                    floor.GetComponent<TeleportationArea>().teleportationProvider = GetComponent<TeleportationProvider>();
                }
            }
        }

        if (sceneEvent.SceneName == "EndScreen" && sceneEvent.SceneEventType == SceneEventType.LoadComplete)
        {
            gameObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            foreach (GameObject go in gameObjects)
            {
                if (go.TryGetComponent<NetworkObject>(out NetworkObject no) && go.tag != "Player")
                {
                    NetworkManager.Destroy(go);
                }

                transform.position = new Vector3(0, 0, 0);
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

        if (IsClient && IsOwner)
        {
            body.SetActive(false);
            head.SetActive(false);
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
    public void RequestGrabbableOwnershipRemoveServerRpc(NetworkObjectReference networkObjectReference)
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
