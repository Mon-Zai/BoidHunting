using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    [SerializeField] private GameObject _shopMenu;
    [SerializeField] private ShopItemUI[] _shopItems;

    public void OnShopClick()
    {
        bool willShow = !_shopMenu.activeSelf;
        _shopMenu.SetActive(willShow);

        if (willShow)
        {
            RefreshItems();
        }
    }
    public void OnCloseClick()
    {
        _shopMenu.SetActive(false);
    }

    public void RefreshItems()
    {
        for (int index = 0; index < _shopItems.Length; index++)
        {
            if (_shopItems[index] != null)
            {
                _shopItems[index].Refresh();
            }
        }
    }
}