using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Range {
    public float min;
    public float max;

    public Range(float min, float max) {
        this.min = min;
        this.max = max;
    }

    public float GetRandom() {
        return Random.Range(min, max);
    }
}

public class Writer : MonoBehaviour
{
    public float characterDelay;

    public Dictionary<string, Range> moodPitch = new Dictionary<string, Range> {
        { "cat", new Range(1.0f, 2.0f) },
        { "cry", new Range(0.4f, 0.5f) },
        { "disappointed", new Range(0.2f, 0.7f) },
        { "dootflute", new Range(0.6f, 1.6f) },
        { "doottrumpet", new Range(0.6f, 1.6f) },
        { "grin", new Range(0.9f, 2.4f) },
        { "halflife", new Range(0.7f, 1.7f) },
        { "happy", new Range(0.5f, 1.5f) },
        { "key", new Range(0.0f, 0.0f) },
        { "keyface", new Range(0.1f, 0.4f) },
        { "neutral", new Range(0.55f, 1.45f) },
        { "question", new Range(0.4f, 1.6f) },
        { "smug", new Range(0.4f, 1.6f) },
        { "surprised", new Range(0.7f, 1.7f) },
        { "wink", new Range(0.6f, 1.6f) },
        { "worried", new Range(0.4f, 1.3f) },
        { "x", new Range(0.2f, 0.8f) }
    };

    protected Text messageBox;
    protected AudioSource audioSource;

    protected string toWrite;
    protected string mood;

    protected void Awake() {
        messageBox = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    protected void Start() {
        toWrite = "";
        InvokeRepeating("Char", characterDelay, characterDelay);
    }
    
    public void Write(string text, string mood) {
        this.messageBox.text = "";
        this.toWrite = text;
        this.mood = mood;
    }

    protected void Char() {
        if(toWrite.Length > 0) {
            messageBox.text += toWrite[0];
            if(toWrite[0] != ' ') {
                audioSource.pitch = moodPitch[mood].GetRandom();
                audioSource.Play();
            }
            toWrite = toWrite.Remove(0, 1);
        }
    }
}
