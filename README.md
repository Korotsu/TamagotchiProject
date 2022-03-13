# TamagotchiProject
## Features

-A fully working tamagotchi pre-configured.
-A strong tool to create or edit your tamagotchi with a fully custom editor (using propertyDrawers).
-A procedurally generated tamagotchi (UI,Gameplay,Interaction,etc) with the information you put in the editor.
-TamagotchiManager class working standalone.
-The ActionManager/ModifierManager/HumorManager are add-ons to the tamagotchiManager class to grant him more functionality, but are working independently from each other.
-The TamagotchiManager allows you to create needs that have statistics which are the main component of the Tamagotchi.
-The TamagotchiManager manage those statistics and use a Utility System linked with some probabilities to get a non-determinist AI.
-Within the editor of the Tamagotchi manager, you can custom many parameters like custom curves for the evolution of statistics, the framerate, requests, names, probabilityStep...
-The modifiers are elements managed by the ModifierManager, those elements are the most important parts to customize the tamagotchiManager:
-They are composed of Influencers, Impacters and Conditions, and a chance to trigger.
-Influencers are elements that will influence when active different stats in impacted needs (influence = modify how stats will evolve on a period of time via a coefficient) they can be time-limited or infinite.
-Impacters are elements that will impact when active different stats in impacted needs (impact change directly the value of the stats (not continue as an influencer)), they can have three types:
-Once = only hit one time, continuous = will update every tick, and regular = will update at a specified frame rate (all managed with coroutine).
-For continuous and regular you can specify in the editor if you want a time-limited impacter which in the case will hit corresponding to his type during the amount of time specified (can be infinite as an influencer).
-If they are not time-limited, you can precisely how many hit you want (can be infinite too).
-Conditions: those are conditions to be checked by the ModifierManager to check if he starts the modifier, Ex: a condition is a "Need" superior/inferior to an amount (fully customizable again).
-You can limit the framerate to check active/inactive modifiers separately.
-ActionManager allows you to create custom action linked with a UI element to give input for the player and allow him to have control on the Tamagotchi ex: Go to sleep.
-Each action has a list of impacters that can be customized to impact positively/negatively the different stats/needs (One action can impact many needs (as all features of the Tamagotchi the goal was to create a
fully flexible and easy use tool)).
-HumorManager (aka Mood Manager), manage moods, a mood is a custom statistic (with all the same elements like custom curves), which have an influencer that is other stats/need that influences them with different ratio.
The mood has consequences that are basically influencers who will start when the mood is active.
-In the editor (script) you can customize UI elements directly to control and customize how UI elements are generated. 


## Installation
- Choose the folder you want the game to be installed in.
- Then git clone ssh: //git@git.isartintra.com: 2424/2021/GP_2023_Tamagotchi_AI/Groupe_09. Git using a git bash terminal or a git software;

## How to use?

-Launch the unity Editor and load the scene name Sample Scene then hit play.
-Or go to the build folder, then launch the build.
-Enjoy!