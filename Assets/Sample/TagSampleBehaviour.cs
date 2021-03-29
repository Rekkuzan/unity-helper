using Rekkuzan.Helper;
using UnityEngine;

public class TagSampleBehaviour : MonoBehaviour
{
    [SerializeField, Tag] string m_tagInspector = "Untagged";

    private void OnValidate()
    {
        this.gameObject.tag = m_tagInspector;
    }
}
