<p align="center">
  <img src="https://cdn.prod.website-files.com/68080400505540af2c69455a%2F680eb41766f6511b23cf8097_2025-04-28%2000-45-59-poster-00001.jpg" alt="Creallies gameplay" width="900">
</p>

<h1 align="center">Creallies</h1>

<p align="center">
  <strong>A mobile creature-collection game where QR codes reveal Allies hidden in the real world.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Engine-Unity%206-000000?logo=unity&logoColor=white" alt="Unity 6">
  <img src="https://img.shields.io/badge/Language-C%23-239120?logo=csharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/Platform-Mobile-ad7fd7" alt="Mobile">
  <img src="https://img.shields.io/badge/Status-Released-2ea44f" alt="Released">
</p>

## Overview

Creallies is a mobile game developed at Créajeux from September to Christmas by five programmers and three artists.

Players scan QR codes placed in the real world to discover creatures called Allies. Each encounter can lead to a capture sequence, after which the creature joins the player's collection and becomes available through the box and Pokédex interfaces.

| | |
|---|---|
| **Role** | Project structure, systems and integration |
| **Team** | 5 programmers, 3 artists |
| **Duration** | September to Christmas |
| **Platform** | Mobile |
| **Engine** | Unity 6000.0.9f1 |

## Core Experience

- Scan and generate QR codes through the mobile camera flow.
- Discover creatures associated with different locations.
- Complete a timing-based capture sequence.
- Collect normal and shiny Allies.
- Browse captured creatures through a box and Pokédex.
- Preserve collection and player data between sessions.

## My Contribution

I established a clear project hierarchy at the beginning of the project and contributed to system integration across the game.

The structure and naming conventions helped a mixed team work in parallel, understand ownership and integrate new content quickly throughout several months of production.

## Code Highlights

| Area | Entry point |
|---|---|
| QR scanning | [`QR Code Scanner.cs`](Assets/Script/QR%20Code%20Scanner.cs) |
| QR generation | [`QR Code Generator.cs`](Assets/Script/QR%20Code%20Generator.cs) |
| Capture flow | [`CaptureManager.cs`](Assets/Script/CaptureManager.cs) |
| Capture interaction | [`CaptureQTE.cs`](Assets/Script/CaptureQTE.cs) |
| Collection and save data | [`GameData.cs`](Assets/Script/GameData.cs) |
| Selected creature state | [`SelectedCreamonManager.cs`](Assets/Script/SelectedCreamonManager.cs) |

## Technical Focus

- Unity and C#
- Mobile camera and QR workflows
- Persistent JSON save data
- Cross-scene game state
- UI integration
- Multidisciplinary production and integration

## Getting Started

1. Clone the repository.
2. Open it through Unity Hub with Unity `6000.0.9f1`.
3. Open `Assets/Scenes/Start.unity`.
4. Configure an Android-capable build target to test the complete camera and QR flow on a device.

## More

- [View the full portfolio case study](https://leopaulvray.com/projects/creallies)
- [Explore my other projects](https://leopaulvray.com)

## Author

Created as a Créajeux team project with development and integration work by [Léo-Paul Vray](https://github.com/Im0-R).
