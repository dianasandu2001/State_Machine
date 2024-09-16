using System.Xml.Serialization;
using UnityEngine;

public class AlertState : IEnemyState
{
    private StatePatternEnemy enemy;
    float searchTimer;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }
    public void UpdateState()
    {
        Search();
        Look();
    }
    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void ToAlertState()
    {
        //we cannot use because we're already on alert state
    }

    public void ToChaseState()
    {
        searchTimer = 0;
        enemy.curreState = enemy.chaseState;
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.curreState = enemy.patrolState;
    }
    void Look()
    {
        //Dbug ray to visual the ey sight
        Debug.DrawRay(enemy.eye.position, enemy.eye.forward * enemy.sightRange, Color.yellow);
        //raycast
        RaycastHit hit; //hit variable gets all the info where the ray hits. we call it hit, something to get info.
        if (Physics.Raycast(enemy.eye.position, enemy.eye.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            //We go here only if the ray hits the player
            //if ray hits player, the enemy sees it and goes nstantly to chase state.
            enemy.chaseTarget = hit.transform;
            ToChaseState(); //we want our state to chase state.
        }
    }
    void Search()
    {
        enemy.indicator.material.color = Color.yellow;

        enemy.navMeshAgent.isStopped = true; // stop the enemy

        //rotate the enemy
        enemy.transform.Rotate(0, enemy.searchTurnSpeed * Time.deltaTime, 0);
        searchTimer += Time.deltaTime;
        if(searchTimer > enemy.searchDuration)
        {
            //Enemy has seached enough. it goes back to patrol state
            ToPatrolState();
        }
    }
}
