using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Connection : MonoBehaviour
{
    public bool isLocal = false;
    private string UrlBottles = $"{Constants.MAIN_URL}/bottles";
    private string UrlStatus = $"{Constants.MAIN_URL}/bottles/status";
    public Text textResponse;

    void Awake()
    {
        if(isLocal)
        {
            UrlBottles = $"{Constants.LOCAL_URL}/bottles";
            UrlStatus = $"{Constants.LOCAL_URL}/bottles/status";
        }
    }

    void Start()
    {
        InvokeRepeating("Connect", 0f, 0.5f);
    }

    public void Connect()
    {
        StartCoroutine(CheckStatus());
    }

    private IEnumerator CheckStatus()
    {
        UnityWebRequest status = UnityWebRequest.Get(UrlStatus);
        yield return status.SendWebRequest();
        string bottlesStatus = status.downloadHandler.text;
        if (bottlesStatus == "true")
        {
            StartCoroutine(GetBottlesAmount());
        }
    }

    private IEnumerator GetBottlesAmount()
    {
        UnityWebRequest response = UnityWebRequest.Get(UrlBottles);
        yield return response.SendWebRequest();
        int bottlesAmount = int.Parse(response.downloadHandler.text);

        StartCoroutine(SetBottlesAmount(bottlesAmount));
    }

    private IEnumerator SetBottlesAmount(int bottlesAmount)
    {
        bottlesAmount--;
        UnityWebRequest request = UnityWebRequest.Get(UrlBottles + "/" + bottlesAmount);
        yield return request.SendWebRequest();

        if (bottlesAmount >= 0)
        {
            textResponse.text = $"Take one down and pass it around - {bottlesAmount} bottles of beer on the wall.";
        }
        else
        {
            textResponse.text = "Trwa oczekiwanie na start...";
            StartCoroutine(SetStatus());
        }
    }

    private IEnumerator SetStatus()
    {
        UnityWebRequest status = UnityWebRequest.Get(UrlStatus + "/" + false);
        yield return status.SendWebRequest();
    }
}
