﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Could be any AI. Holds common methods to AIs
public abstract class PirateAiModel
{
    private PirateModel.PirateType pirateType;
    private MapModel map;
    private ModelLink modelLink;
    protected PirateModel pirateModel;

    protected PirateAiModel(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink)
    {
        this.pirateType = pirateType;
        this.map = map;
        this.modelLink = modelLink;
    }

    public abstract void EndTurn();

    //public PlayerModel GetPlayer()
    //{
    //    List <PathfindingNode> fov = Pathfinding.GetFieldOfView(pirateModel.GetSpace(), pirateModel.GetDetectRange(), map);
    //    foreach (PathfindingNode node in fov)
    //    {
    //        if (node.GetSpace().GetPlayer() != null)
    //        {
    //            return node.GetSpace().GetPlayer();
    //        }
    //    }
    //    // Outside for loop, no players found
    //    return null;
    //}

    public PlayerModel GetPlayerChasing()
    {
        List<PathfindingNode> fov = Pathfinding.GetFieldOfView(pirateModel.GetSpace(), pirateModel.GetDetectRange()*3, map);
        foreach (PathfindingNode node in fov)
        {
            if (node.GetSpace().GetPlayer() != null)
            {
                return node.GetSpace().GetPlayer();
            }
        }
        // Outside for loop, no players found
        return null;
    }

    protected MapModel GetMap()
    {
        return map;
    }

    public void SpawnPirate(SpaceModel spawnPoint)
    {
        //If the pirate is dead check type and create a new one
        if (pirateModel == null)
        {
            switch (pirateType)
            {
                case PirateModel.PirateType.Scout:
                    pirateModel = PirateModel.CreateScoutPirate(spawnPoint);
                    break;
                case PirateModel.PirateType.Frigate:
                    pirateModel = PirateModel.CreateFrigatePirate(spawnPoint);
                    break;
                case PirateModel.PirateType.Platform:
                    pirateModel = PirateModel.CreatePlatformPirate(spawnPoint);
                    break;
                case PirateModel.PirateType.Dreadnought:
                    pirateModel = PirateModel.CreateDreadnaughtPirate(spawnPoint);
                    break;
                case PirateModel.PirateType.Destroyer:
                    pirateModel = PirateModel.CreateDestroyerPirate(spawnPoint);
                    break;
            }

            modelLink.CreatePirateView(pirateModel);
        }
    }
}
