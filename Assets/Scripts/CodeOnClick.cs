using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class CodeOnClick : MonoBehaviour
{
    public GameObject codeonclick2;
    public LevelSwitching levelSwitching;
    int colorRand = 0;
    public GameObject codeBlock;
    public GameObject placeHolder;
    public AudioClip done;
    public AudioClip take;
    public AudioClip put;
    public GameObject codeAssembler;
    GameObject currentBlock;
    int connectionDone = 0;
    private string a;
    Ray ray;
    RaycastHit hit;
    bool connection = false;
    GameObject[] connected = new GameObject[20];
    string[] solution = new string[6] { "Forever", "digitalOn12", "Wait1", "digitalOff12", "Wait1", "EndForever" };
    string[] solution2 = new string[6] { "Forever", "digitalOff12", "Wait1", "digitalOn12", "Wait1", "EndForever" };
    string[] solution3 = new string[6] { "Forever", "Wait1", "digitalOff12", "Wait1", "digitalOn12", "EndForever" };
    string[] solution4 = new string[6] { "Forever", "Wait1", "digitalOn12", "Wait1", "digitalOff12", "EndForever" };
    int solDone = 0;
    int checkSoli = 0;
    int checkSolj = 0;
    int i = 0;
    int j = 0;
    bool clear = false;
    GameObject target;
    int count = 0;
    double red = 0;
    double blue = 0;
    double green = 0;
    Transform position;
    // Start is called before the first frame update
    void Start()
    {
        position = codeAssembler.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) & hit.collider.tag == "codeBlock")
            {
                currentBlock = hit.collider.gameObject;
                AudioSource.PlayClipAtPoint(take, hit.collider.transform.position);

            }
            if (Input.GetMouseButtonDown(0) & hit.collider.name == "reset")
            {
                for(int loopdestroy = 0; loopdestroy < connected.Length; loopdestroy++)
                {
                    GameObject.Destroy(connected[loopdestroy]);
                    connected[loopdestroy] = null;
                }
                //placeHolder.transform.position = position.position;
                codeAssembler.transform.GetChild(0).transform.position = position.position - new Vector3(0, j * 0.33f, 0);
                j = 0;
            }
            if (Input.GetMouseButtonDown(0) & hit.collider.tag == "placeHolder")
            {

                GameObject temp = (GameObject)Instantiate(currentBlock);
                if (temp.gameObject.transform.GetChild(0).transform.childCount == 1)
                {
                    temp.name = currentBlock.name;
                }
                else
                {
                    TextMeshProUGUI value = (temp.gameObject.transform.Find("Canvas/value").gameObject.GetComponent<TextMeshProUGUI>());
                    temp.name = currentBlock.name + value.text;
                    Destroy(temp.gameObject.transform.GetChild(1).gameObject);
                    Destroy(temp.gameObject.transform.GetChild(2).gameObject);
                }
                
                //temp.name = hit.collider.name;
                temp.transform.parent = codeAssembler.transform;
                temp.transform.position = hit.collider.transform.position + new Vector3(0, 0, 0);
                temp.transform.Rotate(0, 270, 0);
                temp.transform.localScale = new Vector3(.33f, 1f, 1f);
                temp.SetActive(true);
                connected[j] = temp;
                codeAssembler.transform.FindChild("placeHolder").transform.position += new Vector3(0, -0.33f, 0);



                //connected[i+1, j] = null;
                print(connected[j].name);
                codeAssembler.transform.position += new Vector3(0, 0.33f, 0);
                //PointA.name = hit.collider.name;
                //PointA.transform.GetChild(0).name = hit.collider.name;
                //PointA.transform.GetChild(1).name = hit.collider.name;
                j++;
                AudioSource.PlayClipAtPoint(put, temp.transform.position);
            }
            if (Input.GetMouseButtonDown(0) & hit.collider.name == "-ve")
            {
                string value = hit.collider.gameObject.transform.parent.Find("Canvas/value").gameObject.GetComponent<TextMeshProUGUI>().text;
                int valueToInt = Convert.ToInt32(value) - 1;
                if (valueToInt < 0)
                {
                    valueToInt = 0;
                }
                hit.collider.gameObject.transform.parent.Find("Canvas/value").gameObject.GetComponent<TextMeshProUGUI>().text = valueToInt + "";
            }
            if (Input.GetMouseButtonDown(0) & hit.collider.name == "+ve")
            {
                string value = hit.collider.gameObject.transform.parent.Find("Canvas/value").gameObject.GetComponent<TextMeshProUGUI>().text;
                int valueToInt = Convert.ToInt32(value) + 1;
                hit.collider.gameObject.transform.parent.Find("Canvas/value").gameObject.GetComponent<TextMeshProUGUI>().text = valueToInt + "";
            }
            if (clear == false)
            {
                checkScore();
            }

        }
        void checkScore()
        {
            if (j >= solution.Length)
            {

                for (checkSoli = 0; checkSoli < solution.Length; checkSoli++)
                {

                    if (connected[checkSoli].name == solution[checkSoli])
                    {
                        clear = true;
                    }
                    else
                    {
                        clear = false;
                        break;
                    }
                }
            }
            if (clear)
            {

                levelSwitching.level1Done = true;
                codeonclick2.GetComponent<CodeOnClick2>().enabled = true;
                this.GetComponent<CodeOnClick>().enabled = false;

            }
            else
            {
                if (j >= solution2.Length)
                {

                    for (checkSoli = 0; checkSoli < solution2.Length; checkSoli++)
                    {

                        if (connected[checkSoli].name == solution2[checkSoli])
                        {
                            clear = true;
                        }
                        else
                        {
                            clear = false;
                            break;
                        }
                    }
                }
                if (clear)
                {
                    levelSwitching.level2Done = true;
                    codeonclick2.GetComponent<CodeOnClick2>().enabled = true;
                    this.GetComponent<CodeOnClick>().enabled = false;
                }
                else
                {
                    if (j >= solution3.Length)
                    {

                        for (checkSoli = 0; checkSoli < solution3.Length; checkSoli++)
                        {

                            if (connected[checkSoli].name == solution3[checkSoli])
                            {
                                clear = true;
                            }
                            else
                            {
                                clear = false;
                                break;
                            }
                        }
                    }
                    if (clear)
                    {
                        levelSwitching.level2Done = true;
                        codeonclick2.GetComponent<CodeOnClick2>().enabled = true;
                        this.GetComponent<CodeOnClick>().enabled = false;
                    }
                    else
                    {
                        if (j >= solution4.Length)
                        {

                            for (checkSoli = 0; checkSoli < solution4.Length; checkSoli++)
                            {

                                if (connected[checkSoli].name == solution4[checkSoli])
                                {
                                    clear = true;
                                }
                                else
                                {
                                    clear = false;
                                    break;
                                }
                            }
                        }
                        if (clear)
                        {
                            levelSwitching.level2Done = true;
                            codeonclick2.GetComponent<CodeOnClick2>().enabled = true;
                            this.GetComponent<CodeOnClick>().enabled = false;
                        }
                    }
                }
            }
        }

    }
}
