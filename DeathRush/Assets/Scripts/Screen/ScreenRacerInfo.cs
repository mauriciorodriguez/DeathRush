using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ScreenRacerInfo : ScreenView
{
    public Text nameClass, skillPoints, levelTypeText, expText, passiveSkillBackgroundText, passiveSkillText;
    public RectTransform expBarFill;
    public List<Button> skillButtons;

    private PlayerData _playerData;
    private RacerData _racerData;

    private void OnEnable()
    {
        _playerData = PlayerData.instance;
        _racerData = _playerData.racerList[_playerData.selectedRacer];
        Init();
    }

    private void Init()
    {
        RacerPortrait.Build(_racerData);
        nameClass.text = _racerData.racerName + " \nthe \"" + _racerData.racerClass.classType.ToString() + "\"";
        skillPoints.text = "Skill Points\n" + (_racerData.skillPoints - _racerData.usedSkillPoints);
        levelTypeText.text = _racerData.racerLevelType.ToString();
        if (_racerData.racerLevelType == RacerData.LevelType.Legendary)
        {
            expBarFill.localScale = Vector3.one;
            expText.text = "MAX";
        }
        else
        {
            var xScale = expBarFill.localScale;
            xScale.x = _racerData.racerExp / _playerData.expNeededToLvlUp[_playerData.racerList[_playerData.selectedRacer].level];
            expBarFill.localScale = xScale;
            expText.text = _racerData.racerExp + "/" + _playerData.expNeededToLvlUp[_playerData.racerList[_playerData.selectedRacer].level];
        }
        passiveSkillBackgroundText.text = Classes.descriptions[_racerData.racerClass.classType];
        passiveSkillText.text = Classes.descriptions[_racerData.racerClass.classType];
        DisableButtons();
    }

    private void DisableButtons()
    {
        for (int i = 0; i < skillButtons.Count; i++)
        {
            skillButtons[i].interactable = false;
            skillButtons[i].GetComponent<Image>().color = skillButtons[i].colors.normalColor;
            skillButtons[i].GetComponentInChildren<Text>().text = StringSplitter.Split(_racerData.racerClass.skillList[i].ToString());
            if (skillButtons[i].GetComponentInChildren<Text>().text == StringSplitter.Split(_racerData.unlockedTierOne.ToString()) ||
                skillButtons[i].GetComponentInChildren<Text>().text == StringSplitter.Split(_racerData.unlockedTierTwo.ToString()) ||
                skillButtons[i].GetComponentInChildren<Text>().text == StringSplitter.Split(_racerData.unlockedTierThree.ToString())
                )
            {
                skillButtons[i].GetComponent<Image>().color = Color.green;
            }
        }
        if (_racerData.skillPoints > _racerData.usedSkillPoints)
        {
            if (_racerData.skillPoints >= 1 && _racerData.usedSkillPoints < 1)
            {
                skillButtons[0].interactable = true;
                skillButtons[1].interactable = true;
            }
            else if (_racerData.skillPoints >= 2 && _racerData.usedSkillPoints < 2)
            {
                skillButtons[2].interactable = true;
                skillButtons[3].interactable = true;
            }
            else if (_racerData.skillPoints >= 3 && _racerData.usedSkillPoints < 3)
            {
                skillButtons[4].interactable = true;
                skillButtons[5].interactable = true;
            }
        }
    }

    public void OnMouseEnterTierOneSkill(Text t)
    {
        var s = (Classes.TypeTierOne)Enum.Parse(typeof(Classes.TypeTierOne), StringSplitter.Merge(t.text));
        ShowTooltip(t.text, Classes.descriptions[s]);
    }

    public void OnMouseEnterTierTwoSkill(Text t)
    {
        var s = (Classes.TypeTierTwo)Enum.Parse(typeof(Classes.TypeTierTwo), StringSplitter.Merge(t.text));
        ShowTooltip(t.text, Classes.descriptions[s]);
    }

    public void OnMouseEnterTierThreeSkill(Text t)
    {
        var s = (Classes.TypeTierThree)Enum.Parse(typeof(Classes.TypeTierThree), StringSplitter.Merge(t.text));
        ShowTooltip(t.text, Classes.descriptions[s]);
    }

    public void OnMouseExitSkill()
    {
        HideTooltip();
    }

    public void OnTierOneButton(int skillPos)
    {
        _racerData.unlockedTierOne = (Classes.TypeTierOne)_racerData.racerClass.skillList[skillPos];
        _racerData.tierOneNumber = skillPos;
        skillUnlocked();
    }

    public void OnTierTwoButton(int skillPos)
    {
        _racerData.unlockedTierTwo = (Classes.TypeTierTwo)_racerData.racerClass.skillList[skillPos];
        _racerData.tierTwoNumber = skillPos - 2;
        skillUnlocked();
    }

    public void OnTierThreeButton(int skillPos)
    {
        _racerData.unlockedTierThree = (Classes.TypeTierThree)_racerData.racerClass.skillList[skillPos];
        _racerData.tierThreeNumber = skillPos - 4;
        skillUnlocked();
    }

    private void skillUnlocked()
    {
        _racerData.usedSkillPoints++;
        Init();
    }
}
