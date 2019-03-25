using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class OnClick : MonoBehaviour
{
    int colorRand = 0;
    public GameObject malecable;
    public GameObject femalecable;
    public GameObject wire;
    public AudioClip done;
    public AudioClip zap;
    int connectionDone = 0;
    private string a;
    Ray ray;
    RaycastHit hit;
    bool connection = false;
    GameObject [,] connected = new GameObject[10,2];
    string[,] solution =new string [2,2] { {"5V","positivePort" } ,{ "GND","negativePort"}};
    int solDone = 0;
    int checkSoli = 0;
    int checkSolj = 0;
    int i = 0;
    int j = 0;
    GameObject target;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) & hit.collider.tag == "port")
            { GameObject temp = (GameObject)Instantiate(malecable);
                temp.name = hit.collider.name;

                temp.transform.position = hit.collider.transform.position + new Vector3(0, 0, 0);
                temp.SetActive(true);
                connected[i, j] = hit.collider.gameObject;
                //connected[i+1, j] = null;
                print(connected[i, j].name);
                //PointA.name = hit.collider.name;
                //PointA.transform.GetChild(0).name = hit.collider.name;
                //PointA.transform.GetChild(1).name = hit.collider.name;
                j++;
                AudioSource.PlayClipAtPoint(zap, temp.transform.position);
            }
            if (Input.GetMouseButtonDown(0) & hit.collider.tag == "ledport")
            {
                GameObject temp = (GameObject)Instantiate(femalecable);
                temp.name = hit.collider.name;
                temp.transform.position = hit.collider.transform.position + new Vector3(0, -0.25f, 0);
                temp.SetActive(true);
                connected[i, j] = hit.collider.gameObject;
                //connected[i + 1, j] = null;
                print(connected[i, j].name);
                //PointA.name = hit.collider.name;
                //PointA.transform.GetChild(0).name = hit.collider.name;
                j++;
                AudioSource.PlayClipAtPoint(zap,temp.transform.position);
            }



            /*if (Input.GetMouseButtonDown(0) & hit.collider.tag == "jumper")
            {
                if(connection == true)
                {
                    connected[i, 1] = hit.collider.name;
                    print(connected[i, 0]);
                    Instantiate(wire);
                    wire.transform.localScale =  new Vector3(.1f, Vector3.Distance(PointA.transform.position, PointB.transform.position) / 2,.1f);
                    wire.transform.position = Vector3.Lerp(PointA.transform.position, PointB.transform.position, (float)0.5);
                    wire.transform.LookAt(PointB.transform.position);
                    wire.transform.Rotate(90, 0, 0);
                    wire.transform.position += new Vector3(0, .2f, 0);
                    connection = false;

                }
                else {
                    connection = true;
                    connected[i,0] = hit.collider.name;
                    print(connected[i, 0]);
                    
                }
            }*/
            if (j == 2)
            {
                GameObject temp = Instantiate(wire);

                GameObject start = temp.transform.GetChild(1).gameObject;
                Collider c = connected[i, 0].GetComponent<Collider>();
                if (connected[i, 0].tag == "port")
                    start.transform.position = new Vector3(connected[i, 0].transform.position.x, c.bounds.max.y, connected[i, 0].transform.position.z);
                else
                    start.transform.position = new Vector3(connected[i, 0].transform.position.x, c.bounds.min.y, connected[i, 0].transform.position.z);

                start = temp.transform.GetChild(2).gameObject;
                c = connected[i, 1].GetComponent<Collider>();
                if (connected[i, 1].tag == "port")
                    start.transform.position = new Vector3(connected[i, 1].transform.position.x, c.bounds.max.y, connected[i, 1].transform.position.z);
                else
                    start.transform.position = new Vector3(connected[i, 1].transform.position.x, c.bounds.min.y, connected[i, 1].transform.position.z);

                //temp.transform.localScale = new Vector3(.1f, Vector3.Distance(connected[i,0].transform.position, connected[i, 1].transform.position) / 2, .1f);
                //temp.transform.position = Vector3.Lerp(connected[i, 0].transform.position, connected[i, 1].transform.position, (float)0.5);
                //temp.transform.LookAt(connected[i, 1].transform.position);
                //temp.transform.Rotate(90, 0, 0);
                //temp.transform.position += new Vector3(0, .3f, 0);
                //temp.transform.Rotate(0, 0, 0);
                Random rand = new Random();
                Color color = Color.red;
                switch (colorRand) {
                    case 0 :  color = Color.red; break;
                    case 1 : color = Color.blue; break;
                    case 2: color = Color.green; break;
                    case 3: color = Color.yellow; break;
                    case 4: color = Color.black; break;
                    case 5: color = Color.white; break;
                }
                colorRand++;
                colorRand %= 6;
                temp.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", color);
                connectionDone++;
                temp.SetActive(true);
                j = 0;
                checkScore();
            }
            //print(connectionDone);
            
        }
        
    }
    void checkScore()
    {
        for (int connect = 0; connect < connectionDone; connect++)
        {
            for (checkSoli = 0; checkSoli < solution.GetLength(0) && solDone < solution.GetLength(0); checkSoli++)
            {
                for (checkSolj = 0; checkSolj < 2; checkSolj++)
                {

                    if (connected[connect, 0].name == solution[checkSoli, checkSolj])
                    {
                        if (connected[connect, 1].name == solution[checkSoli, (checkSolj + 1) % 2])
                        {
                            solDone++;
                            print(solDone);
                            solution[checkSoli, checkSolj] = null;
                            solution[checkSoli, (checkSolj + 1) % 2] = null;
                            break;

                        }
                    }
                }
            }
        }
        if (solDone == solution.GetLength(0))
        {
            AudioSource.PlayClipAtPoint(done, transform.position);
            
        }
    }

}   
