using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Raycast alternative primary weapons</summary>
public class VulkanV64AutoCannons : Gun
{
    // Very specific to this gun
    public GameObject muzzle1;
    public GameObject muzzle2;
    public AudioSource muzzle1Audio;
    public AudioSource muzzle2Audio;
    private bool muzzle1Turn = true;

    const float ADDED_INACCURACY_PER_SHOT = .01f;
    const float REDUCED_INACCURACY_PER_SECOND = .04f;
    const float MAX_INACCURACY = .5f;
    const float PRIMARY_FIRE_RATE = 45f;
    const float SECONDARY_FIRE_RATE = 10f;
    float inaccuracy = 0f;

    public override PlayerWeaponType GetPlayerWeaponType()
    {
        return PlayerWeaponType.VulkanV64AutoCannons;
    }

    public override void Init()
    {
        lastFired = 0;
        fireRate = PRIMARY_FIRE_RATE;
        base.Init();
    }
    /// <summary>Fires a bullet out of either muzzle, alternating each turn.</summary>
    /// <param name="initialVelocity">The velocity of the gun when the bullet is shot.</param>
    public override void PrimaryFire(Vector3 initialVelocity)
    {
        fireRate = PRIMARY_FIRE_RATE;
        if (CanShootAgain())
        {
            inaccuracy += ADDED_INACCURACY_PER_SHOT;
            if(inaccuracy > MAX_INACCURACY)
            {
                inaccuracy = MAX_INACCURACY;
            }
            inaccuracy -= (Time.time - lastFired) * REDUCED_INACCURACY_PER_SECOND;
            if (inaccuracy < 0f)
            {
                inaccuracy = 0f;
            }
            lastFired = Time.time;
            Bullet bullet = bulletPool.SpawnFromPool();

            Vector3 shotDir;
            Debug.Log("Inaccuracy: " + inaccuracy);
            // Gun specific
            if (muzzle1Turn)
            {
                shotDir = muzzle1.transform.forward;
                shotDir.x += Random.Range(-inaccuracy, inaccuracy);
                bullet.Shoot(muzzle1.transform.position, shotDir, initialVelocity);
                muzzle1Audio.Play();
            }
            else
            {
                shotDir = muzzle2.transform.forward;
                shotDir.x += Random.Range(-inaccuracy, inaccuracy);
                bullet.Shoot(muzzle2.transform.position, shotDir, initialVelocity);
                muzzle2Audio.Play();
            }
            muzzle1Turn = !muzzle1Turn;
            ApplyRecoil(shotDir, bullet);
        }
    }

    public override void ReleasePrimaryFire(Vector3 initialVelocity)
    {
    }

    //TODO: Implement secondary fire
    public override void SecondaryFire(Vector3 initialVelocity)
    {
        fireRate = SECONDARY_FIRE_RATE;
        if (CanShootAgain())
        {
            inaccuracy -= (Time.time - lastFired) * REDUCED_INACCURACY_PER_SECOND;
            if (inaccuracy < 0f)
            {
                inaccuracy = 0f;
            }
            lastFired = Time.time;
            Bullet bullet = bulletPool.SpawnFromPool();

            Vector3 shotDir;
            // Gun specific
            if (muzzle1Turn)
            {
                shotDir = muzzle1.transform.forward;
                bullet.Shoot(muzzle1.transform.position, shotDir, initialVelocity);
                muzzle1Audio.Play();
            }
            else
            {
                shotDir = muzzle2.transform.forward;
                bullet.Shoot(muzzle2.transform.position, shotDir, initialVelocity);
                muzzle2Audio.Play();
            }
            muzzle1Turn = !muzzle1Turn;
            ApplyRecoil(shotDir, bullet);
        }
    }


    public override void ReleaseSecondaryFire(Vector3 initialVelocity)
    {

    }
}
