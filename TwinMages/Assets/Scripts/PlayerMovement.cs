using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker.Actions;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public FixedJoystick fixedJoystick;
    [SerializeField] public GameObject otherPlayer;
    [SerializeField] public float speed;

    private Rigidbody _rb;
    private Vector3 _currentDirection;
    private Transform _otherPlayerTransform;
    private bool _enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(otherPlayer, "otherPlayer can't be null");
        _rb = GetComponent<Rigidbody>();
        _otherPlayerTransform = otherPlayer.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_enabled)
        {
            return;
        }
        _currentDirection = Vector3.forward * fixedJoystick.Vertical;
        _rb.MovePosition(transform.position + _currentDirection * speed * Time.deltaTime);
        transform.LookAt(_otherPlayerTransform);
    }

    public void OnTravellingActivated()
    {
        _enabled = false;
    }
    
    public void OnTravellingDesactivated()
    {
        _enabled = true;
    }
}
