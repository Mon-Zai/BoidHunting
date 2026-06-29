using UnityEngine;

public class EnergyHandler
{
    private User _user;
    private int _regenerationRateInSeconds = 60;
    private float timer = 0f;
    public EnergyHandler(User user, int regenerationRateInSeconds = 60)
    {
        _user = user;
        _regenerationRateInSeconds = regenerationRateInSeconds;
    }
    public void CheckEnergy()
    {

        if (_user.GetEnergy() < _user.GetMaxEnergy())
        {
            timer += Time.deltaTime;
            _user.SetTimeToNextEnergy(_regenerationRateInSeconds - (int)timer);
            if (timer >= _regenerationRateInSeconds)
            {
                _user.AddEnergy(1);
                timer = 0f;
            }
        }

    }
}