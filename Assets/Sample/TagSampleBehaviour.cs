using Rekkuzan.Utilities;
using UnityEngine;

public class TagSampleBehaviour : MonoBehaviour
{
    [SerializeField, Tag] string m_tagInspector = "Untagged";

    private void Reset()
    {
        this.gameObject.tag = m_tagInspector;
    }
}
