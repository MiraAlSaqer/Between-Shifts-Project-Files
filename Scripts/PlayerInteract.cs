using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public float interactRange = 3f;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("E Key Pressed!");

            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                Debug.Log("Found: " + interactable.GetTransform().name);
                interactable.Interact(transform);
            }
            else
            {
                Debug.Log("Nothing to interact with");
            }
        }
    }

    public IInteractable GetInteractableObject()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            if (hit.collider.CompareTag("Door") && hit.collider.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }

        return null;
    }
}