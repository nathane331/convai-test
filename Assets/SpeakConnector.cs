using Meta.WitAi.TTS.Utilities;
using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Text.RegularExpressions;


public class SpeakConnector : MonoBehaviour
{
    [SerializeField] ChatGPT chat;
    [SerializeField] public TTSSpeaker _speaker;
    [SerializeField] Animator animator;
    [Range(0f, 1f)]
    [SerializeField] float expressionChangeSpeed = 1f;

    List<string> splitStringList = new List<string>();
    string[] splitString;
    string newSentence;

    AudioClip umm;


    [SerializeField] string emotion;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //default idle
        emotion = "Neutral";
        chat.chatResponse = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(chat.chatResponse != null)
        {
            if (chat.chatResponse.Length < 150) //if the response is short enough
            {
                string[] newSentenceEmotionArray = SplitResponse(chat.chatResponse);
               
                newSentence = newSentenceEmotionArray[0];
                emotion = newSentenceEmotionArray[1];

           

                StartCoroutine(StartTTS(newSentence)); //begin reading
                chat.chatResponse = null;  //set back to null.

                //chat.newTextArea.text = newSentence;

            }
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
                break;
            case "Happy_high":
                //Debug.Log("Current emotion is: " + emotion);

                animator.SetFloat("emotionBlendX", 1f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 1f, expressionChangeSpeed, Time.deltaTime);
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
                break;
            case "Angry":
                //Debug.Log("Current emotion is: " + emotion);
                animator.SetFloat("emotionBlendX", -1f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 0f, expressionChangeSpeed, Time.deltaTime);
                break;
            default:
                animator.SetFloat("emotionBlendX", 0f, expressionChangeSpeed, Time.deltaTime);
                animator.SetFloat("emotionBlendY", 0f, expressionChangeSpeed, Time.deltaTime);
                break;
        }

    }

    IEnumerator StartTTS(string s)
    {
        Debug.Log("ChatGPT response received. Starting TTS.");
        _speaker.Speak(s);
        yield return null;

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
