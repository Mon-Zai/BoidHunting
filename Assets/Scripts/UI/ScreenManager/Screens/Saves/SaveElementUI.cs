using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveElementUI : MonoBehaviour
{
    [SerializeField] private User _user;
    [SerializeField] private TextMeshProUGUI _currencyAmount;
    [SerializeField] private TextMeshProUGUI _energyAmount;

 
    void OnEnable()
    {
        _user = PersistentRoot.Instance.GetComponentInChildren<User>();

        if (_user == null) return;

        UpdateUI();
    }
    private void UpdateUI()
    {
        _currencyAmount.text = _user.GetCurrency().ToString();
        _energyAmount.text = _user.GetEnergy().ToString();
    }
    public void DeleteSave()
    {
        StartCoroutine(DeleteAndReload());
    }

    private IEnumerator DeleteAndReload()
    {
        UserSaveSystem.DeleteSave();

        if (PersistentRoot.Instance != null)
        {
            Destroy(PersistentRoot.Instance.gameObject);
        }

        yield return null;

        SceneManager.LoadScene(0);
    }
}
