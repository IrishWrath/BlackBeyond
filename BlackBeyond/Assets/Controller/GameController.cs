﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the main class of this system. It is the starting point of our code.
public class GameController : MonoBehaviour
{
    public MapController MapController { get; private set; }

    // A model link. TODO This class might be better as a static class
    private ModelLink modelLink;

    // The Prefab for Spaces
    public GameObject spaceView;
    // The Prefab for Player's ship
    public GameObject playership;
    // The Prefab for Pirate ships
    public GameObject pirateship;
    // The Nebula Terrain
    public GameObject nebulaTerrain;

    // Container for spaces
    public GameObject mapGameObject;

    // A reference to the player.
    private PlayerModel playerModel;

    // Use this for initialization. Starting method for our code.
    public void Start()
    {
        this.modelLink = new ModelLink(this, mapGameObject);

        // Creates the map.
        this.MapController = new MapController(125, 250, modelLink);

        // Gets a starting space for the player, based on coordinates. TODO moving away from coordinates, find another method of getting spaces
        SpaceModel playerSpace = MapController.Map.GetSpace(62, 125);

        // Create a player, and set up MVC connections
        this.playerModel = new PlayerModel(playerSpace);
        modelLink.CreatePlayerView(playerModel);
    }

    // Returns the Prefabs
    public GameObject GetSpaceView()
    {
        return spaceView;
    }
    public GameObject GetPlayerView()
    {
        return playership;
    }
    public GameObject GetPirateView()
    {
        return pirateship;
    }
    public GameObject GetNebula()
    {
        return nebulaTerrain;
    }


    // This Update should be avoided. Only place testing code here.
    // Update is called once per frame
    public void Update()
    {

    }
}
