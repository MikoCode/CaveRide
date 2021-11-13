using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 12f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * 18 * Time.deltaTime);  //Bullets move left 
        transform.Rotate(new Vector3(3, 0, 0) * Time.deltaTime * 40);
    }


    
}
