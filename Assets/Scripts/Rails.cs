using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour
{
    
    public PlayerController playerCon;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        MovingHorizontally();
    }
    void MovingHorizontally()
    {
        transform.Translate(Vector2.right * playerCon.speed * Time.deltaTime);

    }

    
}
