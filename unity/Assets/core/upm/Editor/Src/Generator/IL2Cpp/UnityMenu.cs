/*
* Tencent is pleased to support the open source community by making Puerts available.
* Copyright (C) 2020 THL A29 Limited, a Tencent company.  All rights reserved.
* Puerts is licensed under the BSD 3-Clause License, except for the third-party components listed in the file 'LICENSE' which may be subject to their corresponding license terms. 
* This file is subject to the terms and conditions defined in file 'LICENSE', which is part of this source code package.
*/

#if UNITY_2020_1_OR_NEWER
using System.Reflection;
using System.IO;
using System;
#if !PUERTS_GENERAL
using UnityEditor;
using UnityEngine;
#endif

#if !PUERTS_GENERAL && !UNITY_WEBGL && PUERTS_IL2CPP_OPTIMIZATION
namespace PuertsIl2cpp.Editor
{
    namespace Generator {

        public class UnityMenu {
            [MenuItem(Puerts.Editor.Generator.UnityMenu.PUERTS_MENU_PREFIX + "/Generate For xIl2cpp mode (all in one)", false, 2)]
            public static void GenV2() {
                PuertsIl2cpp.Editor.Generator.UnityMenu.GenerateCppWrappers();
                PuertsIl2cpp.Editor.Generator.UnityMenu.GenerateExtensionMethodInfos();
                PuertsIl2cpp.Editor.Generator.UnityMenu.GenerateLinkXML();
                PuertsIl2cpp.Editor.Generator.UnityMenu.GenerateCppPlugin();
                Puerts.Editor.Generator.UnityMenu.GenRegisterInfo();
            }

            [MenuItem(Puerts.Editor.Generator.UnityMenu.PUERTS_MENU_PREFIX + "/Generate/il2cpp c file", false, 6)]
            public static void GenerateCppPlugin()
            {
                var start = DateTime.Now;
#if CPP_OUTPUT_TO_NATIVE_SRC
                var saveTo = Path.Combine(Application.dataPath, "core/upm/Plugins/puerts_il2cpp/");
#elif PUERTS_CPP_OUTPUT_TO_UPM
                var saveTo = Path.Combine(Path.GetFullPath("Packages/com.tencent.puerts.core/"), "Plugins/puerts_il2cpp/");
#else
                var saveTo = Path.Combine(Puerts.Configure.GetCodeOutputDirectory(), "Plugins/puerts_il2cpp/");
#endif
                Directory.CreateDirectory(saveTo);
                FileExporter.CopyXIl2cppCPlugin(saveTo);
                FileExporter.GenMarcoHeader(saveTo);
                Debug.Log("finished! use " + (DateTime.Now - start).TotalMilliseconds + " ms Outputed to " + saveTo);
            }

            [MenuItem(Puerts.Editor.Generator.UnityMenu.PUERTS_MENU_PREFIX + "/Generate/il2cpp wrapper bridge", false, 6)]
            public static void GenerateCppWrappers()
            {   
                var start = DateTime.Now;
#if CPP_OUTPUT_TO_NATIVE_SRC
                var saveTo = Path.Combine(Application.dataPath, "core/upm/Plugins/puerts_il2cpp/");
#elif PUERTS_CPP_OUTPUT_TO_UPM
                var saveTo = Path.Combine(Path.GetFullPath("Packages/com.tencent.puerts.core/"), "Plugins/puerts_il2cpp/");
#else
                var saveTo = Path.Combine(Puerts.Configure.GetCodeOutputDirectory(), "Plugins/puerts_il2cpp/");
#endif

                Directory.CreateDirectory(saveTo);
                FileExporter.GenCPPWrap(saveTo);
                Debug.Log("finished! use " + (DateTime.Now - start).TotalMilliseconds + " ms Outputed to " + saveTo);
            }
            
            [MenuItem(Puerts.Editor.Generator.UnityMenu.PUERTS_MENU_PREFIX + "/Generate/il2cpp wrapper bridge(Configure)", false, 6)]
            public static void GenerateCppWrappersInConfigure()
            {
                var start = DateTime.Now;
#if CPP_OUTPUT_TO_NATIVE_SRC
                var saveTo = Path.Combine(Application.dataPath, "core/upm/Plugins/puerts_il2cpp/");
#elif PUERTS_CPP_OUTPUT_TO_UPM
                var saveTo = Path.Combine(Path.GetFullPath("Packages/com.tencent.puerts.core/"), "Plugins/puerts_il2cpp/");
#else
                var saveTo = Path.Combine(Puerts.Configure.GetCodeOutputDirectory(), "Plugins/puerts_il2cpp/");
#endif

                Directory.CreateDirectory(saveTo);
                FileExporter.GenCPPWrap(saveTo, true);
                Debug.Log("finished! use " + (DateTime.Now - start).TotalMilliseconds + " ms Outputed to " + saveTo);
            }

            [MenuItem(Puerts.Editor.Generator.UnityMenu.PUERTS_MENU_PREFIX + "/Generate/il2cpp ExtensionMethodInfos_Gen.cs", false, 6)]
            public static void GenerateExtensionMethodInfos()
            {
                var start = DateTime.Now;
                var saveTo = Puerts.Configure.GetCodeOutputDirectory();

                Directory.CreateDirectory(saveTo);
                FileExporter.GenExtensionMethodInfos(saveTo);
                Debug.Log("finished! use " + (DateTime.Now - start).TotalMilliseconds + " ms Outputed to " + saveTo);
                AssetDatabase.Refresh();
            }

            [MenuItem(Puerts.Editor.Generator.UnityMenu.PUERTS_MENU_PREFIX + "/Generate/Link.xml", false, 6)]
            public static void GenerateLinkXML()
            {
                var start = DateTime.Now;
                var saveTo = Puerts.Configure.GetCodeOutputDirectory();
                Directory.CreateDirectory(saveTo);
                FileExporter.GenLinkXml(saveTo);
                Debug.Log("finished! use " + (DateTime.Now - start).TotalMilliseconds + " ms Outputed to " + saveTo);
                AssetDatabase.Refresh();
            }


        }
    }
}
#endif
#endif