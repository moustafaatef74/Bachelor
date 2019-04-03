using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class waitRollerCoaster : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AnimationController());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator AnimationController()
    {
        
        yield return new WaitForSeconds(4);
        animator.Play("loop");
        yield return new WaitForSeconds(14);
        SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
    }
}
