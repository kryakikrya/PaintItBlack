using UnityEngine;

[CreateAssetMenu(menuName = "Tags/Tag", fileName = "Tag")]
public class Tag : ScriptableObject
{
    [SerializeField] private string _tagName;
    public string TagName => _tagName;
}
