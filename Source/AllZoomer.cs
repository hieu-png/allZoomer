using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using RimWorld.Planet;
using UnityEngine;
namespace NowanoAllZoomer
{
    /*
    [StaticConstructorOnStartup]
    internal static class SetSize {
        static SetSize() {
            FieldInfo minSize = typeof(CameraMapConfig).GetField("minSize");
            minSize.SetValue(minSize.GetType(),allZoomerSetting.minimumZoom);
        }
    }*/
    [StaticConstructorOnStartup]
    public class AllZoomer : Mod

    {
        static AllZoomer()
        {
            var harmony = new Harmony("com.nowano.allZooming");

            harmony.PatchAll();
        }
		public AllZoomer(ModContentPack content) : base(content)
		{
			base.GetSettings<allZoomerSetting>();

		}
        public override void DoSettingsWindowContents(Rect rect)
		{
            
			allZoomerSetting.DoSettingsWindowContents(rect);
		}
		public override string SettingsCategory()
		{
			return base.Content.Name;
		}
        [HarmonyPatch(typeof(CameraMapConfig_ContinuousPan))]
        [HarmonyPatch(MethodType.Constructor)]
        static class CameraMapConfig_ContinuousPanTranspiler
        {
            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var inst in instructions)
                {
                    if (inst.LoadsConstant(8.2f)) {
                        inst.operand = allZoomerSetting.minimumZoom; 
                    }               
                    yield return inst;
                }                
            }
        }
     
        [HarmonyPatch(typeof(CameraMapConfig_ContinuousPanAndZoom))]
        [HarmonyPatch(MethodType.Constructor)]
        static class CameraMapConfig_ContinuousPanAndZoomTranspiler
        {
            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var inst in instructions)
                {
                    if (inst.LoadsConstant(8.2f)) {
                        inst.operand = allZoomerSetting.minimumZoom; 
                    }               
                    yield return inst;
                }                
            }
        }       
        [HarmonyPatch(typeof(CameraMapConfig_MoreZoom))]
        [HarmonyPatch(MethodType.Constructor)]
        static class CameraMapConfig_MoreZoomTranspiler
        {
            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var inst in instructions)
                {
                    if (inst.LoadsConstant(8.2f)) {
                        inst.operand = allZoomerSetting.minimumZoom; 
                    }               
                    yield return inst;
                }                
            }
        }         
        [HarmonyPatch(typeof(CameraMapConfig_SmoothZoom))]
        [HarmonyPatch(MethodType.Constructor)]
        static class CameraMapConfig_SmoothZoomTranspiler
        {
            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                foreach (var inst in instructions)
                {
                    if (inst.LoadsConstant(8.2f)) {
                        inst.operand = allZoomerSetting.minimumZoom; 
                    }               
                    yield return inst;
                }                
            }
        }           
        
        [HarmonyPatch(typeof(CameraMapConfig),nameof(CameraMapConfig.ConfigFixedUpdate_60))]
        static class CameraMapConfig_Postfix
        {
            [HarmonyPostfix]
            public static void CamPostfix(CameraMapConfig __instance) => __instance.minSize = allZoomerSetting.minimumZoom;

        }  

              
    }
}
