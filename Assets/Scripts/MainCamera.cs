using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform playerPos;
    public Camera camera;
    public ParticleSystem lighting;
    public PlayerController playerCon;
    public Transform camTransform;
    public AudioSource audio;
    public AudioClip thunder;
    private Vector3 camOriginalPos;
    public float camShakeDuration;
    public float smoothSpeed = 0.125f;
    public float camShakeAmount;
    public float timeBtwThunder;
    public float decrementFactor;
    public bool isShaking = false;



    // Start is called before the first frame update
    void Start()
    {
        timeBtwThunder = Random.Range(5,30);
    }

    private void OnEnable()
    {
        

    }

    // Update is called once per frame
    void FixedUpdate() // Camera follows player with smoothly
    {
       
         Vector3 desiredPosition = new Vector3(playerPos.position.x + 5, 0, -10f);
         Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
         transform.position = smoothedPosition;

        

        if(isShaking == true) //shaking camera,after the boolean is set to true. 
        {
            StartCoroutine("camerashake");
            camOriginalPos = smoothedPosition;
            if (isShaking == true)
            {
                camTransform.localPosition = camOriginalPos + Random.insideUnitSphere * camShakeAmount;
                camShakeDuration -= 1*  Time.deltaTime * decrementFactor;
            }
            else
            {
                camShakeDuration = 0f;
                camTransform.localPosition = camOriginalPos;
            }
        }
    }
    private void Update()
    {
        timeBtwThunder -= 1 * Time.deltaTime;
        if(timeBtwThunder <= 0)
        {
            StartCoroutine("Thunder");
            timeBtwThunder = Random.Range(15, 35);
        }
        
           

        
    }



    IEnumerator camerashake()
    {
        
        yield return new WaitForSeconds(0.3f);
        isShaking = false;
    }



    IEnumerator Thunder()  //Making thunder effect, by making background white few times for a very short time,and also playing a audio clip.
    {
        audio.PlayOneShot(thunder,0.6f);
        yield return new WaitForSeconds(0.15f);
        camera.backgroundColor = new Color(0.86f, 0.86f, 0.86f, 1);     
        yield return new WaitForSeconds(0.05f);
        camera.backgroundColor = new Color(0.2958135f, 0.3113208f, 0.2919367f, 0);
        yield return new WaitForSeconds(0.05f);
        camera.backgroundColor = new Color(0.86f, 0.86f, 0.86f, 1);
        yield return new WaitForSeconds(0.05f);
        camera.backgroundColor = new Color(0.2958135f, 0.3113208f, 0.2919367f, 0);
        yield return new WaitForSeconds(0.05f);
        camera.backgroundColor = new Color(0.86f, 0.86f, 0.86f, 1);
        yield return new WaitForSeconds(0.05f);
        camera.backgroundColor = new Color(0.2958135f, 0.3113208f, 0.2919367f, 0);
        
    }


}
