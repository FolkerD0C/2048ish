﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameClient.App.Utils;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameClient.App.Assets
{
    internal class Button : BoxWithFixText, ISimpleUpdatable
    {
        protected enum ButtonState
        {
            Idle,
            Hover,
            Clicked
        }

        protected ButtonState state;

        readonly Guid id;
        public Guid Id => id;

        public event EventHandler? Click;

        public Button(string text, Texture2D texture, SpriteFont font, Color fontColor, Vector2 destination, Vector2 padding) : base(text, texture, font, fontColor, destination, padding)
        {
            id = Guid.NewGuid();
            state = ButtonState.Idle;
        }

        public Button(string text, Texture2D texture, SpriteFont font, Color fontColor, Vector2 destination, float padding) : this(text, texture, font, fontColor, destination, new Vector2(padding, padding))
        {
        }

        public Button(string text, Texture2D texture, SpriteFont font, Color fontColor, Vector2 destination) : this(text, texture, font, fontColor, destination, new Vector2(20, 20))
        {
        }

        public override void Draw(SpriteBatch openSpriteBatch, GameTime gameTime)
        {
            int atlasRow;
            switch (state)
            {
                case ButtonState.Idle:
                    {
                        atlasRow = 0;
                        break;
                    }
                case ButtonState.Hover:
                    {
                        atlasRow = 1;
                        break;
                    }
                case ButtonState.Clicked:
                    {
                        atlasRow = 2;
                        break;
                    }
                default:
                    throw new InvalidOperationException("Invalid button state: " + state + " in button: " + id);
            }

            Rectangle sourceRectangle = new(0, texture.Height / 3 * atlasRow, texture.Width, texture.Height / 3);

            openSpriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            openSpriteBatch.DrawString(font, Text, textDestination, fontColor);
        }

        public void Update(GameTime gameTime)
        {
            switch (state)
            {
                case ButtonState.Idle:
                    {
                        if (MouseUtils.MouseLeftButtonHandle is not null && MouseUtils.MouseLeftButtonHandle != id)
                        {
                            return;
                        }
                        if (MourseInArea())
                        {
                            state = ButtonState.Hover;
                        }
                        break;
                    }
                case ButtonState.Hover:
                    {
                        if (!MourseInArea())
                        {
                            state = ButtonState.Idle;
                        }
                        else if (MouseUtils.LeftDown && MouseUtils.MouseLeftButtonHandle is null)
                        {
                            MouseUtils.LockMouseHandle(id);
                            state = ButtonState.Clicked;
                        }
                        break;
                    }
                case ButtonState.Clicked:
                    {
                        if (!MouseUtils.LeftDown)
                        {
                            MouseUtils.ReleaseMouseHandle(id);
                            state = MourseInArea() ? ButtonState.Hover : ButtonState.Idle;
                            Click?.Invoke(this, EventArgs.Empty);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        protected bool MourseInArea()
        {
            return MouseUtils.XPos >= Destination.X && MouseUtils.XPos <= Destination.X + Width
                && MouseUtils.YPos >= Destination.Y && MouseUtils.YPos <= Destination.Y + Height;
        }
    }
}
