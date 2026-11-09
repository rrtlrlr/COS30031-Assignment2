Note that the 3d branch of the COS30031-Assignment2 repository has been used for Assignment 3 development.

# Dead End
**Team name:** Rahul_Wed_1630_G02

**Team members:**
* Vihanga Gunasekara
* M.F. (student id: 105923445)

**Engine:** Unity

**Unit:** COS30031 – Games Programming | Assessment 3

**Video:** insert yt link

---

## Game Description
Dead End is a 3D game set in the Boroondara region and features five Features of Interests (FOIs) - namely, Villa Alba, Hartwell Ambulance Station, Camberwell Tram Depot, Glenferrie Oval, and Balwyn Library).
A zombie invasion has happened and the player must get through three sections in order to escape the city.
They are provided a gun to fend off against the zombies as well as health heal-ups, sprinting, etc, to make the escape intense and action-based.

## Connection to the Challenge Brief
**Partner / scenario:** "Vicmap: The Race Against Time" provided by Transport Victoria.
**Topic:** We are tasked with creating a game where a fictional emergency arises (1). The player must develop an understanding of important places around the map and FOIs must represent real-world locations (2). Furthermore, they must discern which geospatial information is to be trusted. This may be achieved when information is missing, outdated, incorrectly classified, or misplaced (3). Reliable geospatial information must be used to support decision making regarding the emergency (4).

The way we addressed 1 was by creating a zombie-invasion scenario. The map selects various FOIs from the dataset provided by Transport Victoria and represents them in-game as key locations for progression; every FOI contains what is needed to progress and these features address key design point 2. 

Design points 3 is achieved in section 1 where a misleading map is left by the zombies which leads the player to a herd, and section 3 where a newspaper is outdated and thus falsely claims key information. The player is made to discover each of these unreliable geospatial sources first and the information itself is designed to indicate its unreliability (e.g., the newspaper has a publication date indicating that it is outdated), leaving the decision making up to the player. Each unreliable information source has an equivalent reliable one and informs the player on how to progress and their decision making, addressing brief point 4.

## Controls
| Action | Input |
|---|---|
| Move | WASD / Arrow Keys |
| Jump | Space |
| Shoot | Click |
| Reload | R |
| Pickup | E |
| Sprint | Shift |
| Pause | ESC |

## How to Play
The overall gameplay loop and goal is to:
1. find the information required in each section to move to the next, ultimately fleeing the city.
2. be weary of unreliable information and the influx of zombies attempting to stop you in your tracks.

## Known Bugs
No known bugs at this time

## How to Run the Build
**Simply Play online:** https://105923445.itch.io/dead-end-3d
**The Zip:** the project is also zipped into COS30031-Assignment2-3d.zip as directed.
* When cloning, ensure Unity Editor v6000.3.21f1.
* Issue the `Git Clone https://github.com/rrtlrlr/COS30031-Assignment2/ -b 3d` command 
* How to open the project: Unity Hub → Add project from disk → select the folder
* Start in the MainMenu scene
* Ctrl+Shift+B to build and run (will launch via WebGL so make sure its installed. Will open in browser)
Note that this build is entirely on the '3d' branch of the assignment 2 repository as opposed to a unique GitHub repository for assignment 3 specifically.

## Key Reusable Programming Systems
1. **Bullets** (`Bullet.cs`) — A single bullet prefab object is replicated and shot out when the user clicks and this prefab gets reused each bullet. 
2. **Items** (`Health` prefab,`Ammo` prefab) — These two single assets are reused throughout structures.
3. **Pickups** (`WalkoverPickup.cs`, `ItemOfInterestPickup`, `ItemFloat.cs`) — These scripts are replicated across pickups like Health and Ammo prefabs and encapsulate the 'pickup' functionality, the items idle animation, etc.
4. **Structures** – these are non-essential locations and are copy pasted around the map to hold pickups like health and ammo and make the map more interesting
5. **Material Properties** (`SurfaceZone3D.cs`) – Ground materials with specific properties (namely Mud and Ice) are controlled with this specific class that handles the alteration of player movement according to the surface (e.g., sliding on ice, slowing down in mud)

## Team Contributions
| Team Member | Contribution |
|---|---|
| M.F. (student id: 105923445) | world design, original asset creation (newspapers, maps, ice, etc), various physics surfaces, data driven system, progression game content (keycode,dynamite,boulders,etc), FOI creation, README, accessibility settings (sensitivity,volume) |
| Vihanga Gunasekara | pickup items (ammo,health,dynamite redesign), zombie spawner and navmesh, player movement, 3 optimisations, combat functionality (weapon, shooting, zombie, damage, health, etc), notebook UI design and popups |
| Ethan Gibbons | Absolutely nothing - has not even made contact with the team at any point and contributed 0% to both assignments |
