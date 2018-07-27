using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolAI : PirateAiModel
{
    // Oisín Notes: Don't need a start space, we can get one from the patrol.
    // SpaceModel startSpace = ShipModel.GetSpace();

    // Oisín Notes: We need a patrol route for this AI, We'll try and get that done before adding pursuit logic
    // Once the path is set up, it never changes, so we'll make it in the constructor
    private List<SpaceModel> patrolPath;
    private int currentSpaceOnPath;

    public bool engaged;
    protected PatrolAI(PirateModel.PirateType pirateType, MapModel map, ModelLink modelLink, List<SpaceModel> patrolPoints) : base(pirateType, map, modelLink)
    {
        // Oisín Notes: This constructor takes in a list of patrol points, which we can use to set up the patrol

        patrolPath = new List<SpaceModel>();
        // Oisín Notes: Should be using a for loop, but for now assume that the patrol points have three points.
        // path between point 0 and 1
        patrolPath.AddRange(AStarPathfinding.GetPathToDestination(patrolPoints[0], patrolPoints[1]));
        // path between point 1 and 2
        patrolPath.AddRange(AStarPathfinding.GetPathToDestination(patrolPoints[1], patrolPoints[2]));
        // path between point 2 and 0
        patrolPath.AddRange(AStarPathfinding.GetPathToDestination(patrolPoints[2], patrolPoints[0]));

        // patrolPath should now be one continuous line of spaces. If AStar has bugs, it might break around the edges
        currentSpaceOnPath = 0;


        ////**Oisin is this on the right track?**
        //SpaceModel[] position = new SpaceModel[2];
        //position[0] = new SpaceModel(startSpace); //or (ShipModel.GetSpace());
        //position[1] = new SpaceModel(Random.Range(14, 20), Random.Range(14, 20),map);
        //position[2] = new SpaceModel(Random.Range(19, 25), Random.Range(19, 25),map);

        // Create Patrol route from patrol points
        // PP 1 -> PP 2 -> PP 3... combined into one list. When it reaches the end, it starts again.
    }

    public override void EndTurn()
    {
        if (engaged == true)
        {
            // Pursue
            //**trying to set target to players current location, not sure what its not liking.**
            SpaceModel target = base.pirateModel.GetSpace(); // Oisín Notes: also get player in range of the pirate's space
        }
        else
        {
            // Patrol
            // Oisín notes: I would do a for loop (i = 0, i > base.pirateModel.GetMaxMovement(), i++)
            // inside, check for a player, then move base.pirateModel to the next space on the path with currentSpaceOnPath
            // also check if we're at the end of the path (currentSpaceOnPath == patrolPath.Count) 
            // or (currentSpaceOnPath == patrolPath.Count - 1), depending on how you do it.

            // We don't need the engaged functionality for now, it might be easier to build and perfect 
            // it in the Hunter Killer AI, then copy it in here.

        }
    }



// Moved from PirateController
    //public void Engagement()
    //{
    //    while (pirateModel.GetCurrentMovement() != 0)
    //    {
    //        if (engaged == true)
    //        {
    //            //pirateShip scans spaces inside detection range
    //            //is target within detection range, if yes
    //            //is target within attackRange, attack, reduce movment counter by 1

           // this has been replaced with a shoot function
    //            int armor = PlayerModel.GetArmor();
    //            int currentHealth = PlayerModel.GetHealth();
    //            int shotDamage = pirateModel.GetDamage();
    //            int adjDamage = armor - shotDamage;
    //            if (adjDamage <=0)
    //            {
    //                adjDamage = 0;
    //            }
    //            int remainingHP = currentHealth - adjDamage;
    //            PlayerModel.UpdatePlayerHealth(remainingHP);
    //        }
    //        else
    //        {
    //            engaged = false;
    //            //follow patrol route for 1 movement
    //            //pirateShip scans spaces inside detection range
    //            //if target found engaged = true

    //        }
    //    }


    //}
}
