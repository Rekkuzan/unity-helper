using Rekkuzan.Utilities;
using UnityEngine;

public class LayerSampleBehaviour : MonoBehaviour
{
    [SerializeField, Layer] int m_LayerInspector = 31;

    private void Reset()
    {
        this.gameObject.layer = m_LayerInspector;
    }
}
