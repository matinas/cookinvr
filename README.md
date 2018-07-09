# cookinvr

## Overview

The game is a little cooking game inspired in the VR game [Job Simulator](https://store.steampowered.com/app/448280/Job_Simulator/) and the 2D game [Cook, Serve, Delicious](https://store.steampowered.com/app/247020/Cook_Serve_Delicious/). It's thought to be played in high-end VR headsets like the Vive, Rift or Windows Mixed Reality, but if it comes up well maybe it can be ported to mobile VR headsets or even standalone as I would like to experiment with Oculus Go. The game is suitable for casual gamers and more generally for anyone that wants to spend a good time interacting with different objects in VR in order to make and serve some food. The idea arose basically due to the fact that I would like to experiment a bit more on VR interaction, grabbing things, manipulating objects, etc. so I think a cooking game is a good way to cover all this points.

Roughly speaking, the game puts the player inside a kitchen where he will have to make different orders that will come from clients, having to chop, boil and put together the different ingredients needed for the requested order. There will be a timer for each order so to put a bit of pressure on the player and make it more fun, engaging and challenging.

After initial reflections on the idea I decided to stick to the minimum viable mechanic to keep things simple at first; so no clients or other NPCs, no complex animations, etc. I'll enhance the gameplay workflow as I iterate over it later. So, the basic gameplay workflow I would like to get working for a first version is the following:

1. Next order magically appears in the Order Box (represented as a kind of cartridge or something similar; idea inspired on Job Simulator and First Touch VR games).
2. Grab the order and place it in something we will call the Order Machine which will make the receipt for the order to be shown on a kind of 3D screen in front of the user workstation.
3. Pick the ingredients from the shelves on the sides or the Fridge Box.
4. Place the ingredients one by one on the little Assembly Board that's above the main allowance according to the receipt.
5. Once the order is ready to go, touch the Dispatch Order Button located on the Order Machine.
6. Grab the next order and continue playing.

I think this is a more simplistic scenario than the one I have initially thought, which will force me to implement prototypes for the most basic mechanics (grabbing orders, show order information, grabbing ingredients and putting them together) as well as try them together in their most basic form. So I'll stick to this idea for now, and hope I can have enough time to further improve it later.

The **actual status of the game is shown in the video below**. Check the *Demo section* on the CookinVR PDF document at the root folder for more videos on the game's evolution.

<a target="_blank" href="https://www.youtube.com/watch?v=nuWQtAUw7ZI"><img src="http://img.youtube.com/vi/nuWQtAUw7ZI/0.jpg" alt="Last version" width="640" height="480" border="10" /></a>

> The sections that follows in this README roughly describe some of the studies and decisions made during the development of the game, > as well as the carried out prototyping, user testing, etc. For a more thoroughly description of each section **refer to the PDF document at the root folder**.

## State of the Art

Surprisingly, I have found a bunch of cooking/food serving VR games on the Steam Store based on a similar idea, some of which are listed below. I've checked them all and took some notes about a few possible features and improvements to add to my own.

[Job Simulator](https://store.steampowered.com/app/448280/Job_Simulator/)

[VR The Diner Duo](https://store.steampowered.com/app/530120/VR_The_Diner_Duo/)

[The Cooking VR Game](https://store.steampowered.com/app/857180/The_Cooking_Game_VR/)

[Counter Fight](https://store.steampowered.com/app/551690/Counter_Fight/)

[Dead Hungry](https://store.steampowered.com/app/538710/Dead_Hungry/)

[Taphouse VR](https://store.steampowered.com/app/591680/Taphouse_VR/)

## Storyboarding

I started the storyboard on paper, taking the chance to iterate a bit about how all the cooking machinery will be used (a bit of interaction design as part of the prototyping process). Once I had a convincible version of each one I put them together to get the main scenario in which the user will be immersed. Later on I thought aloud about the whole workflow of the story and moved all what I had to a vector graphics version. Continued storyboarding the rest of the workflow upon this.

The final version of the storyboard in digital vector graphics below. I'm not exactly experienced with graphic design either but having come back down to earth the idea with the paper prototypes before, making these took just a few hours of vector graphics tasks mainly

<img src="https://user-images.githubusercontent.com/5633645/42437672-2711650c-8334-11e8-8120-ca2e2cf76c4a.png" alt="Storyboard 1" style="max-width:100%" width="512" heigth="512">
<img src="https://user-images.githubusercontent.com/5633645/42437673-273c3016-8334-11e8-886f-85403a40c79c.png" alt="Storyboard 1" style="max-width:100%" width="512" heigth="512">
<img src="https://user-images.githubusercontent.com/5633645/42437674-27615512-8334-11e8-9b55-30112f6518db.png" alt="Storyboard 1" style="max-width:100%" width="512" heigth="512">

## Why is it suitable for VR?

Mainly because of the natural interaction requirement, which is feasible in VR because of the positional tracked motion controllers. So the player will be able to reach objects with their hands to grab them, put them in another place, or do whatever they want with them. Doing all this with the more classic ways of interaction in games such as mouse and keyboard or even a gamepad would not be as fun and engaging as with VR.

## Graphic Style

The graphical style will be low poly as I guess it will give a funnier, colorful and more pleasing aesthetics for this type of game, in addition to conveying a nicer mood to the player. Also, as I'm not an expert with 3D modeling, in case I would have to model something, having a low poly style helps avoid taking care of the subtle details so it will be enough to make some simple models with not so much details. I'm planning to use Google Blocks for modeling the game assets, which is another tool I do want to explore and haven't tried it on a project yet, or in the worst case using already made Google Poly models.

## Interaction

The interaction will be natural as the player will use the positional features given by the 6 DoF (Degrees of Freedom) motion controllers to grab and interact with the different objects. Similarly, the locomotion technique to be used is just real walking as the user will be physically walking inside the room to reach the required objects in the virtual environment.

In a future iteration I'm planning to add multiple cooking areas each one with different ingredients and types of food, so in that scenario, the user will probably use teleportation to move between each cooking station, making the navigation approach easy and motion-sickness free.

There won't be any social interaction for now. An interesting addition in a future iteration if everything goes well would be adding multiplayer support so players can compete somehow. In order to do this I will have to take into account some social interaction techniques (how to represent the avatars and make them socially expressive, how to interact with them, etc) as well as adding all the networking stuff. This latter point is another thing I would like to explore more in-depth in a game project, but it's just an extra feature for the time being.

## Discomfort

As the players will be using the positional tracking features of the PC VR headset, they shouldn't feel much discomfort. As mentioned earlier, the navigation techniques used will be real walking and (possibly) teleportation, both of which are proven not to be much nausea inducers.

## Prototypes

The idea was to include the following in the initial prototypes:

1. **The basic interaction with orders:** grabbing an order from the Order Box and putting it into the Order Machine.
2. **The basic visualization of orders:** as part of the Order Machine once an order is placed on it, i.e.: show ingredients of the recipe to be made.
3. **The basic interaction with ingredients and recipe making:** grabbing ingredients and stacking them together to make the recipe, and confirming the order when it's ready.

Exclude the following:

1. **Any custom 3D model.** Use just native primitives as placeholders or assets from the Unity Asset Store or other 3D model stores instead.
2. **Coding the interaction logic from scratch.** Use a predefined library for interaction instead. Initially it will be the Interaction System from the SteamVR Unity plugin, but I'm also planning to check at least the interaction features of VRTK or even the fully physically-based interaction library NewtonVR, and see which one is more useful/efficient for the required interaction tasks (navigation will be real walking so that feature is not really relevant in either case).
3. **Graphics, audio or any other aspects not relevant for the core interactions mechanics.** Put focus on interaction instead, i.e.: grabbing stuff and making the required tasks.

I managed to fulfill all the above points, except for a few sound effects I added mainly for debug purposes (i.e.: a debug sound gives some clues of what's happening without the struggle of putting the headset off and checking debug logs on the monitor). Also, as I had some spare time I have also added a very simple environment to start getting sense of how it feels overall.

<img src="https://user-images.githubusercontent.com/5633645/42438140-8a977f2a-8335-11e8-9b2d-d43e709dd05d.png" alt="Prototype" style="max-width:100%" width="640" heigth="640">

Despite using the SteamVR Interaction System, I had to implement a few interesting interactions on my own, such as pressing a button, placing an order inside a slot or placing a new ingredient in the assembly line stack, while testing how natural those interactions felt. These are things that can't be learned just with a storyboard as that way you can only visualize the interaction but you can't check how well it match the user expectations. I guess the next tasks on User Testing will result in further improvements in this line. 

Check the following videos to get a little idea of how this initial prototypes evolved and how this short workflow implemented as part of the prototype works (grab order, place it in order machine, visualize order, make recipe, dispatch it when ready)::

<a target="_blank" href="https://www.youtube.com/watch?v=V5V5sP47X5c"><img src="http://img.youtube.com/vi/V5V5sP47X5c/0.jpg" alt="Interaction 6DOF" width="320" height="240" border="10" /></a>

_Click on the image to open video (will open in the same tab by default)_

<a target="_blank" href="https://www.youtube.com/watch?v=utdpCmk1VuE"><img src="http://img.youtube.com/vi/utdpCmk1VuE/0.jpg" alt="Interaction 6DOF" width="320" height="240" border="10" /></a>

_Click on the image to open video (will open in the same tab by default)_

<a target="_blank" href="https://www.youtube.com/watch?v=kjMlH1FRab0"><img src="http://img.youtube.com/vi/kjMlH1FRab0/0.jpg" alt="Interaction 6DOF" width="320" height="240" border="10" /></a>

_Click on the image to open video (will open in the same tab by default)_

Check the *Demo section* on the CookinVR PDF document at the root folder for more videos on the game's evolution.

## Testing Plan

Check the *Testing Plan* section of the PDF in the root folder, in which the plan to be carried out based on the above prototypes is described.

## User Testing

Check the *User Testing* section of the PDF in the root folder. The idea was to user test the prototypes with at least 4-5 users but due to lack of time and given that the sessions I was able to carry out ended up taking more time than expected, I was able to complete just two sessions. Anyway, I get a lot of insights from just those. Found the results in the aforementione section of the document.

## Demos

_Click on the images to open videos (will open in the same tab by default)_

Version | Description | Video
--- | --- | ---
1.0 | First prototype version including some very basic ingredient handling  | <a target="_blank" href="https://www.youtube.com/watch?v=V5V5sP47X5c"><img src="http://img.youtube.com/vi/V5V5sP47X5c/0.jpg" alt="1.0" width="320" height="240" border="10" /></a>
1.1 | Added a very basic environment to give the prototype a little more context and enhanced a bit the handling of orders | <a target="_blank" href="https://www.youtube.com/watch?v=utdpCmk1VuE"><img src="http://img.youtube.com/vi/utdpCmk1VuE/0.jpg" alt="1.1" width="320" height="240" border="10" /></a>
1.2 | Added a new way of making the recipe and handling ingredients by placing them into placeholders that show what's the correct ingredient to be placed next | <a target="_blank" href="https://www.youtube.com/watch?v=kjMlH1FRab0"><img src="http://img.youtube.com/vi/kjMlH1FRab0/0.jpg" alt="1.2" width="320" height="240" border="10" /></a>
1.3 | Added the recipe management system so it's quite easy to add new —stackable— recipes. Added hot dogs and sandwiches. The ingredients should be placed in a predefined order and position so there is no place for errors. This was the 1st prototype/approach tested | <a target="_blank" href="https://youtu.be/QfiW2JZQF24?t=1s"><img src="http://img.youtube.com/vi/QfiW2JZQF24/0.jpg" alt="1.3" width="320" height="240" border="10" /></a>
2.0 | Added a new approach to recipe making which is 100% flexible in regards to what can be placed into the assembly dish and how it's placed, so to have a better solution to implement a scoring system based on ingredient's correctness, order, and position upon this. This was the 2nd prototype/approach tested during User Testing | <a target="_blank" href="https://youtu.be/QfiW2JZQF24?t=38s"><img src="http://img.youtube.com/vi/QfiW2JZQF24/1.jpg" alt="2.0" width="320" height="240" border="10" /></a>
3.0 | Improved a bit the overall environment and added the ingredient spawning system which is ―almost― in place. A lot of UX stuff to improve yet based on User Testing results | <a target="_blank" href="https://www.youtube.com/watch?v=WP0IeZvj7qY"><img src="http://img.youtube.com/vi/WP0IeZvj7qY/0.jpg" alt="3.0" width="320" height="240" border="10" /></a>
3.1 | Ingredient spawning system working properly, improved some textures and materials, added some introductory indicators and tooltips at the beginning based on user testing results, as well as a few general bugs found during testing were solved | <a target="_blank" href="https://www.youtube.com/watch?v=8oP8QsAhxFo"><img src="http://img.youtube.com/vi/8oP8QsAhxFo/0.jpg" alt="3.1" width="320" height="240" border="10" /></a>
3.2 | Added scoring system, a few more sound effects, enhanced some graphical elements and solved some bugs. Also a new recipe was added, the Uruguayan Chivito! | <a target="_blank" href="https://www.youtube.com/watch?v=SeoVlZpifJ4"><img src="http://img.youtube.com/vi/SeoVlZpifJ4/0.jpg" alt="3.2" width="320" height="240" border="10" /></a>
4.0 | Enhanced and redesigned environment (mainly cooking allowances) so to make space for more ingredients, improved lighting, added toppings support, added interactable jukebox and spatial sound effects, hands rendering, basic timer functionality and a few additional animations | <a target="_blank" href="https://www.youtube.com/watch?v=nuWQtAUw7ZI"><img src="http://img.youtube.com/vi/nuWQtAUw7ZI/0.jpg" alt="3.2" width="320" height="240" border="10" /></a>

## How to run it

As mentioned earlier, the Unity project is thought to be run with a SteamVR-compatible headset such as HTC Vive, Oculus Rift or Microsoft Mixed Reality Headsets (SteamVR must be installed in the system); in particular, I've tested it with the HTC Vive. The scene to be loaded should be the "Prototype v3" scene included in the Scenes folder of the project. Below there are a few considerations when building and running it in different ways. Take into account that the VR Hands Asset is not part of the uploaded project as it is an Unity Asset Store paid asset, so I prefer to respect author's copyright

### Run from the Unity Editor

The project can be run directly from the Editor in case there are no errors with the scripts and the rest of the components (it was created with Unity 5.6.5). Just hit Play from the Editor and considering you already have SteamVR running the game should be up and running in a few seconds.

### Build and run

1.	Check there are no compiling errors in scripts and/or the rest of the components.
2.	Check PC is selected as Build Platform in the Build Settings (File > Build Settings).
3.	Check OpenVR is selected in the Virtual Reality Supported list in the Rendering section of Player Settings (Edit > Project Settings > Other Settings > Rendering).
4.	Build and Run (File > Build and Run). This will generate the executable file and run it.

## Plan / TBD

-	~~Get a clear idea of the whole workflow for the game in paper (client arrives, place an order, took the order, check order, select ingredients, make food, etc). This will be the most important point I guess as all that will come later will depend on it.~~
-	~~Share the idea with friends so to gather some opinions and early feedback on the abstract idea. Further improve the idea based on this feedback.~~
-	~~Search for similar apps/projects so to check for more ideas. Play Job Simulator or any other VR game strongly based on interaction and take notes~~
-	~~Make some simple prototypes of the workflow on paper. Simple storyboards or something~~
-	~~Make some very simple isolated prototypes of the required interaction for all this to work (using just cubes and other native primitives initially). Basically grabbing objects and piling them together to make a sandwich, for example, prototype the way in which the recipe with the required ingredients will be shown, etc.~~
-	~~Join all prototypes together to get a first version of the core gameplay of the game working.~~
-	~~Make a Test Plan, including goals of the user sessions as well as questions and interviews to be made to the users.~~
-	~~Implement the other alternative to handling orders and making the recipe (more physics based and more flexible, checking ingredients against the correct recipe after dispatching the order).~~
-	~~Add a few more recipes just to check implemented system's extensibility and take the chance to have a better user testing with more (yet simple) recipes.~~
-	~~Test the prototype with real users~~
-	~~Prioritize the results of user testing taking into account both bugs and improvements~~
-	~~Implement some of the high priority tasks from user testing results~~
-	~~Complete the gameplay workflow upon this. Implement the scoring mechanism~~
-	~~Find a way of testing the scoring system, so to be able to try quickly (outside VR) multiple scoring scenarios. This was finally done with Unity Test Tools.~~
-	~~Enhance the scoring system. Avoid making below zero scores, and show penalties in red instead of the usual yellow color~~
-	~~Solve bug related to ingredient placement in the assembly. There are some ingredients that ends up repeated for some reason and this mess up the scoring~~
-	~~Add Uruguayan Chivito recipe and add Uruguayan flag so you can place it as a topping.~~
-	~~Replace the placeholder primitives with real models. The models will be done inside VR using Google Blocks, so one task will be modeling all the needed assets/ingredients, at least for a few simple receipts (e.g.: hamburger, sandwich and hot dog). Or eventually downloading them from Google Poly so to maintain the low poly graphical style. Put the assets into the game and check if all works fine with those~~
-	~~Redesign the environment so more ingredients can be accessible, adding kind of shelves below each allowance.~~
-	~~Add recipe name in the display for each recipe.~~
-	~~Solve a bug that makes that when you make some juggling with the ingredients, if there was an ingredient already placed, once you catch the ingredient with which you are juggling, the game incorrectly takes it as placed in the assembly.~~
-	~~Add labels to each spawner corresponding to which ingredient it spawns. Add an animation so when a new ingredient is spawned the label rotates around the spawner's tip hole.~~
-	~~Add a Timer board so as soon as you receive the new order the timer starts ticking.~~
-	~~Add an animation to the order spawner so it's compressed at first, and when a new order is spawned it goes down quickly together with the order, as if it was throwing the order. In addition, this solves the issue related to the occluded timer board.~~
-	~~Add more toppings like the flag for the Chivito recipe so they can be placed at the top of any recipe.~~
-	~~Replace controllers with some cartoony hands. I ended up using the paid VR Cartoon Hands asset from the Unity Asset Store.~~
-	Add some nice effects. Maybe some cool shaders or something, but have to think about what effects could work yet.
-	~~Add missing sound effects, music, background sound, etc.~~
-	~~Configure spatial audio properly for the different audible elements~~
-	~~Add a radio or something for attaching the background music and be able to turn it on/off~~
- ~~Implement the remaining tasks from user testing results as long as there is some time available~~
-	Implement rounds based on enhanced timer functionality so as soon as you receive the new order it starts ticking and you have an appropriate time for each recipe, show final scoreboard when time runs out, etc.
-	Analyze a strange bug that makes that only in the HMD rendering some ingredients are shown duplicated and with an incorrect perspective (mainly for the cheese and ham as part of the sandwich). In the desktop screen they are shown just fine...

## Extra Features TBD

-	Add more complex manipulation of ingredients, not just placing them together to make the receipt. For example, cutting, boiling, etc.
-	Add chores or other activities beyond cooking (washing dishes, defend against robberies, etc).
-	Add a cool menu and some diegetic interfaces. For example, the menu could be attached to the users hand, watch or something.
-	Add basic NPC clients walking around the saloon, maybe coming to place an order from time to time, waiting for it to be ready, etc.
-	Add a slowed time mode. I would really like to experiment with this but don't know where in the game mechanics put it yet.
-	Add multiple cooking areas each one with different ingredients and types of food. Implement teleportation to move between each cooking station (easy and motion-sickness free navigation approach).
-	Port the game to mobile VR headsets or even standalone if all comes up well. Experimenting with Oculus Go support would be great.
-	Add some reflective surfaces such as the oven's front face or a mirror somewhere, so the player can see himself reflected on it (or at least his head and hands moving in sync with his real movements) so to enhance the embodiment illusion.
-	Add multiplayer support so players can compete somehow. For this I will have to take into account some social interaction techniques (how to represent the avatars in a socially expressive manner, how to interact with them, etc) as well as adding all the networking logic.

