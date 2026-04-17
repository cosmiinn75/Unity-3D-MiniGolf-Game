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

🚀 Devlog: Physics-Sync, Hard Levels & Audio Integration - 12.04.2026
🟢 Progress Summary:
Today was a high-productivity cycle focused on high-difficulty content and physics stabilization. I successfully completed two Hard Difficulty Levels (Hard 1 & Hard 2), solved the critical synchronization issues between the ball and moving platforms as well as a bug in the respawning feature. Additionally, I fixed the UI and I began the audio implementation phase.

🛠️ Key Technical Solved / Implemented:

Physics-Based Platform Sync: Implemented a robust Parenting System for moving platforms. By switching the platform movement to FixedUpdate I eliminated the "jitter" and "falling through" bugs, ensuring the ball stays perfectly grounded while the platform oscillates.

Intelligent Respawn Logic: Fixed a major "Race Condition" bug where the ball would respawn in mid-air if it fell while on a moving platform. Now the last position of the ball is only remembered if it's not on the platform(doesn't have the platform as a parent).

UI Positioning: Fixed a major visual by moving the UI text in the right positions.

Advanced Level Design (Hard 1 & 2): Constructed a Vertical Elevator mechanic for high-altitude hole placements.

Designed a Multi-Path Choice system, forcing players to choose between a safe, long route or a high-risk shortcut.

Audio Feedback System: Integrated the first iteration of sound effects using AudioSource.PlayClipAtPoint. Added a satisfying Putt Sound triggered precisely at the moment of launch, significantly improving the tactile "Game Feel."

Dynamic Ball Physics: Adjusted linearDamping (Drag) dynamically when the ball touches moving platforms. This "friction hack" prevents the ball from sliding off due to centrifugal force or rapid vertical acceleration.

📅 Next Steps:

Starting the development of the Main Menu with good scene management.

Integrating more sound effects and background music.

Adding a pause menu that let's you restart the level or quit to the Main Menu.

Adding a local BestScore that keeps track of the number of strokes you did in the 7 available levels.

🚀 Devlog: Main Menu Architecture, Audio & Navigation - 14.04.2026
🟢 Progress Summary:
Today I established the core game flow by implementing a fully functional Main Menu and a Pause system. I focused on the "Game Feel" by adding interactive audio feedback and ensuring seamless scene transitions across the project.

🛠️ Key Technical Solved / Implemented:

Main Menu Infrastructure: Constructed a dedicated Main Menu scene with a persistent UI layout. Integrated a SceneLoader logic to handle transitions from the menu to the initial levels, ensuring all game states reset correctly upon starting.

Interactive UI Audio: Implemented an audio feedback system for UI elements. Added PointerEnter sound effects to all menu buttons using EventTriggers, providing tactile confirmation when the player hovers over options.

Pause Menu Logic & Input Sync: Fully integrated the Pause Menu using the modern Input System. Refactored the script to use Invoke Unity Events, allowing the "Escape" key to reliably toggle Time.timeScale and manage the Pause UI overlay without input lag or double-triggering.

Navigation & Scene Management: Added "Quit to Main Menu" and "Resume Level" functionality. This involved managing the UnityEngine.SceneManagement namespace to handle level resuming while ensuring the timeScale is restored to 1.0 immediately upon exit.

📅 Next Steps:

Commence development on the Leaderboard system to track "Top 3" performances per level.

Design the "Hall of Fame" UI to display best strokes, time, and dates.

Adding more Audios Sounds for different number of strokes.

🚀 Devlog: Scoring Logic, Persistence & Audio Polishing - 16.04.2026
🟢 Progress Summary: Today I focused on the "behind-the-scenes" architecture, specifically the scoring and session management systems. I successfully debugged critical score duplication issues and finalized the integration of the Audio Mixer across the level playlist.

🛠️ Key Technical Solved / Implemented:

Best Scores & Data Persistence: Implemented the "Hall of Fame" system using JSON serialization and PlayerPrefs. The logic now correctly tracks, sorts, and displays the "Top 5" performances based on total strokes and time, ensuring player records persist after quitting.

Audio Mixer & Spatial SFX: Fully integrated the Audio Mixer to manage SFX and Music groups. Added a dedicated "Hole Sound" triggered upon level completion and linked all UI/gameplay audio sources to the Mixer to allow real-time volume control via the Options menu.

Refactored Level Completion Logic: Solved a critical "double-counting" bug where strokes were being added multiple times. By implementing a state-check (isGameOver) and isolating the scoring authority to the GameManager, I ensured that each level transition is precise and the total session score is mathematically accurate.

Dynamic Session Tracking: Updated the GameSessionManager to handle total time and stroke accumulation across a randomized playlist. Fixed a session-sync issue where the timer would reset between scenes; it now correctly pauses during transitions and resumes upon level load for an accurate "Total Playtime" metric.

🚀 Devlog: Congratulations System & Project Completion - 17.04.2026
🟢 Progress Summary: Today marks a major milestone: Golf Adventure is officially feature-complete! The focus was on the end-game experience, specifically creating a rewarding "Congratulations" system that summarizes the player's journey and finalizing the project for its multi-platform release (Windows & WebGL).

🛠️ Key Technical Solved / Implemented:

End-Game "Congratulations" Portal: Designed and implemented a dedicated victory screen that triggers upon completing the final hole. This system aggregates all session data—total strokes and final time—presenting them in a clean, polished UI that celebrates the player's performance.

Platform-Specific Optimization (WebGL vs. PC): Fine-tuned the build pipeline for dual-release. Implemented texture overrides and high-quality compression settings for the WebGL version to maintain visual clarity in-browser, while ensuring the Windows Standalone build utilizes the full power of the Universal Render Pipeline (URP) for maximum fidelity.

Final Project Polish & Deployment: Conducted a comprehensive sweep of the game loop, from the Main Menu to the final Congratulations page. Verified all physics triggers, scene transitions, and audio cues. The game is now successfully archived and deployed on Itch.io, featuring both an instant-play web version and a high-performance downloadable desktop build.



