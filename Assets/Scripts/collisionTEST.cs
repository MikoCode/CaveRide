using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionTEST : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * 2);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("XD");
        if(collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
