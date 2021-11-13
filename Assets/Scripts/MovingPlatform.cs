using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float speed;
    private float pos1, pos2, nextPos;
    private PlayerController playerCon;
    private int target;
    private float min, max;
    
    // Start is called before the first frame update
    void Start()
    {
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Speed();
        speed = Random.Range(min, max);
        pos1 = transform.position.y + 3.7F;
        pos2 = transform.position.y - 3.7F;
        nextPos = pos1;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y == pos1)
        {
            nextPos = pos2;
        }
        if(transform.position.y == pos2)
        {
            nextPos = pos1;
        }
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, nextPos), speed * Time.deltaTime);
    }


    private void Update()
    {
       
    }



    void Speed()
    {
        if(playerCon.distance < 500)
        {
            min = 3;
            max = 8;
        } 
        else if(playerCon.distance < 1200 && playerCon.distance > 500)
        {
            min = 3.5f;
            max = 8.5f;
        }
        else if (playerCon.distance < 2000 && playerCon.distance > 1200)
        {
            min = 4f;
            max = 8.5f;
        }
        else if (playerCon.distance < 2600 && playerCon.distance > 2000)
        {
            min = 4.5f;
            max = 9f;
        }
        else if (playerCon.distance < 3300 && playerCon.distance > 2600)
        {
            min = 5f;
            max = 9f;
        }
    }
}
