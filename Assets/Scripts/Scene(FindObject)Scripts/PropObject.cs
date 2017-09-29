using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PropObject : MonoBehaviour {

    string propObjectType; //存储道具类型的变量
    bool isUse = true;

    private void Start()
    {
        if(propObjectType == "boomObject")
        {
            Invoke("PlayAni", Random.Range(0, 2.5f));
        }
    }


    //为物体添加一个碰撞检测方法
    private void OnCollisionEnter(Collision collision)
    {
        //当这个物体时道具时，调用此函数
        if (collision.gameObject.tag.CompareTo("Ballet") == 0 && propObjectType == "propObject")
        {
            ObjectManager.Instance.PropObject.Remove(this.transform.parent.gameObject);
            Handheld.Vibrate(); //手机振动
            if (isUse)
            {
                BuffManager.Instance.LuckyBalloon();
                this.transform.parent.GetComponent<PropDestoryCon>().SetIsBroken(true, false);
                isUse = false;
            }
            this.GetComponent<SphereCollider>().enabled = false;
            Destroy(this.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
        //当这个物体是炸弹时，调用此函数
        if (collision.gameObject.tag.CompareTo("Ballet") == 0 && propObjectType == "boomObject")
        {
            ObjectManager.Instance.BoomObject.Remove(this.transform.parent.gameObject);
            Handheld.Vibrate(); //手机振动
            if (isUse)
            {
                BuffManager.Instance.BoomDeBuff();
                isUse = false;
            }
            GameObject boomBroken = Instantiate(Resources.Load("Prefabs/Scene5(Find)/BoomBroken", typeof(GameObject))) as GameObject;
            boomBroken.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            boomBroken.transform.parent = this.transform.parent.transform.parent;
            this.transform.parent.GetComponent<PropDestoryCon>().SetIsBroken(true, true);
            this.transform.GetChild(0).position = this.transform.position;
            this.GetComponent<SphereCollider>().enabled = false;
            Destroy(this.gameObject);
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
    }

    public void SetfoodObjectNumber(string propObjectType)
    {
        this.propObjectType = propObjectType;

    }

    void PlayAni()
    {
        this.GetComponent<Animation>().Play("BoomMovment");
    }

}
