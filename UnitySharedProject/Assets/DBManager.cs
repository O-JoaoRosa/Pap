using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DBManager
{
    public static string username;
    public static int userCarIdSelected;
    public static int Reputation;
    public static int Money;

    public static bool loggedIn { get { return username != null; } }

    public static void LogOut()
    {
        username = null;
    }

}
