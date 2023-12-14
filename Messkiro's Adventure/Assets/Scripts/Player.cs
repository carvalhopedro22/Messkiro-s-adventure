﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    private Rigidbody2D rig;

    private float initialSpeed;
    private Vector2 _direction;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;

    private int handlingObj;

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool isCutting
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }

    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            handlingObj = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            handlingObj = 1;
        }

        OnInput();
        OnRun();
        OnRolling();
        OnCutting();
        OnDigging();
    }

    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement
    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }
    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }
    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1)) // 1 = Botão direito do mouse
        {
            speed = runSpeed;
            _isRolling = true;
        }

        if (Input.GetMouseButtonUp(1)) 
        {
            speed = initialSpeed;
            _isRolling = false;
        }
    }

    void OnCutting() // handlingObj = 0
    {
        if(handlingObj == 0)
        {
            if (Input.GetMouseButtonDown(0)) // 0 = Botão esquerdo do mouse
            {
                isCutting = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initialSpeed;
            }
        }  
    }
    void OnDigging() // handlingObj = 1
    {
        if(handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0)) // 0 = Botão esquerdo do mouse
            {
                isDigging = true;
                speed = 0f;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }   
    }
    #endregion
}
