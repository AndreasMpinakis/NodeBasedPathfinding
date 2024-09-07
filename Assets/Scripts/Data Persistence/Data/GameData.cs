using System;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    //// TopDownCharacterController
    //public Vector3 playerPosition;

    //// Inventory
    //public List<ScriptableObject> itemsInInventory;
    //private int inventoryMaxSpace = 12;

    //// Stackable Items Amount in Inventory
    //public List<int> stackableItemsAmountInInventory;

    //// CharacterEquipment
    //public List<CharacterItemsSaveLoad> allCharacterItemsList;

    //// CharacterStats
    //public List<CharacterStatsSaveLoad> allCharacterStatsList;

    // NPCs Positions
    public List<NPCPositions> allNPCsPositionsList;

    // DialogueForFendrel
    public int conversationBookmarkForFendrel;
    public bool hasMoreToSayForFendrel = true;

    // DialogueForAlys
    public int conversationBookmarkForAlys;
    public bool hasMoreToSayForAlys = true;

    /// <summary>
    /// The values defined in this constructor will be the default values the game starts with when there's no data to load
    /// </summary>
    public GameData()
    {
        //playerPosition = Vector3.zero;

        //itemsInInventory = new List<ScriptableObject>();
        //for (int i = 0; i < inventoryMaxSpace; i++)
        //{
        //    itemsInInventory.Add(null);
        //}

        //stackableItemsAmountInInventory = new List<int>();
        //for (int i = 0; i < inventoryMaxSpace; i++)
        //{
        //    stackableItemsAmountInInventory.Add(-1);
        //}

        //allCharacterItemsList = new List<CharacterItemsSaveLoad>();

        //allCharacterStatsList = new List<CharacterStatsSaveLoad>();

        allNPCsPositionsList = new List<NPCPositions>();
        allNPCsPositionsList.Add(null); // Starts from 1 since the Player does not have NPCMMovement script.
        for (int i = 0; i < Enum.GetNames(typeof(Enums.Characters)).Length; i++)
        {
            allNPCsPositionsList.Add(null);
        }
    }
}
