using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] Vector2 spawnArea = new Vector2(-10f, 10f);
    GameObject[] floors;

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

        NetworkManager.SceneManager.OnSceneEvent += SceneEvent;
    }

    void SceneEvent(SceneEvent sceneEvent)
    {
        if(sceneEvent.SceneEventType == SceneEventType.LoadEventCompleted && IsClient && IsOwner)
        {
            Debug.Log("New scene loaded");
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

            clientCamera.gameObject.SetActive(false);
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
}
