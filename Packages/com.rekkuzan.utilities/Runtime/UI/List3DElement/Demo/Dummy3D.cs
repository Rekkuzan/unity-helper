using UnityEngine;

public class Dummy3D : Rekkuzan.Helper.UI.List3DElement.GameObject3DRenderTexture
{

    [Header("Dummy parameters")]
    public MeshRenderer Renderer;

    private Vector3 randomRotation;

    public void InitializeDummy(Color color)
    {
        Renderer.material.color = color;
        randomRotation = Random.rotationUniform.eulerAngles;
    }

    private void Update()
    {
        Renderer.transform.Rotate(randomRotation * Time.deltaTime, Space.Self);
    }
}
