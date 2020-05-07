using System;
using UnityEngine;



[CreateAssetMenu(fileName = "TUTORIAL", menuName = "Objects/Tutorial Data", order = 0)]
public class TutorialObject : ScriptableObject
{

    public string TutorialName;
    public string TutorialID;
    public bool isEnabled = true;

    [Serializable]
    public struct TutorialSection : IEquatable<TutorialSection>
    {
        public string Name;
        public string ObjectReferenceID;
        public Shared.Position PanelPosition;
        public bool DoctorOnLeft;
        public string Body;

        public bool Equals(TutorialSection other)
        {
            return Name == other.Name && ObjectReferenceID == other.ObjectReferenceID && PanelPosition == other.PanelPosition && DoctorOnLeft == other.DoctorOnLeft && Body == other.Body;
        }

        public override bool Equals(object obj)
        {
            return obj is TutorialSection other && Equals(other);
        }
    }

    public TutorialSection[] TutorialSections;

}