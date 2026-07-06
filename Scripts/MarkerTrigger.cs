using UnityEngine;

public class MarkerTrigger : MonoBehaviour
{
    public SupermarketArrival sceneManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Car hit marker!");
            sceneManager.StartArrivalSequence();
            gameObject.SetActive(false);
        }
    }
}