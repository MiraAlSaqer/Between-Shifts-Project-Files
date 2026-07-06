using UnityEngine;
using System.Collections;

public class LeahInteract : MonoBehaviour, IInteractable
{
    public GameObject dialogueBox;

    public void Interact(Transform interactorTransform)
    {
        if (!dialogueBox.activeSelf)
        {
            dialogueBox.SetActive(true);
            StartCoroutine(HandleInteractionFreeze());
        }
    }

    IEnumerator HandleInteractionFreeze()
    {
        PlayerMovement movement = Object.FindFirstObjectByType<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        while (dialogueBox.activeSelf)
        {
            yield return null;
        }

        if (movement != null) movement.enabled = true;
        Debug.Log("Finished talking to Leah. Father can move again.");
    }

    public Transform GetTransform() => transform;
}