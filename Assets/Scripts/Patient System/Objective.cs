using UnityEngine;

[CreateAssetMenu(fileName = "FILENAME", menuName = "Objects/Objective", order = 0)]
public class Objective : ScriptableObject
{
    public string FriendlyName; // The name we can show to the player
    public string InternalName; // Internal name is what we can use to refer to this objective, should not include capital or spaces!
    public bool Active = true;

    public bool IsDone; // Defines if the objective is done, this is not exposed in the inspector
}