# Somnium
Somnium

Unity Version: 2020.3.3f1

        Förberedelse av en scen:
	- Skapa ett scen
	- Ta bort tidigt skapade kameran (Main Camera). Player prefab har en kamera kopplat till den.
	- Om du vill ha ett dörr till nästa level, lägg till en DoorToNextLevel prefab och ändra i inspectorn till vilket level dörren ska leda.
	(Mer info om hur man gör det hittar du nedanför)

        Prefabs:
	Player:
	- Ändra Player Health: Player -> Player Health (Script) -> Health = int
	- Ändra Bullet Damage: Player -> HeadPoint -> Camera -> Weapon Script -> Bullet Damage = int
	- Ändra Speed of Bullets: Player -> HeadPoint -> Camera -> Weapon Script -> Speed Of Bullets = int
	Info: När spelaren dör, så laddas hela scenen om.

	MeleeEnemy: (OBS! Det måste finnas en Player i scenen för att scripten ska fungera.)
	- Ändra Enemy Speed: MeleeEnemy -> Melee Enemy Script -> Enemy Speed = float
	- Ändra Attack Cooldown: MeleeEnemy -> Melee Enemy Script -> Attack Cooldown = int
	- Ändra Enemy Damage: MeleeEnemy -> Melee Enemy Script -> Enemy Damage = int
	- Ändra Enemy Health: MeleeEnemy -> Melee Enemy Script -> Enemy Health = int

	DistanceEnemy: (OBS! Det måste finnas en Player i scenen för att scripten ska fungera.)
	- Ändra Enemy Speed: DistanceEnemy -> Distance Enemy Script -> Enemy Speed = float
	- Ändra Attack Cooldown: DistanceEnemy -> Distance Enemy Script -> Attack Cooldown = int
	- Ändra Distance Until Shoot: DistanceEnemy -> Distance Enemy Script -> Distance Until Shoot = int
	- Ändra Distance Until Moving: DistanceEnemy -> Distance Enemy Script -> Distance Until Moving = int
	- Ändra Bullet Speed: DistanceEnemy -> Distance Enemy Script -> Bullet Speed = int
	- Ändra Enemy Damage: DistanceEnemy -> Distance Enemy Script -> Enemy Damage = int
	- Ändra Enemy Health: DistanceEnemy -> Distance Enemy Script -> Enemy Health = int

	DoorToNextLevel:
	- Ändra till vilken level dörren ska leda till: DoorToNextLevel -> Change Level Script -> Next Level = string (namnet på scenen. OBS, du måste lägga till scenen till build settings. File -> Build Settings -> Add Open Scenes).

	KeyPuzzle:
	- Ändra Spawnpoints för nyckeln: Flytta på SpawnPoint(respektive 1,2,3,4) GameObject till önskat lokation.
	- Ändra på positionen för sista dörren: Flytta på DoorToNextLevel GameObject till önskat lokation.
	- Ändra på vilket level sista dörren ska leda till: DoorToNextLevel -> Change Level Script -> Next Level = string

	Switch:
	- Ändra på dörren som Switchen "switchar": Switch -> Switch Script -> Door = GameObject
