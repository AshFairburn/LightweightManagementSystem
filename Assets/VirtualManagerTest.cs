using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightweightManagementSystem;

class VirtualScoreManager : VirtualManager
{
    public override void OnManagerRegistered(CoreBehaviour coreBehaviour)
    {
        Debug.Log("Virtual score manager initialized");
    }

    public override void OnManagerUnregistered()
    {
        Debug.Log("Virtual score manager deinitialized");
    }
}

public class VirtualManagerTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        VirtualScoreManager scoreManager = new VirtualScoreManager();
        scoreManager.Initialize();

        VirtualScoreManager sM = CoreBehaviour.GetFirstManager<VirtualScoreManager>();
        sM.Deinitialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
