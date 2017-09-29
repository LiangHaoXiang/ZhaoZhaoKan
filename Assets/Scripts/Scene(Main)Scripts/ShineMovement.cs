using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineMovement : MonoBehaviour {

	void Start () {
        iTween.RotateBy(gameObject, iTween.Hash("time", 20.0f, "z", 6.0f,
            "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.loop));
        iTween.ScaleTo(gameObject, iTween.Hash("time", 2.0f, "x", 1.1f, "y", 1.1f,
            "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
    }
	
}
