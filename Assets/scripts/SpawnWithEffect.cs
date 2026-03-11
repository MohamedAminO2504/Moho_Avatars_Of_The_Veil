using UnityEngine;
using UnityEngine.VFX;

public class AutoSpawnEffect : MonoBehaviour
{
    public VisualEffect spawnVFX;
    public float delayBeforeVisible = 0.3f;

    void Start()
    {
        PlaySpawn();
    }

    void PlaySpawn()
    {
        if (spawnVFX != null)
        {
            spawnVFX.transform.position = transform.position;
            spawnVFX.SendEvent("in");
        }

        StartCoroutine(EnableAfterDelay());
    }

    System.Collections.IEnumerator EnableAfterDelay()
    {
        SetRenderers(false);
        yield return new WaitForSeconds(delayBeforeVisible);
        SetRenderers(true);
    }

    void SetRenderers(bool state)
    {
        foreach (var r in GetComponentsInChildren<Renderer>())
            r.enabled = state;
    }
}