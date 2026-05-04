using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float interactionRadius = 1f;
    public Vector3 InteractionPoint = Vector3.zero;
    void Awake() => inputReader.SpawnStartedEvent += ClickToInteract;

    void OnDisable() => inputReader.SpawnStartedEvent -= ClickToInteract;

    public void ClickToInteract()
    {
        Ray ray = Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            InteractionPoint = hit.point;
            Collider[] colliders = Physics.OverlapSphere(hit.point, interactionRadius);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(gameObject);
                    break;
                }
            }
        }
        else
            Debug.Log("No hit detected");
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(InteractionPoint, interactionRadius);
    }
}