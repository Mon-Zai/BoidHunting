using System;
using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.RemoteConfig;

public class RemoteConfigController : MonoBehaviour
{
    [Serializable]
    public struct ConfigDefaults
    {
        public int maxEnergy;
        public bool enableRewardedAds;
        public int energyRegenSeconds;
        public int playEnergyCost;
        public int victoryCurrencyReward;
    }

    public struct UserAttributes { }
    public struct AppAttributes { }

    [Header("Targets")]
    [SerializeField] private User _user;
    [SerializeField] private UserController _userController;
    [SerializeField] private AdsManager _adsManager;

    [Header("Fallback defaults")]
    [SerializeField] private ConfigDefaults _defaults = new ConfigDefaults
    {
        maxEnergy = 10,
        enableRewardedAds = true,
        energyRegenSeconds = 60,
        playEnergyCost = 1,
        victoryCurrencyReward = 1
    };

    private bool _isFetching;

    private async void Start()
    {
        AutoResolveReferences();
        await InitializeAndFetchAsync();
    }

    private void AutoResolveReferences()
    {
        if (_user == null) _user = FindFirstObjectByType<User>(FindObjectsInactive.Include);
        if (_userController == null) _userController = FindFirstObjectByType<UserController>(FindObjectsInactive.Include);
        if (_adsManager == null) _adsManager = AdsManager.Instance;
    }

    private async Task InitializeAndFetchAsync()
    {
        if (_isFetching) return;
        _isFetching = true;

        try
        {
            if (UnityServices.State != ServicesInitializationState.Initialized)
            {
                await UnityServices.InitializeAsync();
            }

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            RemoteConfigService.Instance.FetchCompleted += OnFetchCompleted;
            await RemoteConfigService.Instance.FetchConfigsAsync(new UserAttributes(), new AppAttributes());
        }
        catch (Exception ex)
        {
            Debug.LogWarning("RemoteConfig fallo, aplicando defaults. " + ex.Message);
            ApplyConfig(_defaults.maxEnergy, _defaults.enableRewardedAds, _defaults.energyRegenSeconds, _defaults.playEnergyCost);
        }
        finally
        {
            _isFetching = false;
        }
    }

    private void OnFetchCompleted(ConfigResponse response)
    {
        int maxEnergy = RemoteConfigService.Instance.appConfig.GetInt("MAX_ENERGY", _defaults.maxEnergy);
        bool enableRewardedAds = RemoteConfigService.Instance.appConfig.GetBool("ENABLE_ADS", _defaults.enableRewardedAds);
        int energyRegenSeconds = RemoteConfigService.Instance.appConfig.GetInt("ENERGY_REGEN", _defaults.energyRegenSeconds);
        int playEnergyCost = RemoteConfigService.Instance.appConfig.GetInt("PLAY_COST", _defaults.playEnergyCost);

        ApplyConfig(maxEnergy, enableRewardedAds, energyRegenSeconds, playEnergyCost);

        Debug.Log("RemoteConfig applied: "
            + "max_energy=" + maxEnergy + ", "
            + "enable_rewarded_ads=" + enableRewardedAds + ", "
            + "energy_regen_seconds=" + energyRegenSeconds + ", "
            + "play_energy_cost=" + playEnergyCost);
    }

    private void ApplyConfig(int maxEnergy, bool enableRewardedAds, int energyRegenSeconds, int playEnergyCost)
    {
        if (_user != null) _user.SetMaxEnergy(maxEnergy);
        if (_userController != null)
        {
            _userController.SetEnergyRegenerationSeconds(energyRegenSeconds);
            _userController.SetPlayEnergyCost(playEnergyCost);
        }
        if (_adsManager != null) _adsManager.SetRewardedAdsEnabled(enableRewardedAds);
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= OnFetchCompleted;
    }
}