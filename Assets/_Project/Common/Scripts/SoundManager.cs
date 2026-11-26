using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace VirusRescue
{
    public class SoundManager : SingletonMonoBehaviour<SoundManager>
    {
        [SerializeField] AudioSource audioSource;
            [SerializeField] List<AudioClip> bgmClips; // Bgmとして扱う音声ファイルをリストで保持
            [SerializeField] List<AudioClip> seClips; // Seとして扱う音声ファイルをリストで保持
        void Start()
        {

        }
            // 引数では再生したいBgmの名前を渡すことで外部から再生指示を行う
        public void PlayBgm(string bgmName)
        {
            for(int i = 0; i < bgmClips.Count; i++)
            {
                if (bgmClips[i] == null) return;
                if (bgmName == bgmClips[i].name)
                {
                    audioSource.PlayOneShot(bgmClips[i]);
                    break;
                }
            }
        }
        public void PlaySE(string seName)
        {
            for (int i = 0; i < seClips.Count; i++)
            {
                if (seClips[i] == null) return;
                if (seName == seClips[i].name)
                {
                    audioSource.PlayOneShot(seClips[i]);
                    break;
                }
            }
        }
    }
}