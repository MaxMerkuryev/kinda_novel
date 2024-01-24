using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiButton : Button {
	private Image _image;
	private const float _fade = 1f;
	private const float _scale = 1.05f;

	protected override void Awake() {
		base.Awake();
		_image = GetComponent<Image>();
	}

	protected override void OnEnable() {
		transform.localScale = Vector3.one;
		_image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
	}

	public override void OnPointerClick(PointerEventData eventData) {
		base.OnPointerClick(eventData);
	}

	public override void OnPointerEnter(PointerEventData eventData) {
		base.OnPointerEnter(eventData);
		transform.DOScale(_scale, 0.1f).SetUpdate(true);
		_image.DOFade(_fade, 0.1f).SetUpdate(true);
	}

	public override void OnPointerExit(PointerEventData eventData) {
		base.OnPointerExit(eventData);
		transform.DOScale(1f, 0.1f).SetUpdate(true);
		_image.DOFade(0f, 0.1f).SetUpdate(true);
	}
}
