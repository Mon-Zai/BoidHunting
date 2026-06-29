using UnityEngine;

public class SavesMenu : MonoBehaviour
{
    [SerializeField] private GameObject _saveMenu;

    public void OnSavesClick()
    {
        if(_saveMenu.activeSelf)
        {
            _saveMenu.SetActive(false);
        }
        else
        {
            _saveMenu.SetActive(true);
        }
    }
}
