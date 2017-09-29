using UnityEngine;
using System.Collections;

public class ContinueControl : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
