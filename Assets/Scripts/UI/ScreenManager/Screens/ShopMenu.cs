using UnityEngine;

public class ShopMenu : BaseScreen
{
    [SerializeField] private ShopItemUI[] _shopItems;
    public void BTN_OnShopClick()
    {
        _screenManager.ShowScreen(ScreenName);
        RefreshItems();
    }
    public void BTN_OnCloseClick()
    {
        _screenManager.HideScreen(ScreenName);
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