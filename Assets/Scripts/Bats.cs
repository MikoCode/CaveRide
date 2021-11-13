using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bats : MonoBehaviour
{
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
        speed = Random.Range(4, 9); 
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }



    void Moving()  //Making Bats fly,their position is determined in Obstacle Spawner script.
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
    
}
