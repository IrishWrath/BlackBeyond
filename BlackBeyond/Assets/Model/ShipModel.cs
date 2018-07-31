﻿// This is a class for ships. It could be a player or a pirate ship
public class ShipModel
{
    // Is this ship animating movement?
    protected bool animatingMovement;

    //ship combat stat variables. Both Player and pirates use these. protected so that subclasses can use
    protected int shipHealth;
    protected int shotDamage;
    protected int attackRange;
    protected int maxMovement;
    protected int currentMovement;
    protected int shotCounter;
    // TODO, sort out how combat works
    protected int shipArmor = 1;

    // This ship's space
    protected SpaceModel currentSpace;
    private ShipController shipController;

    public int GetDamage()
    {
        return shotDamage;
    }
    public int GetHealth()
    {
        return shipHealth;
    }
    public void SetHealth(int health)
    {
        shipHealth = health;
    }
    public int GetArmor()
    {
        return shipArmor;
    }

    public void Shoot(ShipModel enemy)
    {
        if (shotCounter > 0)
        {
            int armor = enemy.GetArmor();
            int currentHealth = enemy.GetHealth();
            int adjDamage = armor - shotDamage;
            if (adjDamage <= 0)
            {
                // Always does at least one damage?
                adjDamage = 1;
            }
            int remainingHP = currentHealth - adjDamage;
            enemy.SetHealth(remainingHP);
            shotCounter -= 1;
            // Creates a laser. Finn's animation
            shipController.CreateLaser(currentSpace, enemy.GetSpace());
        }
    }

    public void FinishedAnimatingMovement()
    {
        animatingMovement = false;
    }

    public int GetCurrentMovement()
    {
        return currentMovement;
    }

    public void UpdateCurrentMovement(int movementUsed)
    {
        currentMovement = currentMovement - movementUsed;
    }

    public SpaceModel GetSpace()
    {
        return currentSpace;
    }

    public void SetController(ShipController controller)
    {
        this.shipController = controller;
    }
}


