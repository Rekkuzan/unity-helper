using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan.Helper.Haptic
{
    public static class HapticHelper
    {
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


            public HapticFeedbackManager()
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            HapticFeedbackKey_LIGHT = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("KEYBOARD_TAP");
            HapticFeedbackKey_MEDIUM = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("VIRTUAL_KEY");
            HapticFeedbackKey_HEAVY = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("LONG_PRESS");
            HapticFeedbackKey_FAIL = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("REJECT");
            UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
            //Alternative way to get the UnityPlayer:
            //int content=new AndroidJavaClass("android.R$id").GetStatic<int>("content");
            //new AndroidJavaClass ("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("findViewById",content).Call<AndroidJavaObject>("getChildAt",0);
#endif
            }

            public bool Execute(KeyType type)
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
        }

        //Cache the Manager for performance
        private static HapticFeedbackManager mHapticFeedbackManager;

        public static bool HapticFeedback(KeyType type)
        {
            if (mHapticFeedbackManager == null)
            {
                mHapticFeedbackManager = new HapticFeedbackManager();
            }
            return mHapticFeedbackManager.Execute(type);
        }
    }
}