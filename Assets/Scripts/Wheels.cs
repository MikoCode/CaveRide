using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour

{
    public PlayerController playerCon;
    public GameManager gM;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerCon.isAlive == true && gM.isPaused == false) // rotating wheels
        {
            transform.Rotate(new Vector3(0, 0, -1) * playerCon.wheelSpeed * Time.deltaTime * 40);
        }
       
        
       
    }
}
