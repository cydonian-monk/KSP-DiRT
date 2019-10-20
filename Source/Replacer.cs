/*
 * Copyright © 2018-2019 Andrew Cummings (Cydonian Monk)
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
using System.Collections.Generic;
using UnityEngine;

namespace DiRT
{
    internal class Replacer
    {
        // General texture replacements.
        internal static readonly List<string> texturePaths = new List<string> { DiRT.REPLACE_TEXTURES };
        internal static readonly List<string> exportTextures = new List<string> { };
        internal static readonly Dictionary<string, Texture2D> mappedTextures = new Dictionary<string, Texture2D>();

        // Print material/texture names when performing texture replacement pass.
        public static bool doExportTextureNames = false;
        public static readonly int BUMPMAP_PROPERTY = Shader.PropertyToID("_BumpMap");

        // Instance.
        public static Replacer instance = null;

        // General texture replacement step.
        void replaceTextures()
        {
            foreach (Material material in Resources.FindObjectsOfTypeAll<Material>())
            {
                // Only get the main texture if this object has one.
                if (material.HasProperty(Shader.PropertyToID("_MainTex")))
                {
                    Texture texture = material.mainTexture;

                    if (texture == null || texture.name.Length == 0 || texture.name.StartsWith("Temp", StringComparison.Ordinal))
                        continue;

                    Texture2D newTexture;
                    mappedTextures.TryGetValue(texture.name, out newTexture);

                    if (newTexture != null)
                    {
                        if (newTexture != texture)
                        {
                            newTexture.anisoLevel = texture.anisoLevel;
                            newTexture.wrapMode = texture.wrapMode;

                            material.mainTexture = newTexture;
                            UnityEngine.Object.Destroy(texture);

                            DiRT.log("Replaced texture " + material.mainTexture.name);
                        }
                    }
                }

                if (material.HasProperty(BUMPMAP_PROPERTY))
                {
                    Texture normalMap = material.GetTexture(BUMPMAP_PROPERTY);
                    if (normalMap == null)
                        continue;
                    Texture2D newNormalMap;
                    mappedTextures.TryGetValue(normalMap.name, out newNormalMap);

                    if (newNormalMap != null)
                    {
                        if (newNormalMap != normalMap)
                        {
                            newNormalMap.anisoLevel = normalMap.anisoLevel;
                            newNormalMap.wrapMode = normalMap.wrapMode;

                            material.SetTexture(BUMPMAP_PROPERTY, newNormalMap);
                            UnityEngine.Object.Destroy(normalMap);

                            DiRT.log("Replaced normalMap " + newNormalMap.name);
                        }
                    }
                }
            }
        }

        // Export the texture names.
        public void exportTextureNames()
        {
            foreach (Material material in Resources.FindObjectsOfTypeAll<Material>())
            {
                // Only get the main texture if this object has one.
                if (material.HasProperty(Shader.PropertyToID("_MainTex")))
                {
                    Texture texture = material.mainTexture;
                    if (texture != null && texture.name.Length > 0 && (!texture.name.StartsWith("Temp", StringComparison.Ordinal)))
                        exportTextures.Add(material.name + " : '" + texture.name + "' (" + texture.width + " x " + texture.height + ")");
                }

                // Attempt to get the NormalMap.
                if (material.HasProperty(BUMPMAP_PROPERTY))
                {
                    Texture normalMap = material.GetTexture(BUMPMAP_PROPERTY);
                    if (normalMap != null && normalMap.name.Length > 0 && (!normalMap.name.StartsWith("Temp", StringComparison.Ordinal)))
                        exportTextures.Add("(NormalMap) " + material.name + " : '" + normalMap.name + "' (" + normalMap.width + " x " + normalMap.height + ")");
                }
            }

            // Write the texture names out to a file in our plugin's folder.
            ConfigNode configFile = new ConfigNode();
            configFile.AddValue("Format", "MaterialName : 'TextureName' (width x height)");
            configFile.AddValue("------", "---------------------------------------------");

            foreach (String texEntry in exportTextures)
            {
                configFile.AddValue("Texture", texEntry);                
            }
            configFile.Save(KSPUtil.ApplicationRootPath + "GameData/" + DiRT.DIRT_DIR + "ExportedTextureList.txt");

            doExportTextureNames = false;
            return;
        }

        // Read configuration and perform pre-load initialisation.
        public void readConfig(ConfigNode configFile)
        {
            string newPath = configFile.GetValue("TextureFolder");
            if ((null != newPath)
             && (!texturePaths.Contains(newPath)))
            {
                texturePaths.Add(newPath);
                DiRT.log("Added texture path: " + newPath);
            }
            string exportNode = configFile.GetValue("exportTextureNames");
            if ((null != exportNode)
             && (exportNode.ToLower().Equals("true")))
            {
                DiRT.log("Will export texture names.");
                doExportTextureNames = true;
            }
        }

        // Post-load initialization.
        public void load()
        {
            foreach (GameDatabase.TextureInfo texInfo in GameDatabase.Instance.databaseTexture)
            {
                Texture2D texture = texInfo.texture;
                if (texture == null)
                    continue;

                foreach (string path in texturePaths)
                {
                    if (!texture.name.StartsWith(path, StringComparison.Ordinal))
                        continue;

                    string originalName = texture.name.Substring(path.Length);

                    // Prevent collisions. The first one in the list wins.
                    if (!mappedTextures.ContainsKey(originalName))
                    {
                        if (originalName.StartsWith("GalaxyTex_", StringComparison.Ordinal))
                            texture.wrapMode = TextureWrapMode.Clamp;

                        mappedTextures.Add(originalName, texture);
                    }
                }
            }        
        }

        public void beginScene()
        {
            replaceTextures();
        }
    }
}
