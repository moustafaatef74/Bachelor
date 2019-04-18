//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Random = System.Random;
namespace Valve.VR.Extras
{
    public class VRLaserClick1 : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;

        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");

        public bool active = true;
        public Color color;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;

        Transform previousContact = null;

        int colorRand = 0;
        public LevelSwitching levelSwitching;
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
        GameObject[] connected = new GameObject[30];
        string[] solution = new string[8] { "Forever", "digitalOn12", "Wait1", "digitalOn13", "digitalOff12", "Wait1", "digitalOff13", "EndForever" };
        int solDone = 0;
        int checkSoli = 0;
        int checkSolj = 0;
        int i = 0;
        int j = 0;
        bool clear = false;
        GameObject target;
        int count = 0;

        private void Start()
        {
            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object");

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.");


            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.identity;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            pointer.transform.localRotation = Quaternion.identity;
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    UnityEngine.Object.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }


        private void Update()
        {
            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.fromInputSource = pose.inputSource;
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            if (bHit && interactWithUI.GetStateUp(pose.inputSource))
            {
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.transform;

                if (hit.collider.tag == "codeBlock")
                {
                    currentBlock = hit.collider.gameObject;
                    AudioSource.PlayClipAtPoint(take, hit.collider.transform.position);
                }

                if (hit.collider.tag == "placeHolder")
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
                    codeAssembler.transform.position += new Vector3(0, 1, 0);
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
                    int valueToInt = System.Convert.ToInt32(value) + 1;
                    hit.collider.gameObject.transform.parent.Find("Canvas/value").gameObject.GetComponent<TextMeshProUGUI>().text = valueToInt + "";
                }
                if (clear == false)
                {
                    checkScore();
                }



                OnPointerClick(argsClick);
            }

            if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
            {
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }
        public struct PointerEventArgs
        {
            public SteamVR_Input_Sources fromInputSource;
            public uint flags;
            public float distance;
            public Transform target;
        }


        public delegate void PointerEventHandler(object sender, PointerEventArgs e);
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
                
            }
        }
    }

    
}