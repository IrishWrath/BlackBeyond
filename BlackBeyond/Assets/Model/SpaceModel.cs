﻿using System;
using UnityEngine;

// Model class for a Space
public class SpaceModel 
{
    public SpaceModel(int row, int column, MapModel map)
    {
        Row = row;
        Column = column;
        this.map = map;
        occupyingShip = null;
    }

    // don't use player, use occupyingShip
    private PlayerModel player;
    private ShipModel occupyingShip;


    public int Row { get; private set; }
    public int Column { get; private set; }

    private readonly MapModel map;
    private SpaceModel[] adjacentSpaces;

    private SpaceController controller;

    // This should be null most of the time. Avoid, outside of pathfinding, these will be null
    public PathfindingNode node;
    // For movement method
    private PathfindingNode moveFunctionNode;

    public SpaceController GetController()
    {
        return controller;
    }

    public void SetController(SpaceController controller)
    {
        this.controller = controller;
    }

    public void OccupySpace(ShipModel ship)
    {
        occupyingShip = ship;
    }
    public void LeaveSpace()
    {
        occupyingShip = null;
    }

    // Asteroids damage the ship that moves through them
    public virtual void GetMovementEffects(ShipModel shipModel)
    {
        // Do nothing, not an asteroid. In a asteroid subclass, the ship will be dealt damage.
    }

    // For Pathfinding

    public void SetNode(PathfindingNode node)
    {
        this.node = node;
    }

    public PathfindingNode GetNode()
    {
        return node;
    }

    public void SetAdjacentSpaces()
    {
        adjacentSpaces = new SpaceModel[6];
        adjacentSpaces[0] = map.GetNE(this);
        adjacentSpaces[1] = map.GetE(this);
        adjacentSpaces[2] = map.GetSE(this);
        adjacentSpaces[3] = map.GetSW(this);
        adjacentSpaces[4] = map.GetW(this);
        adjacentSpaces[5] = map.GetNW(this);
    }

    public SpaceModel[] GetAdjacentSpaces()
    {
        return adjacentSpaces;
    }

    public virtual int GetMovementCost()
    {
        if(occupyingShip == null)
        {
            return 1;
        }
        else
        {
            return 100;
        }
    }

    // Pathfinding End

    public void SetHighlighted(PathfindingNode node, PlayerModel player)
    {
        if (occupyingShip == null)
        {
            this.player = player;
            this.GetController().SetSelectable(node.GetCost());
            this.moveFunctionNode = node;
        }
    }


    public void ClearHighlighted()
    {
        this.GetController().Deselect();
        player = null;
        moveFunctionNode = null;
    }

    public void Clicked()
    {
        if (player != null)
        {
            player.FinishMove(moveFunctionNode);
        }
    }

    public virtual bool BlocksLOS()
    {
        return false;
    }

    public PlayerModel GetPlayer()
    {
        if (occupyingShip != null)
        {
            if (occupyingShip.GetType() == typeof(PlayerModel))
            {
                return (PlayerModel)occupyingShip;
            }
        }
        return null;
    }
}
