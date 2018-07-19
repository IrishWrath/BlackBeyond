﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel 
{
    private PlayerController playerController;
    private SpaceModel playerSpaceModel;

    public SpaceModel playerLocation;

    public int maxPlayerMovement = 3;
    public int currentPlayerMovement = 3;
    public static int playerHealth = 10;
    public static int playerArmor = 1;

    public static int GetHealth()
    {
        return playerHealth;
    }
    public static void UpdatePlayerHealth(int health)
    {
        playerHealth = health;
    }

    public static int GetArmor()
    {
        return playerArmor;
    }

    public PlayerModel(SpaceModel playerSpace)
    {
        this.playerSpaceModel = playerSpace;
        this.playerLocation = playerSpace;
        Debug.Log("Moves Available: " + this.currentPlayerMovement.ToString());
    }

    public PlayerController GetController()
    {
        return playerController;
    }

    public void SetController(PlayerController controller)
    {
        this.playerController = controller;
    }

    public SpaceModel GetSpace()
    {
        return playerSpaceModel;
    }

    public int GetCurrentPlayerMovement()
    {
        return currentPlayerMovement;
    }

    public void UpdateCurrentPlayerMovement(int movementUsed)
    {
        this.currentPlayerMovement = currentPlayerMovement - movementUsed;
    }

    public void UpdatePlayerLocation(SpaceModel location)
    {
        this.playerLocation = location;
    }
}
