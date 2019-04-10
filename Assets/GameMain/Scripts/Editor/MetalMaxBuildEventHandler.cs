﻿using GameFramework;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor.AssetBundleTools;

namespace MetalMax.Editor
{
    public sealed class MetalMaxBuildEventHandler : IBuildEventHandler
    {
        public bool ContinueOnFailure
        {
            get
            {
                return false;
            }
        }

        public void PreprocessAllPlatforms(string productName, string companyName, string gameIdentifier,
            string applicableGameVersion, int internalResourceVersion, string unityVersion, BuildAssetBundleOptions buildOptions, bool zip,
            string outputDirectory, string workingPath, string outputPackagePath, string outputFullPath, string outputPackedPath, string buildReportPath)
        {
            string streamingAssetsPath = Utility.Path.GetCombinePath(Application.dataPath, "StreamingAssets");
            string[] fileNames = Directory.GetFiles(streamingAssetsPath, "*", SearchOption.AllDirectories);
            foreach (string fileName in fileNames)
            {
                if (fileName.Contains(".gitkeep"))
                {
                    continue;
                }

                File.Delete(fileName);
            }

            Utility.Path.RemoveEmptyDirectory(streamingAssetsPath);
        }

        public void PostprocessAllPlatforms(string productName, string companyName, string gameIdentifier,
            string applicableGameVersion, int internalResourceVersion, string unityVersion, BuildAssetBundleOptions buildOptions, bool zip,
            string outputDirectory, string workingPath, string outputPackagePath, string outputFullPath, string outputPackedPath, string buildReportPath)
        {

        }

        public void PreprocessPlatform(Platform platform, string workingPath, string outputPackagePath, string outputFullPath, string outputPackedPath)
        {

        }

        public void PostprocessPlatform(Platform platform, string workingPath, string outputPackagePath, string outputFullPath, string outputPackedPath, bool isSuccess)
        {
//            if (platform != Platform.Windows)
//            {
//                return;
//            }

            string streamingAssetsPath = Utility.Path.GetCombinePath(Application.dataPath, "StreamingAssets");
            if (!Directory.Exists(streamingAssetsPath))
            {
                Directory.CreateDirectory(streamingAssetsPath);
            }
            string[] fileNames = Directory.GetFiles(outputPackagePath, "*", SearchOption.AllDirectories);
            foreach (string fileName in fileNames)
            {
                string destFileName = Utility.Path.GetCombinePath(streamingAssetsPath, fileName.Substring(outputPackagePath.Length));
                FileInfo destFileInfo = new FileInfo(destFileName);
                if (destFileInfo.Directory != null && !destFileInfo.Directory.Exists)
                {
                    destFileInfo.Directory.Create();
                }

                File.Copy(fileName, destFileName);
            }
        }
    }
}
