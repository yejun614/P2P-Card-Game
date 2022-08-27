using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CardData
{
    public string Name;
    public Sprite FrontSprite;
}

[CreateAssetMenu(fileName = "Card Deck", menuName = "Scriptable Objects/Card Deck", order = int.MaxValue)]
public class CardDeck : ScriptableObject
{
    public Sprite CardBackSprite;

    public List<CardData> Cards;

    [SerializeField] private string spritePath;
}
