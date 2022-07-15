using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BridgeRace.Core.Brick;

public static class Cache 
{
    private static Dictionary<Collider, BridgeBrick> bridgeBricks = new Dictionary<Collider, BridgeBrick>();
    public static BridgeBrick GetBridgeBrick(Collider col)
    {
        if (!bridgeBricks.ContainsKey(col))
        {
            bridgeBricks.Add(col, col.gameObject.GetComponent<BridgeBrick>());
        }
        return bridgeBricks[col];
    }
}
