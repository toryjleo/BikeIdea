using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NotifyShot(Vector3 force);  // delegate

public class Gun : MonoBehaviour
{
    public Bullet bulletPrefab;
    private static BulletPool bulletPool;
    private float FireRate = 15;  // The number of bullets fired per second
    float lastFired = 0;


    // Very specific to this gun
    public GameObject muzzle1;
    public GameObject muzzle2;
    private bool muzzle1Turn = true;

    public event NotifyShot BulletShot; // event

    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init() 
    {
        bulletPool = new BulletPool(bulletPrefab);
    }

    public void Shoot(Vector3 initialVelocity) 
    {
        if (Time.time - lastFired > 1 / FireRate)
        {
            lastFired = Time.time;
            Bullet bullet = bulletPool.SpawnFromPool();

            Vector3 shotDir;

            // Gun specific
            if (muzzle1Turn)
            {
                shotDir = muzzle1.transform.forward;
                bullet.Shoot(muzzle1.transform.position, shotDir, initialVelocity);
            }
            else
            {
                shotDir = muzzle2.transform.forward;
                bullet.Shoot(muzzle2.transform.position, shotDir, initialVelocity);
            }
            muzzle1Turn = !muzzle1Turn;
            OnBulletShot(shotDir * bullet.Mass * bullet.muzzleVelocity);
        }
    }

    protected virtual void OnBulletShot(Vector3 forceOfBullet) //protected virtual method
    {
        //if BulletDespawn is not null then call delegate
        BulletShot?.Invoke(-forceOfBullet);
    }
}