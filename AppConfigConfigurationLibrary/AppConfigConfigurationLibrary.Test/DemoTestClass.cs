using NUnit.Framework;

namespace AppConfigConfigurationLibrary.Test
{
    [TestFixture]
    public class DemoTestClass
    {
        [Test]
        public void ReadSettingsFromConfigFileOnDisk_Test()
        {
            // arrange
            var configReader = new ConfigurationClassService();

            // act
            var test = configReader.GetConfiguratioClassInstance();

            // assert
            Assert.IsTrue(test.ConfigurationClassBarSetting == "abcde");
            Assert.IsTrue(test.ConfigurationClassFooSetting == "1x34");
            Assert.IsTrue(test.ConfigurationClassFooSetting2 == "Did you forget task?");
        }

    }
}
