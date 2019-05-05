using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.Extras;
using TMPro;
using Random = System.Random;

public class LevelSwitching : MonoBehaviour
{
    bool blower = false;
    public GameObject cart;
    public GameObject RollerCoasterPlace;
    public bool start = false;
    string currentScene;
    public bool level0Done = false;
    public bool level1Done = false;
    public bool level2Done = false;
    public AudioClip done;
    public AudioClip buzzer;
    public AudioClip TurboSound;
    bool off = false;
    public Vector3 playerPos;
    bool Shifted;
    AsyncOperation asyncLoadLevel;
    public GameObject player;
    public int FunTickets = 0;
    bool level0 = true;
    bool level1 = true;
    bool level2 = true;
    public GameObject Hand;
    public GameObject Time;
    public GameObject Score;
    public GameObject TicketsText;
    public GameObject LevelText ;
    bool Level1Enterence = false;
    bool Level2Enterence = false;
    public GameObject Level1Area;
    public GameObject Level2Area;
    public GameObject Table;
    public GameObject TrashBox;
    public GameObject GreenLED;
    public GameObject RedLED;
    public GameObject Turbo;
    float maxX;
    float minX;
    float maxZ;
    float minZ;
    float maxX2;
    float minX2;
    float maxZ2;
    float minZ2;
    float RmaxX;
    float RminX;
    float RmaxZ;
    float RminZ;
    float TmaxX;
    float TminX;
    float TmaxZ;
    float TminZ;
    float TrashmaxX;
    float TrashminX;
    float TrashmaxZ;
    float TrashminZ;
    bool inRide;
    int countRide = 0;
    GameObject Parent;
    Transform respawnpos;
    GameObject Object;
    int greenorred = 0;
    // Start is called before the first frame update

    void Awake()
    {

        DontDestroyOnLoad(gameObject);

    }
    void Start()
    {
        respawnpos = GreenLED.transform;
        UpdateLevel();
        Collider c = Level1Area.GetComponent<Collider>();
        maxX = c.bounds.max.x;
        minX = c.bounds.min.x;
        maxZ = c.bounds.max.z;
        minZ = c.bounds.min.z;
        Collider c2 = Level2Area.GetComponent<Collider>();
        maxX2 = c2.bounds.max.x;
        minX2= c2.bounds.min.x;
        maxZ2 = c2.bounds.max.z;
        minZ2 = c2.bounds.min.z;
        Collider x = RollerCoasterPlace.GetComponent<Collider>();
        RmaxX = x.bounds.max.x;
        RminX = x.bounds.min.x;
        RmaxZ = x.bounds.max.z;
        RminZ = x.bounds.min.z;
        Collider T = Table.GetComponent<Collider>();
        TmaxX = T.bounds.max.x;
        TminX = T.bounds.min.x;
        TmaxZ = T.bounds.max.z;
        TminZ = T.bounds.min.z;
        Collider Trash = TrashBox.GetComponent<Collider>();
        TrashmaxX = Trash.bounds.max.x;
        TrashminX = Trash.bounds.min.x;
        TrashmaxZ = Trash.bounds.max.z;
        TrashminZ = Trash.bounds.min.z;

    }

    // Update is called once per frame
    void Update()
    {


        if (level0Done && level0)
        {
            AudioSource.PlayClipAtPoint(done, player.transform.position);
            Destroy(GameObject.Find("TurnOffLight"));
            GameObject.Find("Robot Kyle0").GetComponent<Animator>().Play("Idle");
            GameObject.Find("Spot Light").gameObject.GetComponent<Light>().intensity = 10;
            level0 = false;
            Hand.GetComponent<VRLaserClick>().enabled = false;
            Hand.GetComponent<VRLaserClick1>().enabled = true;
            AddScore();
            ResetTime();
            UpdateLevel();
            start = false;
        }
        if (start)
        {
            if(!IsInvoking("TimeCounter"))
            Invoke("TimeCounter", 1f);
        }
        if (!Level1Enterence) {
            if (player.transform.position.x < maxX && player.transform.position.x > minX && player.transform.position.z < maxZ && player.transform.position.z > minZ)
            {
                if (level0Done && !level1Done)
                {
                    Level1Enterence = true;
                    start = true;
                }
            }
        }
        if (!Level2Enterence)
        {
            if (player.transform.position.x < maxX2 && player.transform.position.x > minX2 && player.transform.position.z < maxZ2 && player.transform.position.z > minZ2)
            {
                if (level1Done && !level2Done)
                {
                    Level2Enterence = true;
                    start = true;
                }
            }
        }
        if (!inRide)
        {
            if (player.transform.position.x < RmaxX && player.transform.position.x > RminX && player.transform.position.z < RmaxZ && player.transform.position.z > RminZ)
            {
                if (FunTickets > 0)
                {
                    //Parent = player.transform.parent.gameObject;
                    //player.transform.parent = cart.transform;
                    player.active = false;
                    cart.transform.GetChild(1).gameObject.active = true;
                    FunTickets--;
                    TicketsText.GetComponent<TextMeshProUGUI>().text = FunTickets.ToString();
                    inRide = true;
                }
            }
            countRide = 0;
        }
        if (inRide)
        {
            if (!IsInvoking("RideTime"))
            {
                Invoke("RideTime", 3f);
            }
        }
        if (!IsInvoking("toggle"))
        {
            Invoke("toggle", 0.3f);
        }

        if (level1Done)
        {
            if (level1)
            {
                AudioSource.PlayClipAtPoint(done, player.transform.position);
                level1 = false;
                Hand.GetComponent<VRLaserClick1>().enabled = false;
                Hand.GetComponent<VRLaserClick2>().enabled = true;
                Level1Enterence = false;
                AddScore();
                ResetTime();
                UpdateLevel();
                start = false;
            }

        }
        if (level2Done)
        {
            if (level2)
            {
                AudioSource.PlayClipAtPoint(done, player.transform.position);
                level2 = false;
                //Hand.GetComponent<VRLaserClick2>().enabled = false;
                AddScore();
                ResetTime();
                UpdateLevel();
                start = false;
            }

        }
        if (!IsInvoking("ColorSensor"))
        {
            Invoke("ColorSensor", 0.01f);
        }
        if (!IsInvoking("LEDCreator"))
        {
            Invoke("LEDCreator", 3f);
        }

    }
    void toggle()
    {
        if (Level1Enterence)
        {
            GameObject.Find("Interceptor").transform.FindChild("blueLight").gameObject.GetComponent<Light>().intensity = 0;
            GameObject.Find("Interceptor").transform.FindChild("redLight").gameObject.GetComponent<Light>().intensity = 0;
            off = true;
        }
        else
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
    void AddScore()
    {
        int ScoreInt = Convert.ToInt32(Score.GetComponent<TextMeshProUGUI>().text.Substring(8)) + Convert.ToInt32(Time.GetComponent<TextMeshProUGUI>().text);
        if(Convert.ToInt32(Time.GetComponent<TextMeshProUGUI>().text) > 0)
        {
            FunTickets++;
            if (Convert.ToInt32(Time.GetComponent<TextMeshProUGUI>().text) > 500)
            {
                FunTickets++;
                if (Convert.ToInt32(Time.GetComponent<TextMeshProUGUI>().text) > 700)
                {
                    FunTickets++;
                }
            }
            TicketsText.GetComponent<TextMeshProUGUI>().text = FunTickets.ToString();
        }
        Score.GetComponent<TextMeshProUGUI>().text = Score.GetComponent<TextMeshProUGUI>().text.Substring(0, 8) + ScoreInt;
    }
    void TimeCounter()
    {
        if(Time.GetComponent<TextMeshProUGUI>().text == "")
        {
            Time.GetComponent<TextMeshProUGUI>().text = "1000";
        }
        else
        {
            Time.GetComponent<TextMeshProUGUI>().text = "" + (Convert.ToInt32(Time.GetComponent<TextMeshProUGUI>().text) - 1);
        }
    }
    void ResetTime()
    {
        Time.GetComponent<TextMeshProUGUI>().text = "";
    }
    void UpdateLevel()
    {
        if(LevelText.GetComponent<TextMeshProUGUI>().text.Length < 8)
        LevelText.GetComponent<TextMeshProUGUI>().text = LevelText.GetComponent<TextMeshProUGUI>().text + " " +1;
        else
        {
            int currLevel = Convert.ToInt32(LevelText.GetComponent<TextMeshProUGUI>().text.Substring(8)) +1;
            print(currLevel);
            LevelText.GetComponent<TextMeshProUGUI>().text = LevelText.GetComponent<TextMeshProUGUI>().text.Substring(0, 8) + currLevel;
        }
    }
    void RideTime()
    {
        if(countRide == 0)
        {

        }
        else if (countRide == 1)
        {
            cart.transform.parent.GetComponent<Animator>().enabled = true;
        }
        else if(countRide == 10)
        {
            cart.transform.parent.GetComponent<Animator>().enabled = false;
            player.transform.position = new Vector3(1, 1, 40);
            player.active = true;            
            cart.transform.GetChild(1).gameObject.active = false;
            inRide = false;
            
        }
        countRide++;
    }
    void ColorSensor()
    {
        //Object = GameObject.Find("RedObject");
        //print(TminX + " " + TmaxX+" ");
        if (Object != null)
        {
            if (Object.name == "RedObject" && level2Done)
            {
                if (Object.active == true && Object.transform.position.x < TmaxX - 0.5 && Object.transform.position.x > TminX && Object.transform.position.z < TmaxZ && Object.transform.position.z > TminZ)
                {
                    //Object.transform.GetComponent<ConstantForce>().force = new Vector3(-0.25f,0,0);
                    //Object.transform.GetComponent<Rigidbody>().isKinematic = true;
                    Object.GetComponent<Rigidbody>().constraints |= RigidbodyConstraints.FreezePositionX;
                    Object.transform.GetComponent<ConstantForce>().relativeForce = new Vector3(0, 0, -1);
                    Object.transform.GetComponent<Rigidbody>().isKinematic = false;
                    if (blower == false)
                    {
                        AudioSource.PlayClipAtPoint(TurboSound, Turbo.transform.position);
                        blower = true;
                        print("in");
                    }
                    //print("in");
                }
                if (Object.active == true && Object.transform.position.x < TrashmaxX - 0.5 && Object.transform.position.x > TrashminX && Object.transform.position.z < TrashmaxZ - 0.5 && Object.transform.position.z > TrashminZ)
                {
                    //Object.transform.GetComponent<ConstantForce>().force = new Vector3(-0.25f,0,0);
                    //Object.transform.GetComponent<Rigidbody>().isKinematic = true;

                    Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    //Destroy(Object);
                    //Object.transform.GetComponent<ConstantForce>().force = new Vector3(0, 0, -1);
                    //Object.transform.GetComponent<Rigidbody>().isKinematic = false;

                    //print("in");
                }
            }
        }
    }
    void LEDCreator()
    {
        Destroy(Object);
        


        
        if (greenorred == 0)
        {
            Object = Instantiate(RedLED);
            Object.transform.position = respawnpos.position;
            Object.name = "RedObject";
            Object.SetActive(true);
            Object.GetComponent<ConstantForce>().force = new Vector3(-0.25f, 0, 0);
            Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Object.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
            Object.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;

        }
        else
        {
            Object = Instantiate(GreenLED);
            Object.transform.position = respawnpos.position;
            Object.SetActive(true);
            Object.GetComponent<ConstantForce>().force = new Vector3(-0.25f, 0, 0);
            Object.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Object.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionX;
            Object.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
        greenorred++;
        blower = false;
        greenorred = greenorred % 3;
    }
}
