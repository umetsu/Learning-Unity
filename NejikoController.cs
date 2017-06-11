using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    private const int MinLane = -2;
    private const int MaxLane = 2;
    private const float LaneWidth = 1.0f;

    public float Gravity;
    public float SpeedZ;
    public float SpeedX;
    public float SpeedJump;
    public float AccelerationZ;

    private CharacterController _controller;
    private Animator _animator;
    
    private Vector3 _moveDirection = Vector3.zero;
    private int _targetLane;
    
    // Use this for initialization
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();
        
        // 加速しながら常に前進
        var acceleratedZ = _moveDirection.z + AccelerationZ * Time.deltaTime;
        _moveDirection.z = Mathf.Clamp(acceleratedZ, 0, SpeedZ);
        
        // x方向は目標のポジションまでの差分の割合で速度を計算
        var ratioX = (_targetLane * LaneWidth - transform.position.x) / LaneWidth;
        _moveDirection.x = ratioX * SpeedX;

        // 重力分の力を毎フレーム追加
        _moveDirection.y -= Gravity * Time.deltaTime;

        var globalDirection = transform.TransformDirection(_moveDirection);
        _controller.Move(globalDirection * Time.deltaTime);
        
        // 移動後、接地していたらY方向の速度はリセット
        if (_controller.isGrounded)
        {
            _moveDirection.y = 0;
        }
        
        _animator.SetBool("run", _moveDirection.z > 0.0f);
    }

    private void MoveToLeft()
    {
        if (_controller.isGrounded && _targetLane > MinLane)
        {
            --_targetLane;
        }
    }
    
    private void MoveToRight()
    {
        if (_controller.isGrounded && _targetLane < MaxLane)
        {
            ++_targetLane;
        }
    }
    
    private void Jump()
    {
        if (_controller.isGrounded)
        {
            _moveDirection.y = SpeedJump;
            
            _animator.SetTrigger("jump");
        }
    }
}