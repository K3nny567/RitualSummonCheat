using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using System.IO;

namespace RitualSummonCheat
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("RitualSummon.exe")]
    public class RSCheat : BaseUnityPlugin
    {
        internal static ManualLogSource cheat_logger;
        public static ConfigFile config;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
        private void Awake()
        {
            cheat_logger = Logger;
            config = Config;
            string config_file_path = config.ConfigFilePath;
            if (File.Exists(config_file_path))
            {
                config.Reload();
            }
            LocNames.InitializeLocNames();

            // Plugin startup logic
            _ = new GalleryUnlocker();
            _ = new LockStatus();

            // Write plugin load to log
            cheat_logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
