using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;
using VRGIN.Core;
using VRGIN.Modes;

namespace VRGIN
{
    public class VRLoader: ProtectedBehaviour
    {
        private static string DeviceOpenVR = "OpenVR";
        private static string DeviceNone = "None";

        private static bool _isVREnable = false;
        private static VRLoader _Instance;

        IVRManagerContext context;
        public static VRLoader Instance
        {
            get
            {
                if (_Instance == null)
                {
                    throw new InvalidOperationException("VR Loader has not been created yet!");
                }
                return _Instance;
            }
        }

        public static VRLoader Create(IVRManagerContext context)          

        {
            if (_Instance == null) { 
                _Instance = new GameObject("VRLoader").AddComponent<VRLoader>();
                _Instance.context = context;
            }
            return _Instance;
        }

        protected override void OnAwake()
        {
            /*
            if (_isVREnable)
            {
                StartCoroutine(LoadDevice(DeviceOpenVR));
            }
            else
            {
                StartCoroutine(LoadDevice(DeviceNone));
            }
            */
            StartCoroutine(BootVR(context));
        }


        //public IEnumerator BootVR<I, M>(IVRManagerContext context) where I : GameInterpreter where M : ControlMode
        public IEnumerator BootVR(IVRManagerContext context)
        {

    // After Unity 5.5 need init UnityEngine.VR.VRSettings first.
#if UNITY_5_6
    var newDevice = "OpenVR";
            bool vrMode = newDevice != "None";
            UnityEngine.VR.VRSettings.LoadDeviceByName(newDevice);
            yield return null;

            UnityEngine.VR.VRSettings.enabled = vrMode;
            yield return null;

            while (UnityEngine.VR.VRSettings.loadedDeviceName != newDevice || UnityEngine.VR.VRSettings.enabled != vrMode)
            {
                yield return null;
            }

#endif
            yield return null;
        }

        /// <summary>
        /// VRデバイスのロード。
        /// </summary>
        IEnumerator LoadDevice(string newDevice)
        {
            bool vrMode = newDevice != DeviceNone;

            // 指定されたデバイスの読み込み.
            UnityEngine.VR.VRSettings.LoadDeviceByName(newDevice);
            // 次のフレームまで待つ.
            yield return null;
            // VRモードを有効にする.
            UnityEngine.VR.VRSettings.enabled = vrMode;
            // 次のフレームまで待つ.
            yield return null;

            // デバイスの読み込みが完了するまで待つ.
            while (UnityEngine.VR.VRSettings.loadedDeviceName != newDevice || UnityEngine.VR.VRSettings.enabled != vrMode)
            {
                yield return null;
            }


            if (vrMode)
            {
                // Boot VRManager!
                // Note: Use your own implementation of GameInterpreter to gain access to a few useful operatoins
                // (e.g. characters, camera judging, colliders, etc.)
              //  VRManager.Create<GameInterpreter>(CreateContext("VRContext.xml"));
               // VR.Manager.SetMode<GenericSeatedMode>();
            }
        }
    }
}
