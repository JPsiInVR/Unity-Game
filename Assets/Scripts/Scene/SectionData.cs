using UnityEngine;

[CreateAssetMenu(fileName = "New Section Data", menuName = "Scriptable Objects/Section Data")]
public class SectionData : SceneData
{
    public int SectionNumber => sectionNumber;
    
    [SerializeField]
    private int sectionNumber;
}
