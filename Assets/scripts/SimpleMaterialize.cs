using UnityEngine;

public class SimpleMaterialize : MonoBehaviour
{
    public float duration = 1f;

    private Renderer rend;
    private float timer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.sharedMaterial.SetFloat("MaterializeProgress", 0f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);
        rend.sharedMaterial.SetFloat("MaterializeProgress", t);
    }
}