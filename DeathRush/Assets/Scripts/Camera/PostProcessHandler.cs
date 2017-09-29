using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PostProcessHandler : MonoBehaviour
{
    public Material material;
    private float fadeValue;

    void Start()
    {
        fadeValue = 0;
        if (material != null) material.SetFloat("_FadeEffect", 0);
    }

    void Update()
    {
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (material == null) Debug.LogError("Setear Material");
        Graphics.Blit(src, dst, material);
    }

    public void FadeColor(bool fade)
    {
        if (fade)
        {
            if (material.GetFloat("_FadeEffect") < 0.5)
            {
                fadeValue += Time.deltaTime / 4;
                material.SetFloat("_FadeEffect", fadeValue);
            }
        }
        else
        {
            if (material.GetFloat("_FadeEffect") > 0)
            {
                fadeValue -= Time.deltaTime / 7;
                material.SetFloat("_FadeEffect", fadeValue);
            }
        }
    }
}
