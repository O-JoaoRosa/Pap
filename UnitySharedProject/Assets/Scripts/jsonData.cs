using System;
using System.Collections.Generic;

[Serializable]
public class jsonData
{
    public string UserName;
    public int ID;
    public int reputation;
    public int image;
    public int money;
    public int userCarIDSelected;
}

public class ActivePlayer { public jsonData activePlayer { get; set; } }

