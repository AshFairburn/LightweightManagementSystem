using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightweightManagementSystem;

public class ManagerA : MonoManager
{
    public override void OnManagerRegistered(CoreBehaviour coreBehaviour)
    {
        Debug.Log("Manager Registered");
    }

    public override void OnManagerUnregistered()
    {
        Debug.Log("Manager Unregistered");
    }

    protected override void PostAwake()
    {

    }

    protected override void PostDestroy()
    {

    }
}