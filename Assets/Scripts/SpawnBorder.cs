using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBorder : MonoBehaviour
{
    private ObstacleSpawner spawner;
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnTriggerEnter2D(Collider2D collision) // activating obstacle spawner, after player collides with this gameobject. It is used in both double, and wall events.
    {


        if (collision.gameObject.CompareTag("Player"))
        {
            spawner.isSpawning = true;
        }
   }
}
