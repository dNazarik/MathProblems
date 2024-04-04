using Gameplay.Configs;
using NUnit.Framework;
using UnityEditor;

namespace Tests.Configs
{
    public class ValidateConfigs
    {
        private GameSettings _gameSettings;
        
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var configPath = AssetDatabase.FindAssets("GameSettings t:ScriptableObject");

            _gameSettings = AssetDatabase.LoadAssetAtPath<GameSettings>(AssetDatabase.GUIDToAssetPath(configPath[0]));
        }
        
        [Test]
        public void ValidateAudioConfigHasNoMissedReferences()
        {
            var configPath = AssetDatabase.FindAssets("AudioConfig t:ScriptableObject");

            var config = AssetDatabase.LoadAssetAtPath<AudioConfig>(AssetDatabase.GUIDToAssetPath(configPath[0]));

            foreach (var audioTrack in config.AudioTracks)
            {
                Assert.NotNull(audioTrack.Clip, $"Please check {audioTrack.Type} reference in {nameof(AudioConfig)}");
            }
        }
        
        [Test]
        public void ValidateGameSettingsConfigHasNoMissedReferences()
        {
            Assert.Greater(_gameSettings.ScoreValues.Length, 0);
            Assert.Greater(_gameSettings.TimerBonuses.Length, 0);
        }
        
        [Test]
        public void ValidateGameSettingsValues()
        {
            Assert.Greater(_gameSettings.DefaultSessionTime, 0);
            Assert.GreaterOrEqual(_gameSettings.AbilityResetCooldown, 1);
        }
    }
}
