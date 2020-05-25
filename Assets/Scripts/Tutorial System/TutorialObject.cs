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
        public bool ExternalControls;
        public HighlightObjectType HighlightType;        
        
        public enum HighlightObjectType
        {
            UI,
            Object
        }

        public bool Equals(TutorialSection other)
        {
            return Name == other.Name && ObjectReferenceID == other.ObjectReferenceID && PanelPosition == other.PanelPosition && DoctorOnLeft == other.DoctorOnLeft && Body == other.Body;
        }

        public override bool Equals(object obj)
        {
            return obj is TutorialSection other && Equals(other);
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ObjectReferenceID != null ? ObjectReferenceID.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) PanelPosition;
                hashCode = (hashCode * 397) ^ DoctorOnLeft.GetHashCode();
                hashCode = (hashCode * 397) ^ (Body != null ? Body.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    public TutorialSection[] TutorialSections;

}