using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    
    public Image spriteRenderer;
    public Sprite[] sprite;
    private DontDestroy audioManager;
    public bool isMuted;
    public AudioSource audio;

    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindObjectOfType<DontDestroy>();
        audio =  audioManager.GetComponent<AudioSource>();
        
        isMuted = false;

        if(audio.volume == 0)
        {
            spriteRenderer.sprite = sprite[0];
            isMuted = true;
        }
        else
        {
            spriteRenderer.sprite = sprite[1];
            isMuted = false;
        }


       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Mute()
    {
        if(isMuted == false)
        {
            audio.volume = 0;
            spriteRenderer.sprite = sprite[0];
            isMuted = true;
        }
        else if(isMuted == true)
        {
            audio.volume = 0.04f;
            spriteRenderer.sprite = sprite[1];
            isMuted = false;
        }
       
    }

}
