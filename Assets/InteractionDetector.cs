using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    public GameObject interactionIcon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (interactableInRange != null) Debug.Log(interactableInRange);
            interactableInRange?.Interact();
            interactableInRange.ShowInteractionIcon(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable.CanInteract())
        {
            Debug.Log(interactable.CanInteract());
            interactableInRange = interactable;
            interactableInRange.ShowInteractionIcon(true);
            // if interactable in range is NPC, set icon about NPC head
            // otherwise set icon about player head
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange.ShowInteractionIcon(false);
            interactableInRange = null;
        }
    }
}
