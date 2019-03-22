using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class solvingProblemPortal : MonoBehaviour
{
    public GameObject player;
    public GameObject portal;
    bool notin = true;
    float maxX;
    float minX;
    float maxZ;
    float minZ;

    // Start is called before the first frame update
    void Start()
    {
        Collider c = portal.GetComponent<Collider>();
        maxX = c.bounds.max.x;
        minX = c.bounds.min.x;
        maxZ = c.bounds.max.z;
        minZ = c.bounds.min.z;
        print(maxX + "," + minX + " " + maxZ + "," + minZ);

    }

    // Update is called once per frame
    void Update()
    {
        if (notin)
            if (player.transform.position.x<maxX && player.transform.position.x > minX && player.transform.position.z < maxZ && player.transform.position.z > minZ)
            {

                print("in");
                notin = false;
                SceneManager.LoadScene(2);
            }
    }
}
