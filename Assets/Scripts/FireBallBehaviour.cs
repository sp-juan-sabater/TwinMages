using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Assertions;

public class FireBallBehaviour : MonoBehaviour
{
    //Constants
    private string _kReboundCountKey = "ReboundCount";
    private string _kSpeedKey = "Speed";
    
    private PlayMakerFSM fsm;
    
    // Start is called before the first frame update
    void Start()
    {
        fsm = gameObject.GetComponent<PlayMakerFSM>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        fsm.FsmVariables.GetFsmFloat(_kReboundCountKey).Value = 0;
    }

    public void OnTravellingActivated()
    {
        fsm.FsmVariables.GetFsmFloat(_kSpeedKey).Value = 0;
    }
    
    public void OnTravellingDesactivated()
    {
        Destroy(gameObject);
    }
}
