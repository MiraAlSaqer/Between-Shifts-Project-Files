using UnityEngine;
public interface IInteractable
{
    void Interact(Transform interactorTransform);
    Transform GetTransform();
}