//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;
namespace Valve.VR.Extras
{
    public class VRLaserClick : MonoBehaviour
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
        GameObject[,] connected = new GameObject[10, 2];
        string[,] solution = new string[2, 2] { { "5V", "positivePort" }, { "GND", "negativePort" } };
        int solDone = 0;
        int checkSoli = 0;
        int checkSolj = 0;
        int i = 0;
        int j = 0;
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
                    Object.Destroy(collider);
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
                connectionDone++;
                temp.SetActive(true);
                j = 0;
                //checkScore();
            }
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

                if (hit.collider.tag == "port")
                {
                    GameObject temp = (GameObject)Instantiate(malecable);
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
                if (hit.collider.tag == "ledport")
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
                    AudioSource.PlayClipAtPoint(zap, temp.transform.position);
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
    }

    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }


    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}