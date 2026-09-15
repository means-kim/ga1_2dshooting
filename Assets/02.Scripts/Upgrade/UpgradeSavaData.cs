using UnityEngine;

[System.Serializable]
public class UpgradeSavaData
{
    public string[] Name;
    public int[] Level;

    public UpgradeSavaData(int count)
    {
        Name = new string[count];
        Level = new int[count];
    }
}