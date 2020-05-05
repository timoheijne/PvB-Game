using System;
using UnityEngine;



[CreateAssetMenu(fileName = "TUTORIAL", menuName = "Objects/Tutorial Data", order = 0)]
public class TutorialObject : ScriptableObject
{

    public string TutorialName;
    public string TutorialID;
    public bool isEnabled = true;

    [Serializable]
    public struct TutorialSection
    {
        public string Name;
        public string ObjectReferenceID;
        public Shared.Position PanelPosition;
        public string Body;
    }

    public TutorialSection[] TutorialSections;

}