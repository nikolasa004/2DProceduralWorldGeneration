using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BiomeClass
{
    public string biomeName;
    public Color biomeColor;
    public TileAtlas tileAtlas;

    [Header("Noise Texture Settings")]
    public Texture2D caveNoiseTexture;

    [Header("Generation Settings")]
    public int dirtLayerHeight = 5;  //debljina zemlje
    public bool generateCaves = true;  //da li da se generisu pecine
    public float surfaceValue = 0.25f; //sto veci broj to vise pecina
    public float heightMultiplayer = 25f; //kolike planine da budu

    [Header("Trees")]
    public int treeChance = 15;  //sansa da se pojavi drvo za svaki blok na povrsini
    public int minTreeHeight = 3; //minimalna visina drveta
    public int maxTreeHeight = 6;  //maksimalna visina drveta

    [Header("Addons")]
    public int tallGrassChance = 3;  //1 u x da se na bloku na kojem nije drvo pojavi trava

    [Header("Ore Settings")]
    public OreClass[] ores;  //sve rude
}
