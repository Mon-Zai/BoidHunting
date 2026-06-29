using UnityEngine;
using UnityEngine.UI;

public class RewardedAdsButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private int _rewardAmount = 2;

    private void Awake()
    {
        if (_button == null)
            _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        UpdateButtonState();
    }

    private void Update()
    {
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        if (AdsManager.Instance != null)
            _button.interactable = AdsManager.Instance.IsAdReady();
    }

    private void OnClick()
    {
        if (AdsManager.Instance == null)
        {
            Debug.LogError("RewardedAdsButton: AdsManager is null");
            return;
        }

        AdsManager.Instance.ShowRewardedAd(
            onComplete: OnAdCompleted,
            onSkipped: OnAdSkipped,
            onFailed: OnAdFailed
        );
    }

    private void OnAdCompleted()
    {
        Debug.Log("User watched the ad completely. Rewarding user...");
        GiveRewardToUser(_rewardAmount);
    }

    private void OnAdSkipped()
    {
        Debug.Log("User skipped the ad. No reward given.");
        GiveRewardToUser(_rewardAmount / 2);

    }

    private void OnAdFailed()
    {
        Debug.Log("User failed to watch the ad. No reward given.");
    }

    private void OnDestroy()
    {
        if (_button != null)
            _button.onClick.RemoveListener(OnClick);
    }

    private void GiveRewardToUser(int rewardAmount)
    {
        User user = PersistentRoot.Instance.GetComponentInChildren<User>();
        if (user == null) return;
        if (user.GetEnergy() < user.GetMaxEnergy())
        {
            user.AddEnergy(rewardAmount);
        }
        else
        {
            user.AddCurrency(rewardAmount);
        }

    }
}