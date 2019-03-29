using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerActivation : MonoBehaviour
{
    public GameObject Spot;
    public GameObject teleportingPlane;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (this.transform.name == "TurnOffLight")
        {
            Spot.gameObject.GetComponent<Light>().intensity = 0;
            teleportingPlane.SetActive(true);
            
        }
        if(this.transform.name == "SolvingScenePortal")
        {
            SceneManager.LoadScene(2);
        }
    }
}
