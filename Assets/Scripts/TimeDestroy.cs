using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour // script used to destroy some obstacles after time, just in case that destroyer attached to player didnt destroyed them. It is made for performance purpose.
{
    public int time;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
