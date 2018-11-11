using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightweightManagementSystem
{
    public class ExampleManager : MonoManager
    {
        public override void OnManagerRegistered(CoreBehaviour coreBehaviour)
        {
            // This is called when the manager has been added to the system. The
            // MonoManager automatically adds the manager in the Awake method so
            // this can basically be used as Awake, however if the manager fails
            // to be registered, this won't be called, so to also catch failure
            // cases it's best to use PostAwake() for definite Awake behaviour.

            Debug.Log("Example manager was added to the management system.");
        }

        public override void OnManagerUnregistered()
        {
            // This is called when the manager has been removed from the system.
            // The MonoManager automatically removes the manager upon destruction
            // to ensure references are not leaked in the case where a manager
            // is not removed and destroyed.

            Debug.Log("Example manager was removed from the management system.");
        }

        protected override void PostAwake()
        {
            // Used as a replacement for Awake() because it's used by the 
            // MonoManager for registration behaviour. This is called AFTER
            // the registration.

            Debug.Log("Standard Awake behaviour called.");
        }

        protected override void PostDestroy()
        {
            // Used as a replacement for OnDestroy() because it's used by the
            // MonoManager for unregistration behaviour. This is called AFTER
            // the unregistration.

            Debug.Log("Standard OnDestroy behaviour called.");
        }
    }
}