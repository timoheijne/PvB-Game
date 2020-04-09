using UnityEngine;

[CreateAssetMenu(fileName = "FILENAME", menuName = "Objects/Objective", order = 0)]
public class Objective : ScriptableObject
{
    public string FriendlyName ;
    public string InternalName;
    public bool Active = true;

    public bool IsDone;
}