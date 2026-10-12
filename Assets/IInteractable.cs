using System;
using UnityEngine;

public interface IInteractable
{
    void Interact();
    bool CanInteract();
    void ShowInteractionIcon(bool showIcon);
}
