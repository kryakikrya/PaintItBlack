using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    protected bool _canInteract;

    public bool GetCanInteract() => _canInteract;
    public virtual void Interact(GameObject player) { }
}
