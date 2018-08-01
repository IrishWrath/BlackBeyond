using UnityEngine;
using System.Collections;

public class PlatformAi : PirateAiModel
{
    private SpaceModel location;

    public PlatformAi(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, SpaceModel location) : base(pirateType, map, modelLink)
    {
        this.location = location;
        base.SpawnPirate(location);
    }

    public override void EndTurn(int turnNumber)
    {
        // only spawns pirate if it is dead
        if (turnNumber % 10 == 0)
        {
            base.SpawnPirate(location);
        }

        PlayerModel player = base.GetPlayerChasing();
        if (player != null)
        {
            // gets the model from the superclass, and calls its shoot method.
            // Shoot is a ShipModel method
            base.pirateModel.Shoot(player);
        }

    }
 
  
}
