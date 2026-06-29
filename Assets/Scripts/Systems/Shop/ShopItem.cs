using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private int _itemId;
    [SerializeField] private int _price;
    [SerializeField] private string _itemName;
    [SerializeField] private string _description;

    public int ItemId => _itemId;
    public int Price => _price;
    public string ItemName => _itemName;
    public string Description => _description;
}