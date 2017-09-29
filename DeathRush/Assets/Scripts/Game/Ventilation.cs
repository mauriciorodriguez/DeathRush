using UnityEngine;
using System.Collections;

public class Ventilation : MonoBehaviour {

    public float speed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        transform.Rotate(0, 45 * Time.deltaTime * speed, 0);
	}
}
