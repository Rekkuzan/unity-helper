using System.Collections;
using System.Collections.Generic;
using Rekkuzan.Utilities.Haptic.Android;
using UnityEngine;

namespace Rekkuzan.Utilities.Haptic
{
    public class HapticEventCaller : MonoBehaviour
    {
        [SerializeField]
        private HapticHelper.KeyType _defaultType;


        public void InvokeDefaultHaptic() => HapticHelper.HapticFeedback(_defaultType);

        public void InvokeHaptic(HapticHelper.KeyType type) => HapticHelper.HapticFeedback(type);
    }

}
