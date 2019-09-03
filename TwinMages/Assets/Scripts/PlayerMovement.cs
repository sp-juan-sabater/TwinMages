using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public FixedJoystick fixedJoystick;
    [SerializeField] public GameObject otherPlayer;
    [SerializeField] public float speed;
    
    private Rigidbody _rb;
    private Vector3 _currentDirection;
    private Transform _otherPlayerTransform;

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
        _currentDirection = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        _rb.MovePosition(transform.position + _currentDirection * speed * Time.deltaTime);
        transform.LookAt(_otherPlayerTransform);
    }
}
