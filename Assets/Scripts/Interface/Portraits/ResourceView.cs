﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * This class manages the Resource prefab
 */
public class ResourceView : MonoBehaviour {

    Text _resourceName; //Name of the Resource. Should be only 2 letters.
    public string ResourceName { get { return _resourceName.text; } set { _resourceName.text = value; } }

    Image _underBar; //Bar that represents max resource
    public Color UnderColor { get { return _underBar.color; } set { _underBar.color = value; } }

    Image _overBar; //Bar that represents current resource
    public Color OverColor { get { return _overBar.color; } set { _overBar.color = value; } }

    Text _fraction; //Text on bar that describes the amount
    public string Fraction { get { return _fraction.text; } set { _fraction.text = value; } }

    // Use this for initialization
    void Awake() {
        _resourceName = gameObject.GetComponentsInChildren<Text>()[0];
        _underBar = gameObject.GetComponentsInChildren<Image>()[0];
        _overBar = gameObject.GetComponentsInChildren<Image>()[1];
        _fraction = gameObject.GetComponentsInChildren<Text>()[1];
    }

    /**
     * Scale should be in the range [0, 1]
     * This sets the OverBar's scale
     */
    public void SetBarScale(float scale) {
        Vector3 v = _overBar.gameObject.GetComponent<RectTransform>().localScale;
        v.x = scale;
        _overBar.gameObject.GetComponent<RectTransform>().localScale = v;
    }

    /**
     * This sets the text on the bar describing
     * current / total resource
     */
    public void SetFraction(int numerator, int denominator) {
        _fraction.text = numerator + "/" + denominator;
    }
}