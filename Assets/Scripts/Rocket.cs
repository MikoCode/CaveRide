using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{


    public GameObject rocket;
    private ObstacleSpawner spawner;
    private GameManager manager;
    public ParticleSystem particle;
    public ParticleSystem trail;
    public PlayerController player;
    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip destructionAudio;
    public SpriteRenderer sprite;
    
    private int whichPos;

    private bool didSaveTransform = false;
    public bool isSingle;
    public bool ifPlayed;

    private float timeBeforeShooting;
    public float speed;
    public float color;
    private float spawnTime;
    public float curSpawnTime;
    public float amount;
   [SerializeField] private float shootTime;
    private float yValue;
    private float curChooseTime;
    private float[] yPos = new float[] { 3.89f, 0.4f, -3.11f };

    private Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Shooting");
        StartValues();
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        player   = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        spawner  = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObstacleSpawner>();
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {

        SingleRocket();
        Positions();

        if (didSaveTransform == false && isSingle == false) // activating ChoosePos method, if rocket is spawned during an event, and only if it still needs to be working
        {
            ChoosePos();
        }


        if (manager.isRevived == true || player.isBoosted == true) 
        {
            StopCoroutine("Shooting");

            Destroy(gameObject);
        }
        



        Difficulty();
       
         

       
    }


    void Positions() // setting rocket position
    {
        if (isSingle == false)
        {
          
            transform.position = new Vector2(player.transform.position.x + 14, yPos[whichPos]);
        }

        else if (isSingle == true)
        {
            Shake();
            transform.position = new Vector2(player.transform.position.x + 14, transform.position.y);
        }
    }
    void SingleRocket() // if single rocket is active during an event, it needs to be destroyed
    {
        if (spawner.isDouble == true || spawner.isTriple == true || spawner.isRocket == true) 
        {
            if (isSingle == true)
            {
                StopCoroutine("Shooting");

                
                Destroy(gameObject);
            }

        }



    }


   


   


    void StartValues() // setting start values for variables used in different methods
    {
        curChooseTime = spawnTime;
        pos.y = transform.position.y;
        pos.x = transform.position.x;

        shootTime = Random.Range(1f,3f);
        spawnTime = 0.15f;
        curSpawnTime = 0.2f;
        curChooseTime = spawnTime;
        whichPos = Random.Range(0, 3);

        ifPlayed = false;
    }
    void Shake() // making exclamation to shake a little, on y Axis
    {
        pos.y += Mathf.Sin(Time.time * speed) * amount;
        transform.position = new Vector2(pos.x, pos.y);
    }


    void ChoosePos() // movement of exclamation, it moves few times between lines, to make final position less predictable
    {
        if (isSingle == false) //only applies for rocket spawned during rocket event
        {
            curChooseTime -= 1 * Time.deltaTime;
            if (curChooseTime <= 0)

            {
                if (whichPos == 0)
                {
                    whichPos = Random.Range(1, 3);
                }
                else if (whichPos == 1)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        whichPos = 0;
                    }
                    else
                    {
                        whichPos = 2;
                    }
                }
                else if (whichPos == 2)
                {
                    whichPos = Random.Range(0, 2);
                }

                curChooseTime = spawnTime;
            }
        }
     
    }




    IEnumerator Shooting() // Behaviour of an exlamation (rocket), first it chooses position, then stops and saves position, and after small amount of time it instantiates 7 bullets. Then ,it instantiates particles, play a sound and destroyes
    {
        float chooseTime = 0;
        if(isSingle == true)
        {
            chooseTime = 1;
        }
        else
        {
            chooseTime = Random.Range(2.1f, 2.5f);
        }
          
        
        yield return new WaitForSeconds(chooseTime);

        if (didSaveTransform == false)
        {
            didSaveTransform = true;
            yValue = transform.position.y;
          
        }

     
        yield return new WaitForSeconds(shootTime);

        trail.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= 7; i++)

        {
            audioSource.PlayOneShot(clip, 0.4f);
            Instantiate(rocket, transform.position = new Vector2(transform.position.x - 0.7f, transform.position.y), Quaternion.Euler(new Vector3(Random.Range(0,180),0,0)));
            yield return new WaitForSeconds(0.2f);
        }

        sprite.enabled = false;
        trail.gameObject.SetActive(false);

        if (ifPlayed == false)
        {
            audioSource.PlayOneShot(destructionAudio, 0.5f);
            Instantiate(particle, transform.position, Quaternion.identity);
            ifPlayed = true;
        }

        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

    }





    


    void Difficulty()
    {

        if(isSingle == false)
        {
            if (player.distance > 0 && player.distance < 700)
            {
                shootTime = Random.Range(0.7f, 0.9f);
            }
            else if (player.distance > 700 && player.distance < 1500)
            {
                shootTime = Random.Range(0.6f, 0.8f);
            }
            else if (player.distance > 1500 && player.distance < 2000)
            {
                shootTime = Random.Range(0.5f, 0.7f);
            }
            else if (player.distance > 2000 && player.distance < 2400)
            {
                shootTime = Random.Range(0.4f, 0.6f);
            }
            else if (player.distance > 2400)
            {
                shootTime = Random.Range(0.3f, 0.5f);
            }
        }
        else
        {
            shootTime = Random.Range(0.3f, 0.5f);
        }
       
    }


   


}
