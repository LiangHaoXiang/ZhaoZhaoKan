//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class ObjectManager : MonoBehaviour
//{

//    int objectCount = 0;      //目前生成目标物体的数量
//    int createObjectNumber;   //要生成目标物体的总数量
//    float objectToy;          //相对生成目标位置的x偏移量
//    float objectTox;          //相对生成目标位置的y偏移量
//    float objectToz;          //相对生成目标位置的z偏移量
//    bool IsCreate = false;    //是否生成目标物体
//    int foodNumber;           //当前食物物体的编号
//    int questionNumber;       //生成问题和答案的下标编号
//    public List<GameObject> targetObject = new List<GameObject>();    //存储目标物体的List列表
//    public List<int> targetNumber = new List<int>();   //存储目标数字的List列表
//    [HideInInspector]
//    public Transform objectCenter;              //所有生成物体围绕的中心
//    [HideInInspector]
//    public Transform objectTargetPoint;         //生成物体的目标位置
//    public static ObjectManager instance;

//    public static ObjectManager Instance
//    {
//        get
//        {
//            if (instance == null)
//            {
//                instance = (ObjectManager)FindObjectOfType(typeof(ObjectManager));
//            }

//            return instance;
//        }
//    }

//    private void Awake()
//    {
//        instance = this;
//    }

//    void Start()
//    {
//        //初始生成x,y,z的随机数
//        Resultxyz();
//        //初始生成foodNumber的随机数
//        RamdonNumber();
//    }

//    void Update()
//    {
//        if (SceneManager.GetActiveScene().name.Equals("Scene5(Find)"))
//        {
//            objectCenter = GameObject.Find("ObjectCenter").transform;
//            objectTargetPoint = GameObject.Find("ObjectTargetPoint").transform;
//            if (IsCreate)
//            {

//                if (objectCount <= createObjectNumber)
//                {
//                    if (Mathf.Sqrt(objectTox * objectTox + objectToy * objectToy + objectToz * objectToz) < 2.5f)
//                    {
//                        if (!targetNumber.Contains(foodNumber))
//                        {
//                            if (objectCount > 0)
//                            {
//                                objectCenter.Rotate(0, (360 / createObjectNumber), 0);
//                            }
//                            CreateObject(new Vector3(objectTargetPoint.position.x + objectTox,
//                               objectTargetPoint.position.y + objectToy, objectTargetPoint.position.z + objectToz), foodNumber);
//                            objectCount++;
//                            Resultxyz();
//                        }
//                        else
//                        {
//                            RamdonNumber();
//                        }
//                    }
//                    else
//                    {
//                        Resultxyz();
//                    }
//                }
//            }
//        }

//        if (objectCount == createObjectNumber)
//        {
//            IsCreate = false;
//        }
//    }


//    //控制产生物品的条件
//    public IEnumerator BeginCreate(int createObjectNumber)
//    {
//        AudioSourceManager.Instance.Play(GameObject.Find("GetScoureAudio").gameObject, "ObjectShow");
//        IsCreate = true;
//        objectCount = 0;
//        this.createObjectNumber = createObjectNumber;
//        questionNumber = (int)Random.Range(0, 15);

//        yield return null;
//    }

//    //创建新的气球
//    public void CreateObject(Vector3 objectPosition, int number)
//    {
//        GameObject foodObject = Instantiate(Resources.Load("Prefabs/FoodObject/FoodTexture_" + number, typeof(GameObject))) as GameObject;
//        GameObject bubble = Instantiate(Resources.Load("Prefabs/Scene5(Find)/Bubble",typeof(GameObject))) as GameObject;
//        foodObject.transform.position = objectPosition;        //生成总物体的位置
//        bubble.transform.position = objectPosition;            //生成气泡的位置
//        bubble.transform.parent = foodObject.transform;        //气泡的父物体等于总物体
//        foodObject.transform.GetChild(0).transform.parent = bubble.transform;    //把食物设为气泡的子物体
//        foodObject.transform.parent = GameObject.Find("BallonsCreates").transform;   //把总物体设置为BallonsCreates的子物体
//        bubble.transform.GetChild(0).GetComponent<FoodObject>().SetfoodObjectNumber(number);
//        targetNumber.Add(foodNumber);
//        targetObject.Add(foodObject);
//    }

//    //随机生成物体距离顶点的范围的三维坐标随机数
//    void Resultxyz()
//    {
//        objectToy = (float)Random.Range(-2.5f, 2.5f);
//        objectTox = (float)Random.Range(-1f, 1f);
//        objectToz = (float)Random.Range(-1f, 1f);
//    }

//    //随机生成物体的下标数
//    void RamdonNumber()
//    {
//        foodNumber = (int)Random.Range(1, 50);
//    }

//    //销毁所有物体
//    public void DestoryBallon()
//    {
//        foreach (GameObject T in targetObject)
//        {
//            Destroy(T);
//        }
//        targetObject.Clear();

//        targetNumber.Clear();
//    }

//    public int getTargetNumber()
//    {
//        if (targetNumber != null)
//        {
//            return targetNumber[questionNumber];
//        }
//        else return -1;
//    }

//    public void SetFullObjectCount()
//    {
//        this.objectCount = createObjectNumber;
//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectManager : MonoBehaviour
{

    int objectCount = 0;      //目前生成目标物体的数量
    int createObjectNumber;   //要生成目标物体的总数量
    int boomCount = 0;        //目前生成炸弹的数量
    int createBoomNumber;     //要生成炸弹的总数量
    int nicePropCount = 0;    //目前生成道具的数量
    int createNicePropNumber; //要生成道具的总数量
    float objectToy;          //相对生成目标位置的x偏移量
    float objectTox;          //相对生成目标位置的y偏移量
    float objectToz;          //相对生成目标位置的z偏移量
    bool IsCreate = false;    //是否生成目标物体
    bool isCreateBoom = false;     //是否生成炸弹
    bool isCreateNiceProp = false;    //是否生成道具
    int foodNumber;           //当前食物物体的编号
    int questionNumber;       //生成问题和答案的下标编号
    float disappearTime;      //物体隐藏的时间
    [HideInInspector]
    public List<GameObject> targetObject = new List<GameObject>();    //存储目标物体的List列表
    [HideInInspector]
    public List<int> targetNumber = new List<int>();   //存储目标数字的List列表
    [HideInInspector]
    public List<GameObject> PropObject = new List<GameObject>();  //用于存储道具的List列表
    [HideInInspector]
    public List<GameObject> BoomObject = new List<GameObject>();  //用于存储炸弹的List列表
    [HideInInspector]
    public Transform objectCenter;              //所有生成物体围绕的中心
    [HideInInspector]
    public Transform objectTargetPoint;         //生成物体的目标位置
    [HideInInspector]
    public Transform propCenter;              //道具生成物体围绕的中心
    [HideInInspector]
    public Transform propTargetPoint;         //生成物体的目标位置
    int propPathNumber = 0; //第几号炸弹
    bool isRotate; //是否旋转 
    public static ObjectManager instance;

    public static ObjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (ObjectManager)FindObjectOfType(typeof(ObjectManager));
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
        //初始生成x,y,z的随机数
        Resultxyz();
        //初始生成foodNumber的随机数
        RamdonNumber();
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Scene5(Find)"))
        {
            objectCenter = GameObject.Find("ObjectCenter").transform;
            objectTargetPoint = GameObject.Find("ObjectTargetPoint").transform;
            propCenter = GameObject.Find("PropCenter").transform;
            propTargetPoint = GameObject.Find("PropTargetPoint").transform;

            if (IsCreate)
            {

                if (objectCount < createObjectNumber)
                {
                    if (Mathf.Sqrt(objectTox * objectTox + objectToy * objectToy + objectToz * objectToz) < 2.5f)
                    {
                        if (!targetNumber.Contains(foodNumber))
                        {
                            if (objectCount > 0)
                            {
                                objectCenter.Rotate(0, (360 / createObjectNumber), 0);
                            }
                            CreateObject(new Vector3(objectTargetPoint.position.x + objectTox,
                               objectTargetPoint.position.y + objectToy, objectTargetPoint.position.z + objectToz), foodNumber);
                            objectCount++;
                            Resultxyz();
                        }
                        else
                        {
                            RamdonNumber();
                        }
                    }
                    else
                    {
                        Resultxyz();
                    }
                }
            }

            if (isCreateNiceProp)
            {
                if (nicePropCount < createNicePropNumber)
                {
                    CreateNiceProp(propTargetPoint.position);
                    propCenter.Rotate(0, (360 / (createNicePropNumber + createBoomNumber)), 0);
                    nicePropCount++;
                }
            }

            if (isCreateBoom)
            {
                if (boomCount < createBoomNumber)
                {
                    CreateBoomObject(propTargetPoint.position);
                    propCenter.Rotate(0, (360 / (createNicePropNumber + createBoomNumber)), 0);
                    boomCount++;
                }
            }

        }

        if (objectCount == createObjectNumber)
        {
            IsCreate = false;
        }
        if (nicePropCount == createNicePropNumber)
        {
            isCreateNiceProp = false;
        }
        if (boomCount == createBoomNumber)
        {
            isCreateBoom = false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="createObjectNumber" =产生食物气泡的数量 ></param>
    /// <param name="boomNumber" = 产生炸弹的数量></param>
    /// <param name="nicePropNumber" = 产生道具的数量></param>
    public IEnumerator BeginCreate(int createObjectNumber, int boomNumber, int nicePropNumber , float disappearTime, bool isRotate)
    {
        AudioSourceManager.Instance.Play(GameObject.Find("GetScoureAudio").gameObject, "ObjectShow");
        //生成食物气泡
        IsCreate = true;
        objectCount = 0;
        this.createObjectNumber = createObjectNumber;
        //生成炸弹
        this.isCreateBoom = true;
        this.boomCount = 0;
        this.createBoomNumber = boomNumber;
        //生成道具
        this.isCreateNiceProp = true;
        this.nicePropCount = 0;
        this.createNicePropNumber = nicePropNumber;

        this.isRotate = isRotate;
        this.disappearTime = disappearTime;
        questionNumber = (int)Random.Range(0, 15);

        yield return null;
    }

    //创建新的气球
    public void CreateObject(Vector3 objectPosition, int number)
    {
        GameObject foodObject = Instantiate(Resources.Load("Prefabs/FoodObject/FoodTexture_" + number, typeof(GameObject))) as GameObject;
        GameObject bubble = Instantiate(Resources.Load("Prefabs/Scene5(Find)/Bubble", typeof(GameObject))) as GameObject;
        foodObject.transform.position = objectPosition;        //生成总物体的位置
        bubble.transform.position = objectPosition;            //生成气泡的位置
        if (isRotate)
        {
            //让食物自转
            iTween.RotateBy(foodObject.transform.GetChild(0).transform.gameObject, iTween.Hash("y", 12, "time", 48, "looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.linear));
        }
        bubble.transform.parent = foodObject.transform;        //气泡的父物体等于总物体
        foodObject.transform.GetChild(0).transform.parent = bubble.transform;    //把食物设为气泡的子物体
        foodObject.transform.parent = GameObject.Find("BallonsCreates").transform;   //把总物体设置为BallonsCreates的子物体
        bubble.transform.GetChild(0).GetComponent<FoodObject>().SetfoodObjectNumber(number, disappearTime);
        targetNumber.Add(foodNumber);
        targetObject.Add(foodObject);


    }

    //创建道具
    public void CreateNiceProp(Vector3 propPosition)
    {
        GameObject propObject = Instantiate(Resources.Load("Prefabs/Scene5(Find)/PropFather" + (int)Random.Range(1,5), typeof(GameObject))) as GameObject;
        propObject.transform.position = propPosition;
        propObject.transform.parent = GameObject.Find("BallonsCreates").transform;   //把物体设置为BallonsCreates的子物体
        iTween.MoveTo(propObject, iTween.Hash("time", 20.0f, "path", iTweenPath.GetPath("BoomPath" + propPathNumber % 3), "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear));
        propObject.transform.GetChild(4).GetComponent<PropObject>().SetfoodObjectNumber("propObject");
        PropObject.Add(propObject);
        propPathNumber++;
    }
    //创建炸弹
    public void CreateBoomObject(Vector3 BoomPosition)
    {
        GameObject boomObject = Instantiate(Resources.Load("Prefabs/Scene5(Find)/BoomFather", typeof(GameObject))) as GameObject;
        boomObject.transform.position = BoomPosition;
        boomObject.transform.LookAt(GameObject.Find("MainController").transform);
        boomObject.transform.parent = GameObject.Find("BallonsCreates").transform;   //把物体设置为BallonsCreates的子物体
        boomObject.transform.GetChild(0).GetComponent<PropObject>().SetfoodObjectNumber("boomObject");
        BoomObject.Add(boomObject);
    }

    //随机生成物体距离顶点的范围的三维坐标随机数
    void Resultxyz()
    {
        objectToy = (float)Random.Range(-2.5f, 2.5f);
        objectTox = (float)Random.Range(-0.5f, 0.5f);
        objectToz = (float)Random.Range(-0.5f, 0.5f);
    }

    //随机生成物体的下标数
    void RamdonNumber()
    {
        foodNumber = (int)Random.Range(1, 50);
    }

    //销毁所有物体
    public void DestoryBallon()
    {
        foreach (GameObject T in targetObject)
        {
            Destroy(T);
        }
        targetObject.Clear();
        targetNumber.Clear();
        foreach (GameObject P in PropObject)
        {
            Destroy(P);
        }
        PropObject.Clear();
        foreach(GameObject B in BoomObject)
        {
            Destroy(B);
        }
        BoomObject.Clear();
    }

    public int getTargetNumber()
    {
        if (targetNumber != null)
        {
            return targetNumber[questionNumber];
        }
        else return -1;
    }

    public void SetFullObjectCount()
    {
        this.objectCount = createObjectNumber;
        this.nicePropCount = createNicePropNumber;
        this.boomCount = createBoomNumber;
    }

}



