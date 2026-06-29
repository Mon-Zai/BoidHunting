using UnityEngine;

public class UserController : MonoBehaviour
{
    [SerializeField] private User _user;
    [SerializeField] private VictoryCondition _victoryCondition;
    [SerializeField] private SharedMenu _sharedMenu;
    [SerializeField] private int _energyTimeInSeconds = 60;
    private EnergyHandler _energyHandler;
    private void Start()
    {
        _sharedMenu.Subscribe(SubstractEnergyDefault);
        _energyHandler = new EnergyHandler(_user, _energyTimeInSeconds);
    }
    void Update()
    {
        _energyHandler.CheckEnergy();
    }
    private void OnDisable()
    {
        _sharedMenu.Unsubscribe(SubstractEnergyDefault);
    }
    public void SubstractEnergyDefault()
    {
        _user.SubtractEnergy(1);
    }
}