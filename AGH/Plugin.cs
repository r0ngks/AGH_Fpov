using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace AGH_Fpov
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(CameraPosition), "HeadCam")]
        class HeadCam_Patch
        {
            static AccessTools.FieldRef<CameraPosition, GameObject> cameraPCRef = AccessTools.FieldRefAccess<CameraPosition, GameObject>("CameraPC");

            [HarmonyPostfix]
            {
                
                GameObject femaleCam = GameObject.Find("CH02/CH0002/HS_kiten_02/bip01_02/bip01 Pelvis_02/bip01 Spine_02/bip01 Spine1_02/bip01 Spine2_02/bip01 Spine3_02/bip01 Neck_02/bip01 Neck1_02/bip01 Head_02/CH02_Camera");
                
                if (femaleCam == null) {
                    femaleCam = GameObject.Find("CH02/CH0002/HS_kiten_02/bip01_02/bip01 Pelvis_02/bip01 Spine_02/bip01 Spine1_02/bip01 Spine2_02/bip01 Spine3_02/bip01 Neck_02/bip01 Neck1_02/bip01 Head_02");
                }
                if (femaleCam == null) {
                    femaleCam = GameObject.Find("CH01/CH0001/HS_kiten/bip01/bip01 Pelvis/bip01 Spine/bip01 Spine1/bip01 Spine2/bip01 Spine3/bip01 Neck/bip01 Neck1/bip01 Head/HS_Head/CH01_Camera");
                }
                if (femaleCam == null) {
                    femaleCam = GameObject.Find("CH01/CH0001/HS_kiten/bip01/bip01 Pelvis/bip01 Spine/bip01 Spine1/bip01 Spine2/bip01 Spine3/bip01 Neck/bip01 Neck1/bip01 Head/CH01_Camera");
                }
               
                if (femaleCam != null) {
                    ___CameraPC = femaleCam;
                    ___CameraPC.transform.localEulerAngles = new Vector3(-90f, 90f, 0f);
                    ___CameraPC.transform.localPosition = new Vector3(___CameraPC.transform.localPosition.x, 0.13f , ___CameraPC.transform.localPosition.z);
                }

                if (___PC != null) {
                    ___PC.Head = true;
                }
                if (___MobA != null) {
                    ___MobA.HeadOn();
                }
                if (___MobB != null) {
                    ___MobB.HeadOn();
                }
                if (___MobC != null) {
                    ___MobC.HeadOn();
                }
            }
            
        }
    }
}
