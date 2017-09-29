using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour
{

    public float expPower;
    public float expRadius;
    public float expDamage;
    public float detonTime;
    protected bool touched;
    protected bool deton;
    public LayerMask layersDamege;
    public GameObject feedback;
    private VehicleData _owner;

    // Use this for initialization
    public virtual void Update()
    {
        if (touched)
        {
            detonTime -= Time.deltaTime;
            if (detonTime <= 0)
                Detonator();
        }
    }

    // Update is called once per frame


    public virtual void Detonator()
    {
        touched = false;
    }

    public void SetOwner(VehicleData veDat)
    {
        _owner = veDat;
    }

    public VehicleData GetOwner() { return _owner; }
}
