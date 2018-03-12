/*
 * Copyright © 2018 Andrew Cummings (Cydonian Monk)
 * Copyright © 2013-2015 Davorin Učakar
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace DiRT
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class DiRT : MonoBehaviour
    {
        public static bool isReplacerLoaded = false;

        public static readonly string DIRT_DIR = "DiRT/";
        public static readonly string REPLACE_TEXTURES = DIRT_DIR + "Textures/";
        public static readonly string DIRT_CONFIG = "DiRT_Config";

   		private UrlDir.UrlConfig[] myConfigs;

        public static void log(string s, params object[] args)
        {
            Type callerClass = new StackTrace(1, false).GetFrame(0).GetMethod().DeclaringType;
            UnityEngine.Debug.Log("[DiRT." + callerClass.Name + "] " + String.Format(s, args));
        }

        public void Awake()
        {
            DontDestroyOnLoad(this);
            DiRT.log("Awake {0}", Assembly.GetExecutingAssembly().GetName().Version);

            Replacer.instance = new Replacer();

            myConfigs = GameDatabase.Instance.GetConfigs(DIRT_CONFIG);
            if (myConfigs.Length < 1)
                DiRT.log("No DiRT Config found. Using default path: " + REPLACE_TEXTURES);
            foreach (UrlDir.UrlConfig configFile in myConfigs)
            {
                Replacer.instance.readConfig(configFile.config);
            }
            if (Replacer.doExportTextureNames)
                Replacer.instance.exportTextureNames();    
        }

        public void Start()
        {
            DiRT.log("Start {0}", Assembly.GetExecutingAssembly().GetName().Version);
            if (AssemblyLoader.loadedAssemblies.Any(a => (a.name.Contains("TextureReplacer") || a.name.Contains("Texture Replacer Replaced"))))
            {
                DiRT.log("Either TextureReplacerReplaced or TextureReplacer was found; Skipping DiRT Replacer.");
            }
            else
            {
                Replacer.instance.load();
                isReplacerLoaded = true;
            }
        }     
    }
}
