using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightweightManagementSystem
{
    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoaded()
        {
            // Create the core behaviour
            CoreBehaviour.CreateCoreBehaviour();
        }
    }
}