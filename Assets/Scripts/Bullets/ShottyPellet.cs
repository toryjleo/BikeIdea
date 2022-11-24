using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShottyPellet : Bullet
{
    public override void Init()
    {
        muzzleVelocity = 90;
        mass = .002f; //The Mass controlls how slowed down the bike is by recoil
        damageDealt = 2;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            // TracerMesh should have a Health component
            Health otherHealth = other.GetComponentInChildren<Health>();
            float z = otherHealth.HitPoints;
            otherHealth.TakeDamage(damageDealt);
        }
    }
}
