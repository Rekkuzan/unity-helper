using Rekkuzan.Helper;
using UnityEngine;

public class LayerSampleBehaviour : MonoBehaviour
{
    [SerializeField, Layer] int m_LayerInspector = 31;

    private void OnValidate()
    {
        this.gameObject.layer = m_LayerInspector;
    }
}
