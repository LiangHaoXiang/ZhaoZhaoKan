using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginTimeManager : MonoBehaviour {



    float readyTime;
    bool IsBegin = false;
    bool isReadyGo = false;
    public static BeginTimeManager instance;

    public static BeginTimeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (BeginTimeManager)FindObjectOfType(typeof(BeginTimeManager));
            }
            return instance;

        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Scene5(Find)"))
        {
            if (IsBegin)
            {
                readyTime -= Time.deltaTime;
                if (isReadyGo)
                {
                    AudioSourceManager.Instance.Play(GameObject.Find("IntrodutionAudio").gameObject, "ReadyGo");
                    isReadyGo = false;
                }
                UIManager.Instance.ShowReadyTime("Ready");


                if (0f < readyTime && readyTime < 1f)
                {
                    UIManager.Instance.ShowReadyTime("GO!!!");
                }
                else if (readyTime < 0f)
                {
                    UIManager.Instance.CleanReadyTime();
                    IsBegin = false;
                }
            }
        }
    }
    

    public void ResetReadyTime()
    {
        this.readyTime = 2f;
        IsBegin = true;
        isReadyGo = true;
    }
}
