using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Runtime.Serialization;
using System.Xml;
namespace Bounce
{
    [DataContract]
    public class GraphicalGameObject:GameObject

    {
        public Texture2D Texture;
        public List<GameObjectAnimator> animator;
        public int animatorLayor=0;//0で後ろ1で手前
        public float angle=0;
        public Vector2 origin = new Vector2(-100,-100);
        public GraphicalGameObject(Game1 game,Screen screen,Texture2D Texture, float x, float y, float width, float height) :base(game,screen)
        {
            if (Texture != null)
            {
                this.Texture = Texture;
                origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
            }
            animator = new List<GameObjectAnimator>();
            this.setLocation(x, y);
            this.setSize(width, height);
           
            
        }

        /// <summary>
        /// フレーム毎に呼び出す
        /// </summary>
        /// <param name="delta">deltaTime</param>
        public override void update(float delta)
        {
            base.update(delta);
            foreach(GameObjectAnimator a in animator)
            {
                a.update(delta);
            }
           
        }

        /// <summary>
        /// 描画メソッド
        /// </summary>
        /// <param name="batch">描画するSpriteBatchを指定</param>
        /// <param name="screenAlpha">親Screenの透明度を指定</param>
        public override void Draw(SpriteBatch batch, float screenAlpha)
        {
            batch.Begin(transformMatrix: game.GetScaleMatrix(), blendState: BlendState.Additive);
            if (animatorLayor == 0)
            {
                foreach (GameObjectAnimator a in animator)
                {
                    a.Draw(batch, screenAlpha);
                }
            }
            batch.End();
            batch.Begin(transformMatrix: game.GetScaleMatrix());

            batch.Draw(Texture,destinationRectangle: new Rectangle((int)actX+(int)((Texture.Width/2)*(Width/Texture.Width)), (int)actY+(int)((Texture.Height/2)*(Height / Texture.Height)), (int)Width, (int)Height) , color:  Color.White * alpha*screenAlpha,rotation :angle,origin:origin);
            
            batch.End();
            batch.Begin(transformMatrix: game.GetScaleMatrix(), blendState: BlendState.Additive);
            if (animatorLayor == 1 )
            {
                foreach (GameObjectAnimator a in animator)
                {
                    a.Draw(batch, screenAlpha);
                }
            }
            batch.End();
        }
           /// <summary>
           /// ObjectAnimatorの追加
           /// </summary>
           /// <param name="num"></param>
           public void addAnimator(int num)
        {
            int i = 0;
            for (i = 0; i != num ; i++) { 
            animator.Add(new GameObjectAnimator(this,game));
            }
        }
        /// <summary>
        /// Animatorを削除
        /// </summary>
        /// <param name="num"></param>
        public void removeAnimator(int num)
        {
            animator.RemoveAt(num);
        }
        public void removeAnimator(GameObjectAnimator a)
        {
            animator.Remove(a);
        }
        /// <summary>
        /// Objectの位置をセット
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void setLocation(double x, double y)
        {
            this.X = x; this.Y = y;
            this.actX = X + parent.X;
            this.actY = Y + parent.Y;
        }
        /// <summary>
        /// Objectのサイズをセット
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public override void setSize(double w, double h)
        {
            this.Width = w; this.Height = h;
           
        }
        /// <summary>
        /// Objectの角度をセット
        /// </summary>
        /// <param name="a"></param>
        public void  setAngle(float a)
        {
            angle = dir2Rot(a);
            origin = new Vector2((float)(Texture.Width / 2), (float)(Texture.Height / 2));
        }
        private float dir2Rot(double angle)
        {
            return (float)(angle / 180 * Math.PI);
        }
        private double rot2Dir(float radian)
        {
            return (double)(radian * 180 / Math.PI);
        }

        public static implicit operator Texture2D(GraphicalGameObject v)
        {
            throw new NotImplementedException();
        }
    }
    }

