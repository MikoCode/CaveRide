using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private PlayerPowers playerPow;
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerPow = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPowers>();
        Destroy(gameObject, 16f);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0, 0, 1)); // rotating magnet powerup
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {
            StartCoroutine("Magnetic");
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
           
            transform.position = new Vector2(transform.position.x + 3, transform.position.y);
        }


    }


    IEnumerator Magnetic() // Activates Magnet PowerUp, disables sprite to make illusion of collecting item. Can't destroy yet,because the function would stop working.
    {
        boxCollider.enabled = false;
        playerPow.isMagnetic = true;
        sprite.enabled = false;
        
        yield return new WaitForSeconds(7);
        playerPow.magnetSprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.3f);
        playerPow.magnetSprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.3f);
        playerPow.magnetSprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.3f);
        playerPow.magnetSprite.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.3f);
        playerPow.magnetSprite.color = new Color(1, 1, 1, 0);
        yield return new WaitForSeconds(0.3f);
        playerPow.magnetSprite.color = new Color(1, 1, 1, 1);
        


        playerPow.isMagnetic = false ;
        Destroy(gameObject, 1f);
    }
}
