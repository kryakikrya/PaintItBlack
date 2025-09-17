using Mirror;
using UnityEngine;

public class PlayerInteractor : NetworkBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _interactDistance = 3f;
    [SerializeField] private KeyCode _interactKey = KeyCode.E;

    private InteractableObject _currentTarget;
    private void Update()
    {
        //if (!isLocalPlayer) return;

        if (Input.GetKeyDown(_interactKey))
        {
            if (CheckForInteractable())
            {
                ComandInteract(_currentTarget.netIdentity);
            }
        }
    }

    private bool CheckForInteractable()
    {
        _currentTarget = null;

        print("I tried");

        if (Physics.Raycast(_camera.transform.position,
                            _camera.transform.forward,
                            out RaycastHit hit,
                            _interactDistance))
        {
            print("I tried 2");
            if (hit.collider.TryGetComponent(out InteractableObject interactable))
            {
                print(interactable.gameObject);
                _currentTarget = interactable;
                return true;
            }
        }

        return false;
    }

    [Command]
    private void ComandInteract(NetworkIdentity targetNetId)
    {
        if (targetNetId == null) return;

        InteractableObject interactable = targetNetId.GetComponent<InteractableObject>();

        if (interactable != null && interactable.GetCanInteract())
        {
            interactable.OnServerInteract(gameObject);
        }

    }
}
