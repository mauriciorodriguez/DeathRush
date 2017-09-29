using UnityEngine;
using System.Collections;

public class TempFix : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        print("granade");
        GameObject granade = PoolManager.instance.molotov.GetObject();
        print(granade);
        granade.transform.position = transform.position + transform.forward * 1;
        granade.transform.rotation = transform.rotation;
        //granade.GetComponent<MolotovBomb>().ActiveMolotov(); //cambio
        granade.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * 01, ForceMode.Impulse);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
