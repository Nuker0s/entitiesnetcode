using System.Collections;
using System.Collections.Generic;
using Unity.NetCode;
using UnityEngine;



[UnityEngine.Scripting.Preserve]
public class ServerSystem : ClientServerBootstrap
{
    public override bool Initialize(string defaultWorldName)
    {
        AutoConnectPort = 7979; // Enabled auto connect
        return base.Initialize(defaultWorldName); // Use the regular bootstrap
    }

}
