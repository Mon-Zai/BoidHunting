using UnityEngine;

public class Fence : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _fence;
    private bool isOpen = false;
    public void ToggleFence()
    {
        isOpen = !isOpen;
        _fence.SetActive(!isOpen);
        AudioManager.Instance.Play(AudioManager.SoundType.OPENDOOR);
    }
    public void Interact(GameObject interactor)
    {
        ToggleFence();
    }
}
