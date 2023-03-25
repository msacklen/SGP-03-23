using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] Vector2 spawnArea = new Vector2(-10f, 10f);

    public override void OnNetworkSpawn()
    {
        DisableClientInput();
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

            clientCamera.enabled = false;
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

    private void Start()
    {
        if (IsClient && IsOwner)
        {
            transform.position = new Vector3(Random.Range(spawnArea.x, spawnArea.y), transform.position.y, Random.Range(spawnArea.x, spawnArea.y));
        }
    }
}
