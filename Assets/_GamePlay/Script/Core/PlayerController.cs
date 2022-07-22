using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BridgeRace.Core.Character.LogicSystem;
using BridgeRace.Core.Character.PhysicSystem;
using BridgeRace.Core.Character.NavigationSystem;
using BridgeRace.Core.Character.WorldInterfaceSystem;
using BridgeRace.Core.Data;
using BridgeRace.Core;
using BridgeRace.Core.Brick;

public class PlayerController : AbstractCharacter
{

    [SerializeField]
    WorldInterfaceModule WorldInterfaceModule;
    [SerializeField]
    AbstractNavigationModule NavigationModule;
    [SerializeField]
    AbstractLogicModule LogicModule;
    [SerializeField]
    AbstractPhysicModule PhysicModule;
    CharacterData Data;
    
    

    private CharacterWorldInterfaceSystem WorldInterfaceSystem;
    private CharacterNavigationSystem NavigationSystem;
    private CharacterLogicSystem LogicSystem;
    private CharacterPhysicSystem PhysicSystem;
    
    private void Awake()
    {
        Data = ScriptableObject.CreateInstance(typeof(CharacterData)) as CharacterData;
        WorldInterfaceSystem = new CharacterWorldInterfaceSystem(WorldInterfaceModule);
        NavigationSystem = new CharacterNavigationSystem(NavigationModule);
        LogicSystem = new CharacterLogicSystem(LogicModule);
        PhysicSystem = new CharacterPhysicSystem(PhysicModule);

        LogicSystem.SetCharacterInformation(ContainBrick,gameObject.GetInstanceID());
        LogicSystem.SetCharacterData(Data);
        NavigationSystem.SetCharacterInformation(gameObject.transform,gameObject.GetInstanceID());
        NavigationSystem.SetCharacterData(Data);

        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        #region Update Data Event
        WorldInterfaceSystem.OnUpdateData += NavigationSystem.ReceiveInformation;
        WorldInterfaceSystem.OnUpdateData += LogicSystem.ReceiveInformation;

        NavigationSystem.OnUpdateData += LogicSystem.ReceiveInformation;
        PhysicSystem.OnUpdateData += LogicSystem.ReceiveInformation;

        LogicSystem.Event.SetVelocity += PhysicSystem.SetVelocity;
        LogicSystem.Event.SetRotation += PhysicSystem.SetRotation;
        LogicSystem.Event.SetSmoothRotation += PhysicSystem.SetSmoothRotation;
        #endregion
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        #region Update Data Event
        WorldInterfaceSystem.OnUpdateData -= NavigationSystem.ReceiveInformation;
        WorldInterfaceSystem.OnUpdateData -= LogicSystem.ReceiveInformation;

        NavigationSystem.OnUpdateData -= LogicSystem.ReceiveInformation;
        PhysicSystem.OnUpdateData -= LogicSystem.ReceiveInformation;

        LogicSystem.Event.SetVelocity -= PhysicSystem.SetVelocity;
        LogicSystem.Event.SetRotation -= PhysicSystem.SetRotation;
        LogicSystem.Event.SetSmoothRotation -= PhysicSystem.SetSmoothRotation;
        #endregion
    }

    private void Update()
    {
        WorldInterfaceSystem.Run();
        NavigationSystem.Run();
        LogicSystem.Run();
        PhysicSystem.Run();
    }

    public override void ChangeColor(BrickColor color)
    {
        base.ChangeColor(color);
        LogicSystem.SetCharacterInformation(color);
        NavigationSystem.SetCharacterInformation(color);
    }
}
