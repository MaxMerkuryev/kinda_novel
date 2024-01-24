using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {
	[SerializeField] private Button _button;
	[SerializeField] private TextMeshProUGUI _text;

	public void Init(Action onClick, string text) {
		_button.onClick.AddListener(() => onClick?.Invoke());
		_text.text = text;
		Canvas.ForceUpdateCanvases();
	}
}
