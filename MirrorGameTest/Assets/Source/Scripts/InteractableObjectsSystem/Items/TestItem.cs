using UnityEngine;

public class TestItem : Item
{
    public override void Use(GameObject player)
    {
        print("Item was Used");
    }
}
