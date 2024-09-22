using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan.Utilities.Haptic.Android
{
    public static class HapticHelper
    {
        public static bool HapticEnabled { get; set; } = true;
        
        public enum KeyType
        {
            Light,
            Medium,
            Heavy,
            Fail
        }

        private class HapticFeedbackManager
        {

#if UNITY_ANDROID && !UNITY_EDITOR
        private int HapticFeedbackKey_LIGHT;
        private int HapticFeedbackKey_MEDIUM;
        private int HapticFeedbackKey_HEAVY;
        private int HapticFeedbackKey_FAIL;
        private AndroidJavaObject UnityPlayer;
#endif

            static int _sdkAPI = -1;
            static bool _initialized = false;
            static int getSDKInt()
            {
                if (_sdkAPI > 0)
                {
                    return _sdkAPI;
                }
#if UNITY_ANDROID && !UNITY_EDITOR
                using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
                {
                    _sdkAPI = version.GetStatic<int>("SDK_INT");
                }
#endif
                return _sdkAPI;
            }

            public HapticFeedbackManager()
            {
                _sdkAPI = getSDKInt();

                if (_sdkAPI < 24)
                {
                    return;
                }
                try
                {
#if UNITY_ANDROID && !UNITY_EDITOR
                HapticFeedbackKey_LIGHT = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("KEYBOARD_TAP");
                HapticFeedbackKey_MEDIUM = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("VIRTUAL_KEY");
                HapticFeedbackKey_HEAVY = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("LONG_PRESS");
                HapticFeedbackKey_FAIL =
                       _sdkAPI >= 30
                       ? new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("REJECT")
                       : HapticFeedbackKey_LIGHT;
                //UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
                //Alternative way to get the UnityPlayer:
                int content=new AndroidJavaClass("android.R$id").GetStatic<int>("content");
                UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("findViewById",content).Call<AndroidJavaObject>("getChildAt",0);
#endif
                    _initialized = true;
                } 
                catch (System.Exception)
                {
                    // Fail silently
                    _initialized = false;
                }
            }

            public bool Execute(KeyType type)
            {
                if (_sdkAPI < 24 || !_initialized)
                {
                    return false;
                }
                try
                {

#if UNITY_ANDROID && !UNITY_EDITOR

            int key = HapticFeedbackKey_LIGHT;
            switch (type)
            {
                case KeyType.Light:
                    key = HapticFeedbackKey_LIGHT;
                    break;
                case KeyType.Medium:
                    key = HapticFeedbackKey_MEDIUM;
                    break;
                case KeyType.Heavy:
                    key = HapticFeedbackKey_HEAVY;
                    break;
                case KeyType.Fail:
                    key = HapticFeedbackKey_FAIL;
                    break;
            }

            return UnityPlayer.Call<bool> ("performHapticFeedback", key);
#else
                    return false;
#endif
                }
                catch (System.Exception)
                {
                    // Fail silently
                    _initialized = false;
                }

                return false;
            }
        }

        //Cache the Manager for performance
        private static HapticFeedbackManager mHapticFeedbackManager;

        public static bool HapticFeedback(KeyType type)
        {
            if (HapticEnabled == false)
            {
                return false;
            }
            
            if (mHapticFeedbackManager == null)
            {
                mHapticFeedbackManager = new HapticFeedbackManager();
            }
            return mHapticFeedbackManager.Execute(type);
        }
    }
}