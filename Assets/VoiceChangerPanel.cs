using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceChangerPanel : MonoBehaviour
{
    [SerializeField] SpeakConnector speakConnector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVoice(string voice)
    {
        speakConnector._speaker.Stop();
        speakConnector._speaker.VoiceID = "WIT$"+voice;
        speakConnector._speaker.Speak("Hey, how are you?");

    }
}
