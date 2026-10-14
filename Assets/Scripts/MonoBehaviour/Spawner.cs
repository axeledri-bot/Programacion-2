using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Coroutine spawningCoroutine;

    [Header("Spawning")]
    [SerializeField] private GameObject enemy;  
    [SerializeField] private Transform[] positions;  
    [SerializeField] private int amount;  
    [SerializeField] private float timeBetweenSpawns;  

    private void Awake()
    {
        StartSpawning();
    }
    public void StartSpawning()
    {
        if(spawningCoroutine != null) { return; }

        spawningCoroutine = StartCoroutine(Spawning());
    }
    private IEnumerator Spawning()
    {
        for (int i = 0; i < amount; i++)
        {
            int spawningLocation = Random.Range(0, positions.Length - 1);
            
            Instantiate(enemy, positions[spawningLocation].position, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        yield return null;
    }
}
