using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 场景1 属性
    GameObject textImage1;
    GameObject textImage2;
    GameObject textImage3;
    bool next = true;
    float fillValue1 = 0;
    float fillValue2 = 0;
    float fillValue3 = 0;
    #endregion

    [HideInInspector]
    public Sprite[] sprites1;  //ui_12345题
    [HideInInspector]
    public Sprite[] sprites2;  //NumAndOperator

    [HideInInspector]
    public Slider loadingSlider;    //场景4的加载条
    [HideInInspector]
    public Slider timeSlider;       //时间条

    public static string targetScene;   //最终目标场景

    Vector2 beganTouchPoint;    //开始触摸的点坐标

    [HideInInspector]
    public Image balletCountShow;
    [HideInInspector]
    public Text readyText;
    [HideInInspector]
    public Image titleLevel;    //等级，第几波
    #region 场景5（找你妹）题目面板系列
    [HideInInspector]
    public Image questionPicture;
    [HideInInspector]
    public Text questionObjStr;  //物品名称
    [HideInInspector]
    public Text questionIDText;
    [HideInInspector]
    public GameObject questionIDObject;
    [HideInInspector]
    public Transform questionIDEnterPoint;
    [HideInInspector]
    public Transform questionIDExitPoint;
    [HideInInspector]
    public GameObject group;
    [HideInInspector]
    public Transform groupEnterPoint;
    [HideInInspector]
    public Transform groupExitPoint;
    [HideInInspector]
    public GameObject questionPanel;
    #endregion

    [HideInInspector]
    public Text scoreText;
    [HideInInspector]
    public Text timeShow;   //时间文本框
    [HideInInspector]
    public GameObject sightBead;

    private Sprite[] spt; //下载资源里全部的Sprite
    private Dictionary<string, object> spriteDictionary = new Dictionary<string, object>();


    public static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (UIManager)FindObjectOfType(typeof(UIManager));
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;

        sprites1 = Resources.LoadAll<Sprite>("_RealUI/ui_12345题") as Sprite[];
        sprites2 = Resources.LoadAll<Sprite>("_RealUI/NumAndOperator") as Sprite[];
    }

    void Start()
    {
        textImage1 = GameObject.Find("textImage1");
        textImage2 = GameObject.Find("textImage2");
        textImage3 = GameObject.Find("textImage3");
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Scene1"))
        {
            textImage1Fill();
            Invoke("textImage2Fill", 1.0f);
            Invoke("textImage3Fill", 2.0f);
        }

        if (SceneManager.GetActiveScene().name.Equals("Scene3(Main)"))
        {
            HelpPanelImagesMovement();
        }

        if (SceneManager.GetActiveScene().name.Equals("Scene4(Loading)"))
        {
            loadingSlider = GameObject.Find("LoadingSlider").GetComponent<Slider>();
            loadingSlider.value = GameObject.Find("loadingByAsync").GetComponent<LoadingByAsync>().progress;
        }

        if (SceneManager.GetActiveScene().name.Equals("Scene5(Find)"))
        {
            timeShow = GameObject.Find("TimeText").GetComponent<Text>();
            timeSlider = GameObject.Find("TimeSlider").GetComponent<Slider>();
            balletCountShow = GameObject.Find("BalletCount").GetComponent<Image>();
            titleLevel = GameObject.Find("questionLevel").GetComponent<Image>();
            readyText = GameObject.Find("ReadyText").GetComponent<Text>();
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            sightBead = GameObject.Find("SightBead").gameObject;
            questionPanel = GameObject.Find("Panels").transform.GetChild(0).gameObject;
        }
    }


    #region 场景1 UI
    void textImage1Fill()
    {
        if (textImage1 != null)
            textImage1.GetComponent<Image>().fillAmount = Mathf.Lerp(0.0f, 1.0f, fillValue1 += Time.deltaTime);
    }

    void textImage2Fill()
    {
        if (textImage2 != null)
            textImage2.GetComponent<Image>().fillAmount = Mathf.Lerp(0.0f, 1.0f, fillValue2 += Time.deltaTime);
    }

    void textImage3Fill()
    {
        if (textImage3 != null)
            textImage3.GetComponent<Image>().fillAmount = Mathf.Lerp(0.0f, 1.0f, fillValue3 += Time.deltaTime);
    }
    #endregion

    #region 主界面UI
    /// <summary>
    /// 开始游戏按钮点击事件
    /// </summary>
    public void BeginButtonClick()
    {
        GameObject.Find("Panels").transform.FindChild("ChoosePanel").gameObject.SetActive(true);
    }
    /// <summary>
    /// 关闭选择面板
    /// </summary>
    public void CloseChoosePanelClick()
    {
        GameObject.Find("Panels").transform.FindChild("ChoosePanel").gameObject.SetActive(false);
    }
    /// <summary>
    /// 主场景AR算数按钮事件
    /// 若点击了，那么最终目标场景就是"Scene5(Find)"，这在场景4加载页面的异步加载时用到
    /// </summary>
    public void MainScene_SuanShu_BtnClick()
    {
        targetScene = "Scene5(Find)";
        SceneManager.LoadScene("Scene4(Loading)");
        LevelManager.Instance.isBeginTheGameByNewLevel = true;
    }
    /// <summary>
    /// 激活帮助面板
    /// </summary>
    public void HelpPanelShowClick()
    {
        GameObject.Find("Panels").transform.FindChild("HelpPanel").gameObject.SetActive(true);
    }
    /// <summary>
    /// 关闭帮助面板
    /// </summary>
    public void CloseHelpPanelClick()
    {
        GameObject.Find("HelpPanel").SetActive(false);
    }

    /// <summary>
    /// 主场景帮助面板图片的移动行为
    /// </summary>
    void HelpPanelImagesMovement()
    {
        if (GameObject.Find("HelpPanel") != null)
        {
            GameObject Images = GameObject.Find("HelpPanel").transform.FindChild("Mask").GetChild(0).gameObject;
            //获取装图片的父物体的横坐标
            float Images_X = Images.transform.position.x;
            float ImagesBeginPoint_X = GameObject.Find("HelpPanel").transform.FindChild("Mask").GetChild(1).position.x;
            float ImagesEndPoint_X = GameObject.Find("HelpPanel").transform.FindChild("Mask").GetChild(2).position.x;
            //两图片间x差值
            float ImageDelta_X = Images.transform.GetChild(1).position.x - Images.transform.GetChild(0).position.x;
            //小点点的变化
            if (Images.transform.position.x >= ImagesBeginPoint_X)
            {
                GameObject.Find("points").transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                GameObject.Find("points").transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            }
            else if ((ImagesBeginPoint_X - ImageDelta_X) <= Images.transform.position.x && Images.transform.position.x < ImagesBeginPoint_X)
            {
                GameObject.Find("points").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                GameObject.Find("points").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            }
            else if ((ImagesBeginPoint_X - 2 * ImageDelta_X) <= Images.transform.position.x && Images.transform.position.x < (ImagesBeginPoint_X - ImageDelta_X))
            {
                GameObject.Find("points").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                GameObject.Find("points").transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            }
            else if (ImagesEndPoint_X <= Images.transform.position.x && Images.transform.position.x < (ImagesEndPoint_X + ImageDelta_X))
            {
                GameObject.Find("points").transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                GameObject.Find("points").transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            }

            if (Input.touchCount > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    beganTouchPoint = Input.touches[0].position;
                }
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    //若到左边边缘，只允许往左移
                    if (Images_X >= ImagesBeginPoint_X)
                    {
                        if (Mathf.Abs(Input.touches[0].position.x - beganTouchPoint.x) > Screen.height * 0.1f)
                        {
                            //左移
                            if (Input.touches[0].position.x < beganTouchPoint.x)
                                iTween.MoveBy(Images, iTween.Hash("time", 0.10f, "x", -ImageDelta_X, "easetype", iTween.EaseType.linear));
                        }
                    }
                    //若到右边边缘，只允许往右移
                    else if (Images_X <= ImagesEndPoint_X)
                    {
                        if (Mathf.Abs(Input.touches[0].position.x - beganTouchPoint.x) > Screen.height * 0.1f)
                        {
                            //右移
                            if (Input.touches[0].position.x > beganTouchPoint.x)
                                iTween.MoveBy(Images, iTween.Hash("time", 0.10f, "x", ImageDelta_X, "easetype", iTween.EaseType.linear));
                        }
                    }
                    else//若没到边缘，随便移
                    {
                        if (Mathf.Abs(Input.touches[0].position.x - beganTouchPoint.x) > Screen.height * 0.1f)
                        {
                            //右移
                            if (Input.touches[0].position.x > beganTouchPoint.x)
                                iTween.MoveBy(Images, iTween.Hash("time", 0.10f, "x", ImageDelta_X, "easetype", iTween.EaseType.linear));
                            //左移
                            else
                                iTween.MoveBy(Images, iTween.Hash("time", 0.10f, "x", -ImageDelta_X, "easetype", iTween.EaseType.linear));
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 激活蓝牙面板  并关闭选择面板
    /// </summary>
    public void BluetoothPanelShowClick()
    {
        GameObject.Find("Panels").transform.FindChild("BluetoothPanel").gameObject.SetActive(true);
        CloseChoosePanelClick();
    }
    /// <summary>
    /// 关闭蓝牙面板
    /// </summary>
    public void CloseBluetoothPanelClick()
    {
        GameObject.Find("BluetoothPanel").SetActive(false);
    }


    #endregion

    #region 场景5（找你妹）UI

    /// <summary>
    /// 显示时间条
    /// </summary>
    /// <param name="maxTime"></param>
    public void ShowTimeSlider(float timeValue)
    {
        if (timeSlider != null)
        {
            Transform sliderFillArea = timeSlider.transform.GetChild(0).FindChild("Fill Area").GetChild(0);
            timeSlider.value = timeValue;
        }
    }
    /// <summary>
    /// 显示文本时间
    /// </summary>
    /// <param name="time"></param>
    public void ShowTime(float time)
    {
        if (timeShow != null)
            timeShow.text = "" + (int)time;
    }
    /// <summary>
    /// 清除文本时间
    /// </summary>
    public void CleanTime()
    {
        timeShow.text = "";
    }
    /// <summary>
    /// 显示当前文本子弹数量
    /// </summary>
    /// <param name="balletCount"></param>
    public void ShowBalletCount(int balletCount)
    {
        if (balletCountShow != null)
            balletCountShow.sprite = sprites2[balletCount];
    }
    /// <summary>
    /// 清除当前文本子弹数量
    /// </summary>
    public void CleanBalletCount()
    {
        if (balletCountShow != null)
            balletCountShow.sprite = sprites2[0];
    }
    /// <summary>
    /// 显示文本预备时间
    /// </summary>
    /// <param name="readyTime"></param>
    public void ShowReadyTime(string sign)
    {
        if (readyText != null)
        {
            if (sign.Equals("Ready"))
            {
                readyText.transform.GetChild(0).gameObject.SetActive(true);
                readyText.transform.GetChild(1).gameObject.SetActive(false);
                readyText.transform.GetChild(2).gameObject.SetActive(false);
                readyText.transform.GetChild(3).gameObject.SetActive(false);
                readyText.transform.GetChild(4).gameObject.SetActive(false);
            }
            else if (sign.Equals("GO!!!"))
            {
                readyText.transform.GetChild(0).gameObject.SetActive(false);
                readyText.transform.GetChild(1).gameObject.SetActive(true);
                readyText.transform.GetChild(2).gameObject.SetActive(false);
                readyText.transform.GetChild(3).gameObject.SetActive(false);
                readyText.transform.GetChild(4).gameObject.SetActive(false);
            }
            else if (sign.Equals("Right"))
            {
                readyText.transform.GetChild(0).gameObject.SetActive(false);
                readyText.transform.GetChild(1).gameObject.SetActive(false);
                readyText.transform.GetChild(2).gameObject.SetActive(true);
                readyText.transform.GetChild(3).gameObject.SetActive(false);
                readyText.transform.GetChild(4).gameObject.SetActive(false);
            }
            else if (sign.Equals("Wrong"))
            {
                readyText.transform.GetChild(0).gameObject.SetActive(false);
                readyText.transform.GetChild(1).gameObject.SetActive(false);
                readyText.transform.GetChild(2).gameObject.SetActive(false);
                readyText.transform.GetChild(3).gameObject.SetActive(true);
                readyText.transform.GetChild(4).gameObject.SetActive(false);
            }
            else if (sign.Equals("GameOver"))
            {
                readyText.transform.GetChild(0).gameObject.SetActive(false);
                readyText.transform.GetChild(1).gameObject.SetActive(false);
                readyText.transform.GetChild(2).gameObject.SetActive(false);
                readyText.transform.GetChild(3).gameObject.SetActive(false);
                readyText.transform.GetChild(4).gameObject.SetActive(true);
            }
                
        }
    }
    /// <summary>
    /// 清除文本预备时间
    /// </summary>
    public void CleanReadyTime()
    {
        readyText.transform.GetChild(0).gameObject.SetActive(false);
        readyText.transform.GetChild(1).gameObject.SetActive(false);
        readyText.transform.GetChild(2).gameObject.SetActive(false);
        readyText.transform.GetChild(3).gameObject.SetActive(false);
        readyText.text = "";
    }
    /// <summary>
    /// 显示题目面板（找你妹）
    /// </summary>
    /// <param name="QuestionID"></param>
    /// <param name="pictureNumber"></param>
    public IEnumerator ShowQuestionPanel(int QuestionID, int pictureNumber)
    {
        if (questionPanel != null)
        {
            questionPanel.SetActive(true);

            questionPanel.GetComponent<Animation>().Play();
            yield return new WaitForSeconds(0.5f);
            questionPanel.transform.FindChild("QuestionID").gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            questionPanel.transform.FindChild("Group").GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            questionPanel.transform.FindChild("Group").GetChild(1).gameObject.SetActive(true);

            questionIDText = questionPanel.transform.FindChild("QuestionID").FindChild("questionIDText").GetComponent<Text>();
            //获取图片框
            questionPicture = questionPanel.transform.FindChild("Group").GetChild(0).GetComponent<Image>();
            questionObjStr = questionPanel.transform.FindChild("Group").GetChild(1).GetComponent<Text>();

            questionIDText.text = QuestionID.ToString();
            //给图片赋予材质
            questionPicture.sprite = SetImage(pictureNumber);
            questionObjStr.text = PictureNumberConvert(pictureNumber);

            GameObject.Find("Canvas").transform.FindChild("RightUp").GetChild(2).GetComponent<Button>().interactable = true;

        }
    }

    ///// <summary>
    ///// 显示题目面板（算数）
    ///// </summary>
    ///// <param name="QuestionID"></param>
    ///// <param name="num1"></param>
    ///// <param name="num2"></param>
    ///// <param name="operatorStr"></param>
    //public void ShowQuestionPanel(int QuestionID, int num1, int num2, string operatorStr)
    //{
    //    if (questionPanel != null)
    //    {
    //        questionPanel.SetActive(true);
    //        //找到题目ID相关的物体
    //        questionIDObject = GameObject.Find("QuestionID");
    //        questionIDEnterPoint = GameObject.Find("QuestionIDEnterPoint").transform;
    //        questionIDExitPoint = GameObject.Find("QuestionIDExitPoint").transform;
    //        //找到题目组相关的物体
    //        group = GameObject.Find("Group");
    //        groupEnterPoint = GameObject.Find("GroupEnterPoint").transform;
    //        groupExitPoint = GameObject.Find("GroupExitPoint").transform;
    //        //移动题目ID
    //        iTween.MoveTo(questionIDObject, iTween.Hash("time", 0.5f, "position", questionIDEnterPoint.position,
    //                                                    "easetype", iTween.EaseType.easeOutSine));
    //        //移动题目组
    //        iTween.MoveTo(group, iTween.Hash("time", 0.5f, "position", groupEnterPoint.position,
    //                                         "easetype", iTween.EaseType.easeOutSine));
    //        //获取文本框
    //        questionIDText = GameObject.Find("questionIDText").GetComponent<Image>();
    //        num1Text = GameObject.Find("num1Text").GetComponent<Text>();
    //        operatorImage = GameObject.Find("operator").GetComponent<Image>();
    //        num2Text = GameObject.Find("num2Text").GetComponent<Text>();
    //        equal = GameObject.Find("equal").GetComponent<Image>();
    //        resultText = GameObject.Find("resultText").GetComponent<Text>();
    //        //给他们赋值

    //        questionIDText.sprite = sprites1[QuestionID - 1];


    //        num1Text.text = num1.ToString();
    //        operatorImage.sprite = OperatorConvert(operatorStr);
    //        num2Text.text = num2.ToString();
    //        equal.sprite = sprites2[14];
    //        resultText.text = "?";
    //    }
    //}
    /// <summary>
    /// 根据图片编号转化为相应的物品字符串
    /// </summary>
    /// <returns></returns>
    string PictureNumberConvert(int pictureNumber)
    {
        switch (pictureNumber)
        {
            case 1:
                return "虾米";
            case 2:
                return "菠萝";
            case 3:
                return "薯条";
            case 4:
                return "甜甜圈";
            case 5:
                return "椰子";
            case 6:
                return "果汁";
            case 7:
                return "果汁";
            case 8:
                return "果汁";
            case 9:
                return "雪糕";
            case 10:
                return "鸡腿";
            case 11:
                return "香蕉";
            case 12:
                return "西瓜";
            case 13:
                return "冰淇淋";
            case 14:
                return "薯片包";
            case 15:
                return "汉堡";
            case 16:
                return "披萨";
            case 17:
                return "薯片";
            case 18:
                return "热狗面包";
            case 19:
                return "蔬菜";
            case 20:
                return "小松糕";
            case 21:
                return "红椒";
            case 22:
                return "鳄梨";
            case 23:
                return "辣椒";
            case 24:
                return "西红柿";
            case 25:
                return "巧克饼干";
            case 26:
                return "玉米饼";
            case 27:
                return "洋葱";
            case 28:
                return "辣椒酱";
            case 29:
                return "面包";
            case 30:
                return "葡萄酒";
            case 31:
                return "年糕";
            case 32:
                return "牛角包";
            case 33:
                return "鱼片寿司";
            case 34:
                return "鸡蛋卷";
            case 35:
                return "粗红肠";
            case 36:
                return "炸虾仁";
            case 37:
                return "肉丸串";
            case 38:
                return "紫菜寿司";
            case 39:
                return "糯米糍";
            case 40:
                return "饭团";
            case 41:
                return "饺子";
            case 42:
                return "米饭";
            case 43:
                return "奶酪";
            case 44:
                return "香肠";
            case 45:
                return "香菇";
            case 46:
                return "牛扒";
            case 47:
                return "牛肉";
            case 48:
                return "面包片";
            case 49:
                return "煎蛋";
            default:
                return null;
        }
    }

    /// <summary>
    /// 清除题目面板
    /// </summary>
    public void CleanQuestionPanel()
    {
        if (questionPanel != null)
        {
            questionPanel.SetActive(true);

            questionPanel.transform.FindChild("QuestionID").gameObject.SetActive(true);

            questionPanel.transform.FindChild("Group").GetChild(0).gameObject.SetActive(true);

            questionPanel.transform.FindChild("Group").GetChild(1).gameObject.SetActive(true);

            questionIDText = questionPanel.transform.FindChild("QuestionID").FindChild("questionIDText").GetComponent<Text>();
            //获取图片框
            questionPicture = questionPanel.transform.FindChild("Group").GetChild(0).GetComponent<Image>();
            questionObjStr = questionPanel.transform.FindChild("Group").GetChild(1).GetComponent<Text>();

            questionIDText.text = "";            
            questionPicture.sprite = SetImage(0);
            questionObjStr.text = "";

            GameObject.Find("Canvas").transform.FindChild("RightUp").GetChild(2).GetComponent<Button>().interactable = false;

            questionPanel.transform.FindChild("QuestionID").gameObject.SetActive(false);

            questionPanel.transform.FindChild("Group").GetChild(0).gameObject.SetActive(false);

            questionPanel.transform.FindChild("Group").GetChild(1).gameObject.SetActive(false);

            questionPanel.SetActive(false);
        }
    }
    /// <summary>
    /// 显示当前关卡分数
    /// </summary>
    /// <param name="scoreNumber"></param>
    public void ShowScoreText(int scoreNumber)
    {
        if (scoreText != null)
            scoreText.text = "" + scoreNumber;
    }
    /// <summary>
    /// 清除当前文本关卡分数
    /// </summary>
    public void CleanScoreText()
    {
        if (scoreText != null)
            scoreText.text = "";
    }
    /// <summary>
    /// 显示总分数，在结束面板处
    /// </summary>
    /// <param name="totalScoreText"></param>
    public void ShowTotalScoreText(Text totalScoreText)
    {
        if (totalScoreText != null)
            totalScoreText.text = "总分：" + ScoreManager.Instance.totalScore.ToString();
    }
    /// <summary>
    /// 显示总时间，在结束面板处
    /// </summary>
    /// <param name="totalScoreText"></param>
    public void ShowTotalTimeText(Text totalTimeText)
    {
        if (totalTimeText != null)
            totalTimeText.text = "总用时：" + ((int)TimeManager.Instance.totalTime).ToString();
    }
    /// <summary>
    /// 显示是否有新纪录，在结束面板处
    /// </summary>
    /// <param name="totalScoreText"></param>
    public void ShowRecordText(Text recordText)
    {
        if (recordText != null)
        {
            if (GameManager.instance.CompareRecord() == true)
            {
                recordText.text = "刷新纪录！！";
            }
            else
            {
                recordText.text = "未刷新纪录" + "\n" + "请继续努力";
            }
        }
    }

    /// <summary>
    /// 激活通关提示面板
    /// </summary>
    public void PassLevelPanelShow()
    {
        GameObject.Find("Panels").transform.GetChild(1).gameObject.SetActive(true);

        Handheld.Vibrate(); //手机振动
        //当前关卡
        Image currentLevelText = GameObject.Find("Panels").transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>();
        //当前关卡分数
        Text currentLevelScoreText = GameObject.Find("Panels").transform.GetChild(1).GetChild(2).GetComponent<Text>();
        //时间文本框
        Text currentLevelTimeText = GameObject.Find("Panels").transform.GetChild(1).GetChild(3).GetComponent<Text>();

        currentLevelText.sprite = sprites2[InitGameManager.Instance.level];
        currentLevelScoreText.text = "本关分数：" + ScoreManager.Instance.currentLevelScore.ToString();
        currentLevelTimeText.text = "本关用时：" + ((int)TimeManager.Instance.currentLevelTime).ToString();
    }
    /// <summary>
    /// 通关提示面板的按钮点击事件
    /// </summary>
    public void OnPassLevelPanelClick()
    {
        InitGameManager.Instance.level++;
        LevelManager.Instance.isBeginTheGameByNewLevel = true;
        ResultManager.Instance.ClearTheViewEveryTest();
        ResultManager.Instance.ClearTheViewEveryLevel();
        GameObject.Find("PassLevelPanel").SetActive(false);
    }
    /// <summary>
    /// 激活游戏结束面板
    /// </summary>
    public void GameOverPanelShow()
    {
        GameObject.Find("Panels").transform.GetChild(2).gameObject.SetActive(true);
        AnswerManager.Instance.isOnGame = false;

        Instantiate(Resources.Load("Prefabs/Scene5(Find)/Surprise/ConfettiStars"), GameObject.Find("StarsCreatePoint").transform.position, Quaternion.identity);
        Handheld.Vibrate(); //手机振动
        Transform Data = GameObject.Find("Panels").transform.GetChild(2).GetChild(3);
        Text totalScoreText = Data.GetChild(0).GetComponent<Text>();
        Text totalTimeText = Data.GetChild(1).GetComponent<Text>();
        Text recordText = Data.GetChild(2).GetComponent<Text>();

        StarsChange();  //评星
        PlayerPrefs.SetFloat("result", ScoreManager.Instance.totalScore / (TimeManager.Instance.totalTime - 30));   //存储记录
        PlayerPrefs.Save();
        ShowTotalScoreText(totalScoreText);     //显示总分
        ShowTotalTimeText(totalTimeText);       //显示总时间
        ShowRecordText(recordText);
    }
    /// <summary>
    /// 激活暂停面板
    /// </summary>
    public void PausePanelShow()
    {
        GameObject.Find("Panels").transform.GetChild(3).gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    /// <summary>
    /// 关闭暂停面板
    /// </summary>
    public void ClosePausePanelClick()
    {
        GameObject.Find("PausePanel").SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// 在结束面板和暂停面板处的返回按钮点击事件
    /// </summary>
    public void BackButtonOnPanelClick()
    {
        InitGameManager.Instance.level = 1;
        ScoreManager.Instance.ResetTotalScore();
        TimeManager.Instance.ResetTotalTime();
        ResultManager.Instance.CancelInvokeInitGame();
        ResultManager.Instance.ClearTheViewEveryTest();
        ResultManager.Instance.ClearTheViewEveryLevel();
        Time.timeScale = 1;
        JsonManager.Instance.ClearJsonLists();
        JsonManager.Instance.ReadAllJsonToStoreInLists();

        ObjectManager.Instance.SetFullObjectCount();
        ObjectManager.Instance.DestoryBallon();
        BalletManager.Instance.CanceldelayDoubleSet();
        QuestionManager.Instance.CancelShowQuestion();
        BuffManager.Instance.CancelInvokeProp();

        GameObject.Find("AudioInScenes").transform.FindChild("IntrodutionAudio").GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioInScenes").transform.FindChild("GetScoureAudio").GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioInScenes").transform.FindChild("PropBroken").GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioInScenes").transform.FindChild("EffectAudio").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Scene3(Main)");

    }
    /// <summary>
    /// 在结束面板和暂停面板处的重玩本关按钮点击事件
    /// </summary>
    public void TryAgainButtonOnPanelClick()
    {
        ResultManager.Instance.CancelInvokeInitGame();
        ResultManager.Instance.ClearTheViewEveryTest();
        ResultManager.Instance.ClearTheViewEveryLevel();
        Time.timeScale = 1;
        JsonManager.Instance.ClearJsonLists();
        JsonManager.Instance.ReadAllJsonToStoreInLists();

        ObjectManager.Instance.SetFullObjectCount();
        ObjectManager.Instance.DestoryBallon();
        BalletManager.Instance.CanceldelayDoubleSet();
        QuestionManager.Instance.CancelShowQuestion();
        BuffManager.Instance.CancelInvokeProp();

        GameObject.Find("AudioInScenes").transform.FindChild("IntrodutionAudio").GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioInScenes").transform.FindChild("GetScoureAudio").GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioInScenes").transform.FindChild("PropBroken").GetComponent<AudioSource>().Stop();
        GameObject.Find("AudioInScenes").transform.FindChild("EffectAudio").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Scene5(Find)");
        LevelManager.Instance.isBeginTheGameByNewLevel = true;
    }


    /// <summary>
    /// 场景5（算数）的星星改变
    /// </summary>
    public void StarsChange()
    {
        //星星父物体
        Transform Stars = GameObject.Find("Panels").transform.FindChild("GameOverPanel").FindChild("Stars");

        GameObject[] stars = new GameObject[3];     //星星物体

        Transform[] starsTarget = new Transform[3];  //星星的目标位置

        for (int i = 0; i < 3; i++)
        {
            stars[i] = Stars.FindChild("star" + (i + 1).ToString()).gameObject;
            starsTarget[i] = Stars.FindChild("star" + (i + 1).ToString() + "Position");
        }

        StartCoroutine(StarsMove(stars[0], stars[1], stars[2], starsTarget[0], starsTarget[1], starsTarget[2]));
    }
    /// <summary>
    /// 星星移动，在星星改变内调用
    /// </summary>
    /// <returns></returns>
    IEnumerator StarsMove(GameObject star1, GameObject star2, GameObject star3,
        Transform target1, Transform target2, Transform target3)
    {
        yield return new WaitForSeconds(1.0f);

        if (ScoreManager.Instance.totalScore / (TimeManager.Instance.totalTime - TimeManager.Instance.GetMaxTime()) >= 1.2f)
        {
            Invoke("EnableTryAgainAndBack", 7.0f);
            #region //第一颗星
            star1.SetActive(true);
            //先变大，用时1秒
            iTween.ScaleTo(star1, iTween.Hash("delay", 0.0f, "time", 1.0f, "x", 1.6f, "y", 1.6f,
                                                "looptype", iTween.LoopType.none));
            //再朝目标位置运动，用时0.5秒
            iTween.MoveTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "position", target1.position,
                                                "easetype", iTween.EaseType.easeInOutSine));
            //再运动过程中变回小
            iTween.ScaleTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "x", 1.0f, "y", 1.0f,
                                                "looptype", iTween.LoopType.none));
            //方向要对
            iTween.RotateTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "z", 16.0f,
                                                "looptype", iTween.LoopType.none,
                                                "easetype", iTween.EaseType.easeInSine));
            #endregion

            #region //第二颗星
            yield return new WaitForSeconds(2.0f);
            star2.SetActive(true);
            //先变大，用时1秒
            iTween.ScaleTo(star2, iTween.Hash("delay", 0.0f, "time", 1.0f, "x", 1.6f, "y", 1.6f,
                                                "looptype", iTween.LoopType.none));
            //再朝目标位置运动，用时0.5秒
            iTween.MoveTo(star2, iTween.Hash("delay", 1.0f, "time", 0.5f, "position", target2.position,
                                                "easetype", iTween.EaseType.easeInOutSine));
            //再运动过程中变回小
            iTween.ScaleTo(star2, iTween.Hash("delay", 1.0f, "time", 0.5f, "x", 1.0f, "y", 1.0f,
                                                "looptype", iTween.LoopType.none));
            #endregion

            #region //第三颗星
            yield return new WaitForSeconds(2.0f);
            star3.SetActive(true);
            //先变大，用时1秒
            iTween.ScaleTo(star3, iTween.Hash("delay", 0.0f, "time", 1.0f, "x", 1.6f, "y", 1.6f,
                                                "looptype", iTween.LoopType.none));
            //再朝目标位置运动，用时0.5秒
            iTween.MoveTo(star3, iTween.Hash("delay", 1.0f, "time", 0.5f, "position", target3.position,
                                                "easetype", iTween.EaseType.easeInOutSine));
            //再运动过程中变回小
            iTween.ScaleTo(star3, iTween.Hash("delay", 1.0f, "time", 0.5f, "x", 1.0f, "y", 1.0f,
                                                "looptype", iTween.LoopType.none));
            //方向要对
            iTween.RotateTo(star3, iTween.Hash("delay", 1.0f, "time", 0.5f, "z", -16.0f,
                                                "looptype", iTween.LoopType.none,
                                                "easetype", iTween.EaseType.easeInSine));
            #endregion
        }
        else if ((ScoreManager.Instance.totalScore / (TimeManager.Instance.totalTime - TimeManager.Instance.GetMaxTime()) >= 0.8f) &&
                (ScoreManager.Instance.totalScore / (TimeManager.Instance.totalTime - TimeManager.Instance.GetMaxTime()) < 1.2f))
        {
            Invoke("EnableTryAgainAndBack", 5.0f);
            #region //第一颗星
            star1.SetActive(true);
            //先变大，用时1秒
            iTween.ScaleTo(star1, iTween.Hash("delay", 0.0f, "time", 1.0f, "x", 1.6f, "y", 1.6f,
                                                "looptype", iTween.LoopType.none));
            //再朝目标位置运动，用时0.5秒
            iTween.MoveTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "position", target1.position,
                                                "easetype", iTween.EaseType.easeInOutSine));
            //再运动过程中变回小
            iTween.ScaleTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "x", 1.0f, "y", 1.0f,
                                                "looptype", iTween.LoopType.none));
            //方向要对
            iTween.RotateTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "z", 16.0f,
                                                "looptype", iTween.LoopType.none,
                                                "easetype", iTween.EaseType.easeInSine));
            #endregion

            #region //第二颗星
            yield return new WaitForSeconds(2.0f);
            star2.SetActive(true);
            //先变大，用时1秒
            iTween.ScaleTo(star2, iTween.Hash("delay", 0.0f, "time", 1.0f, "x", 1.6f, "y", 1.6f,
                                                "looptype", iTween.LoopType.none));
            //再朝目标位置运动，用时0.5秒
            iTween.MoveTo(star2, iTween.Hash("delay", 1.0f, "time", 0.5f, "position", target2.position,
                                                "easetype", iTween.EaseType.easeInOutSine));
            //再运动过程中变回小
            iTween.ScaleTo(star2, iTween.Hash("delay", 1.0f, "time", 0.5f, "x", 1.0f, "y", 1.0f,
                                                "looptype", iTween.LoopType.none));
            #endregion
        }

        else if ((ScoreManager.Instance.totalScore / (TimeManager.Instance.totalTime - TimeManager.Instance.GetMaxTime()) >= 0.4f) &&
                (ScoreManager.Instance.totalScore / (TimeManager.Instance.totalTime - TimeManager.Instance.GetMaxTime()) < 0.8f))
        {
            Invoke("EnableTryAgainAndBack", 3.0f);
            #region //第一颗星
            star1.SetActive(true);
            //先变大，用时1秒
            iTween.ScaleTo(star1, iTween.Hash("delay", 0.0f, "time", 1.0f, "x", 1.6f, "y", 1.6f,
                                                "looptype", iTween.LoopType.none));
            //再朝目标位置运动，用时0.5秒
            iTween.MoveTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "position", target1.position,
                                                "easetype", iTween.EaseType.easeInOutSine));
            //再运动过程中变回小
            iTween.ScaleTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "x", 1.0f, "y", 1.0f,
                                                "looptype", iTween.LoopType.none));
            //方向要对
            iTween.RotateTo(star1, iTween.Hash("delay", 1.0f, "time", 0.5f, "z", 16.0f,
                                                "looptype", iTween.LoopType.none,
                                                "easetype", iTween.EaseType.easeInSine));
            #endregion
        }
        else
        {
            Invoke("EnableTryAgainAndBack", 1.0f);
            yield return null;
        }
    }
    /// <summary>
    /// 开火点击按钮事件
    /// </summary>
    public void FireClick()
    {
        BalletManager.Instance.Fire();
    }
    /// <summary>
    /// 装弹按钮事件
    /// </summary>
    public void AddBalletClick()
    {
        BalletManager.Instance.AddBallet();
    }

    //分割精灵图片，并且加载到场景中
    public void SetSpt()
    {
        this.spt = Resources.LoadAll<Sprite>("FREE Food Pack/Materials");
        for (int i = 0; i < spt.Length; i++)
        {
            spriteDictionary.Add(spt[i].name, spt[i]);
        }
    }

    public Sprite SetImage(int objectID)
    {
        if (objectID == 0)
        {
            return Resources.Load<Sprite>("FREE Food Pack/Materials/FoodTexture0");
        }
        string name = "FoodTexture" + objectID;
        Sprite a = null;
        foreach (KeyValuePair<string, object> pair in spriteDictionary)
        {
            if (pair.Key.ToString() == name)
            {
                a = pair.Value as Sprite;
            }
        }
        return a;
    }

    /// <summary>
    /// 让结束面板的按钮起效，在StarsMove（）有调用
    /// </summary>
    public void EnableTryAgainAndBack()
    {
        GameObject.Find("Panels").transform.GetChild(2).FindChild("Buttons").GetChild(0).GetComponent<Button>().interactable = true;
        GameObject.Find("Panels").transform.GetChild(2).FindChild("Buttons").GetChild(1).GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// 每到新的关卡时，提示时间减少，道具出现等等。
    /// </summary>
    public void TipsByLevel()
    {
        Text tipsText = GameObject.Find("TipsText").GetComponent<Text>();
        switch (InitGameManager.Instance.level)
        {
            case 1:
                tipsText.text = "第一关！\n开始寻宝啦~！";
                break;
            case 2:
                tipsText.text = "第二关！\n物品会做迷藏啦！礼物增加啦！";
                break;
            case 3:
                tipsText.text = "第三关！\n题目变多啦！时间增加啦！";
                break;
            case 4:
                tipsText.text = "第四关！\n炸弹增加啦！";
                break;
            case 5:
                tipsText.text = "第五关！\n题目又变多啦！炸弹又增加啦！";
                break;
            case 6:
                tipsText.text = "第六关！\n时间又增加啦！礼物又增加啦！";
                break;
            case 7:
                tipsText.text = "第七关！\n疯狂模式开启啦！";
                break;
            default:
                break;
        }
        if (tipsText != null)
            Invoke("ClearTipsText", 3.0f);
    }

    /// <summary>
    /// 打爆问号气球，提示出现什么效果
    /// </summary>
    /// <param name="luckyNum">气球幸运号码</param>
    public void TipsByLuckyBalloon(int luckyNum)
    {
        Text tipsText = GameObject.Find("TipsText").GetComponent<Text>();

        switch (luckyNum)
        {
            case 1:
                tipsText.text = "时间增加！";
                break;
            case 2:
                tipsText.text = "时间冻结！";
                break;
            case 3:
                tipsText.text = "额外加分！";
                break;
            case 4:
                tipsText.text = "炸弹减少！";
                break;
            default:
                break;

        }
        if (tipsText != null)
            Invoke("ClearTipsText", 1.8f);
    }

    /// <summary>
    /// 打爆炸弹，提示出现什么效果
    /// </summary>
    /// <param name="luckyNum">气球幸运号码</param>
    public void TipsByBoomDebuff(int boomNum)
    {
        Text tipsText = GameObject.Find("TipsText").GetComponent<Text>();

        switch (boomNum)
        {
            case 1:
                tipsText.text = "起雾啦！";
                break;
            case 2:
                tipsText.text = "龙卷风来啦！";
                break;
            case 3:
                tipsText.text = "扣你分啦，哈哈哈！";
                break;
            case 4:
                tipsText.text = "对不起，失败！";
                break;
            default:
                break;

        }
        if (tipsText != null)
            Invoke("ClearTipsText", 1.8f);
    }


    /// <summary>
    /// 不同题目开始时，提示时间减少，道具出现等等。
    /// </summary>
    public void TipsByQuestionID()
    {
        Text tipsText = GameObject.Find("TipsText").GetComponent<Text>();
        switch (InitGameManager.Instance.id)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                tipsText.text = "道具出现了！";
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
        if (tipsText != null)
            Invoke("ClearTipsText", 1.0f);
    }

    /// <summary>
    /// 清除提示文本
    /// </summary>
    /// <param name="tipsText"></param>
    /// <param name="delayTime">延时调用</param>
    /// <returns></returns>
    public void ClearTipsText()
    {
        if (SceneManager.GetActiveScene().name.Equals("Scene5(Find)"))
        {
            Text tipsText = GameObject.Find("TipsText").GetComponent<Text>();
            if (tipsText != null)
            {
                tipsText.text = "";
            }
        }
    }

    /// <summary>
    /// 取消调用清除提示
    /// </summary>
    public void CancelInvokeClearTipsText()
    {
        CancelInvoke("ClearTipsText");
    }

    #endregion
}
