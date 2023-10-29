using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAI : InfantryAI
{
    public GameObject muzzleLocation; // Empty GameObject set to the location of the barrel

    public override void Init()
    {
        alive = true;
        StartingHP = 20;
        score = 300;
        dlScore = 5;
        maxSpeed = 20;
        attackRange = 15;
        minimumRange = 5;
        speedBoost = 30;

        hp = GetComponentInChildren<Health>();
        rb = GetComponent<Rigidbody>();
        animationStateController = GetComponent<CyborgAnimationStateController>();
        this.Despawn += op_ProcessCompleted;
        hp.Init(StartingHP);

        #region Error Checkers

        if (animationStateController == null)
        {
            Debug.LogError("This object needs a CyborgAnimationStateController component");
        }
        if (rb == null)
        {
            Debug.LogError("This object needs a rigidBody component");
        }
        if (hp == null)
        {
            Debug.LogError("This object needs a health component");
        }
        #endregion
    }



    //stats used in construction

    public float hitpoints
    {
        get => hp.HitPoints;
    }

    void Awake()
    {
        Init();
    }

    public override void Update()
    {
        base.Update();
    }


}
