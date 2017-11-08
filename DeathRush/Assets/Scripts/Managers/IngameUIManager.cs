using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class IngameUIManager : Manager
{
    public Image speedpmeterNeedleImage;
    public Text lapsText, killFeedText, damageControlText;
    public Color endRaceColor, enemiesColor, playerColor, destroyedColor;
    public GameObject textPositionContainer;
    public float killFeedTimer;
    public List<string> killFeedStrings;
    [HideInInspector]
    public bool showDamagecontrolCountdown;

    private float _playerSpeed, _killFeedTimer;
    private Vector3 _playerSpeedometerRotation;
    private int _playerLaps;
    private List<Vehicle> _racerList;
    private Dictionary<int, string> _endRacerList, _destroyedRacers;
    private string _positionsTextString;
    private List<Text> positionsText;
    private PlayerData _playerData;
    private float _currentSpeed;
    private float _topSpeed;
    private void Start()
    {
        _racerList = new List<Vehicle>();
        _racerList.AddRange(GameObject.FindGameObjectWithTag(K.TAG_VEHICLES).GetComponentsInChildren<Vehicle>());
        positionsText = new List<Text>();
        positionsText.AddRange(textPositionContainer.GetComponentsInChildren<Text>());
        _endRacerList = new Dictionary<int, string>();
        _destroyedRacers = new Dictionary<int, string>();
        _playerData = PlayerData.instance;
        OnInit();
    }

    protected override void OnInit()
    {
        foreach (var text in positionsText) text.gameObject.SetActive(false);
        for (int i = 0; i < _racerList.Count; i++) positionsText[i].gameObject.SetActive(true);
        foreach (var racer in _racerList)
        {
            racer.OnDeath += VehicleDestroyed;
            racer.OnDeath += DisableVehicleScripts;
            racer.OnRaceFinished += VehicleRaceFinished;
            racer.OnRaceFinished += DisableVehicleScripts;
            if (racer.GetComponent<InputControllerPlayer>())
            {
                racer.OnSpeedCheck += VehiclePlayerSpeed;
                racer.OnLapCount += VehiclePlayerLaps;
            }
        }
    }

    /// <summary>
    /// Vehiculo fue destruido.
    /// </summary>
    /// <param name="v">Vehiculo</param>
    private void VehicleDestroyed(Vehicle v)
    {
        if (!_destroyedRacers.ContainsKey(v.uID))
        {
            v.OnDeath -= VehicleDestroyed;
            _destroyedRacers[v.uID] = v.vehicleVars.vehicleName;
        }
    }

    /// <summary>
    /// Vehiculo finalizo la carrera.
    /// </summary>
    /// <param name="v">Vehiculo</param>
    private void VehicleRaceFinished(Vehicle v)
    {
        if (!_endRacerList.ContainsKey(v.uID))
        {
            v.OnRaceFinished -= VehicleRaceFinished;
    //        v.GetComponentInChildren<Collider>().enabled = false;
            _endRacerList[v.uID] = v.vehicleVars.vehicleName;
        }
    }

    /// <summary>
    /// Deshabilia InputController, VehicleController, VehicleData
    /// </summary>
    /// <param name="v">Vehiculo para deshabilitar</param>
    private void DisableVehicleScripts(Vehicle v)
    {
      /*  foreach (var col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }*/
        v.GetComponent<InputController>().enabled = false;
        //v.GetComponent<VehicleData>().enabled = false;
        //v.GetComponent<Vehicle>().enabled = false;
        v.GetComponent<Rigidbody>().drag = 1;
        v.OnRaceFinished -= DisableVehicleScripts;
    }

    /// <summary>
    /// Informa la velocidad del vehiculo del jugador.
    /// </summary>
    /// <param name="v">Vehiculo del jugador</param>
    private void VehiclePlayerSpeed(Vehicle v)
    {
        _currentSpeed = v.currentSpeed;
        _topSpeed = v.vehicleVars.topSpeed;
        
        var auxVelZ = v.currentSpeed;
        var rnd = UnityEngine.Random.Range(-.5f, .5f);
        if (auxVelZ < 0)
        {
            auxVelZ *= -1;
        }
        if (auxVelZ * K.MPS_TO_MPH_MULTIPLIER > v.vehicleVars.topSpeed)
        {
            _playerSpeed = (v.vehicleVars.topSpeed + rnd) / K.SPEEDOMETER_MAX_SPEED;
        }
        else
        {
            _playerSpeed = ((auxVelZ + rnd) * K.MPS_TO_MPH_MULTIPLIER) / K.SPEEDOMETER_MAX_SPEED;
        }
    }

    /// <summary>
    /// Informa la cantidad de vueltas del jugador.
    /// </summary>
    /// <param name="v">Vehiculo del jugador</param>
    private void VehiclePlayerLaps(Vehicle v)
    {
        _playerLaps = Mathf.FloorToInt(v.lapCount);
    }

    private void Update()
    {
        SortRacerList(_racerList);
        UpdatePlayerLaps();
        UpdatePlayerSpeedometer();
        UpdateKillFeed();
        UpdateTextPositions();
        UpdateCountdownText();
    }

    public void SetDamageControlCountdownText(float timer)
    {
        damageControlText.text = timer.ToString("0.00");
    }

    private void UpdateCountdownText()
    {
        if (!showDamagecontrolCountdown)
        {
            damageControlText.gameObject.SetActive(false);
            return;
        }
        damageControlText.gameObject.SetActive(true);
    }

    public void SetKillFeedText(int killCount)
    {
        if (killCount < killFeedStrings.Count)
        {
            killFeedText.text = killFeedStrings[killCount];
            _killFeedTimer = killFeedTimer;
        }
    }

    private void UpdateKillFeed()
    {
        if (_killFeedTimer > 0)
        {
            _killFeedTimer -= Time.deltaTime;
            killFeedText.gameObject.SetActive(true);
        }
        else
        {
            killFeedText.gameObject.SetActive(false);
            _killFeedTimer = 0;
        }
    }

    private void UpdateTextPositions()
    {
        for (int i = 0; i < _racerList.Count; i++)
        {
            if (_racerList[i].hasEnded)
                positionsText[i].text = "<color=" + ColorTypeConverter.ToRGBHex(endRaceColor) + ">" + (i + 1) + ". " + _racerList[i].vehicleVars.vehicleName + "</color>";
            else if (_racerList[i].isDestroyed)
                positionsText[i].text = "<color=" + ColorTypeConverter.ToRGBHex(destroyedColor) + ">" + (i + 1) + ". " + _racerList[i].vehicleVars.vehicleName + "</color>";
            else if (_racerList[i].GetComponent<VehiclePlayerController>())
                positionsText[i].text = "<color=" + ColorTypeConverter.ToRGBHex(playerColor) + ">" + (i + 1) + ". " + _racerList[i].vehicleVars.vehicleName + "</color>";
            else
                positionsText[i].text = "<color=" + ColorTypeConverter.ToRGBHex(enemiesColor) + ">" + (i + 1) + ". " + _racerList[i].vehicleVars.vehicleName + "</color>";
            positionsText[i].GetComponentInChildren<Image>().sprite = Country.flagSprite[_racerList[i].GetComponent<VehicleData>().country];
        }
    }

    /// <summary>
    /// Obtener la posicion del jugador.
    /// </summary>
    /// <returns>Posicion del jugador al finalizar la carrera</returns>
    public int GetFinalPlayerPosition()
    {
        int index = 0;

        for (int i = 0; i < _racerList.Count; i++)
            if (_racerList[i].GetComponent<VehiclePlayerController>()) index = i;
        return index;
    }

    private void UpdatePlayerLaps()
    {
        if (_playerLaps < K.MAX_LAPS)
        {
            lapsText.text = "Laps " + (_playerLaps + 1) + "/" + K.MAX_LAPS;
            lapsText.GetComponentsInChildren<Text>()[1].text = lapsText.text;
        }
    }

    private void UpdatePlayerSpeedometer()
    {
        var needleRotation = Mathf.Lerp(K.SPEEDOMETER_MIN_ANGLE, -55, _currentSpeed / _topSpeed);
        speedpmeterNeedleImage.transform.eulerAngles = new Vector3(speedpmeterNeedleImage.transform.eulerAngles.x, speedpmeterNeedleImage.transform.eulerAngles.y, needleRotation);

  /*     _playerSpeedometerRotation.z = (_playerSpeed * K.SPEEDOMETER_MAX_ANGLE) + K.SPEEDOMETER_MIN_ANGLE;
       print(_playerSpeedometerRotation);
       speedpmeterNeedleImage.transform.localEulerAngles = _playerSpeedometerRotation;*/
    }

    public void GiveListData(List<Vehicle> t)
    {
        foreach (var item in _racerList)
        {
            t.Add(item);
        }
    }

    /// <summary>
    /// Ordeno de mayor a menor el valor de la posicion de cada corredor.
    /// </summary>
    /// <param name="_rL">Lista de corredores</param>
    private void SortRacerList(List<Vehicle> _rL)
    {
        for (int i = 0; i < _rL.Count - 1; i++)
        {
            if (_rL[i].hasEnded) continue;
            for (int j = i + 1; j < _rL.Count; j++)
            {
                if (_rL[j].lapCount > _rL[i].lapCount)
                {
                    var aux = _rL[i];
                    _rL[i] = _rL[j];
                    _rL[j] = aux;
                }
                else if (_rL[j].lapCount == _rL[i].lapCount && _rL[j].positionWeight < _rL[i].positionWeight)
                {
                    var aux = _rL[i];
                    _rL[i] = _rL[j];
                    _rL[j] = aux;
                }
            }
        }
        for (int i = 0; i < _rL.Count; i++)
        {
            _rL[i].setRacerPosition(i + 1);
        }
    }

    public void OnRestartButtonClicked()
    {
        _playerData.isRaceFinished = true;
        SceneManager.LoadScene((int)SCENES_NUMBER.ScenesMenu);
    }
}
