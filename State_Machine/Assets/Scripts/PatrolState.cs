using UnityEngine;

public class PatrolState : IEnemyState
{
    //We declare a variable called enemy, it's type is statepatter enemy, it is a class
    private StatePatternEnemy enemy;

    int nextWayPoint; //index of the waypoint in the array

    // when we create patrolstate, object in statepattern enemy, function below is invoked automatically
    //this is the constructor. tis functio get the whole statepatternenemy class as paratemer and all the features
    //basically we pass all enemy featured to this script (object)
    // the paremeter variable neame, is statpatterenemy ad we assign i to loocal vatriable "enemy"
    //this means that in the future we get access to enemy's properties by weriting enemy.something
    // for example enemy.searchDuratioon so we get the value of that.

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState()
    {
        Patrol();
        Look();
    }

    public void OnTriggerEnter(Collider other)
    {
        //we check what is trigger us. if it's player , alert state
        if(other.CompareTag("Player"))
        {
            ToAlertState();
        }
    }

    public void ToAlertState()
    {
        enemy.curreState = enemy.alertState;
    }

    public void ToChaseState()
    {
        enemy.curreState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        //we cannot use this becasue we're already in patrol state
    }

    void Look()
    {
        //Dbug ray to visual the ey sight
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.green);
        //raycast
        RaycastHit hit; //hit variable gets all the info where the ray hits. we call it hit, something to get info.
        if(Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            //We go here only if the ray hits the player
            //if ray hits player, the enemy sees it and goes nstantly to chase state.
            enemy.chaseTarget = hit.transform;
            ToChaseState(); //we want our state to chase state.
        }
    }
    void Patrol()
    {
        enemy.indicator.material.color = Color.green;
        enemy.navMeshAgent.destination = enemy.waypoints[nextWayPoint].position;
        enemy.navMeshAgent.isStopped = false;

        //when we get to the currect waypoint, swittch to the nex one
        //we want to check are we at end location
        if(enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            // enemy definitely is at goal position
            
            nextWayPoint = (nextWayPoint + 1) % enemy.waypoints.Length;//this loops the waypoints
        }
    }

}
