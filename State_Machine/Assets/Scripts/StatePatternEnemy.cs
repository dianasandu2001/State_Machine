using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchDuration; //How long we search in Alert State
    public float searchTurnSpeed; // How fast we turin in alert state
    public float sightRange; // how far enemy see this is distance of raycast
    public Transform[] waypoints; //array of waypoints . There caan be any nuber of waypoints
    public Transform eye; // this is the eye. We will send raycast from here
    public MeshRenderer indicator; // thus is the box above enemy it changes colour based on state

    [HideInInspector] public Transform chaseTarget; //thi is what we chase in chase state, usually player
    [HideInInspector] public IEnemyState curreState;
    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public ChaseState chaseState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        patrolState = new PatrolState(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //lets tell enemy that in the beginning its current state is patrol state
        curreState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        curreState.UpdateState(); //we are running the update state in the object that is currect state
        // if the currectstate = patrol state, we run update state fuction in parolstate.cs file
    }
}
