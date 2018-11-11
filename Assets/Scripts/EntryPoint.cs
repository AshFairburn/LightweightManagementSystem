using UnityEngine;
using UnityEngine.SceneManagement;

namespace LightweightManagementSystem
{
    public static class EntryPoint
    {
        private static void FetchManagersInScene(Scene scene)
        {
            CoreBehaviour coreBehaviour = CoreBehaviour.Instance;

            GameObject[] rootObjects = scene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                IManager[] managers = obj.GetComponents<IManager>();
                foreach (IManager manager in managers)
                {
                    //if(coreBehaviour.AddManager(manager))
                    //{
                    //    Debug.Log("Registering " + manager.GetType());
                    //}
                }
            }
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            // Fetch all managers in the active scene
            FetchManagersInScene(scene);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoaded()
        {
            // Create the core behaviour
            CoreBehaviour.CreateCoreBehaviour();

            // Register to the unity scene load
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
}