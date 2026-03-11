using UnityEngine;

public class DebugMaterialProps : MonoBehaviour
{
    void Start()
    {
        var mat = GetComponent<Renderer>().material;

        foreach (var name in mat.GetTexturePropertyNames())
            Debug.Log("Texture: " + name);

        Debug.Log("---- FLOAT PROPERTIES ----");

        // Test des noms probables
        string[] testNames =
        {
            "_Cutoff",
            "_AlphaClip",
            "_AlphaClipThreshold",
            "_ClipThreshold",
            "_Surface",
        };

        foreach (var n in testNames)
        {
            Debug.Log(n + " exists ? " + mat.HasProperty(n));
        }
    }
}