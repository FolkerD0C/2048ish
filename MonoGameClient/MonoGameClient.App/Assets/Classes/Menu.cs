﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameClient.App.Assets;

internal class Menu : IBaseAsset
{
    readonly Guid id;
    public Guid Id => id;

    public Menu()
    {
        id = Guid.NewGuid();
    }

    public void Update(GameTime gameTime)
    {

    }

    public void Draw(SpriteBatch openSpriteBatch, GameTime gameTime)
    {
        throw new NotImplementedException();
    }
}
