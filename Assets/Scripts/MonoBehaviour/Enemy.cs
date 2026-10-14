using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float detectRadius;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private NavMeshAgent agent;


    private void Update()
    {
        //Detteccion al jugador
        if (Physics.CheckSphere(transform.position, detectRadius, playerMask))
        {
            agent.SetDestination(FindAnyObjectByType<PlayerController>().transform.position);
        }
        //Ataque al jugador
        if (Physics.CheckSphere(transform.position, attackRadius, playerMask))
        {
            Vector3 direction = FindAnyObjectByType<PlayerController>().transform.position - transform.position ;
            Ray attackRay = new Ray(transform.position, direction);

            Debug.DrawRay(transform.position, direction * attackRadius, Color.red);
            if(Physics.Raycast(attackRay,attackRadius, playerMask))
            {
                Debug.Log("Le pego al jugador");
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
