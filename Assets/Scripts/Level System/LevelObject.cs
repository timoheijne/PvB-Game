[UnityEngine.CreateAssetMenu(fileName = "Level", menuName = "Objects/Level Object", order = 0)]
public class LevelObject : UnityEngine.ScriptableObject
{
        public string LevelName;
        public Shared.Difficulty Difficulty;
        public string SceneName;

        public PatientFile[] PatientFiles;
}