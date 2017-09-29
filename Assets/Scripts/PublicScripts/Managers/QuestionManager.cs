using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour {

    public static QuestionManager instance;
    public static QuestionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (QuestionManager)FindObjectOfType(typeof(QuestionManager));
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }


    public void DelayShowQuestion()
    {

        Invoke("ShowQuestion", 2f);
    }

    public void ShowQuestion()
    {
        //通过UI展示出题目
        StartCoroutine(UIManager.Instance.ShowQuestionPanel(InitGameManager.Instance.id, ObjectManager.Instance.getTargetNumber()));
    }

    public void CancelShowQuestion()
    {
        CancelInvoke();
    }

}
