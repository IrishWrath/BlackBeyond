﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is dedicated to creating links between the View, Controller and Model
// TODO might be better as a static class with static methods
public class ModelLink
{

    public GameController GameController { get; private set; }
    public Transform MapContainer { get; private set; }

    private StationModel stationModel;

    public ModelLink(GameController gameController, GameObject mapGameObject, StationModel stationModel)
    {
        this.GameController = gameController;
        this.MapContainer = mapGameObject.transform;
        this.stationModel = stationModel;
    }

    // Creates the view and gets the controller for a Space
    public void CreateSpaceView(SpaceModel spaceModel)
    {
        // Creates the space GameObject in the correct position. Formula works for hexes
        GameObject spaceView = UnityEngine.Object.Instantiate(GameController.GetSpaceView(), 
                                                  new Vector2((float)spaceModel.Column * 0.6f, (0 - spaceModel.Row * 1.04f)), Quaternion.identity, MapContainer);
        // Gets the controller from the GameObject.
        SpaceController spaceController = spaceView.GetComponent<SpaceController>();
        // Lets the Controller access the GameObject
        spaceController.SetSpaceView(spaceView);
        // Lets the Controller access the Model
        spaceController.SetSpace(spaceModel);
        // Lets the Model access the Controller, as a callback
        spaceModel.SetController(spaceController);
    }

    // Creates a Nebula space.
    public void CreateNebulaSpace(NebulaSpaceModel nebulaSpaceModel)
    {
        CreateSpaceView(nebulaSpaceModel);
        UnityEngine.Object.Instantiate(GameController.GetNebula(),
                           nebulaSpaceModel.GetController().GetPosition(), Quaternion.identity, MapContainer);
        nebulaSpaceModel.GetController().SetNebula();
    }

    // Creates an asteriod space.
    public void CreateAsteroidSpace(AsteroidSpaceModel asteroidSpaceModel)
    {
        CreateSpaceView(asteroidSpaceModel);
        UnityEngine.Object.Instantiate(GameController.GetAsteroid(),
                           asteroidSpaceModel.GetController().GetPosition(), Quaternion.identity, MapContainer);
        asteroidSpaceModel.GetController().SetAsteroid();
    }

    // Same as above for a Space GameObject
    public void CreatePlayerView(PlayerModel playerModel, Text movementText, GameObject currency, GameObject metal, 
                                 GameObject organics, GameObject gas, GameObject water, GameObject fuel, GameObject fuelMax, 
                                 GameObject totalSpace)
    {
        // Creates the player GameObject in the correct position.
        GameObject playerView = UnityEngine.Object.Instantiate(GameController.GetPlayerView(),
                                                   playerModel.GetSpace().GetController().GetPosition(), Quaternion.identity);
        // Sets the camera following the player
        Camera.main.transform.parent = playerView.transform;
        Camera.main.transform.localPosition = new Vector3(0, 0, -10);

        // Gets the controller from the GameObject.
        PlayerController playerController = playerView.GetComponentInChildren<PlayerController>();
        // Lets the Controller access the GameObject
        playerController.SetShipView(playerView);
        // Lets the Controller access the Model
        playerController.SetModel(playerModel, GameController.soundController);
        // Lets the Model access the Controller, as a callback
        playerModel.SetController(playerController);

        playerController.SetMovementTextInterface(movementText);
        playerController.currency = currency;
        playerController.metal = metal;
        playerController.gas = gas;
        playerController.water = water;
        playerController.fuel = fuel;
        playerController.fuelMax = fuelMax;
        playerController.totalSpace = totalSpace;
    }


    // Same as above for a Space GameObject
    public void CreatePirateView(PirateModel pirateModel)
    {
        // Creates the player GameObject in the correct position.
        GameObject pirateView = UnityEngine.Object.Instantiate(GameController.GetPirateView(),
                                                   pirateModel.GetSpace().GetController().GetPosition(), Quaternion.identity, MapContainer);

        // Gets the controller from the GameObject.
        PirateController pirateController = pirateView.GetComponentInChildren<PirateController>();
        // Lets the Controller access the GameObject
        pirateController.SetShipView(pirateView);
        // Lets the Controller access the Model
        pirateController.SetModel(pirateModel, GameController.soundController);
        // Lets the Model access the Controller, as a callback
        pirateModel.SetController(pirateController);
    }

    // Same as above for a Space GameObject
    public void CreateStationView(Station station)
    {
        // Creates the player GameObject in the correct position. TODO update this formula for hexes
        GameObject stationView = UnityEngine.Object.Instantiate(GameController.GetStationView(),
                                                   station.GetSpace().GetController().GetPosition(), Quaternion.identity, MapContainer);

        // Gets the controller from the GameObject.
        StationController stationController = stationView.GetComponent<StationController>();
        // Lets the Controller access the GameObject
        stationController.SetStationView(stationView);
        // Lets the Controller access the Model
        stationController.SetModel(station);
        // Lets the Model access the Controller, as a callback
        station.SetController(stationController);
        // Pass the dockui from gamecontroller to the stationcontroller
        stationController.SetDockUI(GameController.GetDockUI());

        //stationModel.createStation(stationLocation, stationType);
        stationController.SetStation(station.GetStationType());
    }
}
