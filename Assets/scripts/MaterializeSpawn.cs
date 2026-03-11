using UnityEngine;

public class MaterializeSpawn : MonoBehaviour
{
    public float duration = 1f;

    private Material mat;
    private float timer;

void Start()
{
    mat = GetComponent<Renderer>().material;

    // IMPORTANT
    mat.EnableKeyword("_ALPHATEST_ON");

    // Force le render type
    mat.SetOverrideTag("RenderType", "TransparentCutout");
    mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;

    mat.SetFloat("_Cutoff", 1f);
}
    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        mat.SetFloat("_Cutoff", 1f - t);
    }
}