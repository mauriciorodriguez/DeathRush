using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonRacerInfo : MonoBehaviour
{
    public RawImage selectedRacerInfo;
    public Text racerNameText, racerClassText;
    public Portrait racerPortrait;

    public void UpdateInfo(RacerData rd)
    {
        racerNameText.text = rd.racerName;
        racerClassText.text = rd.racerClass.classType.ToString();
        racerPortrait.Build(rd);
        racerPortrait.transform.localScale = Vector3.one * .3f;
        gameObject.SetActive(true);
    }
}
