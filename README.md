# 2D Procedural World Generation

<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20212735.png" alt="A closer look at the little player, trees, terrain layers, and underground caves" width="900">
</p>

*A little procedural-world experiment that was supposed to become a much bigger game.*

**[▶ Watch the gameplay video](https://youtu.be/WwFut9buozU)** · **[⬇ Download the Windows prototype](https://github.com/nikolasa004/2DProceduralWorldGeneration/releases/tag/v1.0.0)**

## So, what is this?

This is a small 2D Unity project I made about **five years ago**, while I was still in high school. It isn't really a finished game. It's mostly an experiment with **generating a different world each time you play**, with a little frog-like character who can walk around, jump, and break blocks.

The world has forests, deserts, and snowy areas, along with changing terrain heights, trees, underground caves, and ores. Sometimes you get a world that's almost entirely one biome. Other times you might see a forest between two deserts, or a desert interrupting a stretch of green terrain. That's the part I was most interested in: seeing how different the same piece of code could make the world look.

If you'd rather see it moving than look at screenshots, **[here's a short gameplay video](https://youtu.be/WwFut9buozU)** showing the character exploring the different environments.

The screenshots below were taken from the **Unity Editor's Scene view**, zoomed out so that more of the generated world would fit on screen. That's why you can see the editor grid and camera outline in some of them. The video gives a better idea of what actually playing it looks like.

## Six different worlds

Here are six examples of what the generator can produce with more ordinary settings. There's no single fixed layout or required order of biomes: the distribution can change quite a lot from one generation to the next.

<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20210340.png" alt="Generated world with a large desert between forest areas" width="900">
</p>
<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20210417.png" alt="A mostly desert world with scattered underground caves" width="900">
</p>
<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20210448.png" alt="A mainly forested world with rolling hills, trees and caves" width="900">
</p>
<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20210519.png" alt="A snowy biome surrounded by forest" width="900">
</p>
<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20210547.png" alt="Forest terrain between two desert areas" width="900">
</p>
<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20210832.png" alt="A transition between snowy, forest and desert terrain" width="900">
</p>

## What happens when I mess with the settings?

One of my favorite things about this project was changing a few values and seeing what happened. The generator has settings for things like **biome frequency, the amount and shape of caves, and differences in terrain height**. Small changes can give you quite different landscapes. Push them too far, and the results get a little ridiculous.

These next three screenshots aren't examples of normal gameplay. They're **deliberately extreme generations**, just to show how much the parameters can affect the result:

<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20212128.png" alt="Experimental generation with extreme noise settings and a chaotic underground layout" width="900">
</p>
<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20212259.png" alt="Experimental generation showing a very different pattern of underground noise" width="900">
</p>
<p align="center">
  <img src="docs/images/Screenshot%202026-10-19%20212420.png" alt="Experimental generation with exaggerated biome transitions and terrain height differences" width="900">
</p>

They're not necessarily pretty, but I think they're a fun way to show that the world isn't a picture I made in advance.

## How does it work?

The main idea is **Perlin noise**. Unlike choosing a completely random height for every column of blocks, Perlin noise gives you values that change gradually. That's what makes hills look like hills instead of a mess of unrelated vertical lines.

I used it in a few different ways: one noise-based map helps choose which **biome** appears in each part of the world, another affects the **surface height**, and another acts as a mask for **underground caves**. The selected biome also determines which sprites are used for the surface and underground layers, as well as things like trees and tall grass. There are also ore textures and settings for placing different materials underground.

The blocks are created as objects in the Unity scene, with colliders for the solid terrain. The darker cave backgrounds sit behind those blocks, without colliders, so you can walk through the empty spaces and still see underground material behind them. I also grouped terrain objects into chunks to keep the scene a little more organized.

It's not a particularly sophisticated system, and some parts of the code are definitely rough. At the time, I was following a YouTube tutorial, experimenting with the values, and trying to understand why things worked (or didn't). I was learning as I went.

## How it started

Back then, I had started realizing that I didn't really enjoy building games out of other people's art. I wanted to make my own assets, but I'd also tried 3D modeling and found out just how much time it could take. **2D pixel art** seemed like a much more realistic way to make something that felt like mine.

I was also really interested in *Terraria*. It was proof to me that a game didn't need complicated graphics to have an interesting world. And since I was already fascinated by unusual algorithms, the idea of **procedurally generating biomes and terrain** was hard to resist.

The problem was that I barely knew how to program at the time. I was still in high school, with no AI coding assistant to turn to, so I followed a YouTube tutorial and did a lot of figuring things out, testing changes, and getting stuck along the way. Eventually, I got the world generation working, drew some simple art, and added my little character so there was actually someone to move around in it.

I made this over **a few days of winter break**. Then school started again, and later I moved on to another project. What you see here is pretty much where I left it.

## The game I originally had in mind

The procedural world wasn't meant to be the whole game. It was supposed to be **the world outside a kingdom**.

My idea was that the player would spend time inside a walled kingdom, seen from a **top-down perspective**, with shops, quests, and people to interact with. Outside the walls, though, the world would be dangerous and full of monsters. You'd have to go out to gather resources, fight, explore, and bring useful things back home.

The twist was that **the outside world would change every time you left the kingdom**. Maybe magic was responsible; I hadn't worked out the explanation. You could dig, build, and explore during an expedition, but when you returned home, that temporary world would be gone. The next trip would take you somewhere completely different. Some things you discovered could stay with you permanently, and you'd use your finds to complete quests and improve your character back in the kingdom.

So, even though the little frog and the pixel art look rather cheerful, the game I imagined was supposed to become **much darker and more difficult**. I didn't want to make a copy of *Terraria*; I liked the idea of having a safe home and dangerous, ever-changing expeditions outside it.

Obviously, that was far too much for the few days I had. I only finished the first experiment: the world generator and a character who could move through it and break blocks.

Funny enough, the idea never really disappeared. **About five years later, when I started writing a fantasy novel**, I found myself coming back to parts of it: a fortified kingdom, a terrifying world beyond its walls, and people who risk going outside. I also read *The Demon Cycle* years after first imagining this game and was surprised to find some familiar-sounding ideas there. I hadn't known about the books when I came up with my own version, which made the coincidence interesting to me.

## Downloading and trying it

You can get the Windows build from **[Release v1.0.0](https://github.com/nikolasa004/2DProceduralWorldGeneration/releases/tag/v1.0.0)**:

1. Download **`2DProceduralWorldGeneration.zip`**.
2. Extract the entire ZIP, not just the `.exe`.
3. Run the executable to generate a world and explore it.

The character can move left and right, jump, and destroy blocks with the mouse. There's no inventory, quest system, combat, or finished gameplay loop here - those were ideas for a game I never got around to making.

The repository also contains the **Unity source project** (`Assets`, `Packages`, and `ProjectSettings`) and these screenshots, if you want to look at how I put the experiment together.

This was one of my earliest attempts at making my own little world. It isn't polished, and I wouldn't pretend the code is perfect, but I still like that I got something working when I knew so little about programming. That's why I decided to keep it and share it.

Thanks for taking a look!
