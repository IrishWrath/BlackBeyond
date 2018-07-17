﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateModel : MonoBehaviour {
        private PirateController pirateController;
        private SpaceModel pirateSpaceModel;
       
    //ship combat stat variables
       
        private static int shipHealth;
        private int shotDamage;
        private int detectRange;
        private int attackRange;
        public int maxPirateMovement;
        public int currentPirateMovement;
        
    //pirate ship builder template
    public PirateModel(int health, int shotDamage, int detectRange, int attackRange , int maxPirateMovement, int currentPirateMovement)
    {
        shipHealth = health;
        this.shotDamage = shotDamage;
        this.detectRange = detectRange;
        this.attackRange = attackRange;
        this.maxPirateMovement = maxPirateMovement;
        this.currentPirateMovement = currentPirateMovement;
    }

    public static PirateModel CreateScoutPirate()
    {
        return new PirateModel(2, 1, 3, 1, 4, 4);
    }

    public static PirateModel CreateFrigatePirate()
    {
        return new PirateModel(4, 2, 3, 2, 3, 3);
    }

    public static PirateModel CreatePlatformPirate()
    {
        return new PirateModel(4, 1, 4, 4, 0, 0);
    }

    public static PirateModel CreateDestroyerPirate()
    {
        return new PirateModel(7, 3, 3, 3, 2, 2);
    }

    public static PirateModel CreateDreadnaughtPirate()
    {
        return new PirateModel(10, 4, 2, 3, 2, 2);
    }

    public int GetDamage()
    {
        return shotDamage;
    }

    public static int GetHealth()
    {
        return shipHealth;
    }

    public PirateModel(SpaceModel pirateSpace)
        {
            this.pirateSpaceModel = pirateSpace;
        }
        

        public PirateController GetController()
        {
            return pirateController;
        }

        public void SetController(PirateController controller)
        {
            this.pirateController = controller;
        }

        public SpaceModel GetSpace()
        {
            return pirateSpaceModel;
        }

        public int GetCurrentMovement()
        {
            return currentPirateMovement;
        }

        public void UpdateCurrentMovement(int movementUsed)
        {
            currentPirateMovement = currentPirateMovement - movementUsed;
        }
    }
