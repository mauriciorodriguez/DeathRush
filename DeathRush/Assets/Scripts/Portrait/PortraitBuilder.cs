using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PortraitBuilder : MonoBehaviour
{
    public List<Color> skinColors, hairColors;

    private Dictionary<Classes.Type, Dictionary<RacerData.Gender, List<Sprite>>> _heads, _hair, _extra, _tierOne, _tierTwo, _tierThree;

    private void Start()
    {
        InitDictrionary(out _heads, "/Head");
        InitDictrionary(out _hair, "/Hair");
        InitDictrionary(out _extra, "/Extra");
        InitDictrionary(out _tierOne, "/TierOne");
        InitDictrionary(out _tierTwo, "/TierTwo");
        InitDictrionary(out _tierThree, "/TierThree");
        DontDestroyOnLoad(gameObject);
    }

    private void InitDictrionary(out Dictionary<Classes.Type, Dictionary<RacerData.Gender, List<Sprite>>> dict, string lastResourcesFolder)
    {
        dict = new Dictionary<Classes.Type, Dictionary<RacerData.Gender, List<Sprite>>>();
        foreach (Classes.Type classType in Enum.GetValues(typeof(Classes.Type)))
        {
            dict[classType] = new Dictionary<RacerData.Gender, List<Sprite>>();
            foreach (RacerData.Gender gender in Enum.GetValues(typeof(RacerData.Gender)))
            {
                dict[classType][gender] = new List<Sprite>();
                foreach (var sprite in Resources.LoadAll<Sprite>(K.PATH_SPRITES_CLASSES + classType + "/" + gender + lastResourcesFolder))
                {
                    dict[classType][gender].Add(sprite);
                }
            }
        }
    }

    public void Build(Portrait p, RacerData rd)
    {
        if (_heads[rd.racerClass.classType][rd.gender].Count > 0)
        {
            p.head.sprite = _heads[rd.racerClass.classType][rd.gender][rd.headNumber];
            p.head.gameObject.SetActive(true);
        }
        else p.head.gameObject.SetActive(false);
        p.head.color = skinColors[rd.skinColorNumber];
        if (_hair[rd.racerClass.classType][rd.gender].Count > 0)
        {
            p.hair.sprite = _hair[rd.racerClass.classType][rd.gender][rd.hairNumber];
            p.hair.gameObject.SetActive(true);
        }
        else p.hair.gameObject.SetActive(false);
        p.hair.color = hairColors[rd.hairColorNumber];
        if (_extra[rd.racerClass.classType][rd.gender].Count > 0)
        {
            p.extra.sprite = _extra[rd.racerClass.classType][rd.gender][rd.extraNumer];
            p.extra.gameObject.SetActive(true);
        }
        else p.extra.gameObject.SetActive(false);
        if (rd.unlockedTierOne != Classes.TypeTierOne.None)
        {
            p.tOne.sprite = _tierOne[rd.racerClass.classType][rd.gender][rd.tierOneNumber];
            p.tOne.gameObject.SetActive(true);
        }
        else p.tOne.gameObject.SetActive(false);
        if (rd.unlockedTierTwo != Classes.TypeTierTwo.None)
        {
            p.tTwo.sprite = _tierTwo[rd.racerClass.classType][rd.gender][rd.tierTwoNumber];
            p.tTwo.gameObject.SetActive(true);
        }
        else p.tTwo.gameObject.SetActive(false);
        if (rd.unlockedTierThree != Classes.TypeTierThree.None)
        {
            p.tThree.sprite = _tierThree[rd.racerClass.classType][rd.gender][rd.tierThreeNumber];
            p.tThree.gameObject.SetActive(true);
        }
        else p.tThree.gameObject.SetActive(false);
    }

    public void Randomize(Portrait p)
    {
        var g = p.gender;
        var racerClass = p.racerClass;
        int aux;
        if (_heads[racerClass][g].Count > 0)
        {
            p.head.gameObject.SetActive(true);
            aux = UnityEngine.Random.Range(0, _heads[racerClass][g].Count);
            p.headNumber = aux;
            p.head.sprite = _heads[racerClass][g][aux];
            if (skinColors.Count > 0)
            {
                aux = UnityEngine.Random.Range(0, skinColors.Count);
                p.skinColorNumber = aux;
                p.head.color = skinColors[aux];
            }
        }
        else
        {
            p.headNumber = -1;
            p.head.gameObject.SetActive(false);
        }
        if (_hair[racerClass][g].Count > 0)
        {
            p.hair.gameObject.SetActive(true);
            aux = UnityEngine.Random.Range(0, _hair[racerClass][g].Count);
            p.hairNumber = aux;
            p.hair.sprite = _hair[racerClass][g][aux];
            if (hairColors.Count > 0)
            {
                aux = UnityEngine.Random.Range(0, hairColors.Count);
                p.hairColorNumber = aux;
                p.hair.color = hairColors[aux];
            }
        }
        else
        {
            p.hairNumber = -1;
            p.hair.gameObject.SetActive(false);
        }
        if (_extra[racerClass][g].Count > 0)
        {
            p.extra.gameObject.SetActive(true);
            aux = UnityEngine.Random.Range(0, _extra[racerClass][g].Count);
            p.extraNumber = aux;
            p.extra.sprite = _extra[racerClass][g][aux];
        }
        else
        {
            p.extraNumber = -1;
            p.extra.gameObject.SetActive(false);
        }
        p.tierOneNumber = -1;
        p.tOne.gameObject.SetActive(false);
        p.tierTwoNumber = -1;
        p.tTwo.gameObject.SetActive(false);
        p.tierThreeNumber = -1;
        p.tThree.gameObject.SetActive(false);
    }
}
