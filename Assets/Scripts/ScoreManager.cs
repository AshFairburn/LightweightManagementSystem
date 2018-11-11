using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightweightManagementSystem;

public class ScoreManager : MonoManager
{
    [SerializeField]
    private int score;

    public override void OnManagerRegistered(CoreBehaviour coreBehaviour)
    {

    }

    public override void OnManagerUnregistered()
    {

    }

    public void AddScore(int score)
    {
        this.score += score;
    }
}