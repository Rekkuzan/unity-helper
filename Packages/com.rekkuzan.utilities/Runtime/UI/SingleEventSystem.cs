using UnityEngine;
using UnityEngine.EventSystems;

namespace Rekkuzan.Utilities.UI
{
    /// <summary>
    /// Component that enforce having only 1 instance of <see cref="EventSystem"/> during runtime.
    /// Useful when dealing with multiple scene.
    /// </summary>
    public class SingleEventSystem : MonoBehaviour
    {
        private void Start()
        {
            var eventSystem = GetComponent<EventSystem>();
            if (eventSystem == EventSystem.current)
            {
                return;
            }

            Destroy(gameObject);
        }
    }
}
