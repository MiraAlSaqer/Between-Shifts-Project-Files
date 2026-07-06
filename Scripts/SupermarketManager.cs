using UnityEngine;

public class SupermarketManager : MonoBehaviour
{
    [Header("Positions")]
    public Transform counterPosition;

    [Header("Player References")]
    public GameObject walkingFather;
    public MonoBehaviour movementScript;
    public AudioSource footstepAudio;
    public StatusEffectsManager tirednessManager;

    [Header("NPC & Interaction")]
    public GameObject groceryItem;
    public GameObject dialogueUI;

    void Start()
    {
        tirednessManager.StartTiredEffect(60f);
    }

    public void StartShift()
    {
        Rigidbody rb = walkingFather.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        footstepAudio.Stop();
        footstepAudio.enabled = false;

        walkingFather.transform.position = counterPosition.position;
        walkingFather.transform.rotation = counterPosition.rotation;
        movementScript.enabled = false;
    }

    public void CustomerArrived()
    {
        groceryItem.SetActive(true);
        dialogueUI.SetActive(true);
    }
}