using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkinManager : MonoBehaviour
{
    public Sprite[] sprite;
    public SpriteRenderer rend;
    public GameManager manager;
    public Text[] text;
    private Scene activeScene;
    private string sceneName;
    public bool  isNinjaBought;
    public bool isLGBTBought;
    public bool isYellowBought;
   
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        PlayerPrefs.GetInt("skin", 0);
        rend.sprite = sprite[PlayerPrefs.GetInt("skin", 0)];

       



    }

    // Update is called once per frame
    void Update()
    {
        sceneName = activeScene.name;
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteKey("LGBTBought");
            PlayerPrefs.DeleteKey("YellowBought");
            PlayerPrefs.DeleteKey("NinjaBought");
        }

        if(sceneName =="Store")
        {
            Store();
        }
      

    }



    public void SetBasic()
    {
        PlayerPrefs.SetInt("skin", 0);
       
    }

   public void BuyLGBT()
    {
        if (PlayerPrefs.GetInt("LGBTBought") == 0 && PlayerPrefs.GetInt("TotalCoins") >= 5000)
        {
            manager.coins -= 5000;
            PlayerPrefs.SetInt("TotalCoins", manager.totalCoins + manager.coins);
           
            PlayerPrefs.SetInt("skin", 1);
            manager.totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();


            isLGBTBought = true;
            PlayerPrefs.SetInt("LGBTBought", (isLGBTBought ? 1 : 0));
        }
        else if (PlayerPrefs.GetInt("LGBTBought") != 0)
        {
            PlayerPrefs.SetInt("skin", 1);
            isLGBTBought = true;
            PlayerPrefs.SetInt("LGBTBought", (isLGBTBought ? 1 : 0));
        }
        else if(manager.totalCoins < 1000)
        {
            
        }
    }
    
    public void BuyNinja()
    {
        if(PlayerPrefs.GetInt("NinjaBought") == 0 && PlayerPrefs.GetInt("TotalCoins") >= 5000)
        {
            manager.coins -= 5000;
            PlayerPrefs.SetInt("TotalCoins", manager.totalCoins + manager.coins);
          
            PlayerPrefs.SetInt("skin", 2);
            manager.totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
            isNinjaBought = true;
            PlayerPrefs.SetInt("NinjaBought", (isNinjaBought ? 1 : 0));
        }
        else if(PlayerPrefs.GetInt("NinjaBought") != 0)
        {
            PlayerPrefs.SetInt("skin", 2);
            isNinjaBought = true;
            PlayerPrefs.SetInt("NinjaBought", (isNinjaBought ? 1 : 0));
        }
        else if(manager.totalCoins < 5000)
        {
          
        }
       

    }

    public void BuyYellow()
    {
        if (PlayerPrefs.GetInt("YellowBought") == 0 && PlayerPrefs.GetInt("TotalCoins") >= 5000)

        {
            manager.coins -= 5000;
            PlayerPrefs.SetInt("TotalCoins", manager.totalCoins + manager.coins);
            
            PlayerPrefs.SetInt("skin", 3);
            manager.totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
            isYellowBought = true;
            PlayerPrefs.SetInt("YellowBought", (isYellowBought ? 1 : 0));
        }


        else if (PlayerPrefs.GetInt("YellowBought") !=0)

        {
            PlayerPrefs.SetInt("skin", 3);
            isYellowBought = true;
            PlayerPrefs.SetInt("YellowBought", (isYellowBought ? 1 : 0));
        }


        else if (manager.totalCoins < 5000)

        {
         
        }
     
    }



    void Store() // script for entire store. not really proud of it,definitelly too much if statements
    {
        if (PlayerPrefs.GetInt("skin") == 0)
        { 
            
                text[0].text = "Equiped!";
       
        }

        else
        {
            text[0].text = "Equip!";
        }


        if (PlayerPrefs.GetInt("LGBTBought") != 0)
        {
            if (PlayerPrefs.GetInt("skin") == 1)
            {
                text[1].text = "Equiped!";
            }
            else
            {
                text[1].text = "Equip!";
            }
            
        }

        else
        {
            text[1].text = "Buy!";
        }


        if (PlayerPrefs.GetInt("NinjaBought") != 0)
        {
            if (PlayerPrefs.GetInt("skin") == 2)
            {
                text[2].text = "Equiped!";
            }
            else
            {
                text[2].text = "Equip!";
            }
           
        }

        else
        {
            text[2].text = "Buy!";
        }

        if (PlayerPrefs.GetInt("YellowBought") != 0)
        {
            if (PlayerPrefs.GetInt("skin") == 3)
            {
                text[3].text = "Equiped!";
            }
            else
            {
                text[3].text = "Equip!";
            }
            

        }
        else
        {
            text[3].text = "Buy!";

        }
    }
}
