using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAI
{
    public class PromptSwitcher : MonoBehaviour
    {
        [SerializeField] List<PersonalitySO> personalitySOs;
        ChatGPT chat;
        // Start is called before the first frame update
        void Start()
        {
            chat = GetComponent<ChatGPT>();
        }

        public void SwitchPersonality(int i)
        {
            chat.receivedPersonality = personalitySOs[i].PersonalityPrompt;

            chat.gameObject.SetActive(false);
            chat.gameObject.SetActive(true);

            chat.prompt = chat.receivedPersonality + chat.promptInstructions;
            Debug.Log(chat.prompt);
        }


    }
}
