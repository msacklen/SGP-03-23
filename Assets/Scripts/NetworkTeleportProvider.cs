using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkTeleportProvider : TeleportationProvider
{
    //Bool: Does player have right to move rig
    public bool enableTeleportation;

    //If player doesn't have right, disable teleport queue
    public override bool QueueTeleportRequest(TeleportRequest teleportRequest)
    {
        if (!enableTeleportation) return false;
        return base.QueueTeleportRequest(teleportRequest);
    }
}
