﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = SadRogue.Primitives.Color;
using XnaColor = Microsoft.Xna.Framework.Color;

namespace SadConsole.MonoGame
{
    public partial class Game
    {
        /// <summary>
        /// A component to draw how many frames per second the engine is performing at.
        /// </summary>
        public class FPSCounterComponent : DrawableGameComponent
        {
            private readonly Console surface;
            private int frameRate = 0;
            private int frameCounter = 0;
            private TimeSpan elapsedTime = TimeSpan.Zero;


            public FPSCounterComponent(Microsoft.Xna.Framework.Game game)
                : base(game)
            {
                surface = new Console(30, 1) { DefaultBackground = Color.Black };
                surface.Clear();
                DrawOrder = 8;
                Instance.GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
            }


            public override void Update(GameTime gameTime)
            {
                elapsedTime += gameTime.ElapsedGameTime;

                if (elapsedTime > TimeSpan.FromSeconds(1))
                {
                    elapsedTime -= TimeSpan.FromSeconds(1);
                    frameRate = frameCounter;
                    frameCounter = 0;
                }
            }


            public override void Draw(GameTime gameTime)
            {
                frameCounter++;
                surface.Clear();
                surface.Print(0, 0, $"fps: {frameRate}", Color.White, Color.Black);
                surface.Draw(gameTime.ElapsedGameTime);
                
                Game.GraphicsDevice.SetRenderTarget(null);
                MonoGame.Game.Instance.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
                MonoGame.Game.Instance.SpriteBatch.Draw(((Renderers.ConsoleRenderer)surface.Renderer).BackingTexture, Vector2.Zero, XnaColor.White);
                MonoGame.Game.Instance.SpriteBatch.End();
            }
        }
    }
}
