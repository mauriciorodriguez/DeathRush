using UnityEngine;
using System.Collections;

public class Dispel : MonoBehaviour
{
    private float duration = 0.5f;
    private float current;
    
	
	// Update is called once per frame
	void Update ()
    {
        if (this.gameObject.activeSelf)
        {
            current += Time.deltaTime;
            if (current >= duration)
            {
                this.gameObject.SetActive(false);
                current = 0;
            }
        }
	
	}
}
