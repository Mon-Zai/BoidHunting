using UnityEngine;

public class PersistentRoot : MonoBehaviour
{
    [SerializeField] private UserController _userController;
    [SerializeField] public User User;
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
            User = GetComponentInChildren<User>();
            //_sharedMenu = Resources.Load<SharedMenu>("SharedMenu");

            if (User == null)
            {
                Debug.LogError("PersistentRoot: User component not found in children.");
                return;
            }
            
            _sharedMenu.SetUser(User);

            _isApplyingLoadedData = true;
            UserSaveSystem.TryLoad(User);
            _isApplyingLoadedData = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        if (User == null) return;

        User.OnEnergyChanged += HandleEnergyChanged;
        User.OnCurrencyChanged += HandleCurrencyChanged;
    }

    private void OnDisable()
    {
        if (User == null) return;

        User.OnEnergyChanged -= HandleEnergyChanged;
        User.OnCurrencyChanged -= HandleCurrencyChanged;
    }

    private void HandleEnergyChanged(int value)
    {
        if (_isApplyingLoadedData) return;
        UserSaveSystem.Save(User);
    }

    private void HandleCurrencyChanged(int value)
    {
        if (_isApplyingLoadedData) return;
        UserSaveSystem.Save(User);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && User != null)
        {
            UserSaveSystem.Save(User);
        }
    }

    private void OnApplicationQuit()
    {
        if (User != null)
        {
            UserSaveSystem.Save(User);
        }
    }
}