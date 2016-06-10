using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bounce
{
    class endContentScreen:Screen
    {
        TextObject main;
        public endContentScreen(Game1 game, int x = 0, int y = 0) : base(game, x, y)
        {
            main = new TextObject(game, this, Assets.graphics.ui.defultFont, "", Color.White, new Rectangle(0, 0, 1280, 0));
            main.text = "こうしてこの島は大地の力を取り戻し、\n" +
                        "自然豊かな島に戻った。\n" +
                        "キミのおかげだ！ありがとう！\n\n\n\n\n" +
                        "- Bounce スタッフロール -\n" +
                        "\n\n\n" +
                        "スタッフ\n\n" +
                        "　　システム : 市川創大\n\n" +
                        "　　シナリオ : 市川創大\n\n" +
                        "　　グラフィック\n" +
                        "　　　　CG,アニメーション : 市川創大\n" +
                        "　　　　イラスト : 羽田龍史\n\n" +
                        "　　BGM,SE  : 戸村瑚\n\n" +
                        "　　コースデザイナー  : 市川創大\n" +
                        "　　　　　　　　　　　　近藤晏誉\n" +
                        "    テストプレイ : 近藤晏誉\n" +
                        "    　　　　　　　 鈴木想\n\n" +
                        "スペシャルサンクス\n" +
                        "　　赤木義和先生\n\n\n\n" +
                        "　　" + userData.userName + "\n\n\n\n\n" +
                        "制作\n\n\n" +
                        "茗溪学園　コンピューターシステム同好会\n" +
                        "            CS Club\n";
            setUIcell(1, 1);

        }
        public override void update(float deltaTime)
        {
            base.update(deltaTime);
            main.update(deltaTime);
        }
        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            main.Draw(batch, screenAlpha);
        }
    }
}
