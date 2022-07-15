using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BridgeRace.Core.Character.LogicSystem;
using BridgeRace.Core.Character.PhysicSystem;
using BridgeRace.Core.Character.NavigationSystem;
using BridgeRace.Core.Character.WorldInterfaceSystem;
using BridgeRace.Core.Data;
using BridgeRace.Core;


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

    
    

    private CharacterWorldInterfaceSystem WorldInterfaceSystem;
    private CharacterNavigationSystem NavigationSystem;
    private CharacterLogicSystem LogicSystem;
    private CharacterPhysicSystem PhysicSystem;
    
    private void Awake()
    {
        WorldInterfaceSystem = new CharacterWorldInterfaceSystem(WorldInterfaceModule);
        NavigationSystem = new CharacterNavigationSystem(NavigationModule);
        LogicSystem = new CharacterLogicSystem(LogicModule);
        PhysicSystem = new CharacterPhysicSystem(PhysicModule);

        LogicSystem.SetCharacterInformation(type, ContainBrick);
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
        #endregion
    }

    private void Update()
    {
        WorldInterfaceSystem.Run();
        NavigationSystem.Run();
        LogicSystem.Run();
        PhysicSystem.Run();
    }
}
