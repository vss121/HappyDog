using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OpenAI
{
    public class ChatGpt : MonoBehaviour
    {
        private OpenAIApi openai = new OpenAIApi();
        private List<ChatMessage> messages = new List<ChatMessage>();
        private string prompt = "Act as a random stranger in a chat room and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";
        public string sentMessage = "Hi";   // temp
        public string receivedMessage = "receivedMessage";

        public async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = sentMessage
            };

            //AppendMessage(newMessage); 

            if (messages.Count == 0) newMessage.Content = prompt + "\n" + sentMessage;

            messages.Add(newMessage);

            //button.enabled = false;
            //inputField.text = "";
            //inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                
                
                this.receivedMessage = message.Content;  // 변수 저장
                Debug.Log("receivedMessage------"+receivedMessage); // 잘 작동
                Debug.Log("this.receivedMessage------" + this.receivedMessage); // 잘 작동
                messages.Add(message);
                Debug.Log(messages.Count);
                //AppendMessage(message);
                // 메시지 띄우기

            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");

            }

            //button.enabled = true;
            //inputField.enabled = true;

        }
    }
}