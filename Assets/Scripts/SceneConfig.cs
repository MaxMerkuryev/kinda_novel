using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Novel/Scene")]
public class SceneConfig : ScriptableObject {
	[field: SerializeField] public Sprite Illustration { get; private set; }
	[field: SerializeField] public SceneConfig NextScene { get; private set; }
	[field: SerializeField] public SpeechConfig Speech { get; private set; }
	[field: SerializeField] public bool AutoSkip { get; private set; }
	[field: SerializeField] public float AutoSkipTime { get; private set; }
	[field: SerializeField] public List<AnswerConfig> Answers { get; private set; }
}

[Serializable]
public class SpeechConfig {
	[field: SerializeField, TextArea(5, 20)] public string Text { get; private set; }
	[field: SerializeField] public CharacterConfig Character { get; private set; }
}

[Serializable]
public class AnswerConfig {
	[field: SerializeField, TextArea(5, 20)] public string Text { get; private set; }
	[field: SerializeField] public SceneConfig NextScene { get; private set; }
}