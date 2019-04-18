using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;

public class LevelSwitching : MonoBehaviour
{
    string currentScene;
    public bool level0Done = false;
    public bool level1Done = false;
    public AudioClip done;
    bool off = false;
    public Vector3 playerPos;
    bool Shifted;
    AsyncOperation asyncLoadLevel;
    GameObject player;
    public int FunTickets = 0;
    bool level0 = true;
    bool level1 = true;
    public GameObject Hand;
    // Start is called before the first frame update

    void Awake()
    {

        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = GameObject.Find("FPSController").transform.position;
        if (level0Done && level0)
        {
            AudioSource.PlayClipAtPoint(done, this.transform.position);
            Destroy(GameObject.Find("TurnOffLight"));
            GameObject.Find("Robot Kyle0").GetComponent<Animator>().Play("Idle");
            GameObject.Find("Spot Light").gameObject.GetComponent<Light>().intensity = 10;
            FunTickets++;
            level0 = false;
            Hand.GetComponent<VRLaserClick>().enabled = false;
            Hand.GetComponent<VRLaserClick1>().enabled = true;
        }

        if (level1Done)
        {
            if (level1)
            {
                AudioSource.PlayClipAtPoint(done, transform.position);
                level1 = false;
                Hand.GetComponent<VRLaserClick1>().enabled = false;
                FunTickets++;
            }
            if (!IsInvoking("toggle"))
            {
                Invoke("toggle", 0.3f);
            }
        }

    }
    void toggle()
    {
        if (off)
        {
            GameObject.Find("Interceptor").transform.FindChild("blueLight").gameObject.GetComponent<Light>().intensity = 4;
            GameObject.Find("Interceptor").transform.FindChild("redLight").gameObject.GetComponent<Light>().intensity = 4;
            off = false;
        }
        else
        {
            GameObject.Find("Interceptor").transform.FindChild("blueLight").gameObject.GetComponent<Light>().intensity = 0;
            GameObject.Find("Interceptor").transform.FindChild("redLight").gameObject.GetComponent<Light>().intensity = 0;
            off = true;
        }

    }

}
