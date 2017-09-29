using UnityEngine;
using System.Collections;

public class ShakeCamera : MonoBehaviour
{
    public bool Shaking;
    private float ShakeDecay;
    private float ShakeIntensity;

    private Vector3 OriginalPos;
    private Quaternion _defaultRot;
    void Start()
    {
        Shaking = false;
    }

    void Update()
    {
        if (ShakeIntensity > 0)
        {
            //transform.localPosition = OriginalPos + Random.insideUnitSphere * ShakeIntensity;
            transform.localRotation = new Quaternion(_defaultRot.x + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            _defaultRot.y + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            _defaultRot.z + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f,
                                            _defaultRot.w + Random.Range(-ShakeIntensity, ShakeIntensity) * .2f);

            ShakeIntensity -= ShakeDecay;
        }
        else if (Shaking)
        {
            Shaking = false;
            transform.localRotation = _defaultRot;

        }
    }

    /// <summary>
    /// Aplica un efecto de shake ala camara
    /// </summary>
    public void DoShake()
    {
        //OriginalPos = Vector3.zero;//transform.position;

        _defaultRot = transform.localRotation;

        ShakeIntensity = 0.15f;
        ShakeDecay = 0.02f;
        Shaking = true;
    }
}