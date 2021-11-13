using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public SpriteRenderer mesh;
    private PlayerPowers playerPow;
    private GameManager gameManager;
    private Transform target;
    private GameObject player;
    public ParticleSystem particle;
    public ObstacleSpawner spawner;
    private AudioSource audioSource;
    public AudioClip clip;
    public int value;
    
   


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

        playerPow = player.GetComponent<PlayerPowers>();
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObstacleSpawner>();

        target = player.GetComponent<Transform>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        Destroy(gameObject, 13f);
        IncreasingValue();

       
    }

    // Update is called once per frame
    void Update()
    {
        FollowMagnet();

        DestroyOnTurbo();
        if (gameManager.isPaused == false)

        {
            transform.Rotate(new Vector3(0, 3, 0) * Time.deltaTime * 40);
        }

        if(gameManager.isRevived == true)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            mesh.enabled = false;
            
            Destroy(gameObject,0.2f);
        }
       
        
    }


    void IncreasingValue()
    {
        if (gameManager.playerCon.distance > 500) // increasing value of the coins, at later stage of the run
        {
            value = value * 2;

        }
        else if (gameManager.playerCon.distance > 1000)
        {
            value = value * 3;
        }
        else if (gameManager.playerCon.distance > 2000)
        {
            value = value * 4;
        }
    }



    public void OnTriggerEnter2D(Collider2D collision) // If Coin is collected,the number of total coins raises by value defined in inspector
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.coins += value;
            gameManager.coinsText.text = gameManager.coins.ToString();
            mesh.enabled = false;


            audioSource.PlayOneShot(clip, 0.2f);
            
         
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.3f);

        }
        if (collision.gameObject.CompareTag("obstacle")) // if coin collide with obstacle, coin moves a little bit right,to not block a bomb.
        {
            

            transform.position = new Vector2(transform.position.x + 3, transform.position.y);
        }


    }


    void FollowMagnet() // Function that activates when Magnet is collected, coins fly to player if he is close enough.
    {
        if (Vector2.Distance(player.transform.position, gameObject.transform.position) < 12 && (playerPow.isMagnetic == true))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, 25f * Time.deltaTime);
        }
    }


    void DestroyOnTurbo()
    {
        if(spawner.turboCoins == true)
        {
            spawner.turboCoins = false;
            Destroy(gameObject);
        }
    }
   
}


    
