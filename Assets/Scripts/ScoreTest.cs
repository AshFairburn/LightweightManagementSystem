using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightweightManagementSystem;

public class ScoreTest : MonoBehaviour
{
    [SerializeField]
    private int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.R))
        {
            CoreBehaviour cb = CoreBehaviour.Instance;
            ScoreManager scoreManager = cb.GetFirst<ScoreManager>();
            scoreManager.AddScore(score);
        }
	}
}
