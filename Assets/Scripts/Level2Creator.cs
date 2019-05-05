using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Creator : MonoBehaviour
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
        connected[0, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box003").transform.FindChild("3").gameObject;
        connected[0, 1] = GameObject.Find("ColorSensor").transform.FindChild("Chip").transform.FindChild("S0").gameObject;
        Connect();
        i++;
        connected[1, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box003").transform.FindChild("4").gameObject;
        connected[1, 1] = GameObject.Find("ColorSensor").transform.FindChild("Chip").transform.FindChild("S1").gameObject;
        Connect();
        i++;
        connected[2, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box002").transform.FindChild("GND").gameObject;
        connected[2, 1] = GameObject.Find("ColorSensor").transform.FindChild("Chip").transform.FindChild("GND").gameObject;
        Connect();
        i++;
        connected[3, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box002").transform.FindChild("5V").gameObject;
        connected[3, 1] = GameObject.Find("ColorSensor").transform.FindChild("Chip").transform.FindChild("VCC").gameObject;
        Connect();
        i++;
        connected[4, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box003").transform.FindChild("5").gameObject;
        connected[4, 1] = GameObject.Find("ColorSensor").transform.FindChild("Chip").transform.FindChild("S2").gameObject;
        Connect();
        i++;
        connected[5, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box003").transform.FindChild("6").gameObject;
        connected[5, 1] = GameObject.Find("ColorSensor").transform.FindChild("Chip").transform.FindChild("S3").gameObject;
        Connect();
        i++;
        connected[6, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box003").transform.FindChild("2").gameObject;
        connected[6, 1] = GameObject.Find("ColorSensor").transform.FindChild("Chip").transform.FindChild("OUT").gameObject;
        Connect();
        i++;
        connected[7, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box003").transform.FindChild("1").gameObject;
        connected[7, 1] = GameObject.Find("Scene2").transform.FindChild("Blower").transform.FindChild("positivePort").gameObject;
        Connect();
        i++;
        connected[8, 0] = GameObject.Find("arduino (2)").transform.FindChild("Box004").transform.FindChild("GND").gameObject;
        connected[8, 1] = GameObject.Find("Scene2").transform.FindChild("Blower").transform.FindChild("negativePort").gameObject;
        Connect();



        for (int i = 0; i < connected.GetLength(0); i++)
        {
            for (int j = 0; j < connected.GetLength(1); j++)
            {

                    if (connected[i, j].tag == "port")
                    {
                        temp = Instantiate(malecable);
                        temp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                        temp.transform.position = connected[i, j].transform.position + new Vector3(0, 0, 0);
                        temp.transform.parent = GameObject.Find("Scene2Connections").transform;
                    }

                    if (connected[i, j].tag == "ledport")
                    {
                        temp = Instantiate(femalecable);
                        temp.transform.localScale = new Vector3(1.25f, 0.45f, 0.2f);
                        temp.transform.position = connected[i, j].transform.position + new Vector3(0, -0.08f, 0);
                        temp.transform.parent = GameObject.Find("Scene2Connections").transform;

                    }
                    if(connected[i, j].tag == "Zport")
                    {
                        temp = Instantiate(femalecable);
                        temp.transform.localScale = new Vector3(0.3f,1.25f, 0.15f);
                        temp.transform.position = connected[i, j].transform.position + new Vector3(0, -0.07f, 0);
                        temp.transform.parent = GameObject.Find("Scene2Connections").transform;
                    }
                    temp.SetActive(true);
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
        temp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
            case 6: color = Color.magenta; break;
        }
        colorRand++;
        colorRand %= 7;
        temp.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        temp.SetActive(true);
        temp.transform.parent = GameObject.Find("Scene1Connections").transform;
    }
}
