using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    protected abstract void Activate();
    protected abstract void Deactivate();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Deactivate();
        }
    }
}