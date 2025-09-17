using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tags/Tag Registry", fileName = "TagRegistry")]
public class TagRegistry : ScriptableObject
{
    [SerializeField] private List<Tag> _tags = new();
    public IReadOnlyList<Tag> Tags => _tags;
}
