using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using CombatDevelopersEngine;


namespace SampleGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //float modelRotation = 0.0f;

        //Camera things
        Vector3 cameraPosition = new Vector3(0, 0, -6000);
        Vector3 camForwardStep;


        //Using engine variables
        Camera camera;
        Scene scene;
        GraphicModel basicModel1;
        GraphicModel basicModel2;
        GraphicModel basicModel3;
        GraphicModel gModel0;
        Node nodo1;
        Node nodo2;
        SpriteFont font;

       
        

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            DetectOptimalResolution(true);            
         
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("font");       

            camForwardStep = new Vector3(0, 0, 5f);
            //Loads camera
            camera = new Camera("MainCamera");
            camera.SetFrustrumPerspective(MathHelper.ToRadians(45f), graphics.GraphicsDevice.Viewport.AspectRatio, 1f, 1000000f);
            camera.SetLocation(new Vector3(0, 0, 5000));

            scene = new Scene(camera);

            nodo1 = new Node("node1");
            nodo2 = new Node("node2");
            nodo1.SetLocalPosition(new Vector3(0, 0, 0));

            nodo2.SetLocalPosition(new Vector3(2000, 0, 0));

            gModel0 = new GraphicModel("model0");
            gModel0.Content = this.Content;
            gModel0.Asset = "Models\\p1_wedge";
            scene.RootNode.AddChild(gModel0);
            gModel0.SetLocalPosition(0, -1000, 0);
            gModel0.SetLocalScale(0.5f);

            basicModel1 = new GraphicModel("model1");
            basicModel1.Content = this.Content;
            basicModel1.Asset = "Models\\p1_wedge";
            basicModel1.SetLocalScale(0.5f);
            scene.RootNode.AddChild(basicModel1);


            basicModel2 = new GraphicModel("model2");
            basicModel2.Content = this.Content;
            basicModel2.Asset = "Models\\p1_wedge";
            basicModel2.SetLocalPosition(new Vector3(2000, 0, 0));
            basicModel2.SetLocalScale(0.5f);
            nodo1.AddChild(basicModel2);

            basicModel3 = new GraphicModel("model3");
            basicModel3.Content = this.Content;
            basicModel3.Asset = "Models\\spaceship3";
            basicModel3.SetLocalPosition(new Vector3(0, -1000, 0));            
            nodo2.AddChild(basicModel3);

            basicModel3.SetLocalScale(20);

            scene.RootNode.AddChild(nodo1);
            scene.RootNode.AddChild(nodo2);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            
            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.W) || GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
            {
                //camera.setLocation(camera.Position - new Vector3(0, 0, 100f));                
                camera.SetLocation(camera.Position - camForwardStep * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
                //camera.LookAt(camera.Position - camForwardStep, Vector3.Up);               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S) || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                camera.SetLocation(camera.Position + camForwardStep * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
                //camera.LookAt(camera.Direction + camForwardStep, Vector3.Up);
            }
            
           

            nodo2.SetLocalRotation(Quaternion.Concatenate(nodo2.GetLocalRotation(), Quaternion.CreateFromAxisAngle(Vector3.UnitX, (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.1f))));
            nodo2.SetLocalPosition(basicModel2.GetGlobalPosition());

            nodo1.SetLocalRotation(Quaternion.Concatenate(nodo1.GetLocalRotation(), Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.01f))));
            basicModel2.SetLocalRotation(Quaternion.Concatenate(basicModel2.GetLocalRotation(), Quaternion.CreateFromAxisAngle(Vector3.UnitX, (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.01f))));
            
            
            
            gModel0.SetLocalRotation(basicModel2.GetGlobalRotation());

            // This is trivial: we just create one new smoke particle per frame.                       
          
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
           
                        
            scene.Draw();
            //DrawMessage();
            base.Draw(gameTime);
        }

        private void DrawMessage()
        {
            string message = string.Format("Esto es una prueba de texto");

            spriteBatch.Begin();

            spriteBatch.DrawString(font, message, new Vector2(50, 50), Color.White);
            spriteBatch.DrawString(font, "Prueba de solapamiento", new Vector2(600, 300), Color.White);
            spriteBatch.End();
            
        }

        /// <summary>
        /// If the given resolution is supported the game inits with it
        /// </summary>
        /// <param name="iWidth"></param>
        /// <param name="iHeight"></param>
        /// <param name="bFullScreen"></param>
        /// <returns></returns>
        private bool InitGraphicsMode(int iWidth, int iHeight, bool bFullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (bFullScreen == false)
            {
                if ((iWidth <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                && (iHeight <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = iWidth;
                    graphics.PreferredBackBufferHeight = iHeight;
                    graphics.IsFullScreen = bFullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set. To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    if ((dm.Width == iWidth) && (dm.Height == iHeight))
                    {
                        // The mode is supported, so set the buffer formats, apply changes and return
                        graphics.PreferredBackBufferWidth = iWidth;
                        graphics.PreferredBackBufferHeight = iHeight;
                        graphics.IsFullScreen = bFullScreen;
                        graphics.ApplyChanges();
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Detects the optimal resolution regardless of the TV or PC screen
        /// </summary>
        /// <param name="fullscreen"></param>
        private void DetectOptimalResolution(bool fullscreen)
        {

            if (InitGraphicsMode(1920, 1200, fullscreen))
                return;
            
            if (InitGraphicsMode(1920, 1080, fullscreen))
                return;
            
            if (InitGraphicsMode(1366, 768, fullscreen))
                return;

            if (InitGraphicsMode(1280, 720, fullscreen))
                return;

            if (InitGraphicsMode(1024, 768, fullscreen))
                return;

            if (InitGraphicsMode(800, 600, fullscreen))
                return;

            if (InitGraphicsMode(640, 480, fullscreen))
                return;

            this.Exit();

        }
    }
}
