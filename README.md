<p align="center">
  <img src="https://cdn.prod.website-files.com/68080400505540af2c69455a%2F680eb41766f6511b23cf8097_2025-04-28%2000-45-59-poster-00001.jpg" alt="Creallies gameplay" width="900">
</p>

<h1 align="center">Creallies</h1>

<p align="center">
  <strong>A mobile creature-collection game that turns real-world QR codes into encounters.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/Unity-6000.0.9f1-000000?logo=unity&logoColor=white" alt="Unity 6000.0.9f1">
  <img src="https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/Platform-Mobile-ad7fd7" alt="Mobile">
  <img src="https://img.shields.io/badge/Status-Released-2ea44f" alt="Released">
</p>

## Overview

Creallies is a mobile game created in 10 days at Creajeux by a team of five programmers and three artists. Players scan QR codes in their environment to discover creatures, complete a timing-based capture challenge, grow their collection and prepare for battles.

The project was designed around short mobile sessions and a clear loop:

1. Scan a QR code with the device camera.
2. Discover an encounter linked to the scanned zone.
3. Capture the creature through a timing minigame.
4. Save it to the creature box and update the CreaDex.
5. Build a collection and use creatures in battle.

| | |
|---|---|
| **Role** | Project structure, gameplay systems and integration |
| **Team** | 5 programmers, 3 artists |
| **Duration** | 10 days |
| **Platform** | Mobile |
| **Status** | Released in 2025 |

## My Contribution

I focused on giving the team a reliable structure that could support rapid iteration during a short production:

- Established the Unity project hierarchy and shared conventions for a mixed programming and art team.
- Built modular UI and scene-flow systems used across the game's menus and gameplay.
- Integrated features across capture, collection and battle flows.
- Profiled the project and improved performance for mobile hardware.
- Helped keep data and transitions consistent between scenes.

## Technical Highlights

| System | What it does |
|---|---|
| [Persistent game data](Assets/Script/GameData.cs) | Stores player information, the creature box and CreaDex progress as JSON between sessions. |
| [QR scanning](Assets/Script/QR%20Code%20Scanner.cs) | Uses the mobile camera and ZXing to translate a scanned code into an encounter zone. |
| [Capture QTE](Assets/Script/CaptureQTE.cs) | Generates a moving success window and adapts the capture challenge to creature rarity. |
| [Async loading](Assets/Script/LoadingManager.cs) | Handles scene loading, fades and audio changes without blocking the game flow. |

## Tech Stack

- Unity 6000.0.9f1
- C#
- Unity Input System
- Shader Graph
- ZXing QR-code reader
- JSON persistence
- Android/iOS-oriented mobile workflow

## Open the Project

1. Clone this repository.
2. Add the project through Unity Hub.
3. Open it with Unity **6000.0.9f1**.
4. Open the **Start** scene and press Play.

QR scanning requires a camera-enabled device and camera permission. In-editor testing can use an available webcam.

## More

- [View the full portfolio case study](https://leopaulvray.lovable.app/projects/creallies)
- [Explore my other projects](https://leopaulvray.lovable.app)

## Author

Created as a Creajeux team project with systems and integration work by **Léo-Paul Vray**.
