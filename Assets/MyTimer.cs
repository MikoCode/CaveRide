using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyTimer : MonoBehaviour
{

    public float duration;
    public Image fillImage;
    public ParticleSystem particle;
    public AudioSource source;
    public AudioClip clip;
    public GameManager gM;

    // Start is called before the first frame update
    void Start()
    {
        duration = 4;
        fillImage.fillAmount = 1f;
        StartCoroutine(Timer(duration));
    }

    // Update is called once per frame
    void Update()
    {
          if(gM.skipped == true)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            source.PlayOneShot(clip, 0.6f);
            Destroy(gameObject, 0.35f);
            gM.skipped = false;


        }
          

        
    }



    public IEnumerator Timer(float duration) // Timer that appears after player hit obstacle for the first time,counting the time for a revive.
        
    {

        float startTime = Time.time;
        float time = duration;
        float value = 0;
        while(Time.time - startTime < duration)
        {
            time -= Time.deltaTime;
            value = time / duration;
            fillImage.fillAmount = value;
            yield return null;
        }

        if(gM.isRevived == false)
        {
            for (int i = 0; i < 1; i++)
            {
                Instantiate(particle, transform.position, Quaternion.identity);
                source.PlayOneShot(clip, 0.6f);
            }
        }

        yield return new WaitForSeconds(1);
        Destroy(gameObject);
       
       
       
    }

    
}
