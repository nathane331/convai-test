using Meta.WitAi.TTS.Utilities;
using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Text.RegularExpressions;


public class SpeakConnector : MonoBehaviour
{
    [Header("ChatGPT and TTS")]
    [SerializeField] ChatGPT chat;
    [SerializeField] public TTSSpeaker _speaker;

    [Header("Animation")]
    [SerializeField] Animator animator;
    [SerializeField] Blinker blinkerScript;
    [Range(0f, 1f)]
    [SerializeField] float expressionChangeSpeed = 1f;

    [Header("Targets")]
    [SerializeField] Transform eyeTarget;
    [SerializeField] Transform headTarget;
    [SerializeField] Transform talkingLookAtTarget;

    [Header("Movement Parameters")]
    [SerializeField] float eyeMovementDelay = 0.2f;
    [SerializeField] float headMovementDelay = 0.1f;
    [SerializeField] float movementSpeed = 1f;

    private Vector3 originalEyeTargetPosition;
    private Vector3 originalHeadTargetPosition;

    private bool isTalking = false;

    List<string> splitStringList = new List<string>();
    string[] splitString;
    string newSentence;


    AudioClip umm;


    [SerializeField] string emotion;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // Default positions
        originalEyeTargetPosition = eyeTarget.position;
        originalHeadTargetPosition = headTarget.position;
        //default idle
        emotion = "Neutral";
        chat.chatResponse = null;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateIdleBehavior();

        if (chat.chatResponse != null)
        {
            string[] newSentenceEmotionArray = SplitResponse(chat.chatResponse);

            newSentence = newSentenceEmotionArray[0];
            emotion = newSentenceEmotionArray[1];

            isTalking = true; // Character starts talking
            StartCoroutine(StartTTS(newSentence)); // Begin reading
            chat.chatResponse = null;  // Set back to null.

            // Reset positions immediately for talking
            eyeTarget.position = originalEyeTargetPosition;
            headTarget.position = originalHeadTargetPosition;
        }
    }
    void UpdateIdleBehavior()
    {
        if (!isTalking)
        {
            // Example: Making the character look around at random points
            // Adjust the range and timing to suit your needs
            float eyeX = Mathf.Sin(Time.time * 0.5f) * 0.1f;
            float eyeY = Mathf.Cos(Time.time * 0.75f) * 0.1f;
            Vector3 eyeIdlePosition = originalEyeTargetPosition + new Vector3(eyeX, eyeY, 0);
            eyeTarget.position = Vector3.Lerp(eyeTarget.position, eyeIdlePosition, Time.deltaTime * movementSpeed);

            float headX = Mathf.Sin(Time.time * 0.5f) * 0.05f;
            Vector3 headIdlePosition = originalHeadTargetPosition + new Vector3(headX, 0, 0);
            headTarget.position = Vector3.Lerp(headTarget.position, headIdlePosition, Time.deltaTime * movementSpeed);
        }
    }

    void LateUpdate()
    {
        animator.Play("Emotion");
        switch (emotion)
        {
            case "Happy_low":
                //Debug.Log("Current emotion is: " + emotion);

                animator.SetFloat("emotionBlendX", 0.5f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 0.5f, expressionChangeSpeed, Time.deltaTime);
                blinkerScript.AdjustBlinkDelay(2f, 4f); // Adjust blink delay for Happy_low
                break;
            case "Happy_high":
                //Debug.Log("Current emotion is: " + emotion);

                animator.SetFloat("emotionBlendX", 1f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 1f, expressionChangeSpeed, Time.deltaTime);
                blinkerScript.AdjustBlinkDelay(1.5f, 3f); // Adjust blink delay for Happy_high
                break;
            case "Sad_low":
                //Debug.Log("Current emotion is: " + emotion);

                animator.SetFloat("emotionBlendX", -0.5f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", -0.5f, expressionChangeSpeed, Time.deltaTime);
                break;
            case "Sad_high":
                //Debug.Log("Current emotion is: " + emotion);

                animator.SetFloat("emotionBlendX", -1f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", -1f, expressionChangeSpeed, Time.deltaTime);
                break;
            case "Neutral":
                //Debug.Log("Current emotion is: " + emotion);
                animator.SetFloat("emotionBlendX", 0f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 0f, expressionChangeSpeed, Time.deltaTime);
                blinkerScript.AdjustBlinkDelay(3f, 6f); // Reset to default blink delay
                break;
            case "Angry":
                //Debug.Log("Current emotion is: " + emotion);
                animator.SetFloat("emotionBlendX", -1f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 0f, expressionChangeSpeed, Time.deltaTime);
                break;
            default:
                animator.SetFloat("emotionBlendX", 0f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 0f, expressionChangeSpeed, Time.deltaTime);
                blinkerScript.AdjustBlinkDelay(3f, 6f); // Default blinking speed for unhandled emotions
                break;
        }

    }

    IEnumerator StartTTS(string s)
    {
        isTalking = true;
        Debug.Log("ChatGPT response received. Starting TTS.");
        _speaker.Speak(s);

        // Focus on talking target
        float duration = 3f; // Adjust based on TTS length or dynamically calculate
        float timer = 0;
        while (timer < duration)
        {
            if (talkingLookAtTarget != null)
            {
                eyeTarget.position = Vector3.Lerp(eyeTarget.position, talkingLookAtTarget.position, Time.deltaTime * movementSpeed);
                headTarget.position = Vector3.Lerp(headTarget.position, talkingLookAtTarget.position, Time.deltaTime * movementSpeed);
            }
            timer += Time.deltaTime;
            yield return null;
        }

        isTalking = false;
    }

    string[] SplitResponse(string s)
    {
        //also mends + appends the proper text response.

        //break off where the emotion response starts. 
        string[] splitEmotion = s.Split('[');


        splitEmotion[0] = AppendResponse(splitEmotion[0]); //this is my new sentence


        splitEmotion[1] = splitEmotion[1].Replace("]", ""); //this is my emotion

        Debug.Log(splitEmotion[0].ToString());
        Debug.Log(splitEmotion[1].ToString());

        return splitEmotion;
    }

    IEnumerator MoveTargetWithDelay(Transform target, Vector3 direction, float delay)
    {
        yield return new WaitForSeconds(delay);
        // Move towards a new position based on direction. This is a simplified approach.
        Vector3 newPosition = target.position + direction;
        while (Vector3.Distance(target.position, newPosition) > 0.01f)
        {
            target.position = Vector3.MoveTowards(target.position, newPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
    string AppendResponse(string s) //fix the new response
    {
        s = s.Replace("\"", "");

        char lastCharacter = s[s.Length - 1]; //get the last character in the string
                                              //Debug.Log("last character is \"" + lastCharacter + "\"");

        Debug.Log("last character is \"" + lastCharacter + "\"");

        if (lastCharacter == ' ') //if its anything other than a . ! or ?
        {
            s = s.Remove(s.Length - 1, 1);

            lastCharacter = s[s.Length - 1]; //get the last character in the string
            Debug.Log("last character is \"" + lastCharacter + "\"");

            //if last letter is a character with no period
            if (lastCharacter != '.' && lastCharacter != '?' && lastCharacter != '!' && lastCharacter != ',')
                s = s + '.';

            if (lastCharacter == ',')
            {
                s = s.Remove(s.Length - 1, 1) + '.';
            }

        }

        if (lastCharacter == ',')
        {
            s = s.Remove(s.Length - 1, 1) + '.';
        }

        return s;

    }
}