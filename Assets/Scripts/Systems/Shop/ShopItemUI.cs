using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private ShopItem _shopItem;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _statusText;

    private User _user;

    private void Awake()
    {
        _user = PersistentRoot.Instance != null ? PersistentRoot.Instance.GetComponentInChildren<User>() : null;

        if (_buyButton != null)
        {
            _buyButton.onClick.AddListener(BuyItem);
        }
    }

    private void OnEnable()
    {
        if (_user != null)
        {
            _user.OnCurrencyChanged += HandleCurrencyChanged;
            _user.OnOwnedShopItemsChanged += Refresh;
        }

        Refresh();
    }

    private void OnDisable()
    {
        if (_user != null)
        {
            _user.OnCurrencyChanged -= HandleCurrencyChanged;
            _user.OnOwnedShopItemsChanged -= Refresh;
        }
    }

    private void HandleCurrencyChanged(int currency)
    {
        Refresh();
    }

    public void Refresh()
    {
        if (_shopItem == null || _user == null)
        {
            return;
        }

        if (_nameText != null)
        {
            _nameText.text = _shopItem.ItemName;
        }

        if (_priceText != null)
        {
            _priceText.text = _shopItem.Price.ToString();
        }

        bool owned = _user.OwnsShopItem(_shopItem.ItemId);
        bool canBuy = _user.GetCurrency() >= _shopItem.Price;

        if (_statusText != null)
        {
            if (owned)
            {
                _statusText.text = "Owned";
            }
            else if (canBuy)
            {
                _statusText.text = "Available";
            }
            else
            {
                _statusText.text = "Not enough currency";
            }
        }

        if (_buyButton != null)
        {
            _buyButton.interactable = !owned && canBuy;
        }
    }

    public void BuyItem()
    {
        if (_shopItem == null || _user == null)
        {
            return;
        }

        bool purchased = _user.TryBuyShopItem(_shopItem.ItemId, _shopItem.Price);
        if (purchased)
        {
            Refresh();
        }
    }
}