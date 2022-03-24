using System;
using System.Collections.Generic;

[Serializable]
public class jsonData
{
    public string UserName;
    public int ID;
    public int Reputation;
    public int image;
    public int Money;
    public int UserCarIDSelected;
}

public class ActivePlayer { public jsonData activePlayer { get; set; } }

