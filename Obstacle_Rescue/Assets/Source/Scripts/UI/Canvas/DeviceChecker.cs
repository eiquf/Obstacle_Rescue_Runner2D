using System;
using UnityEngine;
using UnityEngine.UI;

public class DeviceChecker
{
    private enum ENUM_Device_Type { Tablet, Phone }
    public Action<CanvasScaler> OnChangedMatch;
    public DeviceChecker() => OnChangedMatch += MatchChange;
    private void MatchChange(CanvasScaler scaler)
    {
        scaler.matchWidthOrHeight = GetDeviceType() == ENUM_Device_Type.Phone ? 1f : 0f;
        OnChangedMatch -= MatchChange;
    }
    private float DeviceDiagonalSizeInInches() => Mathf.Sqrt(Mathf.Pow(Screen.width / Screen.dpi, 2) + Mathf.Pow(Screen.height / Screen.dpi, 2));
    private ENUM_Device_Type GetDeviceType()
    {
#if UNITY_IOS
        // Uncomment for iOS device detection
        // if (UnityEngine.iOS.Device.generation.ToString().Contains("iPad")) return ENUM_Device_Type.Tablet;
        // if (UnityEngine.iOS.Device.generation.ToString().Contains("iPhone")) return ENUM_Device_Type.Phone;
#else
        return DeviceDiagonalSizeInInches() > 6.5f && (Mathf.Max(Screen.width, Screen.height) / Mathf.Min(Screen.width, Screen.height)) < 2f
            ? ENUM_Device_Type.Tablet
            : ENUM_Device_Type.Phone;
#endif
    }
}