# Unity-3D-MiniGolf-Game
A physics-based 3D MiniGolf game built in Unity 6 featuring a smart occlusion camera system and interactive obstacles.
⛳ 3D MiniGolf Adventure
A charming and physics-driven 3D mini-golf game built in Unity 6. Navigate tricky courses, use pipes and portals, and master the camera to sink the perfect putt.

✨ Features
Physics-Based Gameplay: Precise ball control utilizing Unity's built-in physics engine.

Smart Occlusion Camera: A robust, Raycasting system (C#) dynamically detects obstacles (like pipes and walls) blocking the player's view and hides them, ensuring an unobstructed gaming experience.

Custom Camera Controller: Intuitive orbital camera control using the mouse (right-click drag) with a customizable RotateAround mechanic.

Interactive Elements: Challenging course designs featuring winding pipes and stylized "frog mouth" portals.

URP Graphics: Built on the Universal Render Pipeline for optimized performance and a clean, modern visual style.

🛠️ Tech Stack & Skills
Game Engine: Unity 6 (6000.3.12f1 LTS)

Programming Language: C#

Rendering: Universal Render Pipeline (URP)

Input System: Unity's modern Input System package.

Key Mechanisms:

Physics & Collision Detection (Rigidbodies, BoxColliders, MeshColliders, Layers)

Mathematical interqueries (Vector3, Physics.Raycast, Quaternion, Distance, Normalized Vectors)

📂 Project Structure
This repository contains the full Unity project source:

/Assets/Scripts: All C# scripts, including CameraController.cs.

/Assets/Scenes: The first 3 completed playable levels.

/Assets/Prefabs: Reusable game objects (Ball, PipeStraight, FrogPortalExit/Entry, etc.).

/Assets/Idyllic Fantasy Nature: Art assets used to construct the environment.

🚧 Future Roadmap (Upcoming Daily Updates)
[ ] More Scenes organized by difficulty : two more medium levels and two more hard levels.
[ ] Scene Management: Implement a LevelManager for seamless transition between levels (1 through 7).
[ ] Audio: Add dynamic sound effects for collisions, putts, and entering the hole, along with background music.


🚀 Devlog: Medium Level Completion & Camera System Fix - 10.04.2026
🟢 Progress Summary:
Today I successfully completed the First Medium Difficulty Level and overhauled the Camera Obstacle System to handle complex hierarchies.

🛠️ Key Technical Solved:
Advanced Raycasting: Implemented a robust detection logic using IsChildOf. This allows the camera to hit a small "sensor" (a Cube on Layer 6) and trigger the hiding of the entire parent structure (like the Windmill or Pipe tunnels).

Layer-Based Filtering: Refined the script to hide all child renderers except those on Layer 6. This ensures the Raycast doesn't lose its target even when the object becomes "invisible" to the player.

Hierarchy Optimization: Organized the Windmill assets so the FanAnimation is a child of the Base. This fixed the issue where parts of the obstacle remained visible while others disappeared.

Scene 4 (Medium) Setup: Fully configured the environment for the new level, including terrain adjustments and proper obstacle placement.

🚀 Devlog: Aesthetic Overhaul & Dynamic Obstacles - 11.04.2026
🟢 Progress Summary:
Today I finalized the Second Medium Difficulty Level and executed a major visual pivot. The development cycle was extremely efficient, with zero major bugs or compilation regressions.

🛠️ Key Technical Solved / Implemented:

Visual Identity Shift: Replaced standard environment assets with a Sakura-themed aesthetic (pinks/purples). This involved re-mapping materials and adjusting world-space lighting to create a more unique and immersive atmosphere.

Dynamic Timing Obstacles: Integrated a complex moving part,a Hammer strike. Calibrated the collision box to ensure fair but challenging "timing-based" gameplay.

Strategic Level Design: Implemented a "High Risk, High Reward" shortcut. Designed a specific jump that rewards good aiming and power with a hole-in-one.

Environment Detailing: Optimized the placement of rock formations and varied tree hierarchies to break the tiling of the terrain and improve the overall "Game Feel."

📅 Next Steps:

Commence development on Hard Levels.

