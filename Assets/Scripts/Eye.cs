using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public float timeBtwBlink;
    private Vector2 pos;
    public Animator animator;
    private int eyeAnimation = 0;
    private float timeBtwAnimation;
    private string[] eyeTrigger = new string[] { "eye1", "eye2", "eye3", "eye4" };

    // Start is called before the first frame update
    void Start()
    {
        timeBtwAnimation = 5;
        pos.x = transform.position.x;
        pos.y = transform.position.y;
        timeBtwBlink = 10;  
    }

    // Update is called once per frame
    void Update()
    {

       
        MovingEye();
        
    }



   


    void MovingEye()
    {
        timeBtwAnimation -= 1 * Time.deltaTime;
        if(timeBtwAnimation <= 0)
        {
            animator.SetTrigger(eyeTrigger[eyeAnimation]);
            eyeAnimation = Random.Range(0, 4);
            timeBtwAnimation = Random.Range(3,8);
        }
       
    }



   
    

}
