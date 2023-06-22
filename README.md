# Kinetix Contextual Emotes Demo

A point and click scene made in Unity LTS 2022.3.1f1 showcasing the contextual emotes feature of the [Kinetix](https://kinetix.tech/) [SDK for Unity](https://docs.kinetix.tech/gs-1/unity). 

![Demo screenshot](https://github.com/nargacu83/kinetix-contextual-emotes-example-unity/assets/11914315/b4f00274-b79d-48a9-a52a-b651fc2229cf)

This demo is a point and click style game with NPCs and dialogues. Each time you interact with an NPC, your character will play a contextual emote you assigned from the contextual tab in the emote wheel.
There are three interactions:

- Hello : Emote assigned for greeting an NPC.
- Win : Emote assigned the correct answer was selected.
- Lose : Emote assigned the wrong answer was seleted.

This demo is using **Coroutines instead of async functions to be compatible with mobile plateforms and WebGL**.
The dialogue system is using a ScriptableObject where you give a text, choices and of course the contextual emote name to play.

## Usage

1. Clone the repository.
2. Open the project using **Unity LTS 2022.3.1f1**.
3. Open the **SampleScene**.
4. Assign your Virtual World Key on the **DemoManager** GameObject.
5. Play the scene.

## Resources

[Kinetix SDK Docs](https://docs.kinetix.tech/)
[Unity Docs](https://docs.unity3d.com/)
