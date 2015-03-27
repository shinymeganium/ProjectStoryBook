﻿using UnityEngine;
using System.Collections;

// avoid putting anything useless to ongui, it messes up stuff..
public class MessageBox : MonoBehaviour
{
    public GUISkin skin;
    public bool showBox1, showBox2;
    private MessagePlaceholders messages;
    private ChangeBoxContent changeContent;
    private ChoiseButtons answer;
    public bool showMessages;
    public string currentMessage;
    public int count, count2;
    public string[] curTextBoxContent;

    // Use this for initialization
    private void Start()
    {
        showBox1 = false;
        showBox2 = false;
        messages = GetComponent<MessagePlaceholders>();
        changeContent = GameObject.FindGameObjectWithTag("Player").GetComponent<ChangeBoxContent>();
        answer = GetComponent<ChoiseButtons>();
        showMessages = false;
        currentMessage = "";
        count = 0;
        count2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (showBox1 && Input.GetButtonUp("Collect"))
        {
            WriteMessages();
        }
        else if (showBox2 && Input.GetButtonUp("Collect"))
        {
            WriteMessages2();
        }
    }

    private void OnGUI()
    {
        if (showBox1 || showBox2)
        {
            DrawBox();
        }
    }

    private void DrawBox()
    {
        GUI.skin = skin;
        Rect mssbox = new Rect((Screen.width / 2) - (Screen.width / 2.9f), Screen.height / 1.4f, Screen.width / 1.4f, Screen.height / 3.7f);
        GUI.Box(mssbox, currentMessage, skin.GetStyle("Message"));
    }

    // draws the messages to the text box
    private void WriteMessages()
    {
        for (int i = 0; i < messages.dialogues.Length; i++)
        {
            if (curTextBoxContent.GetHashCode() == messages.dialogues[i].GetHashCode())
            {
                if (count < messages.dialogues[i].Length)
                {
                    currentMessage = PrintMessages(messages.dialogues[i], count);
                    count++;
                    if (count == messages.dialogues[i].Length - 1)
                    {
                        if (messages.dialogues[i][messages.dialogues[i].Length - 1] == "$")
                        {
                            showBox1 = false;
                            curTextBoxContent = messages.choises[i];
                            answer.showButtons = true;
                        }
                    }

                    else if (curTextBoxContent.GetHashCode() == messages.dialogues[i].GetHashCode() && count >= messages.dialogues[i].Length)
                    {
                        showBox1 = false;
                        showMessages = false;
                        changeContent.triggers[i] = false;
                        currentMessage = "";
                        count = 0;
                    }
                }
            }
        }
    }

    // when there is a question
    private void WriteMessages2()
    {
        for (int i = 0; i < messages.choises.Length; i++)
        {
            if (curTextBoxContent.GetHashCode() == messages.choises[i].GetHashCode())
            {
                if (count2 < messages.choises[i].Length)
                {
                    currentMessage = PrintMessages(messages.choises[i], count2);
                    count2++;

                    if (curTextBoxContent.GetHashCode() == messages.choises[i].GetHashCode() && count2 >= messages.choises[i].Length)
                    {
                        showBox1 = false;
                        showBox2 = false;
                        showMessages = false;
                        changeContent.triggers[i] = false;
                        currentMessage = "";
                        count = 0;
                        count2 = 0;
                    }
                }
            }
        }
    }

    // generates the message shown in the message box
    private string PrintMessages(string[] messageArray, int messageID)
    {
        currentMessage = "";
        currentMessage += messageArray[messageID];

        return currentMessage;
    }
}