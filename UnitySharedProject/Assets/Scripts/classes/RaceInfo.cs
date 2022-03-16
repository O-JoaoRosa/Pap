using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RaceInfo
{
    public string TrackName;
    public int NumberOfLaps;
    public string TypeOfRace;
    public List<string> lapTimes = new List<string>();
}
