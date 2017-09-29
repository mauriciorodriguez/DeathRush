using UnityEngine;
using System.Collections;

public class AlphaSet : MonoBehaviour
{
    public GameObject rayPoint;
    public float resetDistance;
    private GameObject _player;
    private bool _alphaModified;
    private Color _materialBaseColor;


	// Use this for initialization
	void Start ()
    {
        if(_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        if (GetComponent<Renderer>() != null) _materialBaseColor = GetComponent<Renderer>().material.color;

    }

    public void ChangeAlpha(Vector3 cameraPos, float distance)
    {
        Color vec = GetComponent<Renderer>().material.color;
        vec.a = Mathf.Abs((Vector3.Distance(cameraPos, transform.position) / distance - 0.8f));
        if (vec.a < 0.4f) vec.a = 0.4f;

        GetComponent<Renderer>().material.color = vec;
        if (!_alphaModified) _alphaModified = true;
    }

	// Update is called once per frame
	void Update ()
    {
        if (_alphaModified)
        {
            RaycastHit hit;
            var direction = _player.transform.position - transform.position;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.distance > resetDistance)
                {
                    GetComponent<Renderer>().material.color = _materialBaseColor;
                    _alphaModified = false;
                }
            }
        }
	
	}
}
