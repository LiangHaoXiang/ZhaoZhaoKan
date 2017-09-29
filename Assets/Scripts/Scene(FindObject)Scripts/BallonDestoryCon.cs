using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonDestoryCon : MonoBehaviour
{

    bool IsBroken = false;

    private void Start()
    {
       
    }

    void Update()
    {
        if (IsBroken)
        {

            Destroy(this.gameObject, 4);
            AudioSourceManager.Instance.Play(GameObject.Find("PropBroken").gameObject, "WaterEixt");
            IsBroken = false;
        }

    }

    public void SetIsBroken(bool isBroken)
    {
        this.IsBroken = isBroken;
    }
}
