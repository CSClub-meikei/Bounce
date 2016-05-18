﻿using System;
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
    class EditorScreen:Screen
    {
        mapData map ;
        public  List<mapChip> selectedChips = new List<mapChip>();
        public List<mapChip> RemoveChips = new List<mapChip>();
        ChipToolbar ChipToolbar;
        public eventEditScreen_p eventEditScreen;

        mapChip MoveEditChip;
        bool Moveeditting = false;
        Point mp,op;
        public int selectedLayor = 0;

        public EditorScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            Load("test.xml");
            map.Load(game,this);
            ChipToolbar = new ChipToolbar(game);
            //eventEditScreen = new eventEditScreen_p(game,0,0);
           // eventEditScreen.MoveEdit += new EventHandler(this.startMoveEdit);
            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor) chip.onClick += new EventHandler(this.onSelect);

            foreach (ChipToolBarChip c in ChipToolbar.Controls) c.Drag += new EventHandler((sender, e) => { AddChip(selectedLayor,c.ChipNum); });
           



                    setUIcell(1, 1);
        }
        public void AddChip(int layor,int type)
        {

           mapChip newChip = new mapChip(game, this, type, 0, (int)(Input.getPosition().X - X), (int)(Input.getPosition().Y - Y), 40, 40);
            newChip.onClick+= new EventHandler(this.onSelect);
            map.Layor[layor].Add(newChip);

        }
        public void startMoveEdit(object sender, EventArgs e)
        {
            if (!Moveeditting)
            {
               

                MoveEditChip = new mapChip(game, this, selectedChips[0].type, 0,((eventData_2)selectedChips[0].eventData).X, ((eventData_2)selectedChips[0].eventData).Y, (int)selectedChips[0].Width, (int)selectedChips[0].Height);
                MoveEditChip.isSelected = true;
                MoveEditChip.AllowResize = false;
                MoveEditChip.onUnSelect += new EventHandler(this.endMoveEdit);
                MoveEditChip.alpha = 0.5f;
                MoveEditChip.addAnimator(2);
                // MoveEditChip.animator[0].start(GameObjectAnimator.FLASH, new float[] { 1f, 0.2f, 0, 0.2f, -1 });
                //MoveEditChip.animator[0].setLimit(2f);
               // MoveEditChip.animator[1].setLimit(2f);
                MoveEditChip.animator[0].GLOWHL = Assets.graphics.ui.HL;
                MoveEditChip.animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.4F, 0F, 0.4F, 0.0F, 1F });
                MoveEditChip.animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
                //  selectedChips[0].alpha = 0.8f;
                selectedChips[0].ShowMoveLocation = false;
                Moveeditting = true;
            }else
            {

                endMoveEdit(null, null);

            }
               
        }
        public void endMoveEdit(object sender,EventArgs e)
        {
            Moveeditting = false;

            ((eventData_2)selectedChips[0].eventData).X = (int)MoveEditChip.X;
            ((eventData_2)selectedChips[0].eventData).Y = (int)MoveEditChip.Y;
          // selectedChips[0].alpha = 1;
            selectedChips[0].ShowMoveLocation = true;
            MoveEditChip = null;
        }
        public override void update(float deltaTime)
        {

            ChipToolbar.update(deltaTime);
           // eventEditScreen.update(deltaTime);
            if (MoveEditChip != null) { MoveEditChip.update(deltaTime);}
            
            if (Input.OnMouseDown(Input.MiddleButton))
            {
                mp = new Point(Input.getPosition().X, Input.getPosition().Y);
                op = new Point(X, Y);
            }
            if (Input.IsMouseDown(Input.MiddleButton))
            {
                X = op.X - (mp.X - Input.getPosition().X);
                Y = op.Y - (mp.Y - Input.getPosition().Y);
            }
            if (Input.onKeyDown(Keys.S)) Save("test.xml");


            if (Moveeditting) return;
            base.update(deltaTime);
            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor) chip.update(deltaTime);
            foreach (mapChip c in RemoveChips) foreach (List<mapChip> layor in map.Layor) if (layor.IndexOf(c) != -1) layor.Remove(c);
           // if (selectedChips.Count == 0) eventEditScreen.screenAlpha = 0;
           // else eventEditScreen.screenAlpha = 1;

        }
        public override void Draw(SpriteBatch batch)
        {
            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor) chip.Draw(batch, screenAlpha);
            ChipToolbar.Draw(batch);
            //eventEditScreen.Draw(batch);
            if (MoveEditChip != null) MoveEditChip.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public void onSelect(object sender,EventArgs e)
        {
            if (Input.IsKeyDown(Keys.LeftShift))
            {
                selectedChips.Add((mapChip)sender);
            }
            else{
                selectedChips.Clear();
                selectedChips.Add((mapChip)sender);
            }
            //eventEditScreen.Load(selectedChips[0]);
           // eventEditScreen.screenAlpha = 1;
            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor) chip.isSelected = false;

                    foreach (mapChip chip in selectedChips) chip.isSelected = true;
        }
        public void Save(string fileName)
        {

            

            //DataContractSerializerオブジェクトを作成
            //オブジェクトの型を指定する
            DataContractSerializer serializer =
                new DataContractSerializer(typeof(mapData));
            //BOMが付かないUTF-8で、書き込むファイルを開く
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new System.Text.UTF8Encoding(false);
            XmlWriter xw = XmlWriter.Create(fileName, settings);
            //シリアル化し、XMLファイルに保存する
            serializer.WriteObject(xw, map);
            //ファイルを閉じる
            xw.Close();
        }
        public void Load(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                //DataContractSerializerオブジェクトを作成
                DataContractSerializer serializer =
                    new DataContractSerializer(typeof(mapData));
                //読み込むファイルを開く
                XmlReader xr = XmlReader.Create(fileName);
                //XMLファイルから読み込み、逆シリアル化する
                map = (mapData)serializer.ReadObject(xr);
                //ファイルを閉じる
                xr.Close();
            }else
            {
                map = new mapData();

            }
        }
    }
}