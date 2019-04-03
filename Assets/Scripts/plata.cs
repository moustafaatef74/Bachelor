using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plata : MonoBehaviour
{
    public float scaleX = .0147f;
    public float scaleY = .01875f;
    public GameObject _3dObject;
    Renderer rend;
    void Start()
    {
        rend = _3dObject.GetComponent<Renderer>();
        rend.material.mainTextureScale = new Vector2(scaleX, scaleY);
        rend.materials[1].mainTextureScale = new Vector2(scaleX, scaleY);
        //rend.material.SetTextureOffset("_MainTex", offset);
        //rend.material.SetTextureScale("_MainTex", new Vector2(200, 200));
    }
    void Update()
    {
        rend = _3dObject.GetComponent<Renderer>();
        rend.material.mainTextureScale = new Vector2(scaleX, scaleY);
        rend.materials[1].mainTextureScale = new Vector2(scaleX, scaleY);
    }

}
