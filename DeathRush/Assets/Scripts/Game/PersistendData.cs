using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PersistendData : MonoBehaviour
{
    public static PersistendData instance;

    private List<VehicleData> _vehiculos;
    private Dictionary<VehicleData, VehicleData> _deathDictionary = new Dictionary<VehicleData, VehicleData>();

    private int _currentkills;
    private int _TotalGameKills;

    void Awake()
    {

        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("CarrerKills")) _TotalGameKills = PlayerPrefs.GetInt("CarrerKills");
    }

    public void SetFinishCar(VehicleData dataCar)
    {
        _vehiculos.Add(dataCar);
    }

    public void KillInfo(VehicleData killer, VehicleData killed)
    {

        _deathDictionary.Add(killed, killer);

        if (killer.gameObject.layer == K.LAYER_PLAYER && killed.gameObject.layer == K.LAYER_IA)
        {
            if (PlayerData.instance.racerList[PlayerData.instance.selectedRacer].unlockedTierTwo == Classes.TypeTierTwo.Bloodthirsty)
            {
                killer.currentLife += (killer.maxLife * .25f);
                killer.currentLife = Mathf.Clamp(killer.currentLife, 0, killer.maxLife);
                killer.CheckHealthBar(true);
            }

            _currentkills++;
            GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<IngameUIManager>().SetKillFeedText(_currentkills);
        }
    }

    public void ReleaseAllInfo()
    {
        _currentkills = 0;
        //_vehiculos.Clear();

        if (_deathDictionary.Count > 0)
            _deathDictionary.Clear();
    }

    public List<VehicleData> FinalCars() { return _vehiculos; }

    public Dictionary<VehicleData, VehicleData> FinalKills()
    {
        return _deathDictionary;
    }

    public float GiveKill()
    {
        _TotalGameKills += _currentkills;
        PlayerPrefs.SetInt("CarrerKills", _TotalGameKills);
        return _currentkills;
    }

}
