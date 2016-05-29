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

namespace Bounce.editor
{
    class EditorScreen : Screen
    {

        public string filepath;

        public mapData map;
        public List<mapData> recentMap = new List<mapData>();
        public int unre;

        public mapChip startChip;

        public List<mapChip> selectedChips = new List<mapChip>();
        public List<mapChip> RemoveChips = new List<mapChip>();
        public List<mapChip> InsertChips = new List<mapChip>();

        ChipToolbar ChipToolbar;
        public eventEditScreen eventEditScreen;
        public menuBar MenuBar;

        public bool testPlaying = false;

        public bool showEventEdit = false;

        mapChip MoveEditChip;
        mapChip copyChip;
        public  bool Moveeditting = false;
        Point mp, op;
        public int selectedLayor = 0;
        public bool showEventIcon=true;
        tileObject back;

        public worldScreen TestPlayScreen;

        public EditorScreen(Game1 game, int sx = 0, int sy = 0) : base(game, sx, sy)
        {
            MenuBar = new menuBar(game, this);

            back = new tileObject(game, this, Assets.graphics.game.mapBack, -10000, -10000, 1000, 1000, 20, 20);
            back.alpha = 0.4f;
            //Load("test.xml");

            init();
            ChipToolbar = new ChipToolbar(game);
            //eventEditScreen = new eventEditScreen_p(game,0,0);
            // eventEditScreen.MoveEdit += new EventHandler(this.startMoveEdit);
            eventEditScreen = new eventEditScreen(game, this, 0, 70);

            foreach (ChipToolBarChip c in ChipToolbar.Controls) c.Drag += new EventHandler((sender, e) => { AddChip(selectedLayor, c.ChipNum); });

           

            setUIcell(1, 1);
        }
        public void AddChip(int layor, int type)
        {

            mapChip newChip = new mapChip(game, this, type, 0, (int)(Input.getPosition().X - X), (int)(Input.getPosition().Y - Y), 40, 40);
            newChip.onClick += new EventHandler(this.onSelect);

            if (type == mapChip.ACCEL)
            {
                map.Layor[layor].Insert(0,newChip);
            }
            else
            {
                map.Layor[layor].Add(newChip);
            }
          
            
        }
        public void startMoveEdit(object sender, EventArgs e)
        {
            if (!Moveeditting)
            {


                MoveEditChip = new mapChip(game, this, selectedChips[0].type, 0, ((eventData_2)selectedChips[0].eventData).X, ((eventData_2)selectedChips[0].eventData).Y, (int)selectedChips[0].Width, (int)selectedChips[0].Height);
               
                MoveEditChip.isSelected = true;
                MoveEditChip.AllowResize = false;
                MoveEditChip.EditChipMode = true;
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
            }
            else
            {

                endMoveEdit(null, null);

            }

        }
        public void endMoveEdit(object sender, EventArgs e)
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
            MenuBar.update(deltaTime);
            if (testPlaying)
            {
                TestPlayScreen.update(deltaTime);
                return;
            }
            ChipToolbar.update(deltaTime);
             eventEditScreen.update(deltaTime);
            back.update(deltaTime);
            if (MoveEditChip != null) { MoveEditChip.update(deltaTime); }

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
            if (Input.onKeyDown(Keys.P))
            {
                startTestPlay();
            }
                if (Input.onKeyDown(Keys.F))
            {
                showEventEdit = !showEventEdit;
                if (showEventEdit)
                {
                    eventEditScreen.animator.start(ScreenAnimator.SLIDE, new float[] { 1, 0, 0, -1, -1, 1,1});

                }else
                {
                    eventEditScreen.animator.start(ScreenAnimator.SLIDE, new float[] { 1, -200, 0, -1, -1, 1, 1 });
                }
            }

            keyUpdate(deltaTime);

            //if (Input.onKeyDown(Keys.S)) Save("test.xml");

            if (Input.onKeyDown(Keys.Z) && Input.IsKeyDown(Keys.LeftControl)) undo();
            if (Input.onKeyDown(Keys.Y) && Input.IsKeyDown(Keys.LeftControl)) redo();
            base.update(deltaTime);
           // if (Moveeditting) return;
            
            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor) chip.update(deltaTime);
            foreach (mapChip c in RemoveChips) foreach (List<mapChip> layor in map.Layor) if (layor.IndexOf(c) != -1) layor.Remove(c);
            foreach (mapChip c in InsertChips) foreach (List<mapChip> layor in map.Layor) if (layor.IndexOf(c) != -1) { layor.Remove(c);layor.Insert(0, c); }
            InsertChips.Clear();
                startChip.update(deltaTime);
            map.start = new Point((int)startChip.X, (int)startChip.Y);
            // if (selectedChips.Count == 0) eventEditScreen.screenAlpha = 0;
            // else eventEditScreen.screenAlpha = 1;

        }
        public override void Draw(SpriteBatch batch)
        {
            back.Draw(batch, screenAlpha);
            if (testPlaying) TestPlayScreen.Draw(batch);
            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor) chip.Draw(batch, screenAlpha);
            ChipToolbar.Draw(batch);
            //eventEditScreen.Draw(batch);
            eventEditScreen.Draw(batch);
            MenuBar.Draw(batch);
            if (MoveEditChip != null) MoveEditChip.Draw(batch, screenAlpha);
            startChip.Draw(batch, screenAlpha);
            base.Draw(batch);
        }
        public void onSelect(object sender, EventArgs e)
        {
            if (Moveeditting) return;
            if (Input.IsKeyDown(Keys.LeftShift))
            {
                selectedChips.Add((mapChip)sender);
            }
            else
            {
                selectedChips.Clear();
                selectedChips.Add((mapChip)sender);
            }
            //eventEditScreen.Load(selectedChips[0]);
            // eventEditScreen.screenAlpha = 1;
            foreach (List<mapChip> layor in map.Layor) {
                foreach (mapChip chip in layor)
                {
                    chip.isSelected = false;
                    if (selectedChips[0].eventData.num == chip.eventData.num && selectedChips[0].eventData.num!=0)
                    {
                        
                        chip.animator[0].GLOWHL = Assets.graphics.ui.HL;
                        chip.animator[0].start(GameObjectAnimator.GLOW, new float[] { 1, 0.5F, 0.6f, 0F, 0.4F, 0.0F, 1F });
                        chip.animator[1].start(GameObjectAnimator.FLASH, new float[] { 0.2F, 0.2F, 1F, 0.0F, 0 });
                        chip.eventHilight = true;
                    }else
                    {
                        chip.animator[0].stop();
                        chip.animator[1].stop();
                        chip.eventHilight = false;
                    }
                  
                }
            }

            foreach (mapChip chip in selectedChips)
            {
                chip.isSelected = true;
                chip.animator[0].stop();
                chip.animator[1].stop();
            }
            
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
            }
            else
            {
                map = new mapData();

            }
            Assets.LoadGame(game.Content, map.texSet);

            map.Load(game, this);
            ChipToolbar = new ChipToolbar(game);
            foreach (ChipToolBarChip c in ChipToolbar.Controls) c.Drag += new EventHandler((sender, e) => { AddChip(selectedLayor, c.ChipNum); });
            back = new tileObject(game, this, Assets.graphics.game.mapBack, -10000, -10000, 1000, 1000, 20, 20);


            foreach (List<mapChip> layor in map.Layor) foreach (mapChip chip in layor)
                {
                    chip.onClick += new EventHandler(this.onSelect);
                    chip.AllowDelete = true;
                 //   chip.onUnSelect += new EventHandler(this.RefreshMap);
                }
            startChip = new mapChip(game, this, mapChip.START, 0, map.start.X, map.start.Y, 40, 40);
            startChip.AllowDelete = false;
            startChip.onClick += new EventHandler(this.onSelect);
            RefreshMap();
        }
        public void init()
        {
            map = new mapData();
            map.Load(game, this);
            filepath = "";
            startChip = new mapChip(game, this, mapChip.START, 0, map.start.X, map.start.Y, 40, 40);
            startChip.AllowDelete = false;
            startChip.onClick += new EventHandler(this.onSelect);
        }

        public void keyUpdate(float deltaTime)
        {
           
            if (Input.onKeyDown(Keys.D1))
            {
                AddChip(0, 1);
            }
            if (Input.onKeyDown(Keys.D2))
            {
                AddChip(0, 2);
            }
            if (Input.onKeyDown(Keys.D3))
            {
                AddChip(0, 3);
            }
            if (Input.onKeyDown(Keys.D4))
            {
                AddChip(0, 4);
            }
            if (Input.onKeyDown(Keys.D5))
            {
                AddChip(0, 5);
            }
            if (Input.onKeyDown(Keys.D6))
            {
                AddChip(0, 6);
            }
            if (Input.onKeyDown(Keys.D7))
            {
                AddChip(0, 7);
            }
            if (Input.onKeyDown(Keys.C) && Input.IsKeyDown(Keys.LeftControl))
            {
                mapChip s = selectedChips[0];
                copyChip = new mapChip(game, this, s.type, s.rotate,(int) s.X,(int) s.Y,(int) s.Width, (int)s.Height);
                copyChip.onClick += new EventHandler(this.onSelect);
                copyChip.specialData = s.specialData;


                if(s.eventData is eventData_1)
                {
                    copyChip.eventData = new eventData_1();
                    ((eventData_1)copyChip.eventData).mode = ((eventData_1)s.eventData).mode;
                    ((eventData_1)copyChip.eventData).isLoop = ((eventData_1)s.eventData).isLoop;
                    ((eventData_1)copyChip.eventData).interval = ((eventData_1)s.eventData).interval;
                }else if(s.eventData is eventData_2)
                {
                    copyChip.eventData = new eventData_2();
                    ((eventData_2)copyChip.eventData).X = ((eventData_2)s.eventData).X;
                    ((eventData_2)copyChip.eventData).Y = ((eventData_2)s.eventData).Y;
                    ((eventData_2)copyChip.eventData).isLoop = ((eventData_2)s.eventData).isLoop;
                    ((eventData_2)copyChip.eventData).speed = ((eventData_2)s.eventData).speed;
                    ((eventData_2)copyChip.eventData).interval = ((eventData_2)s.eventData).interval;
                }else if(s.eventData is eventData_3)
                {
                    copyChip.eventData = new eventData_3();
                    ((eventData_3)copyChip.eventData).mode = ((eventData_3)s.eventData).mode;
                }

                copyChip.eventData.type = s.eventData.type;
                copyChip.eventData.num = s.eventData.num;
                copyChip.eventData.delay = s.eventData.delay;


                

            }
            if (Input.onKeyDown(Keys.V) && Input.IsKeyDown(Keys.LeftControl))
            {
                if (copyChip == null) return;

                    mapChip res = CopyChip(copyChip);

                if (res.eventData is eventData_2)
                {
                    ((eventData_2)res.eventData).X = (int)(((eventData_2)res.eventData).X + 40*(int)((Input.getPosition().X - X - res.X)/40));
                    ((eventData_2)res.eventData).Y = (int)(((eventData_2)res.eventData).Y + 40 * (int)((Input.getPosition().Y - Y - res.Y) / 40));
                }
                res.X = (int)(Input.getPosition().X - X);
                res.Y = (int)(Input.getPosition().Y - Y);
            
                 

                 map.Layor[0].Add(res);
            }
            if (Input.onKeyDown(Keys.S) && Input.IsKeyDown(Keys.LeftControl))
            {
                Save(filepath);
                game.ShowToast("保存しました", 2);
                //System.Windows.Forms.MessageBox.Show("保存しました");
            }
            if (Input.onKeyDown(Keys.N))
            {
                showEventIcon = !showEventIcon;
            }
        }
        public mapChip CopyChip(mapChip s)
        {
            mapChip res=new mapChip(game, this, s.type, s.rotate, (int)s.X, (int)s.Y, (int)s.Width, (int)s.Height);
            res = new mapChip(game, this, s.type, s.rotate, (int)s.X, (int)s.Y, (int)s.Width, (int)s.Height);
            res.onClick += new EventHandler(this.onSelect);
            res.specialData = s.specialData;


            if (s.eventData is eventData_1)
            {
                res.eventData = new eventData_1();
                ((eventData_1)res.eventData).mode = ((eventData_1)s.eventData).mode;
                ((eventData_1)res.eventData).isLoop = ((eventData_1)s.eventData).isLoop;
                ((eventData_1)res.eventData).interval = ((eventData_1)s.eventData).interval;
            }
            else if (s.eventData is eventData_2)
            {
                res.eventData = new eventData_2();
                ((eventData_2)res.eventData).X = ((eventData_2)s.eventData).X;
                ((eventData_2)res.eventData).Y = ((eventData_2)s.eventData).Y;
                ((eventData_2)res.eventData).isLoop = ((eventData_2)s.eventData).isLoop;
                ((eventData_2)res.eventData).speed = ((eventData_2)s.eventData).speed;
                ((eventData_2)res.eventData).interval = ((eventData_2)s.eventData).interval;
            }
            else if (s.eventData is eventData_3)
            {
                res.eventData = new eventData_3();
                ((eventData_3)copyChip.eventData).mode = ((eventData_3)s.eventData).mode;
            }

            res.eventData.type = s.eventData.type;
            res.eventData.num = s.eventData.num;
            res.eventData.delay = s.eventData.delay;

            return res;
        }
        public void RefreshMap()
        {
           // int i = 0;
           

            unre++;

            
            // for (i = unre; i <= recentMap.Count; i++) recentMap.RemoveAt(i-1);
            

          // recentMap.Add(map.Clone(game,this));
           // DebugConsole.write(unre.ToString());

        }
        public void undo()
        {
            if (unre > 1)
            {
                unre--;
                map = recentMap[unre-1].Clone(game,this);
              
            }
          //  DebugConsole.write(unre.ToString());
        }
        public void redo()
        {
            if (unre < recentMap.Count)
            {
                unre++;
                map = recentMap[unre-1].Clone(game, this);

            }
           // DebugConsole.write(unre.ToString());
        }
        public void startTestPlay()
        {

            if (filepath == "") filepath = "tmp.bmd";
                Save(filepath);
            TestPlayScreen = new worldScreen(game, map,true,Point.Zero);
            
            TestPlayScreen.stop += new EventHandler(this.stopTestPlay);
           

            screenAlpha = 0;
            eventEditScreen.screenAlpha = 0;
            testPlaying = true;
        }
        public void stopTestPlay(object sender, EventArgs e)
        {
            X =TestPlayScreen.X;
            Y = TestPlayScreen.Y;
            
            screenAlpha = 1;
            eventEditScreen.screenAlpha = 1;
            testPlaying = false;
            TestPlayScreen = null;
            
        }
    }
}
