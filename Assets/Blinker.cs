using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public bool playAnim;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playAnim = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void LateUpdate()
    {
        if (playAnim)
            StartCoroutine("Blink");
    }

    IEnumerator Blink()
        {
            playAnim = false;
            yield return new WaitForSeconds(Random.Range(4f, 8f));
            animator.Play("Blinking.FaceBlinking", 1, 0f);
            Debug.Log("Blinking.");
            playAnim = true;

        }
}
