using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy _AudioManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        // Saving Audio Manager between scenes.

        if (!_AudioManager)
        
            _AudioManager = this ;
        
        else
        {
            Destroy(this.gameObject);
        }



        DontDestroyOnLoad(gameObject);
    }
}
