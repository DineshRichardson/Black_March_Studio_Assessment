# Black_March_Studio_Assessment

This Unity project consists of several assignments related to grid-based game development, including grid block generation, obstacle creation, pathfinding, and enemy AI.

## Assignment 1 – Grid Block Generation

In this assignment, a 10x10 grid of Unity Cubes is generated. Each cube is represented by a GameObject with an attached script containing information about the tile.

### Implementation Details

- **Game Manager:** Utilizes Unity's GameObject system to create a 10x10 grid of cubes.
- **GridBlock:** Each cube has a script attached to it providing information about the specific tile.

## Assignment 2 – Obstacles

A Unity Tool is created to generate obstacles on the grid. The tool features toggleable buttons representing the grid, allowing the user to block specific tiles with obstacles. The obstacle data is stored in a Scriptable Object.

### Implementation Details

- **ObstacleEditor:** Allows users to toggle buttons to represent obstacles on the grid.
- **ObstacleData:** Stores obstacle data.
- **ObstacleManager:** Use the value stored in ObstacleData and spawn obstacle on grid

## Assignment 3 – Pathfinding

A player unit is generated on the map, capable of moving to any selected tile on the grid. The movement is based on a grid-based pathfinding algorithm, avoiding obstacles if implemented in Assignment 2. Input is disabled while the unit is in motion.

### Implementation Details

- **PlayerControl:** Can move to any selected tile and Disabled inputs during unit movement.
- **PathFinding:** Implemented custom PathFinding without using Unity Pathfinding.

## Assignment 4 – Enemy AI

An enemy unit is introduced with the objective of moving closer to the player unit. The enemy unit utilizes the same pathfinding algorithm from Assignment 3 or Unity Pathfinding if the previous assignment was not attempted. The Enemy AI script adheres to proper OOP concepts, inheriting from an AI interface.

### Implementation Details

- **EnemyAI:** Moves toward the player unit.
 
## How to Use

1. Clone the repository.
2. Open the Unity project in the Unity Editor.
3. Explore to the scenes folder and load "Main Scene"
4. Playtest and observe the functionality as described in each assignment.

Feel free to contribute, report issues, or suggest improvements!

## Acknowledgments

- This project is part of an assessment by Black March Studio
