using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackgroun : MonoBehaviour
{
    public Transform backtransform;
    public GameObject cam;
    public PlayerController playerCon;
    private float lenght, startPos;
    public float parralaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parralaxEffect));
            
        float distance = (cam.transform.position.x * parralaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if ( temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if (temp < startPos - lenght)
        {
            startPos -= lenght;
        }
    }


   
}
