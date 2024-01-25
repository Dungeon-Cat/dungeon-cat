# Dungeon Cat

Dungeon Cat is a 2D top-down adventure/puzzle game based around a cat exploring a dungeon. This dungeon is filled with strange beasts, puzzles, and hidden secrets.

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

## Devloping Locally

The Unity Editor version required for the project is [**2022.3.17f**](https://unity.com/releases/editor/qa/lts-releases#:~:text=January%209%2C%202024-,LTS%20Release,2022.3.17f1,-Released%3A%20January)

We'll be using [JetBrains Rider](https://www.jetbrains.com/lp/dotnet-unity/) as our primary IDE for developing the project, default settings

To run the tests locally, from the Unity Editor top bar open `Window > General > Test Runner`

<!-- TODO saved Rider run configuration for running the tests -->