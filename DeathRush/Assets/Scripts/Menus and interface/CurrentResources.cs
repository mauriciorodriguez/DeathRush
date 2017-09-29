using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CurrentResources : MonoBehaviour
{
    private PlayerData _playerData;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
    }

    void Update()
    {
        GetComponent<Text>().text = "Resources: " + _playerData.resources;
    }
}
