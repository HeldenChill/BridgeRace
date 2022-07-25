using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BridgeRace.Core.Brick;
using BridgeRace.Core;

public static class Cache 
{
    private static Dictionary<Collider, BridgeBrick> collider2BridgeBricks = new Dictionary<Collider, BridgeBrick>();
    private static Dictionary<Collider, EatBrick> collider2EatBricks = new Dictionary<Collider, EatBrick>();
    private static Dictionary<GameObject, EatBrick> gameobject2EatBricks = new Dictionary<GameObject, EatBrick>();
    private static Dictionary<GameObject, Bridge> gameobject2Bridges = new Dictionary<GameObject, Bridge>();

    private static Dictionary<Collider, AbstractCharacter> collider2Character = new Dictionary<Collider, AbstractCharacter>();
    public static BridgeBrick GetBridgeBrick(Collider col)
    {
        if (!collider2BridgeBricks.ContainsKey(col))
        {
            collider2BridgeBricks.Add(col, col.gameObject.GetComponent<BridgeBrick>());
        }
        return collider2BridgeBricks[col];
    }

    public static EatBrick GetEatBrick(GameObject gameObject)
    {
        if (!gameobject2EatBricks.ContainsKey(gameObject))
        {
            gameobject2EatBricks.Add(gameObject, gameObject.GetComponent<EatBrick>());
        }
        return gameobject2EatBricks[gameObject];
    }
    public static EatBrick GetEatBrick(Collider col)
    {
        if (!collider2EatBricks.ContainsKey(col))
        {
            EatBrick brick = col.gameObject.GetComponent<EatBrick>();
            collider2EatBricks.Add(col, brick);
            if (!gameobject2EatBricks.ContainsKey(col.gameObject))
            {
                gameobject2EatBricks.Add(col.gameObject, brick);
            }
        }
        return collider2EatBricks[col];
    }


    public static Bridge GetBridge(GameObject obj)
    {
        if (!gameobject2Bridges.ContainsKey(obj))
        {
            gameobject2Bridges.Add(obj, obj.GetComponent<Bridge>());
        }
        return gameobject2Bridges[obj];
    }

    public static AbstractCharacter GetCharacter(Collider col)
    {
        if (!collider2Character.ContainsKey(col))
        {
            collider2Character.Add(col, col.gameObject.GetComponent<AbstractCharacter>());
        }
        return collider2Character[col];
    }
}
