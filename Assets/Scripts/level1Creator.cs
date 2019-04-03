using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level1Creator : MonoBehaviour
{
    public GameObject wire;
    public GameObject[,] connected = new GameObject[10, 2];
    public GameObject femalecable;
    public GameObject malecable;
    int i = 0;
    int colorRand = 0;
    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        connected[0, 0] = GameObject.Find("12");
        connected[0, 1] = GameObject.Find("LED").transform.FindChild("positivePort").gameObject;
        Connect();
        i++;
        connected[1, 0] = GameObject.Find("12");
        connected[1, 1] = GameObject.Find("LED (1)").transform.FindChild("positivePort").gameObject;
        Connect();
        i++;
        connected[2, 0] = GameObject.Find("GND");
        connected[2, 1] = GameObject.Find("LED").transform.FindChild("negativePort").gameObject;
        Connect();
        i++;
        connected[3, 0] = GameObject.Find("GND");
        connected[3, 1] = GameObject.Find("LED (1)").transform.FindChild("negativePort").gameObject;
        Connect();
        i++;

        for (int i = 0; i < connected.GetLength(0); i++)
        {
            for (int j = 0; j < connected.GetLength(1); j++)
            {
                if ((i == 0 && j == 0) || (i == 2 && j == 0))
                {

                }
                else
                {
                    if (connected[i, j].tag == "port")
                    {
                        temp = Instantiate(malecable);
                        temp.transform.position = connected[i, j].transform.position + new Vector3(0, 0, 0);
                    }

                    if (connected[i, j].tag == "ledport") {
                        temp = Instantiate(femalecable);
                        temp.transform.position = connected[i, j].transform.position + new Vector3(0, -0.25f, 0);
                    }
                    temp.SetActive(true);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Connect()
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

        ropeColor(temp);
    }
    void ropeColor(GameObject temp)
    {
        Color color = Color.red;
        switch (colorRand)
        {
            case 0: color = Color.red; break;
            case 1: color = Color.blue; break;
            case 2: color = Color.green; break;
            case 3: color = Color.yellow; break;
            case 4: color = Color.black; break;
            case 5: color = Color.white; break;
        }
        colorRand++;
        colorRand %= 6;
        temp.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        temp.SetActive(true);
    }
}
