﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SadConsole.MonoGame
{
    public partial class Game
    {
        public class SadConsoleGameComponent : DrawableGameComponent
        {
            internal SadConsoleGameComponent(Game game) : base(game)
            {
                DrawOrder = 5;
                UpdateOrder = 5;
            }

            public override void Draw(GameTime gameTime)
            {
                if (SadConsole.Settings.DoDraw)
                {
                    MonoGame.Game game = (MonoGame.Game)Game;

                    Global.DrawFrameDelta = gameTime.ElapsedGameTime;

                    // Clear draw calls for next run
                    SadConsole.Game.Instance.DrawCalls.Clear();

                    // Make sure all items in the screen are drawn. (Build a list of draw calls)
                    Global.Screen?.Draw(gameTime.ElapsedGameTime);

                    ((SadConsole.Game)SadConsole.Game.Instance).InvokeFrameDraw();

                    // Render to the global output texture
                    GraphicsDevice.SetRenderTarget(game.RenderOutput);
                    GraphicsDevice.Clear(SadRogue.Primitives.SadRogueColorExtensions.ToMonoColor(SadConsole.Settings.ClearColor));

                    // Render each draw call
                    game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
                    foreach (DrawCalls.IDrawCall call in SadConsole.Game.Instance.DrawCalls)
                    {
                        call.Draw();
                    }

                    game.SpriteBatch.End();
                    GraphicsDevice.SetRenderTarget(null);

                    // If we're going to draw to the screen, do it.
                    if (SadConsole.Settings.DoFinalDraw)
                    {
                        game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.DepthRead, RasterizerState.CullNone);
                        game.SpriteBatch.Draw(game.RenderOutput, SadRogue.Primitives.SadRogueRectangleExtensions.ToMonoRectangle(SadConsole.Settings.Rendering.RenderRect), Color.White);
                        game.SpriteBatch.End();
                    }
                }
            }

            public override void Update(GameTime gameTime)
            {
                if (SadConsole.Settings.DoUpdate)
                {
                    MonoGame.Game game = (MonoGame.Game)Game;

                    Global.UpdateFrameDelta = gameTime.ElapsedGameTime;

                    if (Game.IsActive)
                    {
                        if (SadConsole.Settings.Input.DoKeyboard)
                        {
                            //Global.KeyboardState.Update(gameTime);
                            //Global.KeyboardState.Process();
                        }

                        if (SadConsole.Settings.Input.DoMouse)
                        {
                            //Global.MouseState.Update(gameTime);
                            //Global.MouseState.Process();
                        }
                    }

                    Global.Screen?.Update(gameTime.ElapsedGameTime);

                    ((SadConsole.Game)SadConsole.Game.Instance).InvokeFrameUpdate();
                }
            }
        }
    }
}
