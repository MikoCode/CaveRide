using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision) // Destroing objects that player leaves behind.
    {
        if(collision.gameObject.tag != "Boost" || collision.gameObject.tag != "bullet")
       {
            //Destroy(collision.gameObject);
        }
      
    }
}
