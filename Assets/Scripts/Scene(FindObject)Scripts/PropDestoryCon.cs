using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropDestoryCon : MonoBehaviour {

    bool IsBroken = false;
    bool IsBoom;
    private void Start()
    {

    }

    void Update()
    {
        //如果该物体是炸弹，执行下面的代码
        if (IsBroken && IsBoom)
        {
            AudioSourceManager.Instance.Play(GameObject.Find("PropBroken").gameObject, "BoomBroken");
            Destroy(this.gameObject, 3);
            IsBroken = false;
        }  
        //如果该物体是道具，执行下面的代码
        if (IsBroken && !IsBoom)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(true);
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(3).gameObject.SetActive(true);
            Destroy(gameObject.transform.FindChild("PropMap").gameObject);
            Destroy(this.gameObject, 3);
            IsBroken = false;
        }

    }

    public void SetIsBroken(bool isBroken , bool isBoom)
    {
        this.IsBroken = isBroken;
        this.IsBoom = isBoom;
    }
}
