using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text backgroundText, visualText;

    private float _planeDistance;
    private RectTransform _rectTransform;
    private Vector2 _tooltipPivot;
    private Camera mainCamera;

    public void SetText(string title, string description)
    {
        backgroundText.text = StringSplitter.Split(title) + "\n" + description;
        visualText.text = "<color=" + ColorTypeConverter.ToRGBHex(Color.green) + ">" + StringSplitter.Split(title) + "</color>\n" + "<color=" + ColorTypeConverter.ToRGBHex(Color.white) + ">" + description + "</color>";
    }

    public void SetTextWithCost(string title, string description, int cost)
    {
        backgroundText.text = StringSplitter.Split(title) + "\n" + description + "\n$ " + cost;
        visualText.text = "<color=" + ColorTypeConverter.ToRGBHex(Color.green) + ">" + StringSplitter.Split(title) + "</color>\n" +
            "<color=" + ColorTypeConverter.ToRGBHex(Color.white) + ">" + description + "</color>\n" +
            "<color=" + ColorTypeConverter.ToRGBHex(Color.yellow) + ">$" + cost + "</color>";
    }

    public void SetTextWithTittle(string title)
    {
        backgroundText.text = StringSplitter.Split(title);
        visualText.text = "<color=" + ColorTypeConverter.ToRGBHex(Color.white) + ">" + StringSplitter.Split(title) + "</color>\n";
    }

    private void OnEnable()
    {
        _planeDistance = GetComponentInParent<Canvas>().planeDistance;

        //Da un valor de 100 en BottomMenu por eso la asignación de abajo  
        _planeDistance = 1;


        _rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var screenPoint = Input.mousePosition;
        if (Input.mousePosition.x >= mainCamera.pixelWidth / 2) screenPoint.x -= 20;
        else screenPoint.x += 20;
        if (Input.mousePosition.y >= mainCamera.pixelHeight / 2) screenPoint.y -= 15;
        else screenPoint.y += 15;
        screenPoint.z = _planeDistance;
        transform.position = mainCamera.ScreenToWorldPoint(screenPoint);
        if (transform.localPosition.x >= 0) _tooltipPivot.x = 1;
        else _tooltipPivot.x = 0;
        if (transform.localPosition.y >= 0) _tooltipPivot.y = 1;
        else _tooltipPivot.y = 0;
        _rectTransform.pivot = _tooltipPivot;
    }
}
