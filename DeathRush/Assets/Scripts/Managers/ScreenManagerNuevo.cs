using UnityEngine;
using System.Collections;

public class ScreenManagerNuevo : MonoBehaviour
{
    public ScreenView firstScreen, firstContinueScreen;
    public ScreenNewGame screenNewGame;
    public ScreenVehiclesShop screenVehiclesShop;
    public ScreenWeaponsShop screenWeaponsShop;
    public ScreenSearchRace screenSearchRace;
    public ScreenHirePilot screenHirePilot;
    public ScreenHUB screenHub;
    public ScreenCountrySelect screenCountrySelect;
    public ScreenOptions screenOptions;
    public ScreenCredits screenCredits;
    public ScreenUpgrades screenUpgrades;
    public ScreenChaosMap screenChaosMap;
    public ScreenPostGame screenPostGame;
    public ScreenRacerInfo screenRacerInfo;
    public ScreenGameOver screenGameOver;
    public BottomMenu arBottomMenu;
    public BottomMenu bottomMenu;
    public PlayerData playerData;
    public GameObject cameraRotationCanvas;
    public ScreenView _currentScreen { get; private set; }

    private void Awake()
    {
        foreach (var screen in GetComponentsInChildren<ScreenView>()) screen.gameObject.SetActive(false);
        if (GameObject.FindGameObjectWithTag(K.TAG_RACE_CONTINUE_CONTROL)) GoToScreen(firstContinueScreen);
        else GoToScreen(firstScreen);
        ConfigureScreens();
    }

    /// <summary>
    /// Desactiva toda las pantallas y activa la proxima.
    /// </summary>
    /// <param name="screen">Proxima pantalla</param>
    public void GoToScreen(ScreenView screen)
    {
        if (screen is ScreenNewGame ||
            screen is ScreenCountrySelect ||
            screen is ScreenOptions ||
            screen is ScreenCredits ||
            screen is ScreenPostGame ||
            screen is ScreenRacerInfo ||
            screen is ScreenGameOver)
        {
            bottomMenu.gameObject.SetActive(false);
            arBottomMenu.gameObject.SetActive(false);
       //     cameraRotationCanvas.SetActive(false);
        }
        else
        {
            bottomMenu.gameObject.SetActive(true);
            arBottomMenu.gameObject.SetActive(true);
          //  cameraRotationCanvas.SetActive(true);
        }
        if (_currentScreen) _currentScreen.gameObject.SetActive(false);
        screen.gameObject.SetActive(true);
        _currentScreen = screen;
        if (playerData) playerData.SavePlayer();
    }

    /// <summary>
    /// Binding de pantallas.
    /// </summary>
    private void ConfigureScreens()
    {
        //Main menu
        screenNewGame.OnNewCampaign += () => GoToScreen(screenCountrySelect);
      //  screenNewGame.OnContinue += () => GoToScreen(screenHub);
        screenNewGame.OnOptions += () => GoToScreen(screenOptions);
        screenNewGame.OnCredits += () => GoToScreen(screenCredits);

        //Garage
        screenVehiclesShop.OnExit += () => GoToScreen(screenChaosMap);

        //HUB
        screenHub.OnStatsSelect += () => GoToScreen(screenRacerInfo);

        //Search Race
        screenSearchRace.OnExit += () => GoToScreen(screenChaosMap);
        screenSearchRace.OnNoPilot += () => GoToScreen(screenHirePilot);
    //    screenSearchRace.OnNoSelectedPilot += () => GoToScreen(screenHub);

        //Country Select
        screenCountrySelect.OnExit += () => GoToScreen(screenNewGame);
        screenCountrySelect.OnCountrySelect += () => GoToScreen(screenHirePilot);

        //Options
        screenOptions.OnExit += () => GoToScreen(screenNewGame);

        //Credits
        screenCredits.OnExit += () => GoToScreen(screenNewGame);

        //Post Game
        screenPostGame.OnExit += () => GoToScreen(screenChaosMap);

        //Racer Info
        screenRacerInfo.OnExit += () => GoToScreen(screenChaosMap);

        //Hire Pilot
        screenHirePilot.OnExit += () => GoToScreen(screenChaosMap);

        //Game Over
        screenGameOver.OnExit += () => GoToScreen(screenNewGame);

        //Bottom Menu
        arBottomMenu.OnShowUpgrade += () => GoToScreen(screenUpgrades);
        arBottomMenu.OnShowChaosMap += () => GoToScreen(screenChaosMap);
        //   bottomMenu.OnShowHUB += () => GoToScreen(screenHub);
        arBottomMenu.OnShowVehiclesShop += () => GoToScreen(screenVehiclesShop);
        arBottomMenu.OnShowWeaponsShop += () => GoToScreen(screenWeaponsShop);
        arBottomMenu.OnShowHireRacer += () => GoToScreen(screenHirePilot);
        arBottomMenu.OnShowSearchForRace += () => GoToScreen(screenSearchRace);
        arBottomMenu.OnGameOver += () => GoToScreen(screenGameOver);

        //Bottom Menu
        bottomMenu.OnShowUpgrade += () => GoToScreen(screenUpgrades);
        bottomMenu.OnShowChaosMap += () => GoToScreen(screenChaosMap);
        //   bottomMenu.OnShowHUB += () => GoToScreen(screenHub);
        bottomMenu.OnShowVehiclesShop += () => GoToScreen(screenVehiclesShop);
        bottomMenu.OnShowWeaponsShop += () => GoToScreen(screenWeaponsShop);
        bottomMenu.OnShowHireRacer += () => GoToScreen(screenHirePilot);
        bottomMenu.OnShowSearchForRace += () => GoToScreen(screenSearchRace);
        bottomMenu.OnGameOver += () => GoToScreen(screenGameOver);
    }
}
