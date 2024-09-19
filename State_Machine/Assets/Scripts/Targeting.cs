using System.Transactions;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public GameObject host;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            host.GetComponent<TurretEnemy>().Shoot();
        }
    }
}
