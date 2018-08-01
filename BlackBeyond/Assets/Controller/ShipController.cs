﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for all ships. TODO Possibly make abstract
public class ShipController : MonoBehaviour
{
    // The space the user is in. TODO should be model only.
    //public SpaceModel CurrentSpaceModel { get; private set; }

    public GameObject laserPrefab;

    // shipview is the ships gameobject
    protected GameObject shipView;
    protected ShipModel shipModel;
    // is this ship moving
    private bool moving = false;

    //destinations
    private List<SpaceModel> destinations;
    private PirateModel pirateToShoot;
    private PlayerModel playerToShootOnFinish;
    private float distanceMoved = 0f;
    private float speed = 2f;

    Vector2 currentLocation, currentDestination;
    private int destinationIndex;


    public void SetModel(ShipModel shipModel)
    {
        this.shipModel = shipModel;
    }

    // Gives the GameObject
    public GameObject GetShipView()
    {
        return shipView;
    }

    // For the Model link, lets this access the GameObject.
    public void SetShipView(GameObject shipView)
    {
        this.shipView = shipView;
    }

    // Moves the ship to a new location
    // made smoother with the update function
    public void MoveShip(PathfindingNode[] destinations)
    {
        moving = true;
        this.destinations = new List<SpaceModel>();
        foreach(PathfindingNode node in destinations)
        {
            this.destinations.Add(node.GetSpace());
        }

        distanceMoved = 0;
        destinationIndex = 1;

        currentLocation = shipView.transform.position;
        currentDestination = destinations[destinationIndex].GetSpace().GetController().GetPosition();
    }

    public void MoveShip(List<SpaceModel> destinations, PirateModel pirateToShoot, PlayerModel playerToShootOnFinish)
    {
        moving = true;
        this.destinations = destinations;
        this.pirateToShoot = pirateToShoot;
        this.playerToShootOnFinish = playerToShootOnFinish;
        distanceMoved = 0;
        destinationIndex = 0;

        currentLocation = shipView.transform.position;
        if (destinations.Count != 0)
        {
            currentDestination = destinations[destinationIndex].GetController().GetPosition();
        }
        else
        {
            currentDestination = new Vector2(-9999, -9999);
        }
    }

	private void Update()
	{
        if (moving && currentDestination.x > -9999)
        {
            // add some distance
            distanceMoved += speed * Time.deltaTime;

            //move along according to speed;
            shipView.transform.position = Vector2.Lerp(currentLocation, currentDestination, distanceMoved);

            //check if we've reached the goal point
            if (distanceMoved >= 1)
            {
                distanceMoved = 0;
                destinationIndex += 1;
                // check if we're done moving
                if (destinationIndex >= destinations.Count)
                {
                    moving = false;
                    shipModel.FinishedAnimatingMovement();
                    if (playerToShootOnFinish != null)
                    {
                        pirateToShoot.Shoot(playerToShootOnFinish);
                        pirateToShoot = null;
                        playerToShootOnFinish = null;
                    }
                }
                else
                {
                    currentLocation = currentDestination;
                    currentDestination = destinations[destinationIndex].GetController().GetPosition();
                }
            }
        }
        else if(currentDestination.x == -9999)
        {
            if (playerToShootOnFinish != null)
            {
                pirateToShoot.Shoot(playerToShootOnFinish);
                pirateToShoot = null;
                playerToShootOnFinish = null;
            }
        }
	}

    public void CreateLaser(SpaceModel start, SpaceModel end)
    {
        var laser = Object.Instantiate(laserPrefab) as GameObject;
        laser.GetComponent<Laser>().SetLine(start.GetController().GetPosition(), end.GetController().GetPosition());
    }

    public void FlipShip(bool turnRight)
    {
        Vector3 newScale = shipView.transform.localScale;
        if (turnRight)
        {

            newScale.x = 1;

        } 
        else
        {
            newScale.x = -1;
        }
        shipView.transform.localScale = newScale;
    }
}
