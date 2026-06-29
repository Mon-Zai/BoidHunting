using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    public string UserName;
    public int Energy;
    public int MaxEnergy;
    public int Currency;
    public int CurrentLevel;
    public int TimeToNextEnergy;
    public List<int> OwnedShopItemIds;

}