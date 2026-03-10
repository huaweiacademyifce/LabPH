using System.Collections;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    private static readonly int ReactionColorProp = Shader.PropertyToID("_ReactionColor");
    private static readonly int LerpFactorProp = Shader.PropertyToID("_LerpFactor");

    private static readonly int ColorProp = Shader.PropertyToID("_Color");
    private static readonly int BaseColorProp = Shader.PropertyToID("_BaseColor");

    private MaterialPropertyBlock mpb;
    private Renderer rend;

    // ESTADO INICIAL DO SHADER
    private Color initialBaseColor;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    // SALVAR ESTADO INICIAL
    public void SaveInitialState()
    {
        var mat = rend.sharedMaterial;

        if (mat != null && mat.HasProperty(BaseColorProp))
        {
            initialBaseColor = mat.GetColor(BaseColorProp);
        }
    }

    // RESTAURAR ESTADO INICIAL
    public void RestoreInitialState()
    {
        var mat = rend.sharedMaterial;
        if (mat == null) return;

        rend.GetPropertyBlock(mpb);

        if (mat.HasProperty(ReactionColorProp))
            mpb.SetColor(ReactionColorProp, initialBaseColor);

        if (mat.HasProperty(LerpFactorProp))
            mpb.SetFloat(LerpFactorProp, 0f);

        rend.SetPropertyBlock(mpb);
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

        if (targetColor.a <= 0.001f)
            targetColor.a = 1f;

        float t = 0f;
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
        SetColor(new Color(1f, 1f, 1f, 0f));
    }

    private Color GetCurrentColor()
    {
        var mat = rend.sharedMaterial;
        if (mat == null) return Color.white;

        if (mat.HasProperty(ColorProp)) return mat.GetColor(ColorProp);
        if (mat.HasProperty(BaseColorProp)) return mat.GetColor(BaseColorProp);

        return Color.white;
    }

    private void SetColor(Color c)
    {
        var mat = rend.sharedMaterial;
        if (mat == null) return;

        if (mat.HasProperty(ReactionColorProp) && mat.HasProperty(LerpFactorProp))
        {
            rend.GetPropertyBlock(mpb);
            mpb.SetColor(ReactionColorProp, c);
            mpb.SetFloat(LerpFactorProp, 1f);
            rend.SetPropertyBlock(mpb);
            return;
        }

        if (mat.HasProperty(ColorProp))
        {
            rend.GetPropertyBlock(mpb);
            mpb.SetColor(ColorProp, c);
            rend.SetPropertyBlock(mpb);
            return;
        }

        if (mat.HasProperty(BaseColorProp))
        {
            rend.GetPropertyBlock(mpb);
            mpb.SetColor(BaseColorProp, c);
            rend.SetPropertyBlock(mpb);
            return;
        }
    }
    public void ResetToBaseColor()
    {
    if (rend == null)
        rend = GetComponent<Renderer>();

    rend.GetPropertyBlock(mpb);

    // volta para estado inicial do shader
    mpb.SetFloat(LerpFactorProp, 0f);

    rend.SetPropertyBlock(mpb);
    }
}