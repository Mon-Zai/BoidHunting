using System;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private string _iOSGameId;
    [SerializeField] private bool _testMode = true;

    private string _gameId;
    public bool IsInitialized { get; private set; }

    public event Action<bool> OnInitializationFinished;


    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId;
#else
        _gameId = string.Empty;
#endif

        if (string.IsNullOrEmpty(_gameId))
        {
            Debug.LogError("AdsInitializer: Game ID vacío. Asignalo en el Inspector.");
            IsInitialized = false;
            OnInitializationFinished?.Invoke(false);
            return;
        }

        if (Advertisement.isInitialized)
        {
            IsInitialized = true;
            Debug.Log("AdsInitializer: Ya estaba inicializado.");
            OnInitializationFinished?.Invoke(true);
            return;
        }

        // Nota: isSupported puede ser false en Editor, lo ignoramos intencionalmente en dev
        Debug.Log($"AdsInitializer: Inicializando con GameID={_gameId}, testMode={_testMode}");
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        IsInitialized = true;
        Debug.Log("AdsInitializer: Inicialización completa.");
        OnInitializationFinished?.Invoke(true);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        IsInitialized = false;
        Debug.LogError($"AdsInitializer: Falló: {error} - {message}");
        OnInitializationFinished?.Invoke(false);
    }
}