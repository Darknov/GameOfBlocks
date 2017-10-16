﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player2Controller : MonoBehaviour {

	public AttackBlockController northBlock, southBlock, eastBlock, westBlock;
    public float blocksSpeed;
    public float cooldownTime;

    public Slider cooldownSlider;

    private float cooldownTimeCounter;
	private float jumpLength;


    void Start () {
		if(northBlock == null || southBlock == null || eastBlock == null || westBlock == null)
            throw new NullReferenceException("You must assign all blocks to " +this.GetType().Name);
        
        if(blocksSpeed == 0)
            throw new ArgumentException("blocksSpeed cannot be 0");

        if (cooldownSlider == null)
            throw new NullReferenceException("You must assign a slider to " + this.GetType().Name);

		cooldownTimeCounter = cooldownTime;

		jumpLength = 1;

		if (jumpLength == 0)
			throw new ArgumentException("jumpLength cannot be 0");

        northBlock.MoveSpeed = blocksSpeed;
        southBlock.MoveSpeed = blocksSpeed;
        eastBlock.MoveSpeed = blocksSpeed;
        westBlock.MoveSpeed = blocksSpeed;

        northBlock.JumpLength = jumpLength;
        southBlock.JumpLength = jumpLength;
        eastBlock.JumpLength = jumpLength;
        westBlock.JumpLength = jumpLength;

        northBlock.MoveDirection = Vector3.back;
        southBlock.MoveDirection = Vector3.forward;
        eastBlock.MoveDirection = Vector3.left;
        westBlock.MoveDirection = Vector3.right;

        northBlock.Initialize();
        southBlock.Initialize();
        eastBlock.Initialize();
        westBlock.Initialize();

		cooldownSlider.minValue = 0;
		cooldownSlider.maxValue = cooldownTime;

    }
	
	void Update () {

		bool canFire = cooldownTimeCounter > cooldownTime;

        if(canFire)
        {
            if (Input.GetKeyDown(KeyCode.W)) ActivateBlockAndResetCooldown(northBlock);
            else if (Input.GetKeyDown(KeyCode.S)) ActivateBlockAndResetCooldown(southBlock);
            else if (Input.GetKeyDown(KeyCode.A)) ActivateBlockAndResetCooldown(westBlock);
            else if (Input.GetKeyDown(KeyCode.D)) ActivateBlockAndResetCooldown(eastBlock);
        }
        else
        {
            cooldownTimeCounter += Time.deltaTime;
        }

		cooldownSlider.value = cooldownTimeCounter;

    }

    void ActivateBlockAndResetCooldown(AttackBlockController block)
    {
        block.SetActivationState(true);
        cooldownTimeCounter = 0;
    }

}