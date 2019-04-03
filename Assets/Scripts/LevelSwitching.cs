using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSwitching : MonoBehaviour
{
    string currentScene;
    public bool level0Done = false;
    public bool level1Done = false;
    public Vector3 playerPos;
    bool Shifted;
    AsyncOperation asyncLoadLevel;
    GameObject player;
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
        player = GameObject.Find("FPSController");
        currentScene = SceneManager.GetActiveScene().name;

        if (level0Done && currentScene == "arduinoScene0")
        {
            print("done");
            asyncLoadLevel = SceneManager.LoadSceneAsync("rollercoaster",LoadSceneMode.Single);
        }
        if (level0Done && currentScene == "Main" && asyncLoadLevel.isDone)
        {
            Destroy(GameObject.Find("SolvingScene0Portal"));
            Destroy(GameObject.Find("TurnOffLight"));
            GameObject.Find("Robot Kyle0").GetComponent<Animator>().Play("Idle");
        }
    }
}
