using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [Header("Atlas")]
    public TileAtlas tileAtlas;  //atlas
    public float seed;

    public BiomeClass[] biomes;
   
    [Header("Biomes")]
    public float biomeFreq;
    public Gradient biomeGradient;
    public Texture2D biomeMap;

    [Header("Generation Settings")]
    public int chunkSize = 16;  //velicina chunka
    public int heightAdd=25; //koliko da ide u vis
    public int worldSize = 100;  //velicina sveta
    public bool generateCaves = true;  //da li da se generisu pecine
    
    [Header("Noise Texture Settings")]
    public float caveFreq = 0.05f; //koliko su pecine ceste
    public float terrainFreq = 0.05f; //koliko je zemlja cesta
    public Texture2D caveNoiseTexture;

    [Header("Ore Settings")]
    public OreClass[] ores;  //sve rude

    public CamController cam;
    public PlayerController player;

    private List<GameObject>worldTileObjects=new List<GameObject> ();
    private BiomeClass currentBiome;
    private Color[] biomesColors;
    private GameObject[] chunks; //lista svih chunkova
    private List<Vector2> worldTiles = new List<Vector2>(); //lista svih tajlova
    /*private void OnValidate()
    {
        DrawTextures();
        DrawTexturesAndOres();
    }*/
    private void Start()
    {
        /*for (int i = 0; i < ores.Length; i++)
        {
            ores[i].spread = new Texture2D(worldSize, worldSize);  //cc
        }*/
        seed = Random.Range(-10000, 10000); //pravi seed
        biomesColors = new Color[biomes.Length];
        for (int i = 0; i < biomes.Length; i++)
        {
            biomesColors[i] = biomes[i].biomeColor;
        }
        //DrawTextures();
        DrawBiomeMap();
        DrawTexturesAndOres();
        CreateChunks(); //pravi chunkove
        GenerateTerrain();  //pravi mapu
        cam.Spawn(new Vector3(player.spawnPos.x, player.spawnPos.y, cam.transform.position.z));
        cam.worldSize = worldSize;
        player.Spawn();
    }
    public void DrawBiomeMap()
    {
        float b;
        Color col;
        biomeMap = new Texture2D(worldSize, worldSize);
        for (int x = 0; x < biomeMap.width; x++)
        {
            for (int y = 0; y < biomeMap.height; y++)
            {
                b = Mathf.PerlinNoise((x + seed) * biomeFreq, (y + seed) * biomeFreq);
                col = biomeGradient.Evaluate(b);
                biomeMap.SetPixel(x, y, col);
            }
        }
        biomeMap.Apply();
    }
    public void DrawTexturesAndOres()
    {
        caveNoiseTexture = new Texture2D(worldSize, worldSize);
        for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                currentBiome = GetCurrentBiome(x, y);
                float v = Mathf.PerlinNoise((x + seed) * caveFreq, (y + seed) * caveFreq);
                if (v > currentBiome.surfaceValue)
                {
                    caveNoiseTexture.SetPixel(x, y, Color.white);
                }
                else
                {
                    caveNoiseTexture.SetPixel(x, y, Color.black);
                }
            }
        }
        caveNoiseTexture.Apply();
        /*for (int x = 0; x < worldSize; x++)
        {
            for (int y = 0; y < worldSize; y++)
            {
                currentBiome = GetCurrentBiome(x, y);
                for (int i = 0; i <ores.Length; i++)
                {
                    ores[i].spread.SetPixel(x, y, Color.black);
                    if (currentBiome.ores.Length >= i+1)
            //cc        {
                        float v = Mathf.PerlinNoise((x + seed) * currentBiome.ores[i].rarity, (y + seed) * currentBiome.ores[i].rarity);
                        if (v > currentBiome.ores[i].size)
                        {
                            ores[i].spread.SetPixel(x, y, Color.white);
                        }
                        ores[i].spread.Apply();
                    }
                }
            }
        }*/
    }
    public void DrawTextures()
    {
        biomeMap = new Texture2D(worldSize, worldSize);
        for (int i = 0; i < biomes.Length; i++)
        {
            biomes[i].caveNoiseTexture = new Texture2D(worldSize, worldSize);  //pravi texturu za pecinu
            for (int j = 0; j < biomes[i].ores.Length; j++)
            {
                biomes[i].ores[j].spread = new Texture2D(worldSize, worldSize);  //pravi texturu za rude
                GenerateNoiseTextures(biomes[i].ores[j].rarity, biomes[i].ores[j].size, biomes[i].ores[j].spread);  //pravi noise za rude
            }
        }
    }
    public void CreateChunks()
    {

        int numChunks = Mathf.CeilToInt((float)worldSize / chunkSize); //broj chunkova
        chunks = new GameObject[numChunks];  //niz svih chunkova
        for (int i = 0; i < numChunks; i++)
        {
            GameObject chunk = new GameObject();
            chunk.name = i.ToString();
            chunk.transform.parent = this.transform;
            chunks[i] = chunk;
        }
    }
    public BiomeClass GetCurrentBiome(int x,int y)
    {
        if (System.Array.IndexOf(biomesColors, biomeMap.GetPixel(x, y)) >= 0)
            return biomes[System.Array.IndexOf(biomesColors, biomeMap.GetPixel(x, y))];

        return currentBiome;
    }
    public void GenerateTerrain() //pravi mapu
    {
        Sprite[] tileSprites; //promenjiva sprite
        for (int x = 0; x < worldSize; x++)
        {
            float height; 

            for (int y = 0; y <worldSize; y++)
            {
                currentBiome = GetCurrentBiome(x, y);
                height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * currentBiome.heightMultiplayer + heightAdd; //visina povrsine
                if(x==5)
                {
                    player.spawnPos = new Vector2(x, height + 2);
                }
                if(y>=height)
                {
                    break;
                }
                if (y<height-currentBiome.dirtLayerHeight)  //ispod zemlje
                {
                   tileSprites =currentBiome.tileAtlas.stone.tileSprites;

                    if (ores[1].spread.GetPixel(x, y).r > 0.5f && ores[1].maxSpawnHeight<height - y && currentBiome.tileAtlas.coal!=null)
                    {
                        tileSprites =currentBiome.tileAtlas.coal.tileSprites;
                    }
                }
                else if(y<height-1)  //ispod trave
                {
                    tileSprites =currentBiome.tileAtlas.dirt.tileSprites;

                    if (ores[0].spread.GetPixel(x, y).r > 0.5f && ores[0].maxSpawnHeight <height - y && currentBiome.tileAtlas.rock!=null)
                    {
                        tileSprites =currentBiome.tileAtlas.rock.tileSprites;
                    }  
                }
                else //na povrsini
                {
                    tileSprites =currentBiome.tileAtlas.grass.tileSprites;
                }


                if (y < height - 1)
                {
                    TileClass backgroundTile = y < height - currentBiome.dirtLayerHeight ? currentBiome.tileAtlas.stone : currentBiome.tileAtlas.dirt;

                    if (backgroundTile != null)
                    {
                        PlaceBackgroundTile(backgroundTile.tileSprites, x, y);
                    }
                }

                if (generateCaves) //ako treba da ima pecina
                {

                    if (caveNoiseTexture.GetPixel(x, y).r > 0.5f) //prave se tajlovi samo ako nije pecina
                    {
                        PlaceTiles(tileSprites, x, y,false,-5);
                    }
                }
                else //ako nema pecina svuda se formiraju tajlovi
                {
                    PlaceTiles(tileSprites, x, y,false,-5);
                }
                if(y>=height-1)
                {
                    int r = Random.Range(0,currentBiome.treeChance);
                    if (r == 1) 
                    {
                        if (worldTiles.Contains(new Vector2(x, y))&&currentBiome.tileAtlas.log!=null) //pravi se drvece
                        {
                            GenerateTrees(Random.Range(currentBiome.minTreeHeight,currentBiome.maxTreeHeight), x, y + 1);
                        }
                    }
                    else
                    {
                        int i = Random.Range(0,currentBiome.tallGrassChance);
                        if (i == 1)
                        {
                            if (worldTiles.Contains(new Vector2(x, y)) && currentBiome.tileAtlas.tallGrass!=null) //pravi se tall grass
                            {
                                PlaceTiles(currentBiome.tileAtlas.tallGrass.tileSprites, x, y + 1,true,-4);
                            }
                        }
                    }
                }
            }
        }
    }
    public void GenerateNoiseTextures(float freq,float limit, Texture2D noiseTexture)  //pravi noise teksturu
    {
        float v;
        
        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                v = Mathf.PerlinNoise((x+seed)*freq, (y+seed)*freq);
                if (v > limit)
                {
                    noiseTexture.SetPixel(x, y, Color.white);
                }
                else
                {
                    noiseTexture.SetPixel(x, y, Color.black);
                }
            }
        }
        noiseTexture.Apply();
    }
    public void GenerateTrees(int treeHeight, int x,int y) //pravi se drvece
    {
        PlaceTiles(currentBiome.tileAtlas.root.tileSprites, x, y,true,-5);
        for (int i = 1; i <treeHeight; i++)
        {
            PlaceTiles(currentBiome.tileAtlas.log.tileSprites, x,y+i,true,-5); //log
        }
        PlaceTiles(currentBiome.tileAtlas.leafs.tileSprites, x, y + treeHeight+1,true,-4);
    }

    public void RemoveTile(int x, int y)
    {
        int index = worldTiles.IndexOf(new Vector2(x, y));

        if (index == -1)
            return;

        Destroy(worldTileObjects[index]);

        worldTileObjects.RemoveAt(index);
        worldTiles.RemoveAt(index);
    }

    public void PlaceTiles(Sprite []tileSprites,int x,int y,bool backgroundElement,int order)  //formira tajlove
    {
        if (!worldTiles.Contains(new Vector2Int(x, y)) && x>=0 && x<=worldSize && y>=0 && y<=worldSize)  //trazi se da li postoji odgovarajuci tajl
        {
            GameObject newTile = new GameObject();
            int chunkCoord = x / chunkSize;
            chunkCoord /= chunkSize;
            newTile.transform.parent = chunks[chunkCoord].transform;
            newTile.AddComponent<SpriteRenderer>();
            if (!backgroundElement)
            {
                newTile.AddComponent<BoxCollider2D>();
                newTile.GetComponent<BoxCollider2D>().size = Vector2.one;
                newTile.tag = "Ground";
            }
            int spriteIndex = Random.Range(0, tileSprites.Length);
            newTile.GetComponent<SpriteRenderer>().sprite = tileSprites[spriteIndex];
            newTile.GetComponent<SpriteRenderer>().sortingOrder = order;
            newTile.name = tileSprites[0].name;
            newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);
            worldTileObjects.Add(newTile);
            worldTiles.Add(newTile.transform.position - Vector3.one * 0.5f);
        }
    }


    private void PlaceBackgroundTile(Sprite[] sprites, int x, int y)
    {
        if (sprites == null || sprites.Length == 0)
            return;

        GameObject background = new GameObject("CaveBackground");

        background.transform.SetParent(chunks[x / chunkSize].transform);
        background.transform.position = new Vector3(x + 0.5f, y + 0.5f, 0f);

        SpriteRenderer renderer =
            background.AddComponent<SpriteRenderer>();

        renderer.sprite = sprites[Random.Range(0, sprites.Length)];
        renderer.sortingOrder = -6;

        // Potamniti
        renderer.color = new Color(0.5f, 0.5f, 0.5f, 1f);
    }
}
