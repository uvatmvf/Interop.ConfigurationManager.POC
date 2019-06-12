using System.Configuration;
using System.IO;
using System.Reflection;

namespace AppConfigConfigurationLibrary
{
    public class ConfigurationClass
    {
        private Configuration _injectedBarConfiguration = null;
        private Configuration _injectedFooConfiguration = null;
        public ConfigurationClass(Configuration injectedBarSource, Configuration injectedFooSource)
        {
            _injectedBarConfiguration = injectedBarSource;
            _injectedFooConfiguration = injectedFooSource;
        }

        public ConfigurationClass()
        {
            
        }

        /// <summary>
        /// This sample property contains an intentional duplicate namespace on the class
        /// </summary>
        public string ConfigurationClassFooSetting => _injectedBarConfiguration != null ? 
            _injectedBarConfiguration.Sections["AppConfigConfigurationLibrary.Properties.BarSettings"]
            .CurrentConfiguration.AppSettings.Settings["Bar"].Value : Properties.ConfigurationClass.Default.CustomSetting1;

        /// <summary>
        /// A second property in the settings file.
        /// </summary>
        public string ConfigurationClassFooSetting2 => _injectedFooConfiguration != null ? _injectedFooConfiguration.AppSettings.Settings["CustomSetting1"].Value : Properties.ConfigurationClass.Default.SomeRandomValue;

        /// <summary>
        /// THere is a second configuration file based setting to load
        /// </summary>
        public string ConfigurationClassBarSetting => _injectedFooConfiguration != null ? _injectedFooConfiguration.AppSettings.Settings["SomeRandomValue"].Value : Properties.BarSettings.Default.Bar;
    }

    public class ConfigurationClassService
    {
        /// <summary>
        /// Attempt to refresh settings with configuration and return configuration class object
        /// </summary>
        /// <returns></returns>
        public ConfigurationClass GetConfiguratioClassInstance()
        {
            Configuration ConfigurationFactory(string filePath)
            {
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                configMap.ExeConfigFilename = filePath;
                //return ConfigurationManager.OpenMappedExeConfiguration(fileMap: configMap, userLevel: ConfigurationUserLevel.None);
                return ConfigurationManager.OpenExeConfiguration(filePath);
            }

            //ConfigurationManager.RefreshSection("appSettings");
            //System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var assemblyContext = Assembly.GetExecutingAssembly();
            var configRootPath = Path.GetDirectoryName(assemblyContext.Location);            
            return new ConfigurationClass(ConfigurationFactory($"{Path.Combine(configRootPath, "AppConfigConfigurationLibrary.config")}"),
                                            ConfigurationFactory($@"{Path.Combine(configRootPath, "AppConfigConfigurationLibrary.config")}"));
        }
    }
}
