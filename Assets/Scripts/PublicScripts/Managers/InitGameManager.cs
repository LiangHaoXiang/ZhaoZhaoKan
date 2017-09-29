using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitGameManager : MonoBehaviour {

    public int id = 0;    //关卡第几题
    public int level = 1; //关卡等级

    public static InitGameManager instance;
    public static InitGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (InitGameManager)FindObjectOfType(typeof(InitGameManager));
            }
            return instance;
        }
    }



    private void Awake()
    {
        instance = this;
    }

     void Start()
    {
        //初始化精灵图的字典
        UIManager.Instance.SetSpt();
    }


    /// <summary>
    /// 初始化游戏
    /// </summary>
    /// <param name="objectNumber">物品固定数量</param>
    /// <param name="level">关卡等级<</param>
    /// <param name="maxGameTime">每关题目的最大时间</param>
    /// <param name="questionCount">每关题目数量</param>
    /// <param name="boomCount">每关炸弹数量</param>
    /// <param name="daoJuCount">每关道具数量</param>
    /// <param name="isRotate">每关物品是否旋转</param>
    public void GameAgain(int objectNumber, int level, float maxGameTime, int questionCount, int boomCount, int daoJuCount,float dissappearTime, bool isRotate)
    {
        StartCoroutine(InitGameObject(objectNumber, boomCount, daoJuCount, dissappearTime, isRotate));
        ChooseQuestionStyle(level);
        StartCoroutine(InitUI(maxGameTime));
        //设置正确答案
        AnswerManager.Instance.isOnGame = true;
    }


    /// <summary>
    /// //初始化游戏场景中的所有物体
    /// </summary>
    /// <param name="createObjectNumber"></param>
    IEnumerator InitGameObject(int createObjectNumber, int boomCount, int daoJuCount, float dissappearTime, bool isRotate)
    {
        yield return ObjectManager.Instance.BeginCreate(createObjectNumber,boomCount,daoJuCount, dissappearTime, isRotate);
        BalletManager.Instance.WaitFire();
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator InitUI(float maxGameTime)
    {
        //显示屏幕上面的等级，第几波
        UIManager.Instance.titleLevel.sprite = UIManager.Instance.sprites2[level];
        //出题面板
        QuestionManager.Instance.DelayShowQuestion();
        AnswerManager.Instance.SetAnswer();
        //预备时间
        BeginTimeManager.Instance.ResetReadyTime();
        yield return TimeManager.Instance.DelaySetBegin(maxGameTime);   //允许开始计时
        if (UIManager.Instance.timeShow != null)
            UIManager.Instance.timeShow.text = TimeManager.Instance.GetMaxTime().ToString();
    }

    void ChooseQuestionStyle(int level)
    {
        switch (level)
        {
            case 1:
                if (id < 5)
                {
                    id++;
                }           
                break;
            case 2:
                if (id < 5)
                {
                    id++;
                }
                break;
            case 3:
                if (id < 5)
                {
                    id++;
                }
                break;
            case 4:
                if (id < 5)
                {
                    id++;
                }
                break;
            case 5:
                if (id < 5)
                {
                    id++;
                }
                break;
            case 6:
                if (id < 5)
                {
                    id++;
                }
                break;
            case 7:
                id++;
                break;
        }
    }

    public void ResetID()
    {
        id = 0;
    } 

} 
