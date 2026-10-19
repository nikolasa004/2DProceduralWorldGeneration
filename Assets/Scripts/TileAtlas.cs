using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="TileAtlas",menuName ="Tile Atlas")]
public class TileAtlas : ScriptableObject  //atlas
{
    [Header("Enviroment")]
    public TileClass stone;
    public TileClass grass;
    public TileClass dirt;
    public TileClass root;
    public TileClass log;
    public TileClass leafs;
    public TileClass tallGrass;
    //public TileClass sand;
   // public TileClass snow;
   // public TileClass sandStone;

    [Header("Ores")]
    public TileClass coal;
    public TileClass rock;
}
