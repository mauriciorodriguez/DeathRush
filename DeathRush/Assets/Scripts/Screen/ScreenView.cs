using UnityEngine;
using System.Collections;
using System;

public class ScreenView : MonoBehaviour
{
    public event Action OnExit = delegate { };

    public Tooltip tooltip;
    public Portrait RacerPortrait;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }

    public void ShowTooltip(string title, string desc, int cost = 0)
    {
        tooltip.gameObject.SetActive(true);
        if (cost > 0) tooltip.SetTextWithCost(title, desc, cost);
        else tooltip.SetText(title, desc);
    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

    /// <summary>
    /// Metodo para volver a la pantalla anterior.
    /// </summary>
	public virtual void OnBackButton()
    {
        Exit();
    }

    protected void Exit()
    {
        OnExit();
    }
}