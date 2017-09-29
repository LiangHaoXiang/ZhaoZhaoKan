using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnswerManager : MonoBehaviour {


    //这个类为判断答案是正确的，正确之后，停止时间的计时，并且重新进行一下一个关卡

    public int answerNumber;
    public int scoreNumber = 0;
    public bool isOnGame;   //在场景5的游戏状态

    public static AnswerManager instance;

    public static AnswerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (AnswerManager)FindObjectOfType(typeof(AnswerManager));
            }
            return instance;

        }

    }

    private void Awake()
    {
        instance = this;
    }


    public void IsAnswerRight(int number)
    {
        if (isOnGame)
        {
            if (ObjectManager.Instance.getTargetNumber() == number)
            {

                if (20f <= TimeManager.Instance.GetTime() && TimeManager.Instance.GetTime() < 30f)                     
                {
                    scoreNumber += 12;
                    ScoreManager.Instance.SetScore(scoreNumber);
                }
                else if (5f <= TimeManager.Instance.GetTime() && TimeManager.Instance.GetTime() < 20f)
                {      
                        scoreNumber += 10;
                        ScoreManager.Instance.SetScore(scoreNumber);          
                }
                else 
                {
                    scoreNumber += 8;
                    ScoreManager.Instance.SetScore(scoreNumber);
                }

                //在答题框显示正确的答案
                AudioSourceManager.Instance.Play(GameObject.Find("GetScoureAudio").gameObject, "Unbelieve");
                ResultManager.Instance.YouAreRight();
                isOnGame = false;
            }
            else
            {
                AudioSourceManager.Instance.Play(GameObject.Find("GetScoureAudio").gameObject, "Wrong");
                scoreNumber -= 5;
                ScoreManager.Instance.SetScore(scoreNumber);
                ResultManager.Instance.YouAreWrong();
                isOnGame = false;

            }

        }
     }
    

    public IEnumerator SetAnswer()
    {
        yield return new WaitForSeconds(2f);
    }

    public void ClearScoreNumber()
    {
        scoreNumber = 0;
    }
    //清除答题文本信息
    public void ClearAnswerText()
    {
    
    }

}
