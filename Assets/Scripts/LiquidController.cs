using System.Collections;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    private static readonly int ReactionColorProp =
        Shader.PropertyToID("_ReactionColor");
    private static readonly int LerpFactorProp =
        Shader.PropertyToID("_LerpFactor");

    private MaterialPropertyBlock mpb;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();
    }

    // 🔹 USADO NOS FRASCOS (REAÇÃO QUÍMICA)
    public void ApplyReaction(Color finalColor, float duration = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateLerp(finalColor, duration));
    }

    private IEnumerator AnimateLerp(Color finalColor, float duration)
    {
        float t = 0f;

        rend.GetPropertyBlock(mpb);
        mpb.SetColor(ReactionColorProp, finalColor);
        mpb.SetFloat(LerpFactorProp, 0f);
        rend.SetPropertyBlock(mpb);

        while (t < duration)
        {
            t += Time.deltaTime;
            float v = Mathf.Clamp01(t / duration);

            mpb.SetFloat(LerpFactorProp, v);
            rend.SetPropertyBlock(mpb);
            yield return null;
        }

        mpb.SetFloat(LerpFactorProp, 1f);
        rend.SetPropertyBlock(mpb);
    }

    // 🔹 USADO NO CONTA-GOTAS (ABSORÇÃO DO INDICADOR)
    public void AbsorbIndicator(Color indicatorColor, float duration = 0.4f)
    {
        StopAllCoroutines();
        StartCoroutine(AbsorbRoutine(indicatorColor, duration));
    }

    private IEnumerator AbsorbRoutine(Color color, float duration)
    {
        float t = 0f;

        rend.GetPropertyBlock(mpb);
        mpb.SetColor(ReactionColorProp, color);
        mpb.SetFloat(LerpFactorProp, 0f);
        rend.SetPropertyBlock(mpb);

        while (t < duration)
        {
            t += Time.deltaTime;
            float v = Mathf.Clamp01(t / duration);

            mpb.SetFloat(LerpFactorProp, v);
            rend.SetPropertyBlock(mpb);
            yield return null;
        }

        mpb.SetFloat(LerpFactorProp, 1f);
        rend.SetPropertyBlock(mpb);
    }

    // 🔹 LIMPA O CONTA-GOTAS (VOLTA A TRANSPARENTE)
    public void Clear()
    {
        rend.GetPropertyBlock(mpb);
        mpb.SetFloat(LerpFactorProp, 0f);
        rend.SetPropertyBlock(mpb);
    }
}
