using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerController playerCon;
    private ObstacleSpawner spawner;
    private MainCamera mainCam;
    private GameManager gM;
    private PlayerPowers playerPow;
    private GameObject player;
    public GameObject particles;
    public GameObject brokenParticle;

    public ParticleSystem particlehit;
    public ParticleSystem breakingShield;
    public ParticleSystem smallHit;
    public SpriteRenderer sprite;
    public BoxCollider2D bc;

    public AudioSource audioSource;
    public AudioClip clip;
    public AudioClip shieldDestroy;
    public AudioClip death;
    public AudioClip revive;

    public Transform[] movePos;

    public float time;
    public float speed, movingObsSpeed;
    public float color;
    public float amount;
    public bool isSingle, isBroken, isMoving;
    public bool canDestroy, didDestroy = false;


    public Vector2 pos;



    // Start is called before the first frame update
    void Start()
    {
        didDestroy = false;
        pos.y = transform.position.y;
        pos.x = transform.position.x;
        color = 1;
        time = 3;
        spawner = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ObstacleSpawner>();
        gM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPow = player.GetComponent<PlayerPowers>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MainCamera>();
        playerCon = player.GetComponent<PlayerController>();
        Destroy(gameObject, 11f);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOn();
        Skip();
        Shake();
    }

    void Shake() // making obstacles to shake a little bit, to make game seem more "alive"
    {
        if (isSingle == true && gM.isPaused == false)
        {
            pos.y += Mathf.Sin(Time.time * speed) * amount;
            pos.x += Mathf.Sin(Time.time * speed) * amount;
            transform.position = new Vector2(pos.x, pos.y);
        }
    }


    void DestroyOn()
    {


        if (gM.isRevived == true && didDestroy == false)
        {
            if (canDestroy == false)
            {
                didDestroy = true;
                bc.enabled = false;
                sprite.enabled = false;
                Destroy(particles);
                Instantiate(smallHit, gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject, 2);
            }
        }


        if (playerCon.isBoosted == true)
        {
            if (isSingle == false)
            {
                Destroy(gameObject);
            }
        }


    }



    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Ending the game when player touches obstacle. Possible only if shield and boost powerups are disabled.

        if (collision.gameObject.CompareTag("Player") && playerPow.isProtected == false && playerCon.isBoosted == false && isBroken == false && playerCon.isShooted == false)
        {
            audioSource.PlayOneShot(clip);
            Destroy(particles);
            playerCon.isShooted = true;
            mainCam.isShaking = true;
            sprite.enabled = false;
            playerCon.animator.enabled = false;
            bc.enabled = false;
            player.transform.position = new Vector2(player.transform.position.x - 2, player.transform.position.y); // moving player left a little bit,to avoid second collision,just in case collision would not work.
       
            Instantiate(particlehit, gameObject.transform.position, Quaternion.identity);

            if (gM.deathsCounter == 0)  // this happens if player dies for the first time. Player have an ability to revive himself.


            {
                if (playerCon.distance > PlayerPrefs.GetFloat("Highscore") - 301 && playerCon.distance < PlayerPrefs.GetFloat("Highscore"))

                {
                    gM.endScore.text = " Only " + (PlayerPrefs.GetFloat("Highscore") - playerCon.distance) + "m left to a new Highscore! ";
                    gM.endScore.gameObject.SetActive(true);
                }
                else
                {
                    gM.endScore.text = "Keep it up !";
                    gM.endScore.gameObject.SetActive(true);
                }
               


                gM.totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
                gM.reviveText.text = "  Revive \n300";
                gM.lastLive = true;
                StartCoroutine("reviveQuestion");
            }

            else if (gM.deathsCounter == 1) // after player dies second time, this happens.

            {
                gM.deathsCounter++;
                playerCon.speed /= 10;
                gM.restartButton.gameObject.SetActive(true);
                gM.blackScreen.gameObject.SetActive(true);
                gM.endScore.gameObject.SetActive(true);
                gM.coinImage.gameObject.SetActive(false);
                gM.scoreText.gameObject.SetActive(false);
                gM.coinsText.gameObject.SetActive(false);
                gM.pauseButton.gameObject.SetActive(false);


                if (playerCon.distance < PlayerPrefs.GetFloat("Highscore"))
                {
                    gM.endScore.text = "You rode " + playerCon.distance + "m!";
                }
                else if (playerCon.distance > PlayerPrefs.GetFloat("Highscore"))
                {
                    gM.endScore.text = "New Highscore! " + playerCon.distance;
                }

                playerCon.isAlive = false;
                playerCon.enabled = false;
                playerCon.particleSparks.enableEmission = false;
                playerCon.rb.gravityScale = 1;

            }









        }

        //this happens if player with turbo collides with obstacle
        else if (collision.gameObject.CompareTag("Player") && playerPow.isProtected == false && playerCon.isBoosted == true && isBroken == false)
        {
           
        
            sprite.enabled = false;
            bc.enabled = false;
            audioSource.PlayOneShot(clip);
            Instantiate(smallHit, gameObject.transform.position, Quaternion.identity);
            Destroy(particles);
            Destroy(gameObject, 2f);


        }

        // if shield powerup enabled and player touches obstacle, shield disapears. Only possible if player does not have boost power up.
        else if ((collision.gameObject.CompareTag("Player") && playerPow.isProtected == true && playerCon.isBoosted == false && isBroken == false))
        {
            mainCam.isShaking = true;
            bc.enabled = false;
            playerPow.isProtected = false;
            sprite.enabled = false;
            audioSource.PlayOneShot(shieldDestroy);
            Instantiate(breakingShield, gameObject.transform.position, Quaternion.identity);
            Instantiate(smallHit, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject, 2f);

        }




        // this happens after player collides with broken particle,during wall event.
        if (collision.gameObject.CompareTag("Player") && isBroken == true)
        {
            mainCam.isShaking = true;
            sprite.enabled = false;
            Instantiate(brokenParticle, gameObject.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(clip, 0.5f);
            Destroy(gameObject, 3);
        }


        // if obstacle collides with an exclamation point, obstacle it destroyed. It has to be done, to make visibility on the line with  an exclamation.
        {
            if (collision.gameObject.tag == ("rocket") && isSingle == true) 
            Destroy(gameObject);
        }




    }


    IEnumerator reviveQuestion() // after player dies,he has option to revive himself. Depending on the will of the player,game can continue,or end.
    {
        gM.deathsCounter++;
        playerCon.speed /= 10;
        canDestroy = true;
        playerCon.isAlive = false;
        playerCon.enabled = false;
        playerCon.particleSparks.enableEmission = false;
        gM.skipButton.gameObject.SetActive(true);
        gM.blackScreen.gameObject.SetActive(true);
        gM.timer.gameObject.SetActive(true);
        gM.reviveButton.gameObject.SetActive(true);
        gM.reviveCoin.gameObject.SetActive(true);
        gM.pauseButton.gameObject.SetActive(false);
 
        
    

        yield return new WaitForSeconds(4);
        if (gM.isRevived == true) // if player revived himself
        {
            playerCon.particleSparks.enableEmission = true;
            playerCon.isAlive = true;
            playerCon.enabled = true;
            playerCon.bc.enabled = true;
            playerCon.animator.enabled = true;
            gM.isRevived = false;
            playerCon.isShooted = false;
            gM.reviveTime = 3;
            playerCon.speed *= 10;
            gM.pauseButton.gameObject.SetActive(true);
            gM.restartButton.gameObject.SetActive(false);
            gM.timer.gameObject.SetActive(false);
            gM.endScore.gameObject.SetActive(false);
            gM.blackScreen.gameObject.SetActive(false);
            gM.reviveButton.gameObject.SetActive(false);
            gM.reviveCoin.gameObject.SetActive(false);
            gM.skipButton.gameObject.SetActive(false);
            spawner.isSpawning = true;
            audioSource.PlayOneShot(revive, 0.6f);
         
             
            






        }
        else // if player didnt use revive button
        {
            if (playerCon.distance < PlayerPrefs.GetFloat("Highscore"))
            {
                gM.endScore.text = "You rode: " + playerCon.distance + "m!";
            }
            else if (playerCon.distance > PlayerPrefs.GetFloat("Highscore"))
            {
                gM.endScore.text = "New Highscore! :" + playerCon.distance;
            }
            playerCon.rb.gravityScale = 1;
            gM.lastLive = true;
            gM.restartButton.gameObject.SetActive(true);
            gM.endScore.gameObject.SetActive(true);
            gM.scoreText.gameObject.SetActive(false);
            gM.coinImage.gameObject.SetActive(false);
            gM.coinsText.gameObject.SetActive(false);
            gM.reviveButton.gameObject.SetActive(false);
            gM.pauseButton.gameObject.SetActive(false);
    
            audioSource.PlayOneShot(death, 0.5f);
          


        }
    }


    void Skip() // skipping the countdown if player does not want to be revived.
    {
        if (gM.skipped == true )
        {
            StopCoroutine("reviveQuestion");

            if (playerCon.distance < PlayerPrefs.GetFloat("Highscore"))
            {
                gM.endScore.text = "You rode: " + playerCon.distance + "m!";
            }

            else if (playerCon.distance > PlayerPrefs.GetFloat("Highscore"))
            {
                gM.endScore.text = "New Highscore! :" + playerCon.distance;
            }

            playerCon.rb.gravityScale = 1;
            gM.lastLive = true;
            gM.restartButton.gameObject.SetActive(true);
            gM.endScore.gameObject.SetActive(true);
            gM.scoreText.gameObject.SetActive(false);
            gM.coinImage.gameObject.SetActive(false);
            gM.coinsText.gameObject.SetActive(false);
            gM.reviveButton.gameObject.SetActive(false);
            gM.pauseButton.gameObject.SetActive(false);
            gM.skipButton.gameObject.SetActive(false);
       
            

            
        }






    }
}