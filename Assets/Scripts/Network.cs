using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

[System.Serializable]
public class KeiResponse {
    public string emotion;
    public string text;
}

public class Network : MonoBehaviour
{
    // USE HTTPS IN ANDROID OR IT WON'T WORK
    public string url = "https://ryg.steffo.eu/api/kei";
    public string kpid;
    public string convid;
    public string previous = "";

    private Sender sender;

    void Awake() {
        sender = GetComponent<Sender>();
    }

    void Start() {
        kpid = PlayerPrefs.GetString("kpid", "");
        if(kpid == "") {
            kpid = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetString("kpid", kpid);
        }
        convid = System.Guid.NewGuid().ToString();
        StartCoroutine(PostRequest("", "true"));
    }

    public IEnumerator PostRequest(string message, string first) {
        WWWForm form = new WWWForm();
        form.AddField("kpid", kpid);
        form.AddField("convid", convid);
        form.AddField("message", message);
        form.AddField("first", first);
        form.AddField("previous", previous);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if(request.isNetworkError) {
            Debug.LogError("Error! " + request.error);
        }
        else {
            try {
                KeiResponse kr = JsonUtility.FromJson<KeiResponse>(request.downloadHandler.text);
                previous = kr.text;
                sender.Change(kr.text, kr.emotion);
            }
            catch (System.ArgumentException) {
                Debug.LogError(request.downloadHandler.text);
            }
        }
    }
}
