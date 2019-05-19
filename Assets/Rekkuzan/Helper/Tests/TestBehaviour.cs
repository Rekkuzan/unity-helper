using UnityEngine;
using Rekkuzan.Helper;

/// <summary>
/// Behaviour to test Helper in scene
/// 
/// TODO: Be able to test without playing mode
/// </summary>
public class TestBehaviour : MonoBehaviour
{
    private void Start()
    {
        TestUnit.Test();
    }
}
