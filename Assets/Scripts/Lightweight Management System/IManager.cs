using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LightweightManagementSystem
{
    public interface IManager
    {
        void OnManagerRegistered(CoreBehaviour coreBehaviour);
        void OnManagerUnregistered();
    }
}