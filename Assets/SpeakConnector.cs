using Meta.WitAi.TTS.Utilities;
using OpenAI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakConnector : MonoBehaviour
{
    [SerializeField] ChatGPT chat;
    [SerializeField] TTSSpeaker _speaker;
    // Start is called before the first frame update
    void Start()
    {
        chat.chatResponse = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(chat.chatResponse != null)
        {
            if (chat.chatResponse.Length < 150) //if the response is short enough
            {


                StartCoroutine(StartTTS(chat.chatResponse)); //begin reading
                chat.chatResponse = null;  //set back to null.

                //chat.newTextArea.text = newSentence;

            }
        }
    }


    IEnumerator StartTTS(string s)
    {
        Debug.Log("ChatGPT response received. Starting TTS.");
        _speaker.Speak(s);
        yield return null;

    }
}
