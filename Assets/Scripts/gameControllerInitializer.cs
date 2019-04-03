using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameControllerInitializer : MonoBehaviour
{
    public GameObject MainGameController;
    // Start is called before the first frame update
    void Start()
    {

            if (!(GameObject.FindWithTag("MainGameController"))) { 
                Instantiate(MainGameController, transform.position, transform.rotation).name = "mainGameController";
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
