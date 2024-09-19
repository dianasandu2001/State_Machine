using UnityEngine;

public class ChaseState : IEnemyState
{
    private StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState()
    {
        Chase();
        Look();
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        enemy.curreState = enemy.alertState;
    }

    public void ToChaseState()
    {
        //we cannot use because we're already in chase state
    }

    public void ToPatrolState()
    {
        enemy.curreState = enemy.patrolState;
    }

    public void ToTrackingState()
    {
        enemy.curreState = enemy.trackingState;
    }
    void Look()
    {
        //need a vector from enemy eye to player
        //this is thhe direction we give to raycast
        Vector3 enemyToTarget = enemy.chaseTarget.position - enemy.eye.position;
        
        //Dbug ray to visual the ey sight
        Debug.DrawRay(enemy.eye.position, enemyToTarget, Color.red);
        //raycast
        RaycastHit hit; //hit variable gets all the info where the ray hits. we call it hit, something to get info.
        if (Physics.Raycast(enemy.eye.position, enemyToTarget, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            //We go here only if the ray hits the player
            //if ray hits player, the enemy sees it and goes nstantly to chase state.
            enemy.chaseTarget = hit.transform;// enemy makes sure that chase target is player
        }
        else
        {
            //if player goes around corner, enemy does not see player
            enemy.lastKnownPlayerPosition = enemy.chaseTarget.position;
            ToTrackingState();
        }
    }
    void Chase()
    {
        enemy.indicator.material.color = Color.red;
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;
    }
}
