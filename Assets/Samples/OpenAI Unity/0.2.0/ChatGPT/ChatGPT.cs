using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;

        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        public string chatResponse;

        private float height;
        private OpenAIApi openai = new OpenAIApi("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");

        private List<ChatMessage> messages = new List<ChatMessage>();
        [SerializeField] public string receivedPersonality;
        public string prompt;
        public string promptInstructions = " Don't break character. Don't ever mention that you are an AI model. Keep responses under 150 characters. At the end of each answer, write a keyword inside square brackets that best describes the emotion of the response:" + "Happy_low, Happy_high, Sad_low, Sad_high, Neutral";

        
        private void Start()
        {
            button.onClick.AddListener(SendReply);

            prompt = promptInstructions;
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);

            item.GetChild(0).GetChild(0).GetComponent<Text>().text = SplitResponse(message.Content);

            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + inputField.text; 
            
            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0613",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();


                chatResponse = message.Content;
                
                messages.Add(message);
                AppendMessage(message);
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
        }

        string SplitResponse(string s)
        {
            //also mends + appends the proper text response.

            //break off where the emotion response starts. 
            string[] splitSentence = s.Split('[', ']');

            string revisedSentence = splitSentence[0];

            
            return revisedSentence;
        }

    }
}
