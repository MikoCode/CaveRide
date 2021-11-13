using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowers : MonoBehaviour
{
    public bool isProtected;
    public SpriteRenderer sprite;
    public SpriteRenderer[] spriteShield;
    public bool isMagnetic;
    public GameObject shield;
    public bool isWithShield;
    private bool isWithTurbo;
    private bool isWithMagnet;
    public ParticleSystem particle;
    public PlayerController playerCon;
    public AudioSource audioSource;
    public AudioClip shieldPickup;
    public GameObject magnet;
    public SpriteRenderer magnetSprite;
    public BoxCollider2D bc;

    
    
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        

        SetPowers();
        
           
        
       
    }

    void SetPowers() // Handling player powers, and connections between them. 
    {
        if (isProtected == true && isWithShield == false)
        {
            shield.gameObject.SetActive(true);
            audioSource.PlayOneShot(shieldPickup, 0.2f);
            spriteShield[0].enabled = true;
            spriteShield[1].enabled = true;
            bc.size = new Vector2(9.8f, 6.6f);
            isWithShield = true;
        }
        else if(isProtected == false )
        {
           
            bc.size = new Vector2(2.11f, 3.8f);
            shield.gameObject.SetActive(false);
            isWithShield = false;
            spriteShield[0].enabled = false;
            spriteShield[1].enabled = false;
         
        }

        if (isMagnetic == true && isWithMagnet == false)
        {
            isWithMagnet = true;
            magnet.gameObject.SetActive(true);
            audioSource.PlayOneShot(shieldPickup, 0.2f);
            magnet.transform.Rotate(new Vector3(0, 0, 2));
        }
        else if( isMagnetic == false)
        {
            magnet.gameObject.SetActive(false);
            isWithMagnet = false;
        }

        if(playerCon.isAlive == true )
        {
            sprite.color = new Color(1, 1, 1, 1);
        }
        else if(playerCon.isAlive == false)
        {
            sprite.color = new Color(0, 0, 0, 1f);
        }
        if(playerCon.isBoosted == true && isWithTurbo == false)
        {
            audioSource.PlayOneShot(shieldPickup, 0.2f);
            isWithTurbo = true;
        }
        else if(playerCon.isBoosted == false)
        {
            isWithTurbo = false;
        }

       
    }


   
}
