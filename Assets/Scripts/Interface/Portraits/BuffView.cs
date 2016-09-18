﻿using UnityEngine;
using UnityEngine.UI;

public class BuffView : MonoBehaviour {
    [SerializeField]
    private Outline _outline;

    [SerializeField]
    private Text _text;
    public string Text { set { _text.text = value; } }

    [SerializeField]
    private Text _duration;
    public string Duration { set { _duration.text = value; } }

    public Color Color {
        set {
            _text.color = value;
            _outline.effectColor = value;
        }
    }
}
