using System.Collections;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    // Custom shader props (se existirem)
    private static readonly int ReactionColorProp = Shader.PropertyToID("_ReactionColor");
    private static readonly int LerpFactorProp = Shader.PropertyToID("_LerpFactor");

    // Standard / URP fallback props
    private static readonly int ColorProp = Shader.PropertyToID("_Color");       // Standard
    private static readonly int BaseColorProp = Shader.PropertyToID("_BaseColor"); // URP Lit

    private MaterialPropertyBlock mpb;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    public void ApplyReaction(Color finalColor, float duration = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateToColor(finalColor, duration));
    }

    public void AbsorbIndicator(Color indicatorColor, float duration = 0.25f)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateToColor(indicatorColor, duration));
    }

    private IEnumerator AnimateToColor(Color targetColor, float duration)
    {
        if (rend == null) yield break;

        // Garante alpha visível (exceto se você QUISER transparente)
        if (targetColor.a <= 0.001f)
            targetColor.a = 1f;

        float t = 0f;

        // Pega cor atual (fallback)
        Color startColor = GetCurrentColor();

        while (t < duration)
        {
            t += Time.deltaTime;
            float v = Mathf.Clamp01(t / duration);
            Color c = Color.Lerp(startColor, targetColor, v);
            SetColor(c);
            yield return null;
        }

        SetColor(targetColor);
    }

    public void Clear()
    {
        // “limpar” = transparente
        SetColor(new Color(1f, 1f, 1f, 0f));
    }

    private Color GetCurrentColor()
    {
        var mat = rend.sharedMaterial;
        if (mat == null) return Color.white;

        if (mat.HasProperty(ColorProp)) return mat.GetColor(ColorProp);
        if (mat.HasProperty(BaseColorProp)) return mat.GetColor(BaseColorProp);

        // se estiver usando shader custom, não dá pra ler fácil via MPB
        return Color.white;
    }

    private void SetColor(Color c)
    {
        var mat = rend.sharedMaterial;
        if (mat == null) return;

        // 1) Se o shader custom existir, usa ele
        if (mat.HasProperty(ReactionColorProp) && mat.HasProperty(LerpFactorProp))
        {
            rend.GetPropertyBlock(mpb);
            mpb.SetColor(ReactionColorProp, c);
            mpb.SetFloat(LerpFactorProp, 1f);
            rend.SetPropertyBlock(mpb);
            return;
        }

        // 2) Fallback Standard
        if (mat.HasProperty(ColorProp))
        {
            rend.GetPropertyBlock(mpb);
            mpb.SetColor(ColorProp, c);
            rend.SetPropertyBlock(mpb);
            return;
        }

        // 3) Fallback URP Lit
        if (mat.HasProperty(BaseColorProp))
        {
            rend.GetPropertyBlock(mpb);
            mpb.SetColor(BaseColorProp, c);
            rend.SetPropertyBlock(mpb);
            return;
        }
    }
}