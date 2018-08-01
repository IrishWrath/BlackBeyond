﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : ShipModel
{
    // Player trade info
    public int playerCurrency = 0;
    public int metalResource = 0;
    public int organicResource = 0;
    public int fuelResource = 0;
    public int gasResource = 0;
    public int waterResource = 0;

    List<PathfindingNode> validMovementSpaces;
    List<PathfindingNode> validShootingSpaces;

    private PlayerController playerController;

    public bool playerCanMove = false;
    private MapModel mapModel;

    public PlayerModel(SpaceModel currentSpace, MapModel mapModel)
    {
        base.currentSpace = currentSpace;

        // Player stats, TODO
        base.shipArmor = 1;
        base.currentMovement = 3;
        base.maxMovement = 3;
        base.attackRange = 2;
        base.shotDamage = 2;
        base.shotCounter = 1;
        base.currentShotCounter = 1;
        base.shipHealth = 10;
        base.maxCargoSpace = 50;
        this.mapModel = mapModel;
        UpdatePlayerLocation(currentSpace);
    }

    public PlayerController GetController()
    {
        return playerController;
    }

    public void SetController(PlayerController controller)
    {
        this.playerController = controller;
        base.SetController(controller);
    }

    public int GetCurrentPlayerMovement()
    {
        return base.currentMovement;
    }

    public void UpdateCurrentPlayerMovement(int movementUsed)
    {
        base.currentMovement = base.currentMovement - movementUsed;
    }

    public void UpdatePlayerLocation(SpaceModel location)
    {
        base.currentSpace.LeaveSpace();
        base.currentSpace = location;
        location.OccupySpace(this);
    }

    //public SpaceModel GetCurrentSpace()
    //{
    //    SpaceModel currentSpace = new SpaceModel (base.GetSpace());
    //    return currentSpace;
    //}

    public bool GetPlayerCanMove()
    {
        return playerCanMove;
    }

    public void SetPlayerCanMove(bool canPlayerMove)
    {
        this.playerCanMove = canPlayerMove;
    }

    // For turn structure
    public void EndTurn()
    {
        ResetShotCounter();
        // Should block player actions until their turn TODO
        if (validMovementSpaces != null)
        {
            if (validMovementSpaces.Count > 0)
            {
                foreach (PathfindingNode node in validMovementSpaces)
                {
                    node.GetSpace().ClearHighlighted();
                }
                validMovementSpaces.Clear();
            }
        }
        if (validShootingSpaces != null)
        {
            if (validShootingSpaces.Count > 0)
            {
                foreach (PathfindingNode node in validShootingSpaces)
                {
                    node.GetSpace().ClearHighlighted();
                }

                validShootingSpaces.Clear();
            }
        }
    }

    public void StartTurn()
    {
        // Unblock player actions TODO

        // reset this player
        base.currentMovement = base.maxMovement;
        playerController.SetCurrentMovement(base.currentMovement, base.maxMovement);
    }

    public void StartShoot()
    {
        if (validMovementSpaces != null)
        {
            if (validMovementSpaces.Count > 0)
            {
                foreach (PathfindingNode node in validMovementSpaces)
                {
                    node.GetSpace().ClearHighlighted();
                }
                validMovementSpaces.Clear();
            }
        }
        if (validShootingSpaces == null || validShootingSpaces.Count == 0)
        {
            // Get all spaces that are valid moves and return into list
            validShootingSpaces = Pathfinding.GetFieldOfView(base.currentSpace, base.attackRange, mapModel);

            SetPlayerCanMove(true);

            foreach (PathfindingNode node in validShootingSpaces)
            {
                node.GetSpace().SetShootHighlighted(node, this);
            }
            base.currentSpace.ClearHighlighted();
        }
        else
        {
            foreach (PathfindingNode node in validShootingSpaces)
            {
                node.GetSpace().ClearHighlighted();
            }
            validShootingSpaces.Clear();
        }
    }

    public void StartMove()
    {
        if (validShootingSpaces != null)
        {
            if (validShootingSpaces.Count > 0)
            {
                foreach (PathfindingNode node in validShootingSpaces)
                {
                    node.GetSpace().ClearHighlighted();
                }

                validShootingSpaces.Clear();
            }
        }
        if (!base.animatingMovement)
        {
            if (validMovementSpaces == null || validMovementSpaces.Count == 0)
            {
                // Get all spaces that are valid moves and return into list
                validMovementSpaces = Pathfinding.GetSpacesForMovementDijkstras(base.currentSpace, base.currentMovement);

                SetPlayerCanMove(true);

                foreach (PathfindingNode node in validMovementSpaces)
                {
                    node.GetSpace().SetHighlighted(node, this);
                }
            }
            else
            {
                foreach (PathfindingNode node in validMovementSpaces)
                {
                    node.GetSpace().ClearHighlighted();
                }
                validMovementSpaces.Clear();
            }
        }
    }

    public void FinishShoot(ShipModel occupyingShip)
    {
        base.Shoot(occupyingShip);
        foreach (PathfindingNode node in validShootingSpaces)
        {
            node.GetSpace().ClearHighlighted();
        }

        validShootingSpaces.Clear();
    }

    public void FinishMove(PathfindingNode destination)
    {
        if ((base.currentMovement - destination.GetCost()) >= 0 && playerCanMove == true)
        {
            UpdatePlayerLocation(destination.GetSpace());
            UpdateCurrentPlayerMovement(destination.GetCost());

            this.GetController().MoveShip(destination.GetPath(true).ToArray());
            playerController.SetCurrentMovement(base.currentMovement, base.maxMovement);


            SetPlayerCanMove(false);
            animatingMovement = true;


            foreach (PathfindingNode node in validMovementSpaces)
            {
                node.GetSpace().ClearHighlighted();
            }

            validMovementSpaces.Clear();
        }
    }
}
