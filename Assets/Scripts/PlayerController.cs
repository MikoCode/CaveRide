using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public MainCamera mainCam;
    public ParticleSystem particleSparks;
    public Animator animator;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public AudioClip moving;
    public AudioSource audio;
    public GameManager gM;
    public Swipe swipeControls;
    private bool slowIncreasing, up, down, finished;
    public bool isBoosted, isAlive, isShooted;
    private float nextUp, nextDown;
    private bool reset;
    public  float distance = 0, speed, wheelSpeed = 3;
    private float multiplier = 0.6f, posX, posY;
    public  float emision;
   
  
    
    
 





    // Start is called before the first frame update
    void Start()
    {
        finished = true;
        isAlive = true;
        slowIncreasing = false;

        InvokeRepeating("SpeedIncreasing", 1.6f, 1f);
       


    }

    // Update is called once per frame
    void Update()

    {
        if(transform.position.y == 0.3f)
        {
            nextDown = -3.20f;
            nextUp = 3.8f;
           
           
        }
        if(transform.position.y == 3.8f)
        {
            nextDown = 0.3f;
          
            
        }
        if(transform.position.y == -3.20f)
        {
            nextUp = 0.3f;
            
            
        }


        if (down == true && up == false)
        {
            if(transform.position.y != nextDown)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, nextDown), Time.deltaTime * 40);
                down = true;
            }
           
            if (transform.position.y == nextDown)
            {
                down = false;
             
            }
            
            
        }

        if (up == true && down == false)
        {

            if(transform.position.y != nextUp)
            {

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, nextUp), Time.deltaTime * 40);
                up = true;
            }
           
            if (transform.position.y == nextUp)
            {
                up = false;
                
            }
            
            
        }
       
        distance = Mathf.Round((transform.position.x + 4.94f) * multiplier);
        MovingVertically();
     
    }
    void FixedUpdate()
    {
        MovingHorizontally();
    }

    

    void MovingVertically() // Allowing player to move to one of the three avaible positions. 
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || swipeControls.SwipeDown) && transform.position.y > -2 && gM.isPaused == false && up == false)
        {
            down = true;
           

           
            StartCoroutine("ChangingPos");
            animator.SetTrigger("Moving");
            
            mainCam.isShaking = true;
          
            audio.PlayOneShot(moving,0.1f);
        }
        else if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || swipeControls.SwipeUp) && transform.position.y < 3 && gM.isPaused == false && down == false)
        {
            
            up = true;
            
        
            StartCoroutine("ChangingPos");
            animator.SetTrigger("Moving");
            mainCam.isShaking = true;
            posX = transform.position.x + 0.3f;
            posY = transform.position.y + 3.5f;
            
            audio.PlayOneShot(moving,0.1f);
        }
    }


    void MovingHorizontally() // moving player to right, with speed which changes with time
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void SpeedIncreasing() // Speed raises with time, cancelled if, speed is equall or higher than 23.5.
    {
        if (speed <= 22f && isAlive == true)
        {
            speed = speed + 0.07f;
            wheelSpeed += 0.07f;
            particleSparks.emissionRate += 1.3f;
            slowIncreasing = true;

        }
        else if (speed <= 24.5f && isAlive == true && slowIncreasing == true)
        {
            speed = speed + 0.007f;
            wheelSpeed += 0.007f;
            particleSparks.emissionRate += 0.5f;

        }









    }


    IEnumerator ChangingPos()
    {
        particleSparks.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        particleSparks.gameObject.SetActive(true);
        
    }

    
    

    
}






