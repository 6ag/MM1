using GameFramework;
using GameFramework.DataTable;
using GameFramework.Sound;
using UnityGameFramework.Runtime;

namespace MetalMax
{
    /// <summary>
    /// 游戏声音扩展
    /// </summary>
    public static class SoundExtension
    {
        private const float FadeVolumeDuration = 1f;
        private static int? s_MusicSerialId = null;

        private static int? m_MusicId = null;

        /// <summary>
        /// 检查指定音乐是否正在播放
        /// </summary>
        /// <param name="soundComponent"></param>
        /// <param name="musicId">音乐id</param>
        /// <returns></returns>
        public static bool CheckPlaying(this SoundComponent soundComponent, int musicId)
        {
            return m_MusicId != null && musicId == m_MusicId;
        }

        /// <summary>
        /// 播放音乐，一般是背景音乐
        /// </summary>
        /// <param name="soundComponent">声音组件</param>
        /// <param name="musicId">声音编号</param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static int? PlayMusic(this SoundComponent soundComponent, int musicId, object userData = null)
        {
            soundComponent.StopMusic();

            IDataTable<DRMusic> dtMusic = GameEntry.DataTable.GetDataTable<DRMusic>();
            DRMusic drMusic = dtMusic.GetDataRow(musicId);
            if (drMusic == null)
            {
                Log.Warning("Can not load music '{0}' from data table.", musicId.ToString());
                return null;
            }

            m_MusicId = musicId;

            PlaySoundParams playSoundParams = new PlaySoundParams
            {
                Priority = 64,
                Loop = true,
                VolumeInSoundGroup = 1f,
                FadeInSeconds = FadeVolumeDuration,
                SpatialBlend = 0f,
            };

            s_MusicSerialId = soundComponent.PlaySound(AssetUtility.GetMusicAsset(drMusic.AssetName), "Music", Constant.AssetPriority.MusicAsset, playSoundParams, null, userData);
            return s_MusicSerialId;
        }

        /// <summary>
        /// 停止播放音乐
        /// </summary>
        /// <param name="soundComponent">声音组件</param>
        public static void StopMusic(this SoundComponent soundComponent)
        {
            if (!s_MusicSerialId.HasValue)
            {
                return;
            }

            soundComponent.StopSound(s_MusicSerialId.Value, FadeVolumeDuration);
            s_MusicSerialId = null;
            m_MusicId = null;
        }

        /// <summary>
        /// 播放音效，一般是游戏音效
        /// </summary>
        /// <param name="soundComponent">声音组件</param>
        /// <param name="soundId">音效编号</param>
        /// <param name="bindingEntity">绑定实体</param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static int? PlaySound(this SoundComponent soundComponent, int soundId, Entity bindingEntity = null, object userData = null)
        {
            IDataTable<DRSound> dtSound = GameEntry.DataTable.GetDataTable<DRSound>();
            DRSound drSound = dtSound.GetDataRow(soundId);
            if (drSound == null)
            {
                Log.Warning("Can not load sound '{0}' from data table.", soundId.ToString());
                return null;
            }

            PlaySoundParams playSoundParams = new PlaySoundParams
            {
                Priority = drSound.Priority,
                Loop = drSound.Loop,
                VolumeInSoundGroup = drSound.Volume,
                SpatialBlend = drSound.SpatialBlend,
            };

            return soundComponent.PlaySound(AssetUtility.GetSoundAsset(drSound.AssetName), "Sound", Constant.AssetPriority.SoundAsset, playSoundParams, bindingEntity != null ? bindingEntity.Entity : null, userData);
        }

        /// <summary>
        /// 播放UI音效
        /// </summary>
        /// <param name="soundComponent">声音组件</param>
        /// <param name="uiSoundId">UI音效编号</param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static int? PlayUISound(this SoundComponent soundComponent, int uiSoundId, object userData = null)
        {
            IDataTable<DRUISound> dtUISound = GameEntry.DataTable.GetDataTable<DRUISound>();
            DRUISound drUISound = dtUISound.GetDataRow(uiSoundId);
            if (drUISound == null)
            {
                Log.Warning("Can not load UI sound '{0}' from data table.", uiSoundId.ToString());
                return null;
            }

            PlaySoundParams playSoundParams = new PlaySoundParams
            {
                Priority = drUISound.Priority,
                Loop = false,
                VolumeInSoundGroup = drUISound.Volume,
                SpatialBlend = 0f,
            };

            return soundComponent.PlaySound(AssetUtility.GetUISoundAsset(drUISound.AssetName), "UISound", Constant.AssetPriority.UISoundAsset, playSoundParams, userData);
        }

        /// <summary>
        /// 是否静音
        /// </summary>
        /// <param name="soundComponent">声音组件</param>
        /// <param name="soundGroupName">声音组名称</param>
        /// <returns></returns>
        public static bool IsMuted(this SoundComponent soundComponent, string soundGroupName)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return true;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return true;
            }

            return soundGroup.Mute;
        }

        /// <summary>
        /// 切换指定组声音静音状态
        /// </summary>
        /// <param name="soundComponent">声音组件</param>
        /// <param name="soundGroupName">声音组名称</param>
        /// <param name="mute"></param>
        public static void Mute(this SoundComponent soundComponent, string soundGroupName, bool mute)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return;
            }

            soundGroup.Mute = mute;

            GameEntry.Setting.SetBool(string.Format(Constant.Setting.SoundGroupMuted, soundGroupName), mute);
            GameEntry.Setting.Save();
        }

        /// <summary>
        /// 获取指定声音组音量
        /// </summary>
        /// <param name="soundComponent"></param>
        /// <param name="soundGroupName"></param>
        /// <returns></returns>
        public static float GetVolume(this SoundComponent soundComponent, string soundGroupName)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return 0f;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return 0f;
            }

            return soundGroup.Volume;
        }

        /// <summary>
        /// 设置指定组声音音量
        /// </summary>
        /// <param name="soundComponent"></param>
        /// <param name="soundGroupName"></param>
        /// <param name="volume"></param>
        public static void SetVolume(this SoundComponent soundComponent, string soundGroupName, float volume)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return;
            }

            soundGroup.Volume = volume;

            GameEntry.Setting.SetFloat(string.Format(Constant.Setting.SoundGroupVolume, soundGroupName), volume);
            GameEntry.Setting.Save();
        }
    }
}
