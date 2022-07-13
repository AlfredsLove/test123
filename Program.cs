using Raylib_cs;
using System.Numerics;
using Perlin;

 class Game{
    //---- variable declaration ----//
    //you sexy son of a bitch
    const int screenWidth = 800;
    const int screenHeight = 450;

   static Vector3 playerPos = new Vector3(0, 0, 0);
    static Vector3 playerSize = new Vector3(2, 2, 2);

    static Vector3 enemyBoxPos = new Vector3(5, 0, 5);
    static Vector3 enemyBoxSize = new Vector3(2, 2, 2);

   static bool collision = false;

    static Color enemyCol = Color.RED;

    static Image image = Raylib.LoadImage("map.png");
    static Texture2D texture = Raylib.LoadTextureFromImage(image);

    static Mesh mesh = Raylib.GenMeshHeightmap(image, new Vector3(100,50,100));    // Generate heightmap mesh (RAM and VRAM)
    static Model model = Raylib.LoadModelFromMesh(mesh);

            
    static Vector3 mapPosition = new Vector3(-8.0f, 0.0f, -8.0f);
    static unsafe void Main()
    {

        Raylib.InitWindow(screenWidth, screenHeight, "raylib [core] example - 3d picking");
        model.materials[0].maps[0].texture = texture;
        Camera3D camera;

        camera.position = new Vector3(0, 0f, 10f);
        camera.up = new Vector3(0f, 1f, 0f);
        camera.target = playerPos;
        camera.fovy = 45.0f;
        camera.projection = CameraProjection.CAMERA_PERSPECTIVE;
        //---- START ----//

        Raylib.UnloadImage(image);



        Raylib.SetCameraMode(camera, CameraMode.CAMERA_THIRD_PERSON);



        Raylib.SetTargetFPS(240);

       

        while (!Raylib.WindowShouldClose())
        {
            Raylib.UpdateCamera(&camera);

            //---- UPDATE ----//

            


            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) {

                playerPos.X += 10 * Raylib.GetFrameTime();

            } else if (Raylib.IsKeyDown(KeyboardKey.KEY_D)){ 

                playerPos.X -= 10 *Raylib.GetFrameTime();
            }

          if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
            {

                playerPos.Z += 10 * Raylib.GetFrameTime();

            }
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
            {

                playerPos.Z -= 10 * Raylib.GetFrameTime();
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
            {

                playerPos.Y += 50 * Raylib.GetFrameTime();

            }
            if (!Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && playerPos.Y >= 0) {

                playerPos.Y -= 9.81f * Raylib.GetFrameTime();
            }


            if ((playerPos.X < (enemyBoxPos.X + enemyBoxSize.X) && (playerPos.X + playerSize.X) > enemyBoxPos.X) &&
           (playerPos.Y < (enemyBoxPos.Y + enemyBoxSize.Y) && (playerPos.Y + playerSize.Y) > enemyBoxPos.Y) &&
           (playerPos.Z < (enemyBoxPos.Z + enemyBoxSize.Z) && (playerPos.Z + playerSize.Z) > enemyBoxPos.Z)) collision = true;

            else collision = false;








            if (collision) enemyCol = Color.GREEN;
            else enemyCol = Color.RED;


            // SHOULD ALWAYS BE LAST!

            //---- DRAWING -----//

            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.RAYWHITE);

            Raylib.BeginMode3D(camera);
            
            Raylib.DrawCubeV(playerPos, playerSize, Color.RED);

           
            Raylib.DrawCubeV(enemyBoxPos, enemyBoxSize, enemyCol);

           // Raylib.DrawModel(model, mapPosition, 3.0f, Color.GREEN);

            /*  for (int x = 0; x < 100; x++)
              {

                      for (int z = 0; z < 100; z++)
                      {


                      Raylib.DrawCubeV(new Vector3(x, 0, z), new Vector3(2, 2, 2), Color.RED);


                      }



              }*/


            Raylib.EndMode3D();

            Raylib.DrawFPS(10, 10);

            Raylib.EndDrawing();


        }
        Raylib.CloseWindow();



    }

   
    
   


}

