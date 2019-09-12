using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;//Assert用

namespace Nukitashi2.Device
{
    class Renderer
    {
        private ContentManager contentManager; //コンテンツ管理者
        private GraphicsDevice graphicsDevice; //グラフィック機器
        private SpriteBatch spriteBatch; //スプライト一括描画用オブジェクト

        //複数画像管理用変数の宣言と生成
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">Game1クラスのコンテンツ管理者</param>
        /// <param name="graphics">Game1クラスのグラフィック機器</param>
        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        internal void DrawNumber(string v1, Vector2 vector2, string gameTime, int v2)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 画像の読み込み
        /// </summary>
        /// <param name="assetName">アセット名（ファイルの名前）</param>
        /// <param name="filepath">画像へのファイルパス</param>
        public void LoadContent(string assetName, string filepath = "./")
        {
            //すでにキー（assetName：アセット名）が登録されているとき
            if (textures.ContainsKey(assetName))
            {
#if DEBUG //DEBUGモードの時のみ下記エラー分をコンソールへ表示
                Console.WriteLine(assetName + "はすでに読み込まれています。\n プログラムを確認してください。");
#endif

                //それ以上読み込まないのでここで終了
                return;
            }
            //画像の読み込みとDictionaryへアセット名と画像を登録
            textures.Add(assetName, contentManager.Load<Texture2D>(filepath + assetName));

        }

        /// <summary>
        /// アンロード
        /// </summary>
        public void Unload()
        {
            textures.Clear();//Dictionaryの情報をクリア
        }

        /// <summary>
        /// 描画開始
        /// </summary>
        public void Begin()
        {
            spriteBatch.Begin();
        }

        /// <summary>
        /// 描画終了
        /// </summary>
        public void End()
        {
            spriteBatch.End();
        }

        /// <summary>
        /// 画像の描画（画像サイズはそのまま）
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="alpha">透明値（1.0f：不透明 0.0f：透明）</param>
        public void DrawTexture(string assetName, Vector2 position, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、画像の読み込み自体できていません");

            spriteBatch.Draw(textures[assetName], position, Color.White * alpha);
        }

        /// <summary>
        /// 画像の描画（画像を指定範囲内だけ描画）
        /// </summary>
        /// <param name="assetName">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="rect">指定範囲</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(string assetName, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            //デバッグモードの時のみ、画像描画前のアセット名チェック
            Debug.Assert(
                textures.ContainsKey(assetName),
                "描画時にアセット名の指定を間違えたか、画像の読み込み自体できていません");

            spriteBatch.Draw(
                textures[assetName], //テクスチャ
                position,            //位置
                rect,                //指定範囲（矩形で指定：左上の座標、幅、高さ）
                Color.White * alpha);//透明値
        }

        ///<summary>数字の描画(整数のみ)</summary>
        ///<param name="">数字画像の名前</param>
        ///<param name="">位置</param>
        ///<param name="">表示したい整数値</param>
        ///<param name="">透明値</param>
        public void DrawNumber(string assetName, Vector2 position, int number, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(assetName), "描画時にアセット名の指定を間違えたか、画像の読み込み自体で来ていません");
            if (number < 0)
            {
                number = 0;
            }

            int width = 32;

            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(textures[assetName], position, new Rectangle((n - '0') * width, 0, width, 64), Color.White);
                position.X += width;
            }
        }
        ///<summary>数字の描画(実数、小数点以下は2桁表示)</summary>
        ///<param name="">数字画像の名前</param>
        ///<param name="">位置</param>
        ///<param name="">表示したい実数時</param>
        ///<param name="">透明値</param>
        public void DrawNumber(string assetName, Vector2 position, float number, float alpha = 1.0f)
        {
            //マイナスは0へ
            if (number < 0.0f)
            {
                number = 0.0f;
            }


            int width = 32;//数字画像1つ分の横幅
                           //小数部は2桁まで、整数部が1桁の時は0で埋める
            foreach (var n in number.ToString("00.00"))
            {
                //小数の「.」か？
                if (n == '.')
                {
                    spriteBatch.Draw(textures[assetName], position, new Rectangle(10 * width, 0, width, 64), Color.White * alpha);
                }
                else
                {
                    //数字の描画
                    spriteBatch.Draw(textures[assetName], position, new Rectangle((n - '0') * width, 0, width, 64), Color.White * alpha);
                }

                //1文字描画したら1桁分右にずらす
                position.X += width;
            }
        }

        ///<summary>画像の読み込み(画像オブジェクト版)</summary>
        ///<param name="assetName">アセット名</param>
        ///<param name="texture">2D画像オブジェクト</param>
        public void LoadContent(string assetName, Texture2D texture)
        {
            //すでにキー(assetName:アセット名)が登録されているとき
            if (textures.ContainsKey(assetName))
            {
#if DEBUG
                Console.WriteLine(assetName + "はすでに読み込まれています。/nプログラムを確認してください");
#endif
                return;
            }
            textures.Add(assetName, texture);
        }

        ///<summary>画像の描画(拡大縮小回転対応版)</summary>
        ///<param name="assetName">ア セ ッ ト 名 </param> 
        /// <param name="positoin">位 置 </param>
        /// <param name="rect">切 り 出 し 範 囲 </param> 
        /// <param name="rotate">回 転 角 度 </param>
        /// <param name="rotatePosition">回 転 軸 位 置 </param> 
        /// <param name="scale">拡 大 縮 小 </param>
        /// <param name="effects">表 示 反 転 効 果 </param> 
        /// <param name="depth">ス プ ラ イ ト 深 度 </param> 
        /// <param name="alpha">透 明 値 </param> 
        public void DrawTexture(string assetName, Vector2 positoin, Rectangle? rect, float rotate, Vector2 rotatePosition, Vector2 scale, SpriteEffects effects = SpriteEffects.None, float depth = 0.0f, float alpha = 1.0f)
        {
            spriteBatch.Draw(textures[assetName], positoin, rect, Color.White * alpha, rotate, rotatePosition, scale, effects, depth);
        }

        ///<summary>画像の描画</summary>
        ///<param name="assetName">アッセット名</param>
        ///<param name="position">位置</param>
        ///<param name="color">色</param>
        ///<param name="alpha">透明値</param>
        public void DrawTexture(string assetName, Vector2 position, Color color, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(assetName), "描画時にアセット名の指定を間違えたか、画像の読み込み自体で来ていません");
            spriteBatch.Draw(textures[assetName], position, color * alpha);
        }
    }
}
