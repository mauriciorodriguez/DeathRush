using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Camera))]
public class DetectiveMode : MonoBehaviour
{
    public Shader detectiveVehicleImageShader;
    public Material postEffectMaterial;
    private RenderTexture _temporaryRenderTexture;
    private Camera _camera;

    void Awake()
    {
        _camera = GetComponent<Camera>();
        if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.ScenesMenu) _camera.SetReplacementShader(detectiveVehicleImageShader, "ImageVehicle");
        if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.DesertTrack) _camera.SetReplacementShader(detectiveVehicleImageShader, "Outline");
    }
    void Update()
    {

     //    _camera.clearFlags = CameraClearFlags.Depth;


        /*  _camera.SetReplacementShader(detectiveVehicleImageShader, "ImageVehicle");
          _temporaryRenderTexture = RenderTexture.GetTemporary(Screen.width, Screen.height);
          _camera.Render();*/

    }
    /*
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (_temporaryRenderTexture == null) return;
        _temporaryRenderTexture.wrapMode = TextureWrapMode.Repeat;
        postEffectMaterial.SetTexture("_DetectiveMap", _temporaryRenderTexture);
        Graphics.Blit(src, dst, postEffectMaterial);
    }*/
}
