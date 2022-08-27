using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardDeck))]
[CanEditMultipleObjects]
public class CardDeckEditor : Editor
{
    private CardDeck cardDeck;
    private SerializedProperty cardBackSprite;
    private SerializedProperty cards;
    private SerializedProperty spritePath;    

    private void OnEnable()
    {
      this.cardDeck = (CardDeck)target;
      this.cardBackSprite = this.serializedObject.FindProperty("cardBackSprite");
      this.cards = this.serializedObject.FindProperty("cards");
      this.spritePath = this.serializedObject.FindProperty("spritePath");
    }

    public override void OnInspectorGUI()
    {
      base.OnInspectorGUI();

      if (GUILayout.Button("Clear All Cards", GUILayout.Height(30)))
      {
        int count = this.cardDeck.Cards.Count;
        Debug.Log($"<color=orange>Clear cards ({count})</color>");

        this.cardDeck.Cards.Clear();
      }

      if (GUILayout.Button("Load cards from sprites", GUILayout.Height(30)))
      {
        string path = this.spritePath.stringValue;
        Debug.Log($"<color=orange>Find assets in \"{path}\"</color>");

        if (!Directory.Exists(path))
        {
          Debug.LogError("Directory is not exists.");
          return;
        }

        var guids = AssetDatabase.FindAssets("t:Sprite", new string[] { path });
        Debug.Log($"Find number of {guids.Length} items.");

        foreach (var guid in guids)
        {
          CardData card;
          string itemPath = AssetDatabase.GUIDToAssetPath(guid);
          Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(itemPath);

          card.Name = sprite.name;
          card.FrontSprite = sprite;

          this.cardDeck.Cards.Add(card);
        }

        Debug.Log("Load cards complete");
      }

      this.serializedObject.ApplyModifiedProperties();
    }
}
