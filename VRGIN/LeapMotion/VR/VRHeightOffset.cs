using UnityEngine;
using System;
using System.Linq;
#if UNITY_2017_2_OR_NEWER
    using UnityEngine.XR;
#else
using XRSettings = UnityEngine.VR.VRSettings;
using XRDevice = UnityEngine.VR.VRDevice;
#endif

public class VRHeightOffset : MonoBehaviour {

  [Serializable]
  public class DeviceHeightPair {
    public string DeviceName;
    public float HeightOffset;

    public DeviceHeightPair(string deviceName, float heightOffset) {
      DeviceName = deviceName;
      HeightOffset = heightOffset;
    }
  }

  public DeviceHeightPair[] _deviceOffsets;

  void Reset() {
    _deviceOffsets = new DeviceHeightPair[1];
    _deviceOffsets[0] = new DeviceHeightPair("oculus", 1f);
  }

  void Start() {
    if (XRDevice.isPresent && XRSettings.enabled && _deviceOffsets != null) {
#if UNITY_5_4_OR_NEWER
      string deviceName = XRSettings.loadedDeviceName;
#else
      string deviceName = XRDevice.family;
#endif
      var deviceHeightPair = _deviceOffsets.FirstOrDefault(d => deviceName.ToLower().Contains(d.DeviceName.ToLower()));
      if (deviceHeightPair != null) {
        transform.Translate(Vector3.up * deviceHeightPair.HeightOffset);
      }
    }
  }
}
