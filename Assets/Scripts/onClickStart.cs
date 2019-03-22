using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class onClickStart : MonoBehaviour
{
    public Button start;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void startGame()
    {
        SceneManager.LoadScene("Main");
    }
}
