using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turbo : MonoBehaviour
{
   
    private PlayerController playerCon;
  

    
   
    
    // Start is called before the first frame update
    void Start()
    {
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Destroy(gameObject, 10f);
    }

  

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
      
            StartCoroutine("SpeedBoost");
            
           
           
        }

        if (collision.gameObject.CompareTag("obstacle")) // moving game object a little bit, if it collides with obstacle, the purpose of it is to make it easier to collect.
        {
            
            transform.position = new Vector2(transform.position.x + 3, transform.position.y);
        }
        

    }


    IEnumerator SpeedBoost() //Player moves faster by 5 times for one second. "isBoosted" bool is used to determine if player can hit obstacle. It is turned off later than speed,
        //                     to avoid situation when player hits obstacle immediately after slowing down, which could be very frustrating.
    {

        playerCon.particleSparks.startSize = 0.6f;
      
        playerCon.isBoosted = true;
        playerCon.speed *= 2.2f;
        playerCon.wheelSpeed *= 2.2f;
       

        yield return new WaitForSeconds(2.4f);

        playerCon.speed /= 2.2f;
        playerCon.wheelSpeed /= 2.2f;

        playerCon.particleSparks.startSize = 0.05f;
       
        yield return new WaitForSeconds(0.4f);
    
        playerCon.isBoosted = false;
        Destroy(gameObject, 3);


    }

    
}
