using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class VRClick : MonoBehaviour
{
    public SteamVR_Input_Sources thisHand;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions._default.GrabPinch.GetStateUp(thisHand))
        {

            //Instantiate(bulletPrefab);
        }
    }



}