using UnityEngine;
namespace TechJuego.Framework.HapticFeedback
{
    public class HapticCall : Singleton<HapticCall>
    {
         AndroidHaptic androidHaptic;
        protected HapticCall()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                androidHaptic = new AndroidHaptic();
            }
        }
        public  void HeavyHaptic()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
        iOSHaptic.PerformUIImpactFeedbackStyleHeavy();
#endif
#if UNITY_ANDROID

                    androidHaptic.PerformUIImpactFeedbackStyleHeavy();
#endif
                }
            }
        }
        public  void MediumHaptic()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
            iOSHaptic.PerformUIImpactFeedbackStyleMedium();
#endif
#if UNITY_ANDROID
                    androidHaptic.PerformUIImpactFeedbackStyleMedium();
#endif
                }
            }
        }
        public  void LightHaptic()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
            iOSHaptic.PerformUIImpactFeedbackStyleLight();
#endif
#if UNITY_ANDROID
                    androidHaptic.PerformUIImpactFeedbackStyleLight();
#endif
                }
            }
        }
        public  void RigidHaptic()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
            iOSHaptic.PerformUIImpactFeedbackStyleRigid();
#endif
#if UNITY_ANDROID
                    androidHaptic.PerformUIImpactFeedbackStyleRigid();
#endif
                }
            }
        }
        public  void SoftHaptic()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
            iOSHaptic.PerformUIImpactFeedbackStyleSoft();
#endif
#if UNITY_ANDROID
                    androidHaptic.PerformUIImpactFeedbackStyleSoft();
#endif
                }
            }
        }
        public  void PerformSuccessFeedback()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
            iOSHaptic.PerformUINotificationFeedbackTypeSuccess();
#endif
#if UNITY_ANDROID
                    androidHaptic.PerformUINotificationFeedbackTypeSuccess();
#endif
                }
            }
        }
        public  void PerformErrorFeedback()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
            iOSHaptic.PerformUINotificationFeedbackTypeError();
#endif
#if UNITY_ANDROID
                    androidHaptic.PerformUINotificationFeedbackTypeError();
#endif
                }
            }
        }
        public  void PerformWarningFeedback()
        {
            if (HapticSetting.GetVibrate())
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
#if UNITY_IOS
            iOSHaptic.PerformUINotificationFeedbackTypeWarning();
#endif
#if UNITY_ANDROID
                    androidHaptic.PerformUINotificationFeedbackTypeWarning();
#endif
                }
            }
        }
    }
}