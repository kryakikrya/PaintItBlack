using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private List<Tag> _tags = new();
    public IReadOnlyList<Tag> Tags => _tags;

    protected bool _canInteract;


    public bool GetCanInteract() => _canInteract;
    public virtual void Interact(GameObject player) { }
}
