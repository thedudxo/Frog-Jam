namespace Frogs.Instances.Jumps
{
    public class JumpAudio
    {
        AudioClip landSounds;
        AudioClip jumpSounds;

        public JumpAudio(AudioClip landSounds, AudioClip jumpSounds)
        {
            this.landSounds = landSounds;
            this.jumpSounds = jumpSounds;
        }

        void PlayRandom(AudioClip clip)
        {
            clip.GetRandomAudioSource().Play();
        }

        public void PlayJump() => PlayRandom(jumpSounds);
        public void PlayLand() => PlayRandom(landSounds);
    }
}
