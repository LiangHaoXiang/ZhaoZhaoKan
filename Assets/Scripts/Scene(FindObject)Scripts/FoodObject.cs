using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodObject : MonoBehaviour
{

    int foodObjectNumber;
    bool isAnswer = true;
    float timeToHide; //过几秒后就隐藏物体
    float timeDuringHide;  //隐藏藏物体的时间
    bool isHide = false; //物体的隐藏状态
    float timeCount = 0;  //用于计时
    GameObject aroundPoint;
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Scene5(Find)"))
        {
            aroundPoint = GameObject.Find("AroundRotatePoint").transform.gameObject;
            GameObject mapCatch = Instantiate(Resources.Load("Prefabs/Scene5(Find)/MapCatch", typeof(GameObject))) as GameObject;
            mapCatch.transform.position = this.transform.position;
            mapCatch.transform.parent = this.transform;
            // this.transform.LookAt(GameObject.Find("MainController").transform);
            Invoke("DelayPlayAnimation", (int)Random.Range(1, 4));
            timeToHide = Random.Range(2, 5);
        }
    }

    private void Update()
    {
        this.transform.parent.transform.parent.transform.RotateAround(aroundPoint.transform.position,Vector3.up,Time.deltaTime * 10);
        timeCount += Time.deltaTime;
        if (timeCount > timeToHide && !isHide)   //时间到了规定的时间并且物体处于没有隐藏的状态即可执行以下代码
        {
            this.GetComponent<MeshRenderer>().enabled = false;   //隐藏物体
            Invoke("FoodRecover", timeDuringHide); //隐藏多少秒之后调用此函数，即恢复不隐藏的状态
            isHide = true;   //设置物体状态为隐藏状态
        }
    }

    //为物体添加一个碰撞检测方法
    private void OnCollisionEnter(Collision collision)
    {
        //当碰撞到该物体的物体是子弹时，直接销毁该物体
        if (collision.gameObject.tag.CompareTo("Ballet") == 0)
        {
            ObjectManager.Instance.targetObject.Remove(this.transform.parent.transform.parent.gameObject);
            Handheld.Vibrate(); //手机振动
            if (isAnswer)
            {
                AnswerManager.Instance.IsAnswerRight(foodObjectNumber);
                isAnswer = false;
            }
            GameObject bubbleParticle = Instantiate(Resources.Load("Prefabs/Scene5(Find)/BubbleBroken", typeof(GameObject))) as GameObject;
            bubbleParticle.transform.position = this.transform.position;
            bubbleParticle.transform.parent = this.transform.parent.transform.parent;
            this.transform.parent.transform.parent.gameObject.AddComponent<BallonDestoryCon>();
            this.transform.parent.transform.parent.gameObject.GetComponent<BallonDestoryCon>().SetIsBroken(true);
            this.GetComponent<SphereCollider>().enabled = false;
            Destroy(this.gameObject.transform.parent.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }

    public void SetfoodObjectNumber(int foodObjectNumber, float hideTime)
    {
        this.foodObjectNumber = foodObjectNumber;
        this.timeDuringHide = hideTime;
    }

    public void DelayPlayAnimation()
    {
        this.transform.parent.GetComponent<Animation>().Play("BubbleMovment" + (int)Random.Range(1, 3));
    }

    void FoodRecover()
    {
        this.GetComponent<MeshRenderer>().enabled = true;   //显示物体出来
        isHide = false; //设置物体的状态为非隐藏的状态
        timeCount = 0; //设计计时器重新开始
    }

}
