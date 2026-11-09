using System.Collections;
using UnityEngine;

public class ZombieDeath : MonoBehaviour
{
    [SerializeField] private Renderer[] zombieRenderers;
    [SerializeField] private float delayBeforeFade = 2.5f;
    [SerializeField] private float fadeDuration = 2f;

    private Material[][] materials;
    private bool isDying;

    private void Awake()
    {
        if (zombieRenderers == null || zombieRenderers.Length == 0)
            zombieRenderers = GetComponentsInChildren<Renderer>();

        materials = new Material[zombieRenderers.Length][];

        for (int i = 0; i < zombieRenderers.Length; i++)
            materials[i] = zombieRenderers[i].materials;
    }

    public void StartDeathSequence()
    {
        if (isDying)
            return;

        isDying = true;
        StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(delayBeforeFade);

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            SetAlpha(Mathf.Lerp(1f, 0f, elapsed / fadeDuration));
            yield return null;
        }

        SetAlpha(0f);
        Destroy(gameObject);
    }

    private void SetAlpha(float alpha)
    {
        foreach (Material[] rendererMaterials in materials)
        {
            foreach (Material material in rendererMaterials)
            {
                if (material.HasProperty("_BaseColor"))
                {
                    Color color = material.GetColor("_BaseColor");
                    color.a = alpha;
                    material.SetColor("_BaseColor", color);
                }
                else if (material.HasProperty("_Color"))
                {
                    Color color = material.GetColor("_Color");
                    color.a = alpha;
                    material.SetColor("_Color", color);
                }
            }
        }
    }
}