using UnityEngine;

public class PlayerShopItems : MonoBehaviour
{
    [SerializeField] private GameObject _hat;

    void Awake()
    {
        User user = PersistentRoot.Instance.User;
        if (user == null) return;

        if (user.OwnedShopItemIds.Contains(0))
        {
            _hat.SetActive(true);
        }
        if (user.OwnedShopItemIds.Contains(1))
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();

            renderer.material.color = Color.white;
        }
    }
}
