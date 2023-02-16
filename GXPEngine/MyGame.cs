using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;

public class MyGame : Game
{
    private int currentScene = 0;
    private bool gameStarted = false;
    Player player;
    Enemy enemy;
    private int timer = 0;

    public MyGame() : base(1366, 768, false)     // Create a window that's 800x600 and NOT fullscreen
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

    private void Init0()
    {

    }

    private void Init1()
    {
        DestroyAll();

        gameStarted = true;

        player = new Player();
        AddChild(player);
        enemy = new Enemy(player);
        AddChild(enemy);
    }

    private void switchClick()
    {
        if (Input.GetMouseButtonDown(0)) { currentScene = 1; }
    }

    private void Shoot(GameObject obj)
    {
        AddChild(new Bullet(obj, -1));
        AddChild(new Bullet(obj, 1));
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        /*
        scene 0
            
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

        if (Input.GetMouseButtonDown(0))
        {
            Shoot(player);
        }

        if (enemy.stopped)
        {
            if (timer > 1000)
            {
                Shoot(enemy);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }


    }


    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();                   // Create a "MyGame" and start it
    }
}