using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerActivation : MonoBehaviour
{
    public GameObject Spot;
    public GameObject teleportingPlane;
    Vector3 playerPos;
    GameObject mainGameController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mainGameController == null)
        {
            mainGameController = GameObject.FindWithTag("MainGameController");
        }
    }
    void OnTriggerEnter(Collider other)
    {
        playerPos = GameObject.Find("FPSController").transform.position;
        if (this.transform.name == "TurnOffLight")
        {
            Spot.gameObject.GetComponent<Light>().intensity = 0;
            //teleportingPlane.SetActive(true);
            GameObject.Find("Robot Kyle0").GetComponent<Animator>().Play("Crouching");
            
        }
        if(this.transform.name == "SolvingScene0Portal")
        {
            
            mainGameController.GetComponent<LevelSwitching>().playerPos = playerPos;
            SceneManager.LoadScene(2);
            
        }
    }
}
