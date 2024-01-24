using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenePlayer : MonoBehaviour {
	[SerializeField] private SceneConfig _startScene;
	[SerializeField] private Button _nextButton;
	[SerializeField] private TextMeshProUGUI _text;
	[SerializeField] private Image[] _illustrations;
	[SerializeField] private AnswerSelector _answerSelector;
	[SerializeField] private Canvas _canvas;

	private SceneConfig _currentScene;
	private bool _typing;
	private int _imageIndex;

	private void Awake() {
		_nextButton.gameObject.SetActive(false);
		_nextButton.onClick.AddListener(HandleNext);
		SwitchTo(_startScene);
	}

	private void SwitchTo(SceneConfig config) {
		if(config == null) return;

		_currentScene = config;
		_nextButton.gameObject.SetActive(false);

		foreach (var i in _illustrations) {
			i.sprite = null;
			i.color = new Color(i.color.r, i.color.g, i.color.b, 0f);
		}

		Crossfade(config.Illustration);
		StartCoroutine(ProceedSpeech(config.Speech));
	}

	private IEnumerator ProceedSpeech(SpeechConfig config) {
		CharacterConfig character = config.Character;

		string text = $"{config.Text}";
		_text.color = character.Color;
		_text.text = string.Empty;

		if (!string.IsNullOrEmpty(character.Name)) {
			text = $"{character.Name}: {text}";
		}

		var fill = FillText(text);
		StartCoroutine(fill);
		_typing = true;

		while (_typing) {
			if (Input.GetMouseButtonDown(0)) {
				StopCoroutine(fill);
				_text.text = text;
				_typing = false;
			}

			yield return null;
		}

		OnSpheechShown();
	}

	private void Crossfade(Sprite nextImage) {
		_illustrations[_imageIndex].DOComplete();
		_illustrations[_imageIndex].DOFade(0, 0.5f);

		_imageIndex += (_imageIndex + 1) % _illustrations.Length;

		_illustrations[_imageIndex].sprite = nextImage;
		_illustrations[_imageIndex].DOComplete();
		_illustrations[_imageIndex].DOFade(1, 0.5f);
	}

	private IEnumerator FillText(string text) {
		foreach (char c in text.ToCharArray()) {
			_text.text += c;
			yield return new WaitForSeconds(0.05f);
		}

		_typing = false;
	}

	private void OnSpheechShown() {
		if (_currentScene.Answers.Count > 0) ShowAnswers(_currentScene.Answers);
		else if (_currentScene.AutoSkip) StartCoroutine(WaitForAutoSkip(_currentScene.AutoSkipTime));
		else _nextButton.gameObject.SetActive(true);
	}

	private void ShowAnswers(List<AnswerConfig> answers) {
		_answerSelector.Show(HandleAnswer, answers);
	}

	private void HandleAnswer(AnswerConfig answer) {
		_answerSelector.Hide();
		SwitchTo(answer.NextScene);
	}

	private void HandleNext() {
		SwitchTo(_currentScene.NextScene);
	}

	private IEnumerator WaitForAutoSkip(float time) {
		yield return new WaitForSeconds(time);
		SwitchTo(_currentScene.NextScene);
	}
}
