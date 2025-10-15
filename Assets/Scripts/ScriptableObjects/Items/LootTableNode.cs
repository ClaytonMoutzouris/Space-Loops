using UnityEngine;

[System.Serializable]

public class LootTableNode
{
    public int dropChance;
    public ItemData item;
    public CardData card;
    public int minDropNum = 1;
    public int maxDropNum = 1;
}
