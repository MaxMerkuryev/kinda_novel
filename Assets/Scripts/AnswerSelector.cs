using System;
using System.Collections.Generic;
using UnityEngine;

public class AnswerSelector : MonoBehaviour {
	[SerializeField] private AnswerButton _buttonPrefab;
	[SerializeField] private RectTransform _buttonsContainer;
	[SerializeField] private RectTransform _dividerPrefab;

	private List<GameObject> _elements = new List<GameObject>();

	public void Show(Action<AnswerConfig> onClick, List<AnswerConfig> answers) {
		gameObject.SetActive(true);

		for (int i = 0; i < answers.Count; i++) {
			AnswerConfig config = answers[i];
			AnswerButton button = Instantiate(_buttonPrefab, _buttonsContainer);
			button.Init(() => { onClick?.Invoke(config); }, config.Text);
			_elements.Add(button.gameObject);
			
			if(i < answers.Count - 1) {
				_elements.Add(Instantiate(_dividerPrefab, _buttonsContainer).gameObject);
			}
		}
	}

	public void Hide() {
		gameObject.SetActive(false);

		for (int i = 0; i < _elements.Count; i++) {
			Destroy(_elements[i]);
		}

		_elements.Clear();
	}
}
