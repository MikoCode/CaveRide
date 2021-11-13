using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerPowers isProtected;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
        sprite = gameObject.GetComponent<SpriteRenderer>();
        isProtected = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowers>();
        Destroy(gameObject, 10f);
    }
    // Update is called once per frame
    void Update()
    {
       

    }
     public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isProtected.isProtected = true;
            sprite.enabled = false;
            Destroy(gameObject,1f);
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            transform.position = new Vector2(transform.position.x + 7, transform.position.y);
        }








    }




}
