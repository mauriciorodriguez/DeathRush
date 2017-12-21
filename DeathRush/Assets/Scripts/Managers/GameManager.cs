using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class GameManager : Manager
{
    public Text youWin;
    public Text raceFinishedText;
    public Text youDiedText;
    public Button restartButton;
    public GameObject scoreBackground;
    public Vehicle playerReference { get; private set; }
    public GameObject pauseCanvas;
    public static bool disableShoot = false;
    public GameObject continueAfterRaceControlPrefab;
    public List<GameObject> IAVehiclesPrefabs;

    private List<Vehicle> _enemiesReferences;
    private IngameUIManager _ingameUIManagerReference;
    private bool paused = false;
    private bool _oneTime;
    private bool _gameOver;
    private List<Text> _finalTextPosition;
    private List<GameObject> _playerCameras;
    private PlayerData _playerData;
    private int _posiblePosition;


    private struct IADifficultyStats
    {
        public float maxLifeMultiplier, currentLifeMultiplier, damageMultiplier, topSpeedMultiplier, accelMultiplier;
    }
    private Dictionary<int, IADifficultyStats> difficultyPerCompetitorAmount = new Dictionary<int, IADifficultyStats>()
    {
        { 1, new IADifficultyStats()
            {
                maxLifeMultiplier = .20f,
                damageMultiplier = .20f,
                topSpeedMultiplier = .20f,
                accelMultiplier = .20f
            }
        },
        { 2, new IADifficultyStats()
            {
                maxLifeMultiplier = .15f,
                damageMultiplier = .15f,
                topSpeedMultiplier = .15f,
                accelMultiplier = .15f
            }
        },
        { 3, new IADifficultyStats()
            {
                maxLifeMultiplier = .1f,
                damageMultiplier = .1f,
                topSpeedMultiplier = .1f,
                accelMultiplier = .1f
            }
        },
        { 4, new IADifficultyStats()
            {
                maxLifeMultiplier = .05f,
                damageMultiplier = .05f,
                topSpeedMultiplier = .05f,
                accelMultiplier = .05f
            }
        },
        { 5, new IADifficultyStats()
            {
                maxLifeMultiplier = 0,
                damageMultiplier = 0,
                topSpeedMultiplier = 0,
                accelMultiplier = 0
            }
        }
    };

    private void Awake()
    {
        _playerData = PlayerData.instance;
        _playerCameras = GameObject.FindGameObjectsWithTag(K.TAG_PLAYERCAMERA).ToList();
        _enemiesReferences = new List<Vehicle>();
        InstantiateRacers();
        //EnablePlayerVehicle();

        disableShoot = false;
        _oneTime = false;
        _posiblePosition = 1;

        if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.WaterTomb) Physics.IgnoreLayerCollision(K.LAYER_OBSTACLE,K.LAYER_PLAYER,true);
        else Physics.IgnoreLayerCollision(K.LAYER_OBSTACLE, K.LAYER_PLAYER, false);
    }

    private void InstantiateRacers()
    {
        // Player
        PlayerData.instance.racersQty = 1;
        var vehicleContainer = GameObject.FindGameObjectWithTag(K.TAG_VEHICLES);
        var vehicleSpawnPoints = vehicleContainer.GetComponentsInChildren<Transform>().Where(x => x.tag == K.TAG_SPAWN_POINT).ToList();
        playerReference = Instantiate(_playerData.racerList[_playerData.selectedRacer].racerVehicle.gameObject).GetComponent<Vehicle>();
        playerReference.InstantiateWeapons(_playerData.racerList[_playerData.selectedRacer].equippedPrimaryWeapon, _playerData.racerList[_playerData.selectedRacer].equippedSecondaryWeapon, _playerData.racerList[_playerData.selectedRacer].equippedGadget);
        playerReference.EnableComponents(true);
        int random = UnityEngine.Random.Range(0, vehicleSpawnPoints.Count - 1);
        playerReference.transform.position = vehicleSpawnPoints[random].transform.position;
        playerReference.transform.rotation = vehicleSpawnPoints[random].transform.rotation;
        playerReference.transform.parent = vehicleSpawnPoints[random].transform.parent;
        DestroyImmediate(vehicleSpawnPoints[random].gameObject);
        playerReference.racer = _playerData.racerList[_playerData.selectedRacer];
        playerReference.GetComponent<VehicleData>().country = _playerData.countryType;
        playerReference.GetComponent<VehicleData>().gender = _playerData.racerList[_playerData.selectedRacer].gender;
        playerReference.GetComponent<VehicleData>().uiRef = GetComponent<IngameUIManager>();
        playerReference.playerRacerID = _playerData.racerList[_playerData.selectedRacer].id;
        GetComponent<EnemiesManager>().vehiclePlayer = playerReference;
        // Camara
        playerReference.GetComponent<InputControllerPlayer>().camera = Instantiate(playerReference.GetComponent<InputControllerPlayer>().camera);
        playerReference.GetComponent<InputControllerPlayer>().camera.target = playerReference.transform;
        playerReference.GetComponent<InputControllerPlayer>().camera.Init();

        // IA
        vehicleSpawnPoints = vehicleContainer.GetComponentsInChildren<Transform>().Where(x => x.tag == K.TAG_SPAWN_POINT).ToList();
        List<Country.NamesType> countries = _playerData.countryChaos.Keys.ToList().Where(x => x != _playerData.countryType && _playerData.countryChaos[x] < 100).ToList();
        int j = 0;
        int iaPrefabsLenght = _playerData.playerUpgrades.Contains(Upgrade.Type.AlienVehicle) ? IAVehiclesPrefabs.Count : IAVehiclesPrefabs.Count - 1;

        for (int i = 0; i < vehicleSpawnPoints.Count; i++)
        {
            if (i < countries.Count)
            {
                PlayerData.instance.racersQty++;
                GameObject go;
                go = Instantiate(IAVehiclesPrefabs[UnityEngine.Random.Range(0, iaPrefabsLenght)]);
                go.transform.position = vehicleSpawnPoints[i].transform.position;
                go.transform.rotation = vehicleSpawnPoints[i].transform.rotation;
                go.transform.parent = vehicleSpawnPoints[i].transform.parent;
                var vd = go.GetComponent<VehicleData>();
                var vehicle = go.GetComponent<Vehicle>();

                //Dificultad Progresiva (CAMBIAR)
                vd.maxLife += (vd.maxLife * difficultyPerCompetitorAmount[countries.Count].maxLifeMultiplier);
                vd.currentLife = vd.maxLife;
                vehicle.vehicleVars.torque += (vehicle.vehicleVars.torque * difficultyPerCompetitorAmount[countries.Count].accelMultiplier);
                vehicle.vehicleVars.topSpeed += (vehicle.vehicleVars.topSpeed * difficultyPerCompetitorAmount[countries.Count].topSpeedMultiplier);
                //foreach (var weapon in vehicle.weaponManager.GetComponentsInChildren<Weapon>()) weapon.damage += (weapon.damage * difficultyPerCompetitorAmount[countries.Count].damageMultiplier);


                vd.country = countries[j++];
                var genderTypes = Enum.GetNames(typeof(RacerData.Gender));
                vd.gender = (RacerData.Gender)Enum.Parse(typeof(RacerData.Gender), genderTypes[UnityEngine.Random.Range(0, genderTypes.Length)]);
                vehicle.vehicleName = Country.countriesNames[vd.country][vd.gender][UnityEngine.Random.Range(0, Country.countriesNames[vd.country][vd.gender].Count)];

                go.GetComponent<VehicleIAController>()._nextCheckpoint = GameObject.FindGameObjectWithTag(K.TAG_CHECKPOINTS).GetComponentInChildren<Checkpoint>();
                go.GetComponent<Vehicle>().playerRacerID = -1;
                _enemiesReferences.Add(go.GetComponent<Vehicle>());
                GetComponent<EnemiesManager>().vehiclesIAs.Add(go.GetComponent<Vehicle>());


                //PARCHES
                if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.InsideTheCore)
                {
                    var goController = go.GetComponent<VehicleIAController>();
                    if (goController.vehicleVars.vehicleType == VehicleVars.Type.Buggy) goController.vehicleVars.downForce = 30;
                    if (goController.vehicleVars.vehicleType == VehicleVars.Type.Bigfoot) goController.vehicleVars.downForce = 20;
                    if (goController.vehicleVars.vehicleType == VehicleVars.Type.Truck) goController.vehicleVars.downForce = 20;
                    if (goController.vehicleVars.vehicleType == VehicleVars.Type.Alien) goController.vehicleVars.downForce = 30;

                    if (playerReference.vehicleVars.vehicleType == VehicleVars.Type.Buggy) playerReference.vehicleVars.downForce = 30;
                    if (playerReference.vehicleVars.vehicleType == VehicleVars.Type.Bigfoot) playerReference.vehicleVars.downForce = 20;
                    if (playerReference.vehicleVars.vehicleType == VehicleVars.Type.Truck) playerReference.vehicleVars.downForce = 20;
                    if (playerReference.vehicleVars.vehicleType == VehicleVars.Type.Alien) playerReference.vehicleVars.downForce = 30;

                    goController.vehicleVars.stuckToTheFloor = false;
                }
            
                if (SceneManager.GetActiveScene().buildIndex == (int)SCENES_NUMBER.WaterTomb)
                {
                    var goController = go.GetComponent<VehicleIAController>();
                    goController.vehicleVars.stuckToTheFloor = true;
                    playerReference.vehicleVars.stuckToTheFloor = true;
                }
                else
                {
                    var goController = go.GetComponent<VehicleIAController>();
                    goController.vehicleVars.stuckToTheFloor = false;
                    playerReference.vehicleVars.stuckToTheFloor = false;
                }
            }
        }
        foreach (var spawnPoint in vehicleSpawnPoints.ToList()) Destroy(spawnPoint.gameObject);
    }

    private void Start()
    {
        _ingameUIManagerReference = GameObject.FindGameObjectWithTag(K.TAG_MANAGERS).GetComponent<IngameUIManager>();
        OnInit();
    }

    protected override void OnInit()
    {
        foreach (var racer in _enemiesReferences) racer.OnDeath += EnemyVehicleDestroyed;
        CreateGameObjectForControl();
    }

    public List<Vehicle> GetAllRacers()
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        vehicles.Add(playerReference);
        vehicles.AddRange(_enemiesReferences);
        return vehicles;
    }

    private void Update()
    {
        if (!_gameOver)
        {
            if (Mathf.FloorToInt(playerReference.lapCount) == K.MAX_LAPS)
            {
                playerReference.RaceFinished();
                ((VehiclePlayerController)playerReference).EndRaceHandbrake();
                GameOver("Race Finished");
                _gameOver = true;
            }
            foreach (var enemy in _enemiesReferences)
            {
                if (Mathf.FloorToInt(enemy.lapCount) == K.MAX_LAPS && !enemy.hasEnded)
                {
                    int posToCalculateChaos;
                    if (enemy.racerPosition == PlayerData.instance.racersQty) posToCalculateChaos = 4;
                    else posToCalculateChaos = enemy.racerPosition;
                    switch (posToCalculateChaos)
                    {
                        case 1:
                            _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] -= 25;
                            break;
                        case 2:
                            _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] -= 20;
                            break;
                        case 3:
                            _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] -= 15;
                            break;
                        default:
                            _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] += 15;
                            break;
                    }
                    _posiblePosition++;
                    enemy.RaceFinished();
                    enemy.enabled = false;
                    enemy.backDust.Stop();
                }
            }
            if (_enemiesReferences.Count == 0)
            {
                playerReference.RaceFinished();
                ((VehiclePlayerController)playerReference).EndRaceHandbrake();
                //playerReference.enabled = false;
                GameOver("You Win");
                _gameOver = true;
            }

            if (playerReference.gameObject.GetComponent<PlayerVehicleData>().currentLife <= 0)
            {
                playerReference.Die();
                ((VehiclePlayerController)playerReference).EndRaceHandbrake();
                //playerReference.enabled = false;
                GameOver("You Lose");
                _gameOver = true;
            }
        }


        PauseInput();

        //BORRAR
        if (Input.GetKey(KeyCode.F10))
        {
            playerReference.gameObject.GetComponent<PlayerVehicleData>().currentLife = 0;
        }
    }

    private void PauseInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && disableShoot == paused)
        {
            if (paused)
            {
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f;
                playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = true;
                disableShoot = false;
            }
            else
            {
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0f;
                Cursor.lockState = CursorLockMode.None;
                playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = false;
                disableShoot = true;
            }

            pauseCanvas.SetActive(!paused);
            Cursor.visible = !paused;
            paused = !paused;
        }

        if (disableShoot)
        {
            if (!_oneTime)
            {
                playerReference.gameObject.GetComponent<InputControllerPlayer>().enabled = false;
                //playerReference.enabled = false;
                if (!scoreBackground.activeSelf) playerReference.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                foreach (var enemy in _enemiesReferences)
                {
                    enemy.gameObject.GetComponent<InputControllerIA>().enabled = false;
                    enemy.enabled = false;
                    if (!scoreBackground.activeSelf) enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                }
                _oneTime = true;
            }
        }
        else
        {
            if (_oneTime)
            {
                playerReference.gameObject.GetComponent<InputControllerPlayer>().enabled = true;
                playerReference.enabled = true;
                playerReference.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                foreach (var enemy in _enemiesReferences)
                {
                    enemy.gameObject.GetComponent<InputControllerIA>().enabled = true;
                    enemy.enabled = true;
                    enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
                _oneTime = false;
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        pauseCanvas.SetActive(!paused);
        Cursor.visible = !paused;
        paused = !paused;
        playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = true;
        disableShoot = false;
    }

    /// <summary>
    /// Acciones al finalizar la carrera.
    /// </summary>
    /// <param name="s">String para finalizar la carrera</param>
    private void GameOver(string s)
    {
        switch (s)
        {
            case "You Win":
                {
                    youWin.gameObject.SetActive(true);
                    playerReference.setRacerPosition(_posiblePosition);
                    SaveDamageInfo();
                }
                break;
            case "Race Finished":
                {
                    playerReference.GetComponent<VehicleData>().damageControlSaved = true;
                    playerReference.GetComponent<VehicleData>().uiRef.showDamagecontrolCountdown = true;
                    raceFinishedText.gameObject.SetActive(true);
                    SaveDamageInfo();
                    foreach (var enemy in _enemiesReferences)
                        if (!enemy.isDestroyed && !enemy.hasEnded)
                        {
                            int posToCalculateChaos;
                            if (enemy.racerPosition == PlayerData.instance.racersQty) posToCalculateChaos = 4;
                            else posToCalculateChaos = enemy.racerPosition;
                            switch (posToCalculateChaos)
                            {
                                case 1:
                                    _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] -= 25;
                                    break;
                                case 2:
                                    _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] -= 20;
                                    break;
                                case 3:
                                    _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] -= 15;
                                    break;
                                default:
                                    _playerData.countryChaos[enemy.GetComponent<VehicleData>().country] += 15;
                                    break;
                            }
                            enemy.RaceFinished();
                            enemy.enabled = false;
                        }
                }
                break;
            case "You Lose":
                {
                    youDiedText.gameObject.SetActive(true);
                    SaveDamageInfo();
                }
                break;
            default:
                break;
        }
        BasicSettings();
        restartButton.gameObject.SetActive(true);
    }

    private void BasicSettings()
    {
        disableShoot = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerReference.gameObject.GetComponentInChildren<WeaponsManager>().enabled = false;
        playerReference.gameObject.GetComponent<InputControllerPlayer>().enabled = false;
        //playerReference.enabled = false;

        //FREEZE DE ROTACION DEL PLAYER
        playerReference.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;


        scoreBackground.SetActive(true);

        //GUARDA LOS CONTENEDORES DE TEXTOS VACIOS PARA LUEGO ASIGNARLE POSTERIORMENTE SUS RESPECTIVOS NOMBRES.
        _finalTextPosition = scoreBackground.GetComponentsInChildren<Text>().ToList();

        //OBTIENE LOS NOMBRES DEL LISTADO DE POSICIONES DE LOS VEHÍCULOS
        var getFinalPosition = _ingameUIManagerReference.textPositionContainer.GetComponentsInChildren<Text>(true).ToList();

        //LE ASIGNA A CADA TEXTO SU RESPECTIVO NOMBRE.
        for (int i = getFinalPosition.Count - 1; i >= 0; i--)
        {
            if (getFinalPosition[i].isActiveAndEnabled)
            {
                _finalTextPosition[i].gameObject.SetActive(true);
                _finalTextPosition[i].text = getFinalPosition[i].text;
            }
            else _finalTextPosition[i].gameObject.SetActive(false);
        }

        //OBTIENE POSICION DEL PLAYER
        var finalPlayerPosition = _ingameUIManagerReference.GetFinalPlayerPosition();

        //OBTIENE SELECTOR AMARILLO
        var selectPlayer = scoreBackground.GetComponentInChildren<Image>();

        //EL PLAYER SE CONVIERTE EN HIJO DEL SELECTPLAYER
        selectPlayer.transform.SetParent(_finalTextPosition[finalPlayerPosition].transform);
        //POSICIONA TEXTO
        selectPlayer.rectTransform.localPosition = Vector3.zero;

        //SE HACE 2 VECES PORQUE SINO NO FUNCIONA.... UN MISTERIO.
        //SE LE ASIGNA UN TEXTO RANDOM COMO PADRE.
        selectPlayer.transform.SetParent(scoreBackground.GetComponentsInChildren<RectTransform>()[1].transform);
        //EL PLAYER SE CONVIERTE DE NUEVO HIJO DEL SELECTPLAYER.
        _finalTextPosition[finalPlayerPosition].transform.SetParent(selectPlayer.transform);

        _ingameUIManagerReference.textPositionContainer.SetActive(false);

        _ingameUIManagerReference.lapsText.gameObject.SetActive(false);
        playerReference.GetComponent<VehiclePlayerController>().wrongDirectionText.gameObject.SetActive(false);
     //   playerReference.GetComponent<VehiclePlayerController>().visualNitro.transform.parent.transform.parent.transform.parent.gameObject.SetActive(false);
        playerReference.GetComponentInChildren<WeaponsManager>().crosshair.SetActive(false);
        _ingameUIManagerReference.speedpmeterNeedleImage.gameObject.SetActive(false);

    }

    void SaveDamageInfo()
    {
        var pd = playerReference.gameObject.GetComponent<VehicleData>();
        var aux = _playerData.racerList.Find(x => x.id == playerReference.playerRacerID);
        aux.maxLife = pd.maxLife;
        aux.currentLife = pd.currentLife;
        aux.lastRacePosition = pd.GetComponent<Vehicle>().racerPosition;
        /*PlayerPrefs.SetInt("MaxLife", (int)playerReference.gameObject.GetComponent<VehicleData>().maxLife);
        PlayerPrefs.SetInt("CurrentLife", (int)playerReference.gameObject.GetComponent<VehicleData>().currentLife);*/
    }

    private void EnemyVehicleDestroyed(Vehicle v)
    {
        if (_enemiesReferences.Contains(v))
        {
            _playerData.countryChaos[v.GetComponent<VehicleData>().country] += 20;
            v.OnDeath -= EnemyVehicleDestroyed;
            _enemiesReferences.Remove(v);
        }
    }

    /// <summary>
    /// Crea un gameobject vacio que se utiliza para volver al menu HUB luego de terminar una carrera
    /// </summary>
    private void CreateGameObjectForControl()
    {
        if (!GameObject.FindGameObjectWithTag(K.TAG_RACE_CONTINUE_CONTROL))
        {
            var go = Instantiate(continueAfterRaceControlPrefab);
            go.tag = K.TAG_RACE_CONTINUE_CONTROL;
        }
    }
}
