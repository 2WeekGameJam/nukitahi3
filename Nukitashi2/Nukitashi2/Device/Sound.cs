using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace Nukitashi2.Device
{
    class Sound
    {
        #region

        //コンテンツ管理
        private ContentManager contentManager;
        //MP3管理用
        private Dictionary<string, Song> bgms;
        //WAV管理用
        private Dictionary<string, SoundEffect> soundEffects;
        //WAVインスタンス管理用
        private Dictionary<string, SoundEffectInstance> seInstances;
        //WAVインスタンスの再生管理用ディクショナリ
        private Dictionary<string, SoundEffectInstance> sePlayDict;
        //現在再生中のMP3のアセット名
        private string currentBGM;

        ///<summary>コンストラクタ</summary>
        ///<param name="content">Game1のコンテンツ管理者</param>
        public Sound(ContentManager content)
        {
            //Game1クラスのコンテンツ管理者と紐づけ
            contentManager = content;
            //BGMは繰り返し再生
            MediaPlayer.IsRepeating = true;

            //各Dictionaryの実体生成
            bgms = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
            seInstances = new Dictionary<string, SoundEffectInstance>();
            //再生Listの実体生成
            sePlayDict = new Dictionary<string, SoundEffectInstance>();
            //何もしていないのでnullで初期化
            currentBGM = null;
        }

        ///<summary>開放</summary>
        public void Unload()
        {
            //ディクショナリ
            bgms.Clear();
            soundEffects.Clear();
            seInstances.Clear();
            sePlayDict.Clear();
        }

        #endregion

        ///<summary>Assert用エラーメッセージ</summary>
        ///<param name="name">使えないであろうメソット名</param>
        ///<returns></returns>
        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名(" + name + ")がありません" +
                "アセット名の確認、Dictionaryに登録しているか確認してください";
        }

        #region BGM(MP3:MediaPlayer)関連

        ///<summary>BGM(MP3)読み込み</summary>
        ///<Param name="name">アセット名</Param>
        ///<param name="filepath">ファイルパス</param>
        public void LoadBGM(string name, string filepath = "./")
        {
            //すでに登録されているか
            if (bgms.ContainsKey(name))
            {
                return;
            }
            //MP3の読み込みとDictionaryへ登録
            bgms.Add(name, contentManager.Load<Song>(filepath + name));
        }

        ///<summary>BGMが停止中か？</summary>
        ///<return>停止中ならtrue</return>
        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        ///<summary>BGMが再生中か？</summary>
        ///<return>再生中ならtrue</return>
        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        ///<summary>BGMが一時停止中か？</summary>
        ///<return>一時停止中ならtrue</return>
        public bool IsPauseBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        ///<summary>BGMを停止</summary>
        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        ///<summary>BGM再生</summary>
        ///<param name="name"></param>
        public void PlayBGM(string name)
        {
            //アセット名がディクショナリに登録されているか？
            Debug.Assert(bgms.ContainsKey(name), ErrorMessage(name));

            //同じ曲か？
            if (currentBGM == name)
            {
                //再生中なら、停止処理
                return;
            }
            //BGMは再生中か
            if (IsPauseBGM())
            {
                StopBGM();
            }
            //ボリューム設定(BGMに比べて音量半分が普通)
            MediaPlayer.Volume = 0.5f;
            currentBGM = name;
            MediaPlayer.Play(bgms[currentBGM]);
        }

        ///<summary>BGMの一時停止</summary>
        public void PauseBGM()
        {
            if (IsPlayingBGM())
            {
                MediaPlayer.Pause();
            }
        }

        ///<summary>一時停止からの再生</summary>
        public void ResumeBGM()
        {
            if (IsPauseBGM())
            {
                MediaPlayer.Resume();
            }
        }

        ///<summary>BGMループフラグを更新</summary>
        ///<param name="loopFlag"></param>
        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }
        #endregion BGM(MP3:MediaPlayer関連  

        #region WAV(SE:SoundEffect)関連

        public void LoadSE(string name, string filepath = "./")
        {
            if (soundEffects.ContainsKey(name))
            {
                return;
            }
            soundEffects.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        public void PlaySE(string name)
        {
            Debug.Assert(soundEffects.ContainsKey(name), ErrorMessage(name));
            soundEffects[name].Play();
        }
        #endregion

        #region

        ///<summary>WAVインスタンスの作成</summary>
        ///<param name="name">アセット米</param>
        public void CreateSEInstance(string name)
        {
            if (seInstances.ContainsKey(name))
            {
                return;
            }

            Debug.Assert(soundEffects.ContainsKey(name), "先に" + name + "の読み込み処理を行ってください");
            seInstances.Add(name, soundEffects[name].CreateInstance());
        }

        ///<summary></summary>
        ///<param name="name">アセット名</param>
        ///<param name="no">管理番号</param>
        ///<param name="loopFlag"></param>
        public void PlaySEInstance(string name, int no, bool loopFlag = false)
        {
            Debug.Assert(seInstances.ContainsKey(name), ErrorMessage(name));

            if (sePlayDict.ContainsKey(name + no))
            {
                return;
            }
            var data = seInstances[name];
            data.IsLooped = loopFlag;
            data.Play();
            sePlayDict.Add(name + no, data);
        }
        ///<summary>指定SEの停止</summary>
        ///<param name="name"></param>
        ///<param name="no"></param>
        public void StopedSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            if (sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Stop();
            }
        }

        ///<summary>再生中のSEすべて停止</summary>
        public void StoppedSE()
        {
            foreach (var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Playing)
                {
                    se.Value.Stop();
                }
            }
        }

        ///<summary>指定したSEを削除</summary>
        ///<param name="name">アセット名</param>
        ///<param name="no">管理番号</param>
        public void RemoveSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            sePlayDict.Remove(name + no);
        }

        ///<summary>全てのSE削除</summary>
        public void RemoveSE()
        {
            sePlayDict.Clear();
        }

        ///<summary>指定したSEを一時停止</summary>
        ///<param name="name">アセット名</param>
        ///<param name="no">管理番号</param>
        public void PauseSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }
            if (sePlayDict[name + no].State == SoundState.Playing)
            {
                sePlayDict[name + no].Pause();
            }
        }

        ///<summary>すべてのSEを一時停止</summary>
        public void PauseSE()
        {
            foreach (var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Playing)
                {
                    se.Value.Pause();
                }
            }
        }

        ///<summary>指定した一時停止から復帰</summary>
        ///<param name="name">アセット名</param>
        ///<param name="no">管理番号</param>
        public void ResumSE(string name, int no)
        {
            if (sePlayDict.ContainsKey(name + no) == false)
            {
                return;
            }

            if (sePlayDict[name + no].State == SoundState.Paused)
            {
                sePlayDict[name + no].Resume();
            }
        }

        ///<summary>一時停止中のすべてのSEを復帰</summary>
        public void ResumSE()
        {
            foreach (var se in sePlayDict)
            {
                if (se.Value.State == SoundState.Paused)
                {
                    se.Value.Resume();
                }
            }
        }

        ///<summary>SEインスタンスが再生中か</summary>
        ///<param name="name">アセット名</param>
        ///<param name="no">管理番号</param>
        ///<returns>再生中ならtrue</returns>
        public bool IsPlayingSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Playing;
        }

        ///<summary>SEインスタンスが停止中か</summary>
        ///<param name="name">アセット名</param>
        ///<param name="no">管理番号</param>
        ///<returns>停止中ならtrue</returns>
        public bool IsStoppedSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Stopped;
        }
        ///<summary>SEインスタンスが一時停止中か？</summary>
        ///<param name="name"></param>
        ///<param name="no"></param>
        ///<returns></returns>
        public bool IsPauseSEInstance(string name, int no)
        {
            return sePlayDict[name + no].State == SoundState.Paused;
        }
        #endregion
    }
}
