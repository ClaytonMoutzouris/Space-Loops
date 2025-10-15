using NUnit.Framework.Interfaces;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "ScriptableObjects/Items/LootTable")]
public class LootTable : ScriptableObject
{
    public List<LootTableNode> nodes;
    public List<LootTableNode> cardNodes;
    public List<ItemData> guaranteedDrops;
    public List<CardData> guaranteedCards;

    //Old method using weights
    public List<ItemData> GetLootOld(int numRandomItems = 1)
    {
        List<ItemData> items = new List<ItemData>();

        int weightTotal = 0;
        List<int> ranges = new List<int>();

        foreach (LootTableNode node in nodes)
        {
            weightTotal += node.dropChance;
            ranges.Add(weightTotal);
        }

        int random;

        for (int r = 0; r < numRandomItems; r++)
        {
            random = Random.Range(0, weightTotal);

            for (int i = 0; i < ranges.Count; i++)
            {
                if (random < ranges[i])
                {
                    if (nodes[i].item != null)
                    {
                        items.Add(nodes[i].item);
                    }
                    break;
                }
            }
        }


        foreach (ItemData node in guaranteedDrops)
        {
            items.Add(node);
        }

        return items;
    }

    public virtual List<ItemData> GetLoot(int luck = 0)
    {
        List<ItemData> items = new List<ItemData>();

        foreach (LootTableNode node in nodes)
        {
            if (node.dropChance >= Random.Range(0, 100) + 1 + luck)
            {
                for (int i = 0; i < Random.Range(node.minDropNum, node.maxDropNum + 1); i++)
                {
                    items.Add(node.item);
                }

            }
        }



        return items;
    }

    public virtual List<CardData> GetCards(int luck = 0)
    {
        List<CardData> cards = new List<CardData>();

        foreach (CardData node in guaranteedCards)
        {
            cards.Add(node);

        }

        foreach (LootTableNode node in nodes)
        {
            if (node.dropChance >= Random.Range(0, 100) + 1 + luck)
            {
                for (int i = 0; i < Random.Range(node.minDropNum, node.maxDropNum + 1); i++)
                {
                    cards.Add(node.card);
                }

            }
        }


        return cards;
    }

}
