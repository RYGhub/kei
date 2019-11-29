using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sender : MonoBehaviour
{
    protected InputField input;
    protected Button button;
    protected Writer writer;
    protected Mood mood;

    protected Network network;
    
    void Awake() {
        input = GetComponentInChildren<InputField>();
        button = GetComponentInChildren<Button>();
        writer = GetComponentInChildren<Writer>();
        mood = GetComponentInChildren<Mood>();
        network = GetComponent<Network>();
    }

    public void OnButtonPress() {
        if(input.text == "") return; 
        StartCoroutine(network.PostRequest(input.text, "false"));
        input.text = "";
        input.interactable = false;
        button.interactable = false;
    }

    public void Change(string text, string mood) {
        input.interactable = true;
        button.interactable = true;
        this.mood.SetMood(mood);
        this.writer.Write(text, mood);
    }
}
