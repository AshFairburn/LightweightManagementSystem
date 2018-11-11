using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightweightManagementSystem
{
    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoaded()
        {
            if (CoreBehaviour.IsCompileTimeEnabled()) // Ensure the core behaviour is enabled
            {
                CoreBehaviour.CreateCoreBehaviour();
            }
            else // Core behaviour is not enabled
            {
                Debug.LogWarning("Lightweight management system is currently disabled, add 'LWMS' to Scripting Define Symbols in the player settings to enable it.");
            }
        }
    }
}