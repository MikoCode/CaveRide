using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObstacle : MonoBehaviour
{
    public SpriteRenderer[] renderer;
    public Obstacle[] bomb;
    public GameObject[] particles;
    public Sprite sprite;
    private int random;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {

        if(renderer[random] != null) // choosing a renderer for a wall obstacle. Green bomb shows player the way to go.
        {
            renderer[random].sprite = sprite;
        }
        
        bomb[random].isBroken = true; // making chosen bomb safe to hit.

        Destroy(particles[random]);

    }
}
