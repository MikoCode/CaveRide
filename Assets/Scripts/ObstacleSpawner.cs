using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ObstacleSpawner : MonoBehaviour
{
    private PlayerController playerCon;
    public  PlayerPowers playerPow; 
    public  Transform playerPos;
    public TextMeshProUGUI dangerText;
    public AudioSource source;
    public AudioClip rocketSound;
    

    public GameObject[] obstacle;
    public GameObject[] powerUps;
    public GameObject[] Triple;
    public GameObject[] coin;
    public GameObject[] RocketEv;
    public GameObject exclamation;
    public GameObject twoExclamations;
    public GameObject bats;
    public GameObject[] laser;
    public GameObject obstacleDouble;
    public GameObject spawnBorder;

    public bool danger, turboCoins;
    public bool isSpawning, isDanger;
    public bool isRocket, started;
    public bool isDouble, isTriple;
    private bool spawnedTurboCoins = false;

    private int whichPower;
    private int whenDouble;
    private int howManyEx = 0; 
    private int minEvent, maxEvent, whichPos, howManyBats;

    private float[] batPosUp  = new float[] {3,3.5f,4f,4,5f };
    private float[] batPosDown = new float[] { -5, -4.5f, 4f, 3.6f, 3f };
    private float[] posY = new float[] { 3.7f, 0.25f, -3.35f };
   [SerializeField] private float targetScore = 200;
    private float whichEvent;
    private float curRocketSpawn;
    private float curObsSpawnTime, curBoostSpawnTime, curCoinSpawnTime, curBackSpawnTime;
    public  float spawnTime, minSpawnTime;
    private float doubleDistance,  nextWall;


 
    

   
   
    
 
    
    
   

    // Start is called before the first frame update
    void Start()
    {
        StartValues();
        Invoke("starting", 1f);
        
        
     
       
        playerCon = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        




    }

    // Update is called once per frame
    void Update()
    {
       

        if (isSpawning == true && playerCon.isAlive == true && started == true)   // Spawning obstacles only when events are disabled

        { 
            SpawnObstacle();
            SingleRocket();
            PowerUps();
        }

        if (playerCon.isAlive && started  == true && playerCon.isBoosted == false && isTriple == false && isSpawning == true)
        {
            Coin();
        }

        if (playerCon.isBoosted == false)
        {
            Events();
            spawnedTurboCoins = false;
        }
        else if(playerCon.isBoosted == true && spawnedTurboCoins == false) 
        {
            TurboCoins();
            spawnedTurboCoins = true;
        }
       
        Background();
        Difficulty();



    }

    void StartValues() //starting values for some variables, that are needed in other methods.
    {
        whichEvent = Random.Range(0, 3);
        curBoostSpawnTime = Random.Range(15, 25);
        howManyBats = Random.Range(1, 6);
        whichPos = Random.Range(0, 3);
        whenDouble = Random.Range(190, 300);
        curCoinSpawnTime = Random.Range(0.5f, 2.5f);
        curBackSpawnTime = Random.Range(5, 10);
        curRocketSpawn = Random.Range(15, 25);
        minEvent = 150;
        nextWall = 18;
        maxEvent = 350;
        curObsSpawnTime = spawnTime;
        doubleDistance = 15.5f;
        isSpawning = true;
    }
    void SpawnObstacle() // Basic Obstacle Spawning. Core gameplay element.
    {


        curObsSpawnTime -= 1 * Time.deltaTime;

        if (curObsSpawnTime <= 0)
        {


            if (isDouble == false)
            {



                Instantiate(obstacle[0], new Vector2(playerPos.position.x + 35, posY[whichPos]), Quaternion.identity);
                if (whichPos == 0)
                {
                    whichPos += Random.Range(1, 3);
                }
                else if (whichPos == 1)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        whichPos += 1;
                    }
                    else
                    {
                        whichPos -= 1;
                    }
                }
                else if (whichPos == 2)
                {
                    whichPos -= Random.Range(1, 3);
                }




            }



            curObsSpawnTime = Random.Range(minSpawnTime, spawnTime);
        }

    }

    void Events() // Starting events. There are three events, wall,double and rocket. The purpose of them is to add more variety to gameplay. 
    {
      

        if (playerCon.distance > whenDouble && playerCon.isBoosted == false)
            
        {
            
            whenDouble += Random.Range(minEvent, maxEvent);
            if (whichEvent == 0)
            {
                whichEvent += Random.Range(1, 4);
            }
            else if(whichEvent == 2)
            {   
                if(Random.Range(0,2) == 0)
                {
                    whichEvent = Random.Range(0, 2);
                }
                else
                {
                    whichEvent = 3;
                }
              
            }
            else if(whichEvent == 1)
            {
                if(Random.Range(0,1) == 0)
                {
                    whichEvent = Random.Range(2, 4);
                }
                else
                {
                    whichEvent = 0;
                }
            }
            else if(whichEvent == 3)
            {
                whichEvent = Random.Range(0, 3);
            }


            if (whichEvent == 0)
            {
            
                StartCoroutine("SpawnDouble");
            }
            else if (whichEvent == 1)
            {
                StartCoroutine("RocketEventCour");
              
            }
            else if (whichEvent == 2)
            {
                
                StartCoroutine("WallEvent");
              

            } 
            else if(whichEvent == 3 )
            {
                StartCoroutine("MovingEvent");
            }
        }
        
    }

    IEnumerator SpawnDouble() // Couroutine of Double Obstacle Event. It spawns two obstacles on the same X axis,but on different Y axis.
    {
        isSpawning = false;
        isDouble = true;

        yield return new WaitForSeconds(1.6f);

        float position = playerPos.position.x + 22;
        int y = Random.Range(0, 2);
        for (int i = 0; i <= Random.Range(5, 8); i++)
        {
            Instantiate(obstacleDouble, new Vector2(position, posY[y]), Quaternion.identity); // first spawn double, than calculate position of the next one.
            if (y == 0)
            {
                if (Random.Range(0, 5) == 0)
                {
                    y = 0;
                }
                else
                {
                    y += 1;
                }
            }
            else
            {
                if (Random.Range(0, 5) == 0)
                {
                    y = 1;
                }
                else
                {
                    y -= 1;
                }
            }
            position += doubleDistance;
        }
        for (int i = 0; i < 2; i++)
        {
            Instantiate(spawnBorder, new Vector2(position - 15, posY[1]), Quaternion.identity);
            position += 10;
        }
        yield return new WaitForSeconds(1);

        isDouble = false;

    }





    IEnumerator RocketEventCour() // Couroutine of RocketEvent, it instantiates two exclamation points,their movement is handled in rocket script.
    {
        isRocket = true;
        isSpawning = false;

        for (howManyEx = 0; howManyEx < 2; howManyEx++)

        {
            source.PlayOneShot(rocketSound, 1f);
            Instantiate(twoExclamations);
        }




        yield return new WaitForSeconds(4.5f);
        isSpawning = true;
        isRocket = false;

    }
    IEnumerator WallEvent() // an event , in which player has to destroy green obstacles in order to break a wall of bombs.
    {
        isSpawning = false;
        isTriple = true;

        yield return new WaitForSeconds(1);

        SpawnTriple();

        yield return new WaitForSeconds(6);

        isTriple = false;

        yield return new WaitForSeconds(3);

        isSpawning = true;


    }
    IEnumerator MovingEvent()
    {
        isSpawning = false;
        isTriple = true;
        yield return new WaitForSeconds(1);

        SpawnMoving();

        yield return new WaitForSeconds(6);

        isTriple = false;
        yield return new WaitForSeconds(3);
        isSpawning = true;
    }

    void SpawnTriple() // spawning object in wall event.
    {
        if (isTriple == true)
        {
            isSpawning = false;
            float position = playerPos.position.x + 18;


            for (int i = 0; i <= Random.Range(4, 7); i++)
            {
                Instantiate(Triple[0], new Vector2(position, posY[1]), Quaternion.identity);
                position += nextWall;
            }
            for (int i = 0; i < 2; i++)
            {
                Instantiate(spawnBorder, new Vector2(position - 16, posY[1]), Quaternion.identity);
                position += 10;
            }

        }

    }

    void SingleRocket() // spawning a single exclamation,which shoots player on single line. The purpose of it is to motivate player to use all three lines.
    {
        float up = 3.8f;
        float down = -3.11f;

        curRocketSpawn -= 1 * Time.deltaTime;

        if (curRocketSpawn <= 0)
        {
            for (howManyEx = 0; howManyEx < 1; howManyEx++)

            {
                source.PlayOneShot(rocketSound, 1f);
                if (playerPos.position.y >= 1)
                {
                    Instantiate(exclamation, new Vector2(transform.position.x, up), Quaternion.identity);
                }
                else if (playerPos.position.y <= 0)
                {
                    Instantiate(exclamation, new Vector2(transform.position.x, down), Quaternion.identity);
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        Instantiate(exclamation, new Vector2(transform.position.x, up), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(exclamation, new Vector2(transform.position.x, down), Quaternion.identity);
                    }

                }


            }
            curRocketSpawn = Random.Range(12, 25);
        }

    }


    void SpawnMoving()
    {
        if (isTriple == true)
        {
            isSpawning = false;
            float position = playerPos.position.x + 18;
            float nextPosX = playerCon.transform.position.x + 35;
            int howMany = Random.Range(4, 7);


            for (int i = 0; i <= howMany; i++)
            {
                Instantiate(coin[2], new Vector2(nextPosX, posY[0]), Quaternion.identity);
                Instantiate(coin[2], new Vector2(nextPosX, posY[2]), Quaternion.identity);
                nextPosX += 20;
            }

            for (int i = 0; i <= howMany; i++)
            {
                Instantiate(Triple[1], new Vector2(position, posY[1]), Quaternion.identity);
                position += 20;
            }
            for (int i = 0; i < 2; i++)
            {
                Instantiate(spawnBorder, new Vector2(position - 25, posY[1]), Quaternion.identity);
                position += 6;
            }

        }

    }


    void Coin() // Spawning Coins
    {
        curCoinSpawnTime -= 1 * Time.deltaTime;

        if (curCoinSpawnTime <= 0)
        {

            Instantiate(coin[Random.Range(0, 4)], new Vector2(playerPos.position.x + 25, posY[Random.Range(0,3)] ), Quaternion.identity);
          
            curCoinSpawnTime = Random.Range(1f, 3.2f);

        }
    }


    void TurboCoins()
    {

        turboCoins = true;
        float nextPosX = playerPos.position.x + 30;
        int nextPosY = Random.Range(0, 3);
        for (int y = 0; y <= Random.Range(2,5); y++)
        {
            for (int i = 0; i <= Random.Range(1, 3); i++)
            {
                Instantiate(coin[2], new Vector2(nextPosX, posY[nextPosY]), Quaternion.identity);
                nextPosX += 4;
            }

            nextPosX += 15;

            if(nextPosY == 0)
            {
                nextPosY += Random.Range(1, 3);
            }
            else if(nextPosY == 2)
            {
                nextPosY -= Random.Range(1, 3);
            }
            else
            {
                if(Random.Range(0,2) == 0)
                {
                    nextPosY = 0;

                }
                else
                {
                    nextPosY = 2;
                }
            }
        }
       
    }

    void Background() // instatiating bats that flies in the background
    {
        curBackSpawnTime -= Time.deltaTime;
        if(curBackSpawnTime <= 0)
        {
            int random = Random.Range(0, 2);
            if(random == 1)
            {
                for (int i = 0; i <= howManyBats; i++)
                {
                    Instantiate(bats, new Vector2(playerPos.position.x + 60, batPosUp[Random.Range(0,5)]), Quaternion.identity);

                }
            }
            else
            {
                for (int i = 0; i <= howManyBats; i++)
                {
                    Instantiate(bats, new Vector2(playerPos.position.x + 60, batPosDown[Random.Range(0,5)]), Quaternion.identity);

                }
            }
           
            curBackSpawnTime = Random.Range(10, 36);
            howManyBats = Random.Range(1, 6);

        }
    }




    
    

   


    void PowerUps() // Spawning PowerUps
    {

        //0 - speed boost,1 - magnet, 2- shield

        curBoostSpawnTime -= 1 * Time.deltaTime;

        if(curBoostSpawnTime <= 0)
        {
            if(playerCon.isBoosted == false)
            {
                if (playerPow.isProtected == false && playerPow.isMagnetic == false)
                {
                    whichPower = Random.Range(0, 3);
                }
                else if (playerPow.isProtected == true && playerPow.isMagnetic == false)
                {
                    whichPower = Random.Range(0, 2);
                }
                else if (playerPow.isProtected == false && playerPow.isMagnetic == true)
                {
                    if (Random.Range(0, 2) == 0)
                    {
                        whichPower = 0;
                    }
                    else
                    {
                        whichPower = 2;
                    }

                }
            }
            


            Instantiate(powerUps[whichPower], new Vector2(playerPos.position.x + 24, posY[Random.Range(0, 3)]), Quaternion.identity);
            curBoostSpawnTime = Random.Range(8,16);
        }

    }


    void Difficulty() // Increasing difficulty with distance beaten.
    {
        if (playerCon.distance >= targetScore  && playerCon.distance <= 1700 )
        {

            if (spawnTime > 0.45f)
            {
                spawnTime -= 0.025f;
               
            }

            if(minSpawnTime > 0.3f)
            {
                minSpawnTime -= 0.014f;
            }

            if(playerCon.speed < 20)
            {
                maxEvent += 12;
                minEvent += 12;
            }

            if(nextWall > 15)
            {
                nextWall -= 0.3f;
            }

            if(doubleDistance > 14)
            {
                doubleDistance -= 0.3f;
            }


            targetScore += 100;

        }
        else if(playerCon.distance > 1700 && playerCon.distance >= targetScore)
        {

            if (spawnTime > 0.35f)
            {
                spawnTime -= 0.025f;

            }

            if (minSpawnTime > 0.25f)
            {
                minSpawnTime -= 0.014f;
            }

            if (nextWall > 14)
            {
                nextWall -= 0.1f;
            }

            if (doubleDistance > 13)
            {
                doubleDistance -= 0.1f;
            }

            targetScore += 400;
        }
    }


    public void starting() // starting spawning after small amount of time, to make start easier
    {
        started = true;
    }








}
