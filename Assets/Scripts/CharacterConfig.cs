using UnityEngine;

[CreateAssetMenu(menuName = "Novel/Character")]
public class CharacterConfig : ScriptableObject {
	[field: SerializeField] public string Name { get; private set; }
	[field: SerializeField] public Color Color { get; private set; }
}