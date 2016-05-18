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
    public class GameObject
    {
        
        protected ContentManager Content;
        [DataMember]public double X = 0;
        [DataMember]public double Y = 0;
        /// <summary>
        /// 最終的に描画される位置 Draw内ではこれを使う
        /// </summary>
        public double actX = 0;
        /// <summary>
        /// 最終的に描画される位置 Draw内ではこれを使う
        /// </summary>
        public double actY = 0;
        [DataMember]public double Width = 100;
        [DataMember]public double Height = 100;
        public double velocityX = 0;
        public double velocityY = 0;
        public float alpha = 1.0F;
        protected Game1 game;
        public Screen parent;

        public GameObject(Game1 game,Screen screen) 
        {
            this.game=game;
            this.Content = game.Content;
            this.parent = screen;
           
        }
        /// <summary>
        /// フレーム毎に呼び出す
        /// </summary>
        /// <param name="delta">deltaTime</param>
        public virtual void update(float delta)
        {
            this.actX = X + parent.X;
            this.actY = Y + parent.Y;
        }

        /// <summary>
        /// 描画メソッド
        /// </summary>
        /// <param name="batch">SpriteBatchを指定</param>
        /// <param name="screenAlpha">親Screenの透明度</param>
        public virtual void Draw(SpriteBatch batch,float screenAlpha)
        {

        }
        /// <summary>
        /// Objectの座標をセット
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public virtual void setLocation(double x,double y)
        {
            this.X = x;this.Y = y;
            this.actX = X + parent.X;
            this.actY = Y + parent.Y;
        }
        /// <summary>
        /// オブジェクトのサイズをセット
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public virtual void setSize(double w,double h)
        {
            this.Width = w;this.Height = h;
        }
    }
}
