using UnityEngine;

public class PersistentRoot : MonoBehaviour
{
    [SerializeField] private UserController _userController;
    [SerializeField] private User _user;
    [SerializeField] private SharedMenu _sharedMenu;

    public static PersistentRoot Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _userController = GetComponentInChildren<UserController>();
            _user = GetComponentInChildren<User>();
            _sharedMenu = Resources.Load<SharedMenu>("SharedMenu");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}