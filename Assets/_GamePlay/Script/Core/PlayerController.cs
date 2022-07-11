using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BridgeRace.Core.Character.LogicSystem;
using BridgeRace.Core.Character.PhysicSystem;
using BridgeRace.Core.Character.NavigationSystem;
using BridgeRace.Core.Character.WorldInterfaceSystem;
public class PlayerController : MonoBehaviour
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
    }

    private void OnEnable()
    {
        #region Update Data Event
        WorldInterfaceSystem.Data.OnUpdateData += NavigationSystem.ReceiveInformation;
        WorldInterfaceSystem.Data.OnUpdateData += LogicSystem.ReceiveInformation;

        NavigationSystem.Data.OnUpdateData += LogicSystem.ReceiveInformation;
        PhysicSystem.Data.OnUpdateData += LogicSystem.ReceiveInformation;

        LogicSystem.Event.SetVelocity += PhysicSystem.SetVelocity;
        #endregion
    }

    private void OnDisable()
    {
        #region Update Data Event
        WorldInterfaceSystem.Data.OnUpdateData -= NavigationSystem.ReceiveInformation;
        WorldInterfaceSystem.Data.OnUpdateData -= LogicSystem.ReceiveInformation;

        NavigationSystem.Data.OnUpdateData -= LogicSystem.ReceiveInformation;
        PhysicSystem.Data.OnUpdateData -= LogicSystem.ReceiveInformation;

        LogicSystem.Event.SetVelocity -= PhysicSystem.SetVelocity;
        #endregion
    }

    private void Update()
    {
        WorldInterfaceSystem.Run();
        NavigationSystem.Run();
        LogicSystem.Run();
    }
}
