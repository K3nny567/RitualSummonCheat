using System.Collections.Generic;
using BepInEx.Configuration;

namespace RitualSummonCheat
{
    public static class LocNames
    {
        public static string menu_lang = "en";
        public static readonly string[] lang_array = { "en" };
        public static LocConfigSection[] config_loc_array = {
            new LocConfigSection("BasicStatus", new string[] {""}, new string[] {
                "Infinite Health", "",
                "Health", "",
                "Infinite Mana", "",
                "Mana", "",
                "Infinite Stamina", "",
                "Stamina", "",
            }),
            new LocConfigSection("", new string[] {"",}, new string[] {
                "Restart", "Game restart required",
                "option_SameGrade", "Same grade in item selection",
            }),
        };
        public static Dictionary<string, LocConfigSection> config_loc_dict = new();
        public static void InitializeLocNames()
        {
            int section_order = 0;
            foreach (LocConfigSection loc_section in config_loc_array)
            {
                config_loc_dict[loc_section.name] = loc_section;
                loc_section.order = section_order;
                section_order++;
            }
        }
        public static LocConfigSection GetSection(string section)
        {
            return config_loc_dict[section];
        }
        public static string GetSectionLocName(string section, string lang = "")
        {
            if (config_loc_dict.ContainsKey(section))
            {
                string loc = GetSection(section).Loc_name(lang);
                return "--------------------" +
                    $"{GetSectionOrder(section):D2}.{loc}" +
                    "--------------------";
            }
            return section;
        }
        public static string GetEntryLocName(string section, string key, string lang = "")
        {
            try
            {
                return GetSection(section).entry_dict[key].Loc_name(lang);
            }
            catch
            {
                return key;
            }
        }
        public static string GetLocDesc(string section, string key, string lang = "")
        {
            try
            {
                return GetSection(section).entry_dict[key].Loc_desc(lang);
            }
            catch
            {
                return "";
            }
        }
        public static int GetSectionOrder(string section)
        {
            try
            {
                return GetSection(section).order;
            }
            catch
            {
                return -1;
            }
        }
        public static int GetEntryOrder(string section, string key)
        {
            try
            {
                return GetSection(section).entry_dict[key].order;
            }
            catch
            {
                return -1;
            }
        }
        public static ConfigDescription TryGetEntryDesc(ConfigDefinition con_def)
        {
            try
            {
                if (RSCheat.config.TryGetEntry(con_def, out ConfigEntry<bool> bool_entry))
                {
                    return bool_entry.Description;
                }
            } catch { }
            try
            {
                if (RSCheat.config.TryGetEntry(con_def, out ConfigEntry<int> int_entry))
                {
                    return int_entry.Description;
                }
            } catch { }
            try
            {
                if (RSCheat.config.TryGetEntry(con_def, out ConfigEntry<string> string_entry))
                {
                    return string_entry.Description;
                }
            } catch { }
            try
            {
                if (RSCheat.config.TryGetEntry(con_def, out ConfigEntry<float> float_entry))
                {
                    return float_entry.Description;
                }
            } catch { }
            return null;
        }
        public static void ResetLocName(string lang = null)
        {
            lang ??= menu_lang;
            foreach (ConfigDefinition con_def in RSCheat.config.Keys)
            {
                string section = con_def.Section;
                string key = con_def.Key;
                ConfigDescription desc = TryGetEntryDesc(con_def);
                if (desc != null && desc.Tags != null && desc.Tags.Length > 0)
                {
                    ConfigurationManagerAttributes entry_attr = desc.Tags[0] as ConfigurationManagerAttributes;
                    entry_attr.Category = GetSectionLocName(section, lang);
                    entry_attr.DispName = GetEntryLocName(section, key, lang);
                    entry_attr.Description = GetLocDesc(section, key, lang);
                }
            }
        }
    }
}
