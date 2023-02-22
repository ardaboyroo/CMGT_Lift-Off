using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using System.IO.Ports;
using GXPEngine.GXPEngine.Utils;
using GXPEngine.Core;

public class MyGame : Game
{
    ArduinoInput arduinoInput = new ArduinoInput();

    public static Vector2 screenSize = new Vector2(1366, 768);

    private int currentScene = 0;
    private int timer = 0;

    private int playerTimer = 0;
    private int playerShootCooldown = 1000; // in ms

    private bool gameStarted = false;

    Terrain terrain;
    Player player;
    Enemy enemy;
    public List<Enemy> enemies;

    public MyGame() : base((int)screenSize.x, (int)screenSize.y, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        Init0();
    }

    private void DestroyAll()
    {
        // Destroys every game object
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }

    // Start menu
    private void Init0()
    {
        AddChild(new Sprite("Assets/Start_Menu_Background.png"));
    }

    // Main game
    private void Init1()
    {
        DestroyAll();

        gameStarted = true;

        //terrain = new Terrain(width, height, 0, 64, Map.map, this);
        //AddChild(terrain);
        AddChild(new NewTerrain());

        player = new Player();
        AddChild(player);
        //player.AddChild(new Camera(0,0,1366,768));

        enemies = new List<Enemy>();
        enemy = new Enemy(player);
        AddChild(enemy);
        enemies.Add(enemy);
    }

    // Restart scene
    private void Init2()
    {

    }

    private void switchClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Start menu
            if (currentScene == 0)
            {
                currentScene = 1;
            }

            // Restart screen
            if (currentScene == 2)
            {
                currentScene = 1;
            }
        }
    }

    private void Shoot(GameObject obj)
    {
        AddChild(new Bullet(obj, -1));
        AddChild(new Bullet(obj, 1));
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
                switchClick();
                break;
            case 1:
                if (!gameStarted)
                {
                    Init1();
                }
                break;
        }

        if (!gameStarted)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && playerTimer >= playerShootCooldown)
        {
            playerTimer = 0;
            Shoot(player);
            Console.WriteLine("Player Shot");
        }

        foreach (Enemy thisEnemy in enemies)
        {
            if (thisEnemy.stopped)
            {
                if (timer > 1000)
                {
                    Shoot(thisEnemy);
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



        //int[] terrainPos = terrain.UpdateTerrain((int)player.x, (int)player.y);
        //terrain.SetXY(terrainPos[0], terrainPos[1]);

        //Console.WriteLine(terrainPos[0]);

        // change the entire game position s
        x = -player.x + width / 2;
        y = -player.y + height / 2;
        //terrain.x = -x + terrainPos[0];
        //terrain.y = -y + terrainPos[1];

        //terrain.x += 100;

        //Console.WriteLine(y);

    }


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}