using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightweightManagementSystem;

namespace LightweightManagementSystem
{
    public class ExampleVirtualManagerCreator
    {
        public static void TestExampleManager()
        {
            // Create the new manager
            ExampleVirtualManager manager = new ExampleVirtualManager();

            // Initialize the manager, it will be registered automatically into the management system.
            manager.Initialize();

            // Get the manager through the core behaviour to demonstrate that it can be acquired from anywhere.
            ExampleVirtualManager managerReference = CoreBehaviour.GetFirstManager<ExampleVirtualManager>();

            // Deinitialize the manager and remove it from the management system.
            managerReference.Deinitialize();
        }
    }

    class ExampleVirtualManager : VirtualManager
    {
        public override void OnManagerRegistered(CoreBehaviour coreBehaviour)
        {
            // Registration for virtual managers are initiated from the Initialize()
            // method.

            Debug.Log("Virtual score manager initialized");
        }

        public override void OnManagerUnregistered()
        {
            // Unregistration for virtual managers are initiated from the Deinitialize()
            // method.

            Debug.Log("Virtual score manager deinitialized");
        }
    }
}