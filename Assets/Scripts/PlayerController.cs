using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Kinetix.UI;

/// <summary>
/// Simple Point & click controller
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private DemoManager demoManager;

    [SerializeField] private float interactDistance = 1.0f;
    [SerializeField] private LayerMask clickLayers;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private NpcController target;

    private void Awake()
    {
        navMeshAgent.stoppingDistance = interactDistance;
    }

    private void Update()
    {
        // Check the distance between the player and it's destination position
        if (Vector3.Distance(transform.position, navMeshAgent.destination) < interactDistance)
        {
            navMeshAgent.isStopped = true;

            // If the player clicked on a npc and it has a dialogue, interact with it
            if (target is NpcController && target.NpcDialogue)
            {
                demoManager.DialogueBox.ShowDialogue(target.NpcDialogue);
                target = null;
            }
        }

        // Move the character on mouse left click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Prevent the player to move or interact when the emote wheel or dialogue box is shown
            if (KinetixUI.IsShown || demoManager.DialogueBox.IsShown)
            {
                return;
            }

            // Get mouse position using the new input system
            float mouseX = Mouse.current.position.x.ReadValue();
            float mouseY = Mouse.current.position.y.ReadValue();

            // Get ray from mouse position
            Ray mouseRay = gameCamera.ScreenPointToRay(new Vector3(mouseX, mouseY, 0));

            // Check if the raycast hit any surface
            if (Physics.Raycast(mouseRay, out RaycastHit hit, 100, clickLayers))
            {
                // Set the npc target if the player clicked on one
                target = hit.collider.GetComponent<NpcController>();

                navMeshAgent.isStopped = false;
                navMeshAgent.destination = hit.point;
            }
        }
    }
}