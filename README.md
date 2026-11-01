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
| Sprint | Shift |
| Pause | ESC |

## How to Play
The overall gameplay loop and goal is to:
1. find the information required in each section to move to the next, ultimately fleeing the city.
2. be weary of unreliable information and the influx of zombies attempting to stop you in your tracks.

## Known Bugs
populate once known

## How to Run the Build
**Simply Play online:** insert itchio link
**The Zip:** the project is also zipped into COS30031-Assignment2-3d.zip as directed.
* When cloning, ensure Unity Editor v6000.3.21f1.
* Issue the `Git Clone https://github.com/rrtlrlr/COS30031-Assignment2/ -b 3d` command 
* How to open the project: Unity Hub → Add project from disk → select the folder
* Start in the MainMenu scene
* Ctrl+Shift+B to build and run (will launch via WebGL so make sure its installed. Will open in browser)
Note that this build is entirely on the '3d' branch of the assignment 2 repository as opposed to a unique GitHub repository for assignment 3 specifically.

## Key Programming Systems
1. **Zombie Spawning System** – fill out if relevent
2. **Bullets** (`Bullet.cs`) — fill out if relevent
3. **Pickups** (`x` and `y`) — fill out if relevent
4. **Items** – fill out if relevent
5. **Structures** – these are non-essential locations and are copy pasted around the map to hold pickups like health and ammo and make the map more interesting
6. **Material Properties** (`SurfaceZone3D.cs`) – Ground materials with specific properties (namely Mud and Ice) are controlled with this specific class that handles the alteration of player movement according to the surface (e.g., sliding on ice, slowing down in mud)

## Team Contributions
| Team Member | Contribution |
|---|---|
| M.F. (student id: 105923445) | fill out as needed |
| Vihanga Gunasekara | fill out as needed |
| Ethan Gibbons | Absolutely nothing - has not even made contact with the team at any point and contributed 0% to both assignments |
