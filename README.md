# Dead End
**Team name:** Rahul_Wed_1630_G02

**Team members:** Mack Fitzpatrick, Vihanga Gunasekara

**Engine:** Unity

**Unit:** COS30031 – Games Programming | Assessment 2

**Video:** https://www.youtube.com/watch?v=QOAxrZEtrFY

---

## Game Description
Dead End is a top-down game set in the Boroondara region and features five Features of Interests (FOIs) - namely, Villa Alba, Hartwell Ambulance Station, Camberwell Tram Depot, Glenferrie Oval, and Balwyn Library).
A zombie invasion has happened and the player must get through three sections in order to escape the city.
They are provided a gun to fend off against the zombies as well as health heal-ups, sprinting, etc, to make the escape intense and action-based.

## Connection to the Challenge Brief
**Partner / scenario:** "Vicmap: The Race Against Time" provided by Transport Victoria.
**Topic:** We are tasked with creating a 2D game where a fictional emergency arises (1). The player must develop an understanding of important places around the map and FOIs must represent real-world locations (2). Furthermore, they must discern which geospatial information is to be trusted. This may be achieved when information is missing, outdated, incorrectly classified, or misplaced (3). Reliable geospatial information must be used to support decision making regarding the emergency (4).

The way we addressed 1 was by creating a zombie-invasion scenario. The map selects various FOIs from the dataset provided by Transport Victoria and represents them in-game as key locations for progression; every FOI contains what is needed to progress and these features address key design point 2. 

Design points 3 is achieved in section 1 where a misleading map is left by the zombies which leads the player to a herd, and section 3 where a newspaper is outdated and thus falsely claims key information. The player is made to discover each of these unreliable geospatial sources first and the information itself is designed to indicate its unreliability (the fake map uses an informal font and is made to look overall suspicious and the newspaper has a publication date indicating that it is obselete), leaving the decision making up to the player. Each unreliable information source has an equivalent reliable one and informs the player on how to progress and their decision making, addressing brief point 4.

## Controls
| Action | Input |
|---|---|
| Move | WASD / Arrow Keys |
| Shoot | Click |
| Reload | R |
| Sprint | Shift |
| Open Notepad | N |
| Pause | ESC |

## How to Play
The overall gameplay loop and goal is to:
1. find the information required in each section to move to the next, ultimately fleeing the city.
2. be weary of unreliable information and the influx of zombies attempting to stop you in your tracks.

## How to Run the Build
**Play online:** https://105923445.itch.io/dead-end

## Key Programming Systems
1. **Zombie Spawning System** (`ZombieSpawner.cs`) — Spawns zombies at random points around a radius, reused across the whole map (all sections)
2. **Bullets** (`Bullet.cs`) — A single bullet game object that is spawned when the gun is fired and despawned when it hits something. It is reused whenever the player shoots
3. **Pickups** (`AmmoPickup` and `HealthPickup`) — Individual objects that the user can collect. Vastly scattered across FOIs and structures in the world showing high reusability.
4. **Items** – A DisplayItem script can be attached to any object with a trigger collider which causes that object’s sprite to be displayed via the a UI image in the canvas
5. **Structures** – these are non-essential locations and are copy pasted around the map to hold pickups like health and ammo and make the map more interesting

## Team Contributions
| Team Member | Contribution |
|---|---|
| Mack Fitzpatrick | All But 5 Sprites, World Design, Items and Interaction, Section 3 Progression, Section 2 Progression, Section 1 Progression, FOIs, Tilemaps, Deliverables |
| Vihanga Gunasekara | Zombies, Notepad, Weapon, Health, Section 3 Progression, Dynamite Sprites, Pickup and Player Sprites, Added Music, UI Menus |
| Ethan Gibbons | Absolutely nothing - has not even made contact with the team at any point |
