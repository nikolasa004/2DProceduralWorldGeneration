using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newTileClass",menuName ="Tile Class")]
public class TileClass : ScriptableObject  //o tajlovima
{
    public string tileName;
    //public Sprite tileSprite;
    public Sprite[] tileSprites;  //razliciti tajlovi za isto
    public bool isBackground = true;
}
