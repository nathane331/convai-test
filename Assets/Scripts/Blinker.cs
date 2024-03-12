using System.Collections;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public Animator animator;
    public float minBlinkDelay = 3f;
    public float maxBlinkDelay = 6f;

    private bool isBlinking = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartBlinking();
    }

    // Initiate the blinking process
    void StartBlinking()
    {
        if (!isBlinking)
        {
            StartCoroutine(BlinkRoutine());
        }
    }

    // The coroutine that handles the blinking logic
    IEnumerator BlinkRoutine()
    {
        isBlinking = true;

        while (true)
        {
            float delay = Random.Range(minBlinkDelay, maxBlinkDelay);
            yield return new WaitForSeconds(delay);

            // Play the blinking animation
            animator.Play("Blinking.FaceBlinking", -1, 0f); // Using -1 as the layer index to play it on the base layer
            Debug.Log("Blinking.");
        }

        // Not reachable due to the infinite loop, but here for structure.
        // isBlinking = false; 
    }


    public void AdjustBlinkDelay(float minDelay, float maxDelay)
    {
        minBlinkDelay = minDelay;
        maxBlinkDelay = maxDelay;
    }
}