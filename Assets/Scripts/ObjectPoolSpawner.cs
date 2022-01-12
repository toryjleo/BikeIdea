using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSpawner : MonoBehaviour
{

    public EnemyAI objectToPool; 
    public Gun gunToPool;
    public GameObject player;
    private List<EnemyAI> pool;
    public float size;
    public float spawnDistance = 100; //the number of units away from the player that the enemy spawns 

    // Start is called before the first frame update
    void Start()
    {
        INIT(); //TODO: call init somewhere else if warrented. 
    }

    private void INIT()
    {
        pool = new List<EnemyAI>();

        
        

        for (int i = 0; i < size; i++)
        {

            //TODO:will have to create a general spawn method in the future so as not to doop code HERE&&HERE1
            Vector3 spawnVector = new Vector3(player.transform.position.x, player.transform.position.y, spawnDistance);
            Quaternion ranRot = Quaternion.Euler(0, Random.Range(0, 359), 0);


            spawnVector = ranRot * spawnVector;

            EnemyAI newEnemy = Instantiate(objectToPool, spawnVector, Quaternion.identity);


            newEnemy.Init();
            newEnemy.setUpEnemy(player);
            newEnemy.Despawn += op_ProcessCompleted; //this line adds the despawn event to this entity 
            newEnemy.gameObject.SetActive(true);


            pool.Add(newEnemy);
        }
    }

    // THis is the method that sets the entity to Deactive and bascially is uesd to kill the entitiy 
    public void op_ProcessCompleted(SelfDespawn entity)
    {
        entity.gameObject.SetActive(false);
        //TODO: Add Logic here to make sure Entity either remains in the pool or becomes a new entity
    }

    /// <summary>
    /// Respawns Enemy when they are detected as dead by the update functio
    /// </summary>
    /// <param name="deddude"> is the Enemy Ai that died </param>
    public void Respawn(EnemyAI deddude)
    {

        //TODO: Doop code HERE&&HERE1 
        Vector3 spawnVector = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z+spawnDistance);
        Quaternion ranRot = Quaternion.Euler(0, Random.Range(0, 359), 0);
        spawnVector = ranRot * spawnVector;

        deddude.gameObject.transform.position = spawnVector;
        deddude.Init();
        deddude.gameObject.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {
        
        
        foreach (EnemyAI g in pool)
        {
            

           

            if(g.gameObject.activeSelf)
            {
                g.seperate(pool); //
            } else
            {
                

                print("dead Dude");
                Respawn(g); 
            }

            

          
        }
    }
}
