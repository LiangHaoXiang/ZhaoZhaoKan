using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool isBeginTheGameByNewLevel = true;

    public int objectNumber;    //每关的物品数量
    public float maxGameTime;   //每关题目的最大时间
    public int questionCount;   //每关题目数量
    public int boomCount;       //每关炸弹数量
    public int daoJuCount;        //每关是否有道具
    public float dissappearTime;    //物品消失时间
    public bool isRotate;       //每关物品是否旋转

    public static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (LevelManager)FindObjectOfType(typeof(LevelManager));
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
            if (isBeginTheGameByNewLevel)
            {
                StartCoroutine(StartGameByLevel(InitGameManager.Instance.level));
                isBeginTheGameByNewLevel = false;
            }
        }

    }

    //public IEnumerator StartGameByLevel(int level)
    //{
    //    if (level < 7)
    //    {
    //        yield return new WaitForSeconds(0.1f);
    //        if (level == 1)
    //            InitGameManager.Instance.GameAgain(15,level);
    //        if (level == 2)
    //            InitGameManager.Instance.GameAgain(16, level);
    //        if (level == 3)
    //            InitGameManager.Instance.GameAgain(17, level);
    //        if (level == 4)
    //            InitGameManager.Instance.GameAgain(18, level);
    //        if (level == 5)
    //            InitGameManager.Instance.GameAgain(19,level);
    //        if (level == 6)
    //            InitGameManager.Instance.GameAgain(20,level);
    //    }
    //}

    /// <summary>
    /// 根据不同等级关卡做出不同的反应
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public IEnumerator StartGameByLevel(int level)
    {
        switch (level)
        {
            case 1:
                UIManager.Instance.TipsByLevel();
                objectNumber = 15;
                maxGameTime = 30;
                questionCount = 3;
                boomCount = 3;
                daoJuCount = 1;
                dissappearTime = 0.0f;
                isRotate = true;
                yield return new WaitForSeconds(0.1f);
                InitGameManager.Instance.GameAgain(objectNumber, level, maxGameTime, questionCount, boomCount, daoJuCount, dissappearTime, isRotate);
                break;
            case 2:
                UIManager.Instance.TipsByLevel();
                objectNumber = 15;
                maxGameTime = 30;
                questionCount = 3;
                boomCount = 5;
                daoJuCount = 2;
                dissappearTime = 0.8f;
                isRotate = true;
                yield return new WaitForSeconds(0.1f);
                InitGameManager.Instance.GameAgain(objectNumber, level, maxGameTime, questionCount, boomCount, daoJuCount, dissappearTime, isRotate);
                break;
            case 3:
                UIManager.Instance.TipsByLevel();
                objectNumber = 16;
                maxGameTime = 35;
                questionCount = 4;
                boomCount = 5;
                daoJuCount = 2;
                dissappearTime = 1.0f;
                isRotate = false;
                yield return new WaitForSeconds(0.1f);
                InitGameManager.Instance.GameAgain(objectNumber, level, maxGameTime, questionCount, boomCount, daoJuCount, dissappearTime, isRotate);
                break;
            case 4:
                UIManager.Instance.TipsByLevel();
                objectNumber = 16;
                maxGameTime = 35;
                questionCount = 4;
                boomCount = 6;
                daoJuCount = 2;
                dissappearTime = 1.2f;
                isRotate = true;
                yield return new WaitForSeconds(0.1f);
                InitGameManager.Instance.GameAgain(objectNumber, level, maxGameTime, questionCount, boomCount, daoJuCount, dissappearTime, isRotate);
                break;
            case 5:
                UIManager.Instance.TipsByLevel();
                objectNumber = 17;
                maxGameTime = 38;
                questionCount = 5;
                boomCount = 6;
                daoJuCount = 3;
                dissappearTime = 1.5f;
                isRotate = false;
                yield return new WaitForSeconds(0.1f);
                InitGameManager.Instance.GameAgain(objectNumber, level, maxGameTime, questionCount, boomCount, daoJuCount, dissappearTime, isRotate);
                break;
            case 6:
                UIManager.Instance.TipsByLevel();
                objectNumber = 17;
                maxGameTime = 38;
                questionCount = 5;
                boomCount = 6;
                daoJuCount = 3;
                dissappearTime = 2.0f;
                isRotate = false;
                yield return new WaitForSeconds(0.1f);
                InitGameManager.Instance.GameAgain(objectNumber, level, maxGameTime, questionCount, boomCount, daoJuCount, dissappearTime, isRotate);
                break;
            case 7:
                UIManager.Instance.TipsByLevel();
                objectNumber = 18;
                maxGameTime = 40;
                questionCount = 3000;
                boomCount = 8;
                daoJuCount = 4;
                dissappearTime = 2.0f;
                isRotate = true;
                yield return new WaitForSeconds(0.1f);
                InitGameManager.Instance.GameAgain(objectNumber, level, maxGameTime, questionCount, boomCount, daoJuCount, dissappearTime, isRotate);
                break;
            default:
                break;
        }

    }

}