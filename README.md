# Dungeon Cat

Dungeon Cat is a 2D top-down adventure/puzzle game based around a cat exploring a dungeon. This dungeon is filled with strange beasts, puzzles, and hidden secrets.

## Current Functionality
The vast majority of the core extensible components on which the entirety of the game is built are now implemented. They include:
- Basic navigation of the scene by the user with our cat avatar.
- User interaction with various entities, including structural entities (doors, walls, etc.) and items.
- User interaction with characters for dialogue.
Because our envisioned structure is quite minimal, there are only two major building blocks we have yet to implement, the saving functionality and a fighting system.

## Local Development

### Prerequisites: 

The Unity Editor version required for the project is [**2022.3.17f**](https://unity.com/releases/editor/qa/lts-releases#:~:text=January%209%2C%202024-,LTS%20Release,2022.3.17f1,-Released%3A%20January)

The preferred IDE for scripting is [JetBrains Rider](https://www.jetbrains.com/lp/dotnet-unity/)

### Instructions:
1. Clone this repository
2. Navigate to the root directory of the cloned repository
3. Open ./DungeonCat with the specified Unity Editor

### Testing:
To run the tests locally, from the Unity Editor top bar open `Window > General > Test Runner`. 

### Running:
To run the project, press the play button at the top middle of the Unity Editor. Press it again to stop the project.

### Building:
TBD

## Repo Structure

### Unity Project

The main Unity Project is located in the `/DungeonCat` directory.

The Unity scene files that will be predominantly edited by the Scene Masters are in `/DungeonCat/Assets/Scenes`

The Unity scripts that will be predominatly edited by the Scripting Engineers are in `/DungeonCat/Assets/Scripts`

Our test suite is located in `/DungeonCat/Assets/Tests`, both `EditMode` and `PlayMode`

### Reports

Our project reports are stored in `/reports`

### CI / CD

Out GitHub actions scripts will be in `/.github/workflows` 
<!-- TODO saved Rider run configuration for running the tests -->
