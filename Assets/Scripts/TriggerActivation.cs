using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerActivation : MonoBehaviour
{
    public GameObject Spot;
    public GameObject teleportingPlane;
    public GameObject player;
    Vector3 playerPos;
    GameObject mainGameController;
    bool notin = true;
    float maxX;
    float minX;
    float maxZ;
    float minZ;
    // Start is called before the first frame update
    void Start()
    {
        mainGameController = GameObject.FindWithTag("MainGameController");
        Collider c = teleportingPlane.GetComponent<Collider>();
        maxX = c.bounds.max.x;
        minX = c.bounds.min.x;
        maxZ = c.bounds.max.z;
        minZ = c.bounds.min.z;
        player = mainGameController.GetComponent<LevelSwitching>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if(mainGameController == null)
        {
            mainGameController = GameObject.FindWithTag("MainGameController");
        }
        if (notin)
        {
            if (player.transform.position.x < maxX && player.transform.position.x > minX && player.transform.position.z < maxZ && player.transform.position.z > minZ)
            {
                Spot.gameObject.GetComponent<Light>().intensity = 0;
                mainGameController.GetComponent<LevelSwitching>().start = true;
                //teleportingPlane.SetActive(true);
                GameObject.Find("Robot Kyle0").GetComponent<Animator>().Play("Crouching");
                GameObject.Find("Robot Kyle0").transform.FindChild("Robot0Sign").gameObject.SetActive(true);
                notin = false;
            }
        }
    }

}
