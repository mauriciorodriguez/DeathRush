using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WheelEffects : MonoBehaviour
{
    public GameObject SkidTrailPrefab;
    public Material dustSkidTrail;
    public Material pavementSkidTrail;
    public static Transform skidTrailsDetachedParent;
    public bool skidding { get; private set; }
        
    private GameObject _skidTrail;
    private WheelCollider _wheelCollidernRef;

    private void Start()
    {
        _wheelCollidernRef = GetComponent<WheelCollider>();
        if (skidTrailsDetachedParent == null)
        {
            skidTrailsDetachedParent = new GameObject("Skid Trails - Detached").transform;
        }

    }

    /// <summary>
    /// Comienza a dejar huella de la rueda.
    /// </summary>
    public void Skid()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.DesertTrack && SkidTrailPrefab.GetComponent<TrailRenderer>().sharedMaterial != dustSkidTrail)
        {
            if (SkidTrailPrefab.GetComponent<TrailRenderer>().sharedMaterial != dustSkidTrail)
            SkidTrailPrefab.GetComponent<TrailRenderer>().sharedMaterial = dustSkidTrail;
        }

        else if (SkidTrailPrefab.GetComponent<TrailRenderer>().sharedMaterial != pavementSkidTrail) SkidTrailPrefab.GetComponent<TrailRenderer>().sharedMaterial = pavementSkidTrail;     
        if (!skidding)
        {
            StartCoroutine(StartSkidTrail());
        }
    }

    /// <summary>
    /// Corrutina para la huella de la rueda.
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartSkidTrail()
    {
        skidding = true;
        _skidTrail = Instantiate(SkidTrailPrefab);

        while (_skidTrail == null)
        {
            yield return null;
        }
        _skidTrail.transform.parent = transform;
        _skidTrail.transform.localPosition = -Vector3.up * _wheelCollidernRef.radius;
    }

    /// <summary>
    /// Finaliza la huella de la rueda.
    /// </summary>
    public void EndSkidTrail()
    {
        if (!skidding)
        {
            return;
        }
        skidding = false;
        _skidTrail.transform.parent = skidTrailsDetachedParent;
        Destroy(_skidTrail.gameObject, 10);
    }
}