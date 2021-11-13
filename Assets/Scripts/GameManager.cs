using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
  
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI totalCoinsText;
    public TextMeshProUGUI pausedText;
    public TextMeshProUGUI reviveText;
    public TextMeshProUGUI endScore;
    public Image blackScreen;
    public Image timer;
    public Button restartButton;
    public Button pauseButton;
    public Button skipButton;
    public Button reviveButton;
    public Image coinImage;
    public Rigidbody2D hatRb;
    public PlayerController playerCon;
    public GameObject hat;
    public GameObject reviveCoin;
    public int deathsCounter;
    public bool isRevived;
    public float reviveTime;
    public bool lastLive;
    public bool isPaused, skipped;
    private bool testMode = true;
    public int totalCoins;
    public int coins;
    private int[] imagePositions = new int[] { 10, 100, 1000, 10000,100000,1000000 };
    private int i;
    private string gameId = "4070429";
   


    // Start is called before the first frame update
    void Start()
    {
        
        
        Advertisement.Initialize(gameId, testMode);
        isPaused = false;
        reviveTime = 3;
        totalCoins = PlayerPrefs.GetInt("TotalCoins", 0);
        coins = 0;

      if (PlayerPrefs.GetFloat("Highscore") == 0)
        {
            highScoreText.text = "Best Score \n" + "Yet to Achieve!  ";
        }

        else
        {
            highScoreText.text = "Best Score \n" + "     " + PlayerPrefs.GetFloat("Highscore", 0).ToString();
        }
      

        deathsCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
          
             totalCoins = 0;
             coins = 0;
             PlayerPrefs.DeleteAll();
        }
            
        totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();

        Hat();
        TotalCoins();
        HighScore();
        ImagePos();
;       
    }


   public void PlayAgain() // Restart Button
    {
        //if (Advertisement.IsReady())
       // {
       //     Advertisement.Show();
       // }


        SceneManager.LoadScene("GamePlay");
    }

    private void HighScore() // Saving HighScore with PlayerPrefs
    {
        scoreText.text = playerCon.distance.ToString() + "m";

        if (playerCon.distance > PlayerPrefs.GetFloat("Highscore", 0) && playerCon.isAlive == false) 
        {
            PlayerPrefs.SetFloat("Highscore", playerCon.distance);
            highScoreText.text = "Best Score : \n " + "    " + playerCon.distance.ToString() + "m";
        }


    }


    private void TotalCoins()
    {

        PlayerPrefs.SetInt("TotalCoins", totalCoins + coins) ; 
        PlayerPrefs.Save();
    }
    void ImagePos() //Adjust Coin Image position, to a number of coins. With every 0 in a number,image must go to the right a little bit.
    {
        if (coins >= imagePositions[i])
        {
            i++;
            coinImage.transform.position = new Vector2(coinImage.transform.position.x + 0.6f, coinImage.transform.position.y);
        }
      
 
    }


  public  void Revive()  // Revive button
    {
        if (deathsCounter == 1 && totalCoins >= 300)
        {
            endScore.gameObject.SetActive(false);
            timer.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
            reviveCoin.gameObject.SetActive(false);
            reviveText.gameObject.transform.position = new Vector2(reviveText.gameObject.transform.position.x + 0.5f, reviveText.gameObject.transform.position.y + 1);
            reviveText.text = "Get Ready!"; ;
            reviveTime = 0;
            isRevived = true;
            totalCoins -= 300;
        }
        

        else if(totalCoins < 300 && deathsCounter == 1)
        {
            reviveCoin.gameObject.SetActive(false);
            reviveText.gameObject.transform.position = new Vector2(reviveText.gameObject.transform.position.x + 0.5f, reviveText.gameObject.transform.position.y );
            reviveText.text = ("No Money!");
        }
      
       
      
    }



    void Hat() // Player drops his hat, if he have last live.
    {

        if (lastLive == true)
        {
            if(hat != null)
            {
                hatRb.gravityScale = 1;
                hat.transform.Rotate(new Vector3(0, 0, 3));
                Destroy(hat, 1.5f);
            }
           
        }
       
    }



    public void PauseMenu() // activating pause menu,  with button, buy reducing time scale to 0. It also activates a bool which stops other obstacles that are based on frames, not time. Also activating pause text


    {


        if (isPaused == false)
        {
            Time.timeScale = 0;
            isPaused = true;
            blackScreen.gameObject.SetActive(true);
            pausedText.gameObject.SetActive(true);
        }

        else
        {
            blackScreen.gameObject.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            pausedText.gameObject.SetActive(false);
        }
            
        
       
    }
    public void LoadMainMenu() 
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1; // In case clicking main menu during pause, time scale is set to 1.
    }


    public void Skip()
    {
        if(isRevived == false)
        {
            skipped = true;
        }
      
    }



}
