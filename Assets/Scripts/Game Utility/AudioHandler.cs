using UnityEngine;

namespace Game_Utility
{
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource sfxPlayer;
        [SerializeField] private AudioSource musicPlayer;
        [SerializeField] private AudioClip logCut;
        [SerializeField] private AudioClip levelUp;
        [SerializeField] private AudioClip levelDown;
        [SerializeField] private AudioClip gameOver;

        private void Start()
        {
            GameEvents.OnCutTheLog += PlayCutAudio;
            GameEvents.OnIncreaseDifficulty += PlayLevelUpAudio;
            GameEvents.OnDecreaseDifficulty += PlayLevelDownAudio;
            GameEvents.OnGameOver += PlayGameOverAudio;
        }

        private void OnDestroy()
        {
            GameEvents.OnCutTheLog -= PlayCutAudio;
            GameEvents.OnIncreaseDifficulty -= PlayLevelUpAudio;
            GameEvents.OnDecreaseDifficulty -= PlayLevelDownAudio;
            GameEvents.OnGameOver -= PlayGameOverAudio;
        }

        private void PlayCutAudio()
        {
            sfxPlayer.PlayOneShot(logCut);
        }
    
        private void PlayLevelUpAudio()
        {
            sfxPlayer.PlayOneShot(levelUp);
        }
    
        private void PlayLevelDownAudio()
        {
            sfxPlayer.PlayOneShot(levelDown);
        }
    
        private void PlayGameOverAudio()
        {
            Destroy(musicPlayer);
            sfxPlayer.PlayOneShot(gameOver);
        }
    
        public void SetMusicActive(bool shouldActive)
        {
            musicPlayer.gameObject.SetActive(shouldActive);
        }

        public void SetSoundActive(bool shouldActive)
        {
            sfxPlayer.gameObject.SetActive(shouldActive);
        }

        public bool IsMusicOn()
        {
            return musicPlayer.gameObject.activeSelf;
        }

        public bool IsSoundOn()
        {
            return sfxPlayer.gameObject.activeSelf;
        }
    }
}
