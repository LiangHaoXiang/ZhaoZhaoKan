using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{

    public static BuffManager instance;

    public static BuffManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (BuffManager)FindObjectOfType(typeof(BuffManager));
            }
            return instance;

        }

    }

    public void LuckyBalloon()
    {
        System.Random r = new System.Random();
        int n = r.Next(1, 5);

        UIManager.Instance.TipsByLuckyBalloon(n);
        switch (n)
        {
            //时间增加
            case 1:
                AudioSourceManager.Instance.Play(GameObject.Find("EffectAudio").gameObject, "AddTime");
                float gameTime = TimeManager.Instance.GetTime();
                gameTime += 5;
                TimeManager.Instance.SetTime(gameTime);
                break;
            //时间冻结
            case 2:
                GameObject.Find("ParticleCamera").transform.GetChild(0).gameObject.SetActive(true);
                AudioSourceManager.Instance.Play(GameObject.Find("EffectAudio").gameObject, "Ice");
                TimeManager.Instance.SetIncreaseTimeMultiple(0);
                Invoke("Recover", 5);
                break;
            //额外加分
            case 3:
                AudioSourceManager.Instance.Play(GameObject.Find("EffectAudio").gameObject, "AddScore");
                AnswerManager.Instance.scoreNumber += 5;
                ScoreManager.Instance.SetScore(AnswerManager.Instance.scoreNumber);
                break;
            //减少炸弹
            case 4:
                AudioSourceManager.Instance.Play(GameObject.Find("EffectAudio").gameObject, "disapper");

                Destroy(ObjectManager.Instance.BoomObject[0]);
                ObjectManager.Instance.BoomObject.RemoveAt(0);
                break;

            default:
                break;
        }
    }
    /// <summary>
    /// 恢复原样
    /// </summary>
    void Recover()
    {
        GameObject.Find("ParticleCamera").transform.GetChild(0).gameObject.SetActive(false);

        TimeManager.Instance.SetIncreaseTimeMultiple(1);
    }

    void DebuffRecover()
    {
        GameObject.Find("ParticleCamera").transform.GetChild(1).gameObject.SetActive(false);
        GameObject.Find("ParticleCamera").transform.GetChild(2).gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.FindChild("MiddleCenter").FindChild("BoomImage").gameObject.SetActive(false);
    }

    public void BoomDeBuff()
    {
        System.Random r = new System.Random();
        int n = r.Next(1, 5);
        UIManager.Instance.TipsByBoomDebuff(n);
        switch (n)
        {
            //雾霾
            case 1:
                AudioSourceManager.Instance.Play(GameObject.Find("EffectAudio").gameObject, "Fog");
                GameObject.Find("ParticleCamera").transform.GetChild(1).gameObject.SetActive(true);
                Invoke("DebuffRecover", 5);
                break;
            //龙卷风
            case 2:
                AudioSourceManager.Instance.Play(GameObject.Find("EffectAudio").gameObject, "Wind");
                GameObject.Find("ParticleCamera").transform.GetChild(2).gameObject.SetActive(true);
                Invoke("DebuffRecover", 5);
                break;
            //扣分
            case 3:
                AudioSourceManager.Instance.Play(GameObject.Find("EffectAudio").gameObject, "CutTime");
                AnswerManager.Instance.scoreNumber -= 5;
                ScoreManager.Instance.SetScore(AnswerManager.Instance.scoreNumber);
                break;
            //直接下一关
            case 4:
                GameObject.Find("Canvas").transform.FindChild("MiddleCenter").FindChild("BoomImage").gameObject.SetActive(true);
                Invoke("DebuffRecover", 2);
                ResultManager.Instance.YouAreWrong();
                AnswerManager.Instance.isOnGame = false;
                break;
            default:
                break;
        }
    }


    public void CancelInvokeProp()
    {
        CancelInvoke();
    }
}
