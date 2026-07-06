using UnityEngine;

public class CashierTrigger : MonoBehaviour
{
    public SupermarketManager manager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.StartShift();
            gameObject.SetActive(false);
        }
    }
}