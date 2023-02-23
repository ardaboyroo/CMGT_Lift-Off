using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using System.IO.Ports;
using GXPEngine.GXPEngine.Utils;
using GXPEngine.Core;
using TiledMapParser;
using System.Threading;

public class MyGame : Game
{
    private int enemyBulletSpeed = 5;

    ArduinoInput arduinoInput = new ArduinoInput();

    public static Vector2 screenSize = new Vector2(1366, 768);

    public int score = 0;

    private int selected = 0;

    public int currentScene = 0;
    private int timer = 0;

    private int playerTimer = 0;
    private int playerShootCooldown = 1000; // in ms
    private int lastRotation = 0;

    private Sound cannonSFX;
    private Sound menuMusic;
    public Sound shipSinking;

    private HUD hud;

    private bool gameStarted = false;
    private bool init0 = false;

    public Player player;
    NewTerrain terrain;
    EnemySpawner enemySpawner;
    public List<Enemy> enemies;

    public MyGame() : base((int)screenSize.x, (int)screenSize.y, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        Init0();
    }

    public int[,] GetMap()
    {
        return terrain.map;
    }

    private void DestroyAll()
    {
        // Destroys every game object
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }

        // Resets camera position
        x = 0;
        y = 0;
    }

    // Start menu
    private void Init0()
    {
        DestroyAll();

        init0 = true;

        LoadAllSounds();
        menuMusic.Play();

        AddChild(new Sprite("Assets/Start_Menu_Background.png"));
        AddChild(new HUD(currentScene));
    }

    // Main game
    private void Init1()
    {
        DestroyAll();

        gameStarted = true;
        init0 = false;

        terrain = new NewTerrain();
        AddChild(terrain);

        player = new Player();
        AddChild(player);

        enemies = new List<Enemy>();
        enemySpawner = new EnemySpawner(player);
        AddChild(enemySpawner);

        AddChild(new HUD(currentScene));
    }

    // Restart scene
    private void Init2()
    {
        DestroyAll();

        gameStarted = false;

        hud = new HUD(currentScene);
        AddChild(hud);
        score = 0;
    }

    private void switchClick()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(Key.F))
        {
            // Start menu
            if (currentScene == 0)
            {
                currentScene = 1;
            }

            // Restart screen
            if (currentScene == 2)
            {
                if (selected == 1)
                {
                    currentScene = 1;
                }
                if (selected == 2)
                {
                    currentScene = 0;
                }
            }
        }
    }

    private void LoadAllSounds()
    {
        cannonSFX = new Sound("Assets/CannonSFX.mp3");
        menuMusic = new Sound("Assets/Menu music.mp3", true);
        shipSinking = new Sound("Assets/ShipSinking.mp3");
    }

    private void Shoot(GameObject obj, int bulletSpeed = 10)
    {
        AddChild(new Bullet(obj, -1, bulletSpeed));
        AddChild(new Bullet(obj, 1, bulletSpeed));
        cannonSFX.Play();
    }

    // For every game object, Update is called every frame, by the engine:
    public void Update()
    {
        arduinoInput.update();
        /*
        
        scene 0:
            Start menu
                Title
                Play button

        scene 1:
            Main game
                Map
                player and enemies
                Current score

        scene 2:
            Restart menu
                High score
                Current score
                Restart button
            
        */
        switch (currentScene)
        {
            case 0:
                if (!init0)
                {
                    Init0();
                }
                switchClick();
                break;
            case 1:
                if (!gameStarted)
                {
                    Init1();
                }
                break;
            case 2:
                if (gameStarted)
                {
                    Init2();
                }
                switchClick();
                break;
        }

        if (currentScene == 2)
        {
            if (ArduinoInput.isConnected)
            {
                if (lastRotation < ArduinoInput.rotationCounter)
                {
                    hud.background.currentFrame = 2;
                    selected = 2;
                }
                else if (lastRotation > ArduinoInput.rotationCounter)
                {
                    hud.background.currentFrame = 1;
                    selected = 1;
                }

            }
            if (Input.GetKeyDown(Key.D))
            {
                hud.background.currentFrame = 2;
                selected = 2;
            }
            else if (Input.GetKeyDown(Key.A))
            {
                hud.background.currentFrame = 1;
                selected = 1;
            }
        }
        lastRotation = ArduinoInput.rotationCounter;

        if (!gameStarted)
        {
            return;
        }

        if (!player.isAlive)
        {
            currentScene = 2;
        }

        if (Input.GetKeyDown(Key.F) && playerTimer >= playerShootCooldown)
        {
            playerTimer = 0;
            player.shot = true;
            Shoot(player);
        }

        foreach (Enemy thisEnemy in enemies)
        {
            if (thisEnemy.stopped)
            {
                if (timer > 1000)
                {
                    thisEnemy.shot = true;
                    thisEnemy.currentFrame = 0;
                    Shoot(thisEnemy, enemyBulletSpeed);
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }

        // Cooldown player shooting:
        playerTimer += Time.deltaTime;

        // This changes the map position so the player stays in the middle
        x = -player.x + width / 2;
        y = -player.y + height / 2;


        /*
        //int[] terrainPos = terrain.UpdateTerrain((int)player.x, (int)player.y);
        //terrain.SetXY(terrainPos[0], terrainPos[1]);

        //terrain.x = -x + terrainPos[0];
        //terrain.y = -y + terrainPos[1];

        //terrain.x += 100;
        */
    }


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}