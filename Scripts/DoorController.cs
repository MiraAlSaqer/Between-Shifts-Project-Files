using UnityEngine;

public class DoorController : MonoBehaviour, IInteractable
{
    public bool isOpen = false;

    [Header("Audio")]
    public AudioSource doorSoundSource;

    private Vector3 posClosed = new Vector3(-310.2f, -246.8f, 1.4f);
    private Vector3 rotClosed = new Vector3(-0.117f, 0.281f, 84.604f);

    private Vector3 posOpen = new Vector3(-311.9f, -277.4f, 0f);
    private Vector3 rotOpen = new Vector3(0f, 0f, 90.308f);

    public void Interact(Transform interactorTransform)
    {
        isOpen = !isOpen;
        doorSoundSource.Play();

        if (isOpen)
        {
            transform.localPosition = posOpen;
            transform.localRotation = Quaternion.Euler(rotOpen);
        }
        else
        {
            transform.localPosition = posClosed;
            transform.localRotation = Quaternion.Euler(rotClosed);
        }

        Debug.Log("Door moved and sound triggered!");
    }

    public Transform GetTransform() => transform;
}