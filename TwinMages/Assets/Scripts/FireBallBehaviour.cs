using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FireBallBehaviour : MonoBehaviour
{
    [SerializeField] public GameObject GameManager;

    private FireBallSpawner _fireBallSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(GameManager, "GameManager can't be null");
        _fireBallSpawner = GameManager.GetComponent<FireBallSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        _fireBallSpawner.SetNeedSpawnFireball();
    }

    //void OnCollisionEnter(Collision collision)
    //{
        //Debug.Log("Contact");
    //}
}
