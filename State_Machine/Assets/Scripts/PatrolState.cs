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
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        
    }

    public void ToChaseState()
    {
        
    }

    public void ToPatrolState()
    {
        
    }

    void Patrol()
    {
        enemy.indicator.material.color = Color.yellow;
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
