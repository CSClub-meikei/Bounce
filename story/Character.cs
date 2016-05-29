using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce.story
{
    class Character:GraphicalGameObject
    {
        int _face;
        Texture2D[] faces;
        public int face
        {
            get { return _face; }
            set {
                _face = value;
                Texture = faces[face];
            }
        }
        bool _active;

        bool isShow;

        public bool active
        {
            get { return _active; }
            set {
                if (_active != value)
                {
                   // System.Windows.Forms.MessageBox.Show(_active.ToString());
                    _active = value;
                    if (active)
                    {
                        animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.5f });
                    }
                    else
                    {
                        animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.5f, 0.5f });
                    }
                }
                _active = value;
               
            }
        }



        public Character(Game1 game, Screen screen, Texture2D[] Texture, float x, float y, float width, float height) : base(game, screen, Texture[0], x, y, width, height)
        {
            faces = Texture;
            addAnimator(2);
        }
        public void show(Point point)
        {
            if (isShow) return;
            animator[0] = new GameObjectAnimator(this, game);
            animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, point.X, point.Y, 1f, -1,1,1 });
            animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 0, 0.5f });
            isShow = true;
        }
        public void hide(Point point)
        {
            if (!isShow) return;
            animator[0] = new GameObjectAnimator(this,game);
            animator[0].start(GameObjectAnimator.SLIDE, new float[] { 1, point.X, point.Y, 1f, -1, 1,1 });
            animator[1].start(GameObjectAnimator.fadeInOut, new float[] { 1, 0.5f });
            isShow = false;
        }
        public override void update(float delta)
        {
            base.update(delta);
        }
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            base.Draw(batch, screenAlpha);
        }

    

    }
}
