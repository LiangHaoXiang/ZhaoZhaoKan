using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BalletManager : MonoBehaviour
{
    [HideInInspector]
    public Transform balletObjectShow;      //子弹发射起始点
    [HideInInspector]
    public Transform balletShowOtherDir;    //子弹发射朝向      
    int BalletCount;                    //子弹的目前个数
    int totalCount;                     //子弹的总数
    public bool isFire = false;                    //是否开火
    bool isAddBalletAudio = true;           //换子弹的声音
    float isAddBallettime = 0f;             //换子弹的冷却时间
    GameObject foodBallet;                  //装载子弹
    [HideInInspector]
    public GameObject foodShow;                    //装饰子弹专用
    bool isCreatefoodBallet = true;                //是否显示转载子弹
    public static BalletManager instance;

    public static BalletManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (BalletManager)FindObjectOfType(typeof(BalletManager));
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

    }

    void Update()
    {

        if (SceneManager.GetActiveScene().name.Equals("Scene5(Find)"))
        {
            balletObjectShow = GameObject.Find("BalletGun").transform;
            balletShowOtherDir = GameObject.Find("BalletShowOtherDir").transform;

            if (isFire)
            {
                if (isCreatefoodBallet)
                {
                    foodShow = Instantiate(Resources.Load("Prefabs/FoodObject/FoodTexture_" + ObjectManager.Instance.getTargetNumber(), typeof(GameObject))) as GameObject;
                    isCreatefoodBallet = false;
                    foodShow.transform.position = balletObjectShow.position;
                    foodShow.transform.parent = balletObjectShow.transform;
                    foodShow.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    Destroy(foodShow.transform.GetChild(0).GetComponent<FoodObject>());
                    Destroy(foodShow.transform.GetChild(0).GetComponent<SphereCollider>());
                    Destroy(foodShow.transform.GetChild(0).GetComponent<Rigidbody>());
                    foodShow.GetComponent<BalletObject>().enabled = true;
                }
                if (BalletCount == 0)
                {
                    foodShow.SetActive(false);
                }
            }


            isAddBallettime += Time.deltaTime;
            if (isAddBallettime > 0.5f)
            {
                isAddBalletAudio = true;
            }

            UIManager.Instance.ShowBalletCount(BalletCount);
        }
    }
    /// <summary>
    /// 开火，发射
    /// </summary>
    public void Fire()
    {
        if (isFire && AnswerManager.Instance.isOnGame)
        {
            if (BalletCount == 0)
            {
                Play("NoBallet"); //此处改为咔嚓咔嚓的声音
            }
            if (BalletCount > 0)
            {
                BalletCount--;
                CreateBalletObject(balletShowOtherDir.position);
                UIManager.Instance.sightBead.GetComponent<Animation>().Play("RedSightChange");
                Play("BalletFireAudio");
            }

        }
    }
    /// <summary>
    /// 装弹
    /// </summary>
    public void AddBallet()
    {
        if (isFire && BalletCount == 0)
        {
            if (foodShow != null)
            {
                Invoke("showTheFoodShow", 0.5f);
            }
            Invoke("ResrtBalletCount", 0.5f);
            Invoke("DelaySetIsFire", 0.5f);
            isFire = false;
            if (isAddBalletAudio)
            {
                Play("AddBallet");
                isAddBalletAudio = false;
                isAddBallettime = 0;
            }
        }
    }

    void ResrtBalletCount()
    {
        BalletCount = 3;
    }

    void CreateBalletObject(Vector3 balletShootDir)
    {
        foodBallet = Instantiate(Resources.Load("Prefabs/FoodObject/FoodTexture_" + ObjectManager.Instance.getTargetNumber(), typeof(GameObject))) as GameObject;
        foodBallet.transform.position = balletObjectShow.position;
        // foodBallet.transform.parent = balletObjectShow.transform;
        foodBallet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        foodBallet.transform.GetChild(0).tag = "Ballet";
        foodBallet.transform.GetChild(0).GetComponent<FoodObject>().enabled = false;
        foodBallet.GetComponent<BalletObject>().enabled = true;
        foodBallet.GetComponent<BalletObject>().setBalletShootDir(balletShootDir);
        //   Ballet.transform.GetChild(0).LookAt(balletShowOtherDir);

    }


    public void Play(string str)
    {
        AudioClip clip = AudioSourceManager.Instance.GetAudioSource(str);//调用Resources方法加载AudioClip资源
        PlayAudioClip(clip);
    }

    public void PlayAudioClip(AudioClip clip)
    {
        if (clip == null)
            return;
        AudioSource source = (AudioSource)balletObjectShow.gameObject.GetComponent("AudioSource");
        source.clip = clip;
        source.Play();

    }

    public void WaitFire()
    {
        ResrtBalletCount();
        Invoke("delayDoubleSet", 2f);
    }

    public void delayDoubleSet()
    {
        setIsFire(true);
        isCreatefoodBallet = true;
    }

    public void setIsFire(bool isFire)
    {
        this.isFire = isFire;
    }

    public void DelaySetIsFire()
    {
        setIsFire(true);
    }

    public void showTheFoodShow()
    {
        if (foodShow != null)
            foodShow.SetActive(true);
    }

    public void CanceldelayDoubleSet()
    {
        CancelInvoke();
    }

}



