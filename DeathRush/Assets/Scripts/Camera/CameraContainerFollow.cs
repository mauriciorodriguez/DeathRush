using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CameraContainerFollow : MonoBehaviour
{
    public VehicleVars.Type vehicleType;
    public Transform target = null;
    public float rotationDamping = 3f;
    public float speedBackView;
    public float distance = 7;
    public bool frontView;
    public float raycastDistance, height;

    private float _distanceHeight;
    private Rigidbody _rbTarget;
    public float vehicleAngleToRotate;
    private float rotationX;
    private bool _canRotate;

    /*private float _minDistance;
    private float _maxDistance;*/

    public void Init()
    {
        frontView = true;
        _rbTarget = target.GetComponent<Rigidbody>();
        transform.position = target.position + Vector3.up * height;
        _distanceHeight = transform.position.y - target.position.y;
        GetComponentInChildren<VehicleCamera>().Init(target.transform);
        // _minDistance = Vector3.Distance(transform.position, target.transform.position /*+ Vector3.up * 2f*/);
        //_maxDistance = _minDistance - 1f;*/
        // _crosshairFixedZPostion = new Vector3(Input.mousePosition.x,Input.mousePosition.y,0);
    }

    void Update()
    {
        if (!target) return;
        float speed = (_rbTarget.transform.InverseTransformDirection(_rbTarget.velocity).z) * K.MPS_TO_MPH_MULTIPLIER;
        float speedFactor = Mathf.Clamp01(speed / target.GetComponent<Vehicle>().vehicleVars.topSpeed);

        //float currentDistance = Mathf.Lerp(_minDistance, _maxDistance, speedFactor);

        //Calcula los angulos de rotación actuales
        float targetRotationAngle = target.eulerAngles.y;
        float currentRotationAngle = transform.eulerAngles.y;

        // Rotación de camara en marcha atrás.
        if (!frontView)
        {
            targetRotationAngle += 180;
            //Damp de la rotación en el eje Y.
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, speedBackView * Time.deltaTime);
        }

        //Damp de la rotación en el eje Y.
        else currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, targetRotationAngle, rotationDamping * Time.deltaTime);


        //Rota eje X en situaciones de vehículo inclinado.

        if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.InsideTheCore)
        {
            if (target.transform.localEulerAngles.x > vehicleAngleToRotate - 10 && target.transform.localEulerAngles.x < 90) _canRotate = true;
            else _canRotate = false;

            if (_canRotate && rotationX >= -0.5 && rotationX < vehicleAngleToRotate) rotationX += Time.deltaTime * 20;
            else if (!_canRotate && rotationX > 0) rotationX -= Time.deltaTime * 20;
        }


        //Convierte el angulo a rotación.
        Quaternion currentRotation = Quaternion.Euler(rotationX, currentRotationAngle, 0);

        //Altura de la camara.
        transform.position = target.position + new Vector3(0, _distanceHeight, 0);
        transform.position -= currentRotation * Vector3.forward * distance;


        transform.LookAt(target.position + Vector3.up * 3);

        RaycastHit hit;

        ray = new Ray(transform.position, transform.forward+Vector3.up*0.2f);
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            var go = hit.collider.gameObject;
            if (go.layer == K.LAYER_OBSTACLE)
            {
                //print(go);
                if (go.GetComponent<AlphaSet>() != null)
                {
                    go.GetComponent<AlphaSet>().ChangeAlpha(transform.position, raycastDistance);
                    /*Color vec = go.GetComponent<Renderer>().material.color;
                    vec.a = Mathf.Abs((Vector3.Distance(transform.position, go.transform.position) / raycastDistance - 0.7f));
                    go.GetComponent<Renderer>().material.color = vec;*/
                }
            }

        }
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
    }

    private Ray ray;

}
