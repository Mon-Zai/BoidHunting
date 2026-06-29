using UnityEngine;

public class PersistentRoot : MonoBehaviour
{
    [SerializeField] private UserController _userController;
    [SerializeField] private User _user;
    [SerializeField] private SharedMenu _sharedMenu;

    public static PersistentRoot Instance { get; private set; }

    private bool _isApplyingLoadedData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _userController = GetComponentInChildren<UserController>();
            _user = GetComponentInChildren<User>();
            _sharedMenu = Resources.Load<SharedMenu>("SharedMenu");

            if (_user == null)
            {
                Debug.LogError("PersistentRoot: User component not found in children.");
                return;
            }

            _isApplyingLoadedData = true;
            UserSaveSystem.TryLoad(_user);
            _isApplyingLoadedData = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (_user == null) return;

        _user.OnEnergyChanged += HandleEnergyChanged;
        _user.OnCurrencyChanged += HandleCurrencyChanged;
    }

    private void OnDisable()
    {
        if (_user == null) return;

        _user.OnEnergyChanged -= HandleEnergyChanged;
        _user.OnCurrencyChanged -= HandleCurrencyChanged;
    }

    private void HandleEnergyChanged(int value)
    {
        if (_isApplyingLoadedData) return;
        UserSaveSystem.Save(_user);
    }

    private void HandleCurrencyChanged(int value)
    {
        if (_isApplyingLoadedData) return;
        UserSaveSystem.Save(_user);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && _user != null)
        {
            UserSaveSystem.Save(_user);
        }
    }

    private void OnApplicationQuit()
    {
        if (_user != null)
        {
            UserSaveSystem.Save(_user);
        }
    }
}