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

        [HarmonyPatch(typeof(CameraPosition), "Awake")]
        class CameraPosition_Patch
        {
            [HarmonyPostfix]
            static void PostFix(ref GameObject ___CameraPC)
            {

                GameObject femaleCam = GameObject.Find("CH01/CH0001/HS_kiten/bip01/bip01 Pelvis/bip01 Spine/bip01 Spine1/bip01 Spine2/bip01 Spine3/bip01 Neck/bip01 Neck1/bip01 Head/CH01_Camera");
                
                if (femaleCam == null) {
                    femaleCam = GameObject.Find("CH01/CH0001/HS_kiten/bip01/bip01 Pelvis/bip01 Spine/bip01 Spine1/bip01 Spine2/bip01 Spine3/bip01 Neck/bip01 Neck1/bip01 Head/HS_Head/CH01_Camera");
                }
                
                if (femaleCam != null) {
                    ___CameraPC = femaleCam;
                    ___CameraPC.transform.localEulerAngles = new Vector3(-90f, 90f, 0f);
                    ___CameraPC.transform.localPosition = new Vector3(___CameraPC.transform.localPosition.x, 0.13f , ___CameraPC.transform.localPosition.z);
                }
            }
            
        }
        [HarmonyPatch(typeof(CameraPosition), "HeadCam")]
        class HeadCam_Patch
        {
            [HarmonyPostfix]
            static void PostFix(ref ObjectMaterialSet_PC ___PC, ref MobA_Set ___MobA, ref MobB_Set ___MobB, ref MobC_Set ___MobC)
            {
                ___PC.Head = true;
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
