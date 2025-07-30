using UnityEngine;
using UnityEditor;
using System.IO;

public class TextureResizer : EditorWindow
{
    private enum ResizeMode { Both, WidthOnly, HeightOnly }

    private ResizeMode resizeMode = ResizeMode.Both;
    private int targetWidth = 256;
    private int targetHeight = 256;

    [MenuItem("Tools/Resize Selected Textures")]
    private static void ShowWindow()
    {
        GetWindow<TextureResizer>("Resize Textures");
    }

    private void OnGUI()
    {
        GUILayout.Label("Resize Selected Textures", EditorStyles.boldLabel);

        resizeMode = (ResizeMode)EditorGUILayout.EnumPopup("Resize Mode", resizeMode);

        if (resizeMode == ResizeMode.Both || resizeMode == ResizeMode.WidthOnly)
            targetWidth = EditorGUILayout.IntField("Width", targetWidth);

        if (resizeMode == ResizeMode.Both || resizeMode == ResizeMode.HeightOnly)
            targetHeight = EditorGUILayout.IntField("Height", targetHeight);

        if (GUILayout.Button("Resize"))
        {
            ResizeTextures();
        }
    }

    private void ResizeTextures()
    {
        Object[] selectedTextures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);

        foreach (Object obj in selectedTextures)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            Texture2D original = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            if (original == null)
            {
                Debug.LogWarning($"Skipped: {path} is not a Texture2D.");
                continue;
            }

            string fullPath = Path.Combine(Application.dataPath.Replace("Assets", ""), path);
            byte[] fileData = File.ReadAllBytes(fullPath);

            Texture2D readableTex = new Texture2D(2, 2);
            readableTex.LoadImage(fileData); // make readable

            int newWidth = targetWidth;
            int newHeight = targetHeight;

            float aspect = (float)readableTex.width / readableTex.height;

            switch (resizeMode)
            {
                case ResizeMode.WidthOnly:
                    newHeight = Mathf.RoundToInt(targetWidth / aspect);
                    break;
                case ResizeMode.HeightOnly:
                    newWidth = Mathf.RoundToInt(targetHeight * aspect);
                    break;
                case ResizeMode.Both:
                    // keep as is, may stretch
                    break;
            }

            Texture2D resizedTex = new Texture2D(newWidth, newHeight, TextureFormat.RGBA32, false);
            resizedTex.SetPixels(ResizeBilinear(readableTex, newWidth, newHeight));
            resizedTex.Apply();

            byte[] pngData = resizedTex.EncodeToPNG();
            if (pngData != null)
            {
                File.WriteAllBytes(fullPath, pngData);
                Debug.Log($"Resized: {path} to {newWidth}x{newHeight}");
            }

            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        AssetDatabase.Refresh();
        Debug.Log("Resize Complete!");
    }

    private Color[] ResizeBilinear(Texture2D source, int newWidth, int newHeight)
    {
        Color[] newPixels = new Color[newWidth * newHeight];
        float ratioX = 1.0f / ((float)newWidth / (source.width - 1));
        float ratioY = 1.0f / ((float)newHeight / (source.height - 1));

        for (int y = 0; y < newHeight; y++)
        {
            int yFloor = Mathf.FloorToInt(y * ratioY);
            float yLerp = y * ratioY - yFloor;

            for (int x = 0; x < newWidth; x++)
            {
                int xFloor = Mathf.FloorToInt(x * ratioX);
                float xLerp = x * ratioX - xFloor;

                Color topLeft = source.GetPixel(xFloor, yFloor);
                Color topRight = source.GetPixel(Mathf.Min(xFloor + 1, source.width - 1), yFloor);
                Color bottomLeft = source.GetPixel(xFloor, Mathf.Min(yFloor + 1, source.height - 1));
                Color bottomRight = source.GetPixel(Mathf.Min(xFloor + 1, source.width - 1), Mathf.Min(yFloor + 1, source.height - 1));

                Color top = Color.Lerp(topLeft, topRight, xLerp);
                Color bottom = Color.Lerp(bottomLeft, bottomRight, xLerp);
                newPixels[y * newWidth + x] = Color.Lerp(top, bottom, yLerp);
            }
        }

        return newPixels;
    }
}