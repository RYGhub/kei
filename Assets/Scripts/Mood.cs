using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mood : MonoBehaviour
{
    public Dictionary<string, Sprite> moodSprites;

    protected Image image;

    void Awake()
    {
        image = GetComponent<Image>();
        moodSprites = new Dictionary<string, Sprite> {
            { "cat", Resources.Load<Sprite>("cat") },
            { "cry", Resources.Load<Sprite>("cry") },
            { "disappointed", Resources.Load<Sprite>("disappointed") },
            { "dootflute", Resources.Load<Sprite>("dootflute") },
            { "doottrumpet", Resources.Load<Sprite>("doottrumpet") },
            { "grin", Resources.Load<Sprite>("grin") },
            { "halflife", Resources.Load<Sprite>("halflife") },
            { "happy", Resources.Load<Sprite>("happy") },
            { "key", Resources.Load<Sprite>("key") },
            { "keyface", Resources.Load<Sprite>("keyface") },
            { "neutral", Resources.Load<Sprite>("neutral") },
            { "question", Resources.Load<Sprite>("question") },
            { "smug", Resources.Load<Sprite>("smug") },
            { "surprised", Resources.Load<Sprite>("surprised") },
            { "wink", Resources.Load<Sprite>("wink") },
            { "worried", Resources.Load<Sprite>("worried") },
            { "x", Resources.Load<Sprite>("x") }
        };
    }

    void Start() {
    }

    public void SetMood(string mood) {
        image.sprite = moodSprites[mood];
    }
}
