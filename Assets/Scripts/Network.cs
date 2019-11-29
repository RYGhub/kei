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
    public string url = "http://192.168.1.17:44445/api/kei";
    public string kpid;

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
        StartCoroutine(PostRequest("", "true"));
    }

    public IEnumerator PostRequest(string message, string first) {
        WWWForm form = new WWWForm();
        form.AddField("kpid", kpid);
        form.AddField("message", message);
        form.AddField("first", first);

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if(request.isNetworkError) {
            Debug.LogError("Error! " + request.error);
        }
        else {
            try {
                KeiResponse kr = JsonUtility.FromJson<KeiResponse>(request.downloadHandler.text);
                sender.Change(kr.text, kr.emotion);
            }
            catch (System.ArgumentException e) {
                Debug.LogError(request.downloadHandler.text);
            }
        }
    }
}
