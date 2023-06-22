using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public DialogueSO NpcDialogue;
    [SerializeField] private NavMeshAgent navMeshAgent;

    [Header("Objective Completed")]
    [SerializeField] private float destinationReachDistance = 1.0f;
    [SerializeField] private Transform objectiveCompletedDestination;

    private bool hasDestination = false;
    [SerializeField] private bool activeWhenDestinationReached = true;

    private void Awake()
    {
        navMeshAgent.stoppingDistance = destinationReachDistance;
    }

    private void Update()
    {
        // Check the distance between the player and it's destination position
        if (hasDestination && Vector3.Distance(transform.position, navMeshAgent.destination) < destinationReachDistance)
        {
            navMeshAgent.isStopped = true;

            // Set the active state
            gameObject.SetActive(activeWhenDestinationReached);
        }
    }
    public void OnObjectiveCompleted()
    {
        // Remove the dialogue to prevent any interaction 
        NpcDialogue = null;

        // Move the NPC to it's "finished" position
        GoTo(objectiveCompletedDestination.position);
    }

    /// <summary>
    /// Makes the NPC go to a position
    /// </summary>
    /// <param name="position"></param>
    /// <param name="disableWhenReached">Disable the NPC when arrived</param>
    public void GoTo(Vector3 position)
    {
        navMeshAgent.destination = position;
        hasDestination = true;
    }
}
