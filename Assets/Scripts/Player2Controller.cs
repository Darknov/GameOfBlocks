﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player2Controller : MonoBehaviour
{
    public static bool p2GamePad = false;

    public float blocksSpeed;
    public float boostSpeed;
    public float timeOfInverseControlOfPlayer2 = 3.0f;
    public float timeOfswitchMovementOfPlayer1 = 5.0f;
    private float jumpLength = 1;
    public GameAccelerator gameAccelerator;
    public RandomAttackBlockGenerator randomBlockGenerator;
    private AttackBlock activeBlock;
    public Transform northSpawn, southSpawn, eastSpawn, westSpawn;
    private AttackBlock northBlock, southBlock, eastBlock, westBlock;
    public PlatformBoard platformBoard;
    public ShadowBlock blockShadow;

    private float horizontalAxisPlayer2;
    private float verticalAxisPlayer2;
    private bool isAxisHorizontalInUse;
    private bool isAxisVerticalInUse;

    public static bool isDestroyBlockAvailable;
    public static bool isDestroyBlockActivated;
    public static int movementSwitchCounter = 3;

    public Text movementSwitchAlert;
    private Material tempMaterial;

    void Start()
    {
        if (northSpawn == null || southSpawn == null || eastSpawn == null || null == westSpawn)
            throw new NullReferenceException("You must assign all spawn points to " + this.GetType().Name);

        if (blocksSpeed == 0)
            throw new ArgumentException("blocksSpeed cannot be 0");

        CreateInitialAttackBlocks();

        isAxisHorizontalInUse = false;
        isAxisVerticalInUse = false;

		isDestroyBlockAvailable = false;
        isDestroyBlockActivated = false;
    }

    void Update()
    {
		Debug.Log ("isDestroyBlockAvailable" + isDestroyBlockAvailable);
		Debug.Log ("isDestroyBlockActivated" + isDestroyBlockActivated);
        Debug.Log("p2gamePad: " + p2GamePad);
		//Debug.Log ("DestroyBlock" + activeBlock.isDestroyBlock);

        blocksSpeed = gameAccelerator.player2Speed;

        if (p2GamePad)
        {
            horizontalAxisPlayer2 = Input.GetAxisRaw("HorizontalJoyPlayer2");
            verticalAxisPlayer2 = Input.GetAxisRaw("VerticalJoyPlayer2");

            bool canFire = activeBlock == null;

            if (canFire)
            {
                /*if (Input.GetKey("joystick 2 button 7") && isDestroyBlockAvailable)
                {
                    isDestroyBlockAvailable = false;
                    isDestroyBlockActivated = true;
                }*/

               

                if (Input.GetKeyDown("joystick 2 button 0"))
                {
                    ActivateBlock(northBlock);
                    northBlock = null;
                }
                else if (Input.GetKeyDown("joystick 2 button 2"))
                {
                    ActivateBlock(southBlock);
                    southBlock = null;
                }
                else if (Input.GetKeyDown("joystick 2 button 3"))
                {
                    ActivateBlock(westBlock);
                    westBlock = null;
                }
                else if (Input.GetKeyDown("joystick 2 button 1"))
                {
                    ActivateBlock(eastBlock);
                    eastBlock = null;
                }

            }

            bool canTurn = !canFire;

            if (canTurn)
            {
                if (Input.GetKey("joystick 2 button 4"))
                {
                    activeBlock.ChangeSpeed(boostSpeed);
                }
                else
                {
                    activeBlock.MoveSpeed = blocksSpeed;
                }

                if (IsVertical(activeBlock))
                {
                    if (isAxisHorizontalInUse == false)
                    {
                        if (horizontalAxisPlayer2 == -1)
                        {
                            activeBlock.GoToYourLeft();
                            isAxisHorizontalInUse = true;
                            UpdateShadow();
                        }
                        else if (horizontalAxisPlayer2 == 1)
                        {
                            activeBlock.GoToYourRight();
                            isAxisHorizontalInUse = true;
                            UpdateShadow();
                        }
                    }
                }
                else
                {
                    if (isAxisVerticalInUse == false)
                    {
                        if (verticalAxisPlayer2 == 1)
                        {
                            activeBlock.GoToYourRight();
                            isAxisVerticalInUse = true;
                            UpdateShadow();
                        }
                        else if (verticalAxisPlayer2 == -1)
                        {
                            activeBlock.GoToYourLeft();
                            isAxisVerticalInUse = true;
                            UpdateShadow();
                        }
                    }
                }

                if (horizontalAxisPlayer2 == 0)
                    isAxisHorizontalInUse = false;

                if (verticalAxisPlayer2 == 0)
                    isAxisVerticalInUse = false;

                if (Input.GetKeyDown(KeyCode.Joystick2Button5))
                {
                    activeBlock.TurnNinetyDegreesAndUpdateTriggers();
                    UpdateShadow();
                }
            }
        }
        else if (!p2GamePad)
        {

            bool canFire = activeBlock == null;

            if (canFire)
            {
               /*if (Input.GetKeyDown(KeyCode.Alpha0) && isDestroyBlockAvailable)
                {
                    isDestroyBlockAvailable = false;
                    isDestroyBlockActivated = true;
                }*/
               
                if (Input.GetKeyDown(KeyCode.Alpha8) && movementSwitchCounter > 0)
                {
                    timeOfswitchMovementOfPlayer1 = 3.0f;

                    movementSwitchAlert.text = "Automovement switch starts in 1s! ";// + + "s !";
                    Invoke("switchAutomovement", 1);
                }

                if (Player1Controller.isAutomovementOn == false)
                {
                    if (movementSwitchAlert != null)
                        movementSwitchAlert.text = "Automovement switch ends in " + (int)timeOfswitchMovementOfPlayer1 + "s !";
                    timeOfswitchMovementOfPlayer1 -= Time.deltaTime;
                    Debug.Log(timeOfswitchMovementOfPlayer1);
                    if (timeOfswitchMovementOfPlayer1 < 0)
                    {
                        if (movementSwitchAlert != null)
                            movementSwitchAlert.text = "";
                        Player1Controller.isAutomovementOn = true;
                        // timeOfswitchMovementOfPlayer1 = 3.0f;
                    }
                }
               
                if (Input.GetKeyDown("i"))
                {
                    ActivateBlock(northBlock);
                    northBlock = null;
                }
                else if (Input.GetKeyDown("k"))
                {
                    ActivateBlock(southBlock);
                    southBlock = null;
                }
                else if (Input.GetKeyDown("j"))
                {
                    ActivateBlock(westBlock);
                    westBlock = null;
                }
                else if (Input.GetKeyDown("l"))
                {
                    ActivateBlock(eastBlock);
                    eastBlock = null;
                }

                if (Player2Controller.isDestroyBlockActivated && activeBlock != null)
                {
                    activeBlock.isDestroyBlock = true;
                    Player2Controller.isDestroyBlockActivated = false;
                    Player2Controller.isDestroyBlockAvailable = false;

                }

            }

            bool canTurn = !canFire;

            if (canTurn)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    activeBlock.ChangeSpeed(boostSpeed);
                }
                else
                {
                    activeBlock.MoveSpeed = blocksSpeed;
                }

                if (IsVertical(activeBlock))
                {
                    if (Input.GetKeyDown(KeyCode.J))
                    {
                        activeBlock.GoToYourLeft();
                        UpdateShadow();
                    }
                    else if (Input.GetKeyDown(KeyCode.L))
                    {
                        activeBlock.GoToYourRight();
                        UpdateShadow();
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.I))
                    {
                        if (activeBlock == westBlock)
                        {
                            activeBlock.GoToYourRight();
                            UpdateShadow();
                        }
                        else
                        {
                            activeBlock.GoToYourLeft();
                            UpdateShadow();
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.K))
                    {
                        if (activeBlock == westBlock)
                        {
                            activeBlock.GoToYourLeft();
                            UpdateShadow();
                        }
                        else
                        {
                            activeBlock.GoToYourRight();
                            UpdateShadow();
                        }
                    }


                }

                Debug.Log("Use Horizontal: " + isAxisHorizontalInUse);
                Debug.Log("Use Vertical: " + isAxisVerticalInUse);
                Debug.Log("Use Vertical: " + isAxisVerticalInUse);
                Debug.Log("Use Vertical: " + isAxisVerticalInUse);

                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    activeBlock.TurnNinetyDegreesAndUpdateTriggers();
                    UpdateShadow();
                }
            }
        }
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            movementSwitchCounter = 3;
            Player1Controller.hommingMissleCounter = 3;
            movementSwitchAlert.text = "";
        }

    }
    void switchAutomovement()
    {
        movementSwitchCounter--;
        Player1Controller.isAutomovementOn = false;
    }


    bool IsVertical(AttackBlock attackBlock)
    {
        if (northBlock == null || southBlock == null) return true;
        else return false;
    }

    void ActivateBlock(AttackBlock block)
    {
        this.activeBlock = block;
        this.activeBlock.Activate();
        Color color = Player2Controller.isDestroyBlockActivated ? Color.yellow : Color.red;
        tempMaterial = block.GetComponentInChildren<Renderer>().material;

        if (Player2Controller.isDestroyBlockActivated)
            block.isDestroyBlock = true;

        if (block.isDestroyBlock == false)
            block.PlatformHit += OnActiveBlockPlatformHit;
        else
            block.PlatformHit += OnDestroyBlockPlatformHit;

        block.DestroyAttackBlock += RespawnEmptyBlocks;
        blockShadow.CreateShadow(this.activeBlock.gameObject);
        SetAttackBlockColor(color, activeBlock);
    }

    public AttackBlock GetActiveBlock()
    {
        return this.activeBlock;
    }

    void OnActiveBlockPlatformHit(object source, EventArgs args)
    {
        if (activeBlock == null) return;
        SetAttackBlockColor(Color.white, activeBlock);
        platformBoard.addBlock(activeBlock, tempMaterial);

        blockShadow.DestroyShadow();
        RespawnEmptyBlocks(source, args);
    }

    void OnDestroyBlockPlatformHit(object source, EventArgs args)
    {
        if (activeBlock == null) return;
        SetAttackBlockColor(Color.black, activeBlock);

        Destroy(activeBlock.gameObject);
        blockShadow.DestroyShadow();
        RespawnEmptyBlocks(source, args);
    }

    void RespawnEmptyBlocks(object source, EventArgs args)
    {
        if (activeBlock == null) return;

        if (activeBlock.MoveDirection.Equals(Vector3.back))
            northBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength,
                activeBlock.MoveDirection);
        else if (activeBlock.MoveDirection == Vector3.forward)
            southBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength,
                activeBlock.MoveDirection);
        else if (activeBlock.MoveDirection == Vector3.left)
            eastBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength,
                activeBlock.MoveDirection);
        else if (activeBlock.MoveDirection == Vector3.right)
            westBlock = randomBlockGenerator.createRandomBlock(activeBlock.transform.parent, blocksSpeed, jumpLength,
                activeBlock.MoveDirection);

        this.activeBlock.transform.parent = null;
        this.activeBlock = null;
    }

    void SetAttackBlockColor(Color color, AttackBlock activeBlock)
    {
        var childrenMaterials = activeBlock.gameObject.GetComponentsInChildren<Renderer>();

        foreach (var item in childrenMaterials)
        {
            item.material.color = color;
        }
    }

    void CreateInitialAttackBlocks()
    {
        northBlock = randomBlockGenerator.createRandomBlock(northSpawn, blocksSpeed, jumpLength, Vector3.back);
        southBlock = randomBlockGenerator.createRandomBlock(southSpawn, blocksSpeed, jumpLength, Vector3.forward);
        eastBlock = randomBlockGenerator.createRandomBlock(eastSpawn, blocksSpeed, jumpLength, Vector3.left);
        westBlock = randomBlockGenerator.createRandomBlock(westSpawn, blocksSpeed, jumpLength, Vector3.right);
    }

    private void UpdateShadow()
    {
        blockShadow.DestroyShadow();
        blockShadow.CreateShadow(activeBlock.gameObject);
    }
}