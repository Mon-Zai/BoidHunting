using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private AdsInitializer _adsInitializer;
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] private string _iOSAdUnitId = "Rewarded_iOS";
    [SerializeField] private bool _rewardedAdsEnabled = true;

    public static AdsManager Instance { get; private set; }

    private string _adUnitId;
    private bool _isLoaded;

    private Action _onRewardedComplete;
    private Action _onRewardedSkipped;
    private Action _onRewardedFailed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        ResolveAdUnitId();
    }

    private void Start()
    {
        if (_adsInitializer == null) return;

        _adsInitializer.OnInitializationFinished += HandleAdsInitializationFinished;
        _adsInitializer.InitializeAds();
    }
    public void SetRewardedAdsEnabled(bool enabled)
    {
        _rewardedAdsEnabled = enabled;
    }
    private void ResolveAdUnitId()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#elif UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#else
        _adUnitId = string.Empty;
#endif
    }

    private void HandleAdsInitializationFinished(bool success)
    {
        if (!success) return;

        LoadAd();
    }

    private void LoadAd()
    {
        if (!Advertisement.isInitialized || string.IsNullOrEmpty(_adUnitId))
        {
            return;
        }

        _isLoaded = false;
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowRewardedAd(Action onComplete, Action onSkipped = null, Action onFailed = null)
    {
        if (!_rewardedAdsEnabled)
        {
            Debug.Log("AdsManager: rewarded ads deshabilitado por Remote Config.");
            onFailed?.Invoke();
            return;
        }
        if (!_isLoaded)
        {
            Debug.LogWarning("AdsManager: El ad todavía no está listo.");
            onFailed?.Invoke();
            return;
        }

        _onRewardedComplete = onComplete;
        _onRewardedSkipped = onSkipped;
        _onRewardedFailed = onFailed;

        Advertisement.Show(_adUnitId, this);
    }

    public bool IsAdReady()
    {
        return _isLoaded && _rewardedAdsEnabled;
    }

    // LOAD LISTENER
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId != _adUnitId) return;

        _isLoaded = true;
        Debug.Log("AdsManager: Ad listo.");
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        if (adUnitId != _adUnitId) return;

        _isLoaded = false;
        Debug.LogError($"AdsManager: Error cargando {adUnitId}: {error} - {message}");
    }

    // SHOW LISTENER
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState state)
    {
        if (adUnitId != _adUnitId) return;

        if (state == UnityAdsShowCompletionState.COMPLETED)
        {
            _onRewardedComplete?.Invoke();
        }
        else if (state == UnityAdsShowCompletionState.SKIPPED)
        {
            _onRewardedSkipped?.Invoke();
        }
        else
        {
            _onRewardedFailed?.Invoke();
        }

        ClearRewardCallbacks();
        _isLoaded = false;
        LoadAd();
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        if (adUnitId != _adUnitId) return;

        _onRewardedFailed?.Invoke();

        ClearRewardCallbacks();
        _isLoaded = false;
        LoadAd();
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    private void ClearRewardCallbacks()
    {
        _onRewardedComplete = null;
        _onRewardedSkipped = null;
        _onRewardedFailed = null;
    }

    private void OnDestroy()
    {
        if (_adsInitializer != null)
        {
            _adsInitializer.OnInitializationFinished -= HandleAdsInitializationFinished;
        }

        if (Instance == this)
        {
            Instance = null;
        }
    }
}