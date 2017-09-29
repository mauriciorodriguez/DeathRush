using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class ScreenGameOver : ScreenView
{
    public BottomMenu bottomMenu;
    public Text gameoverText;

    private PlayerData _playerData;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        PlayerPrefs.DeleteAll();
        if (_playerData.countryChaos.Keys.Where(x => x != _playerData.countryType).ToList().Aggregate(0, (a, c) => a += _playerData.countryChaos[c]) >= 500) gameoverText.text = "You Win";
        else gameoverText.text = "You Lose";
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }
}
