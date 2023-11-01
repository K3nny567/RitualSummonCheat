using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace RitualSummonCheat
{
    public class LockStatus : BasePatchClass
    {
        private static ConfigEntry<bool> infiniteHealth;
        private static ConfigEntry<int> health;
        private static ConfigEntry<bool> infiniteMana;
        private static ConfigEntry<int> mana;
        private static ConfigEntry<bool> infiniteStamina;
        private static ConfigEntry<int> stamina;

        public LockStatus()
        {
            string section = "BasicStatus";
            infiniteHealth = TrackBindConfig(section, "Infinite Health", false);
            health = TrackBindConfig(section, "Health", 100, new AcceptableValueRange<int>(0, 100), true);
            infiniteMana = TrackBindConfig(section, "Infinite Mana", false);
            mana = TrackBindConfig(section, "Mana", 100, new AcceptableValueRange<int>(0, 100), true);
            infiniteStamina = TrackBindConfig(section, "Infinite Stamina", false);
            stamina = TrackBindConfig(section, "Stamina", 100, new AcceptableValueRange<int>(0, 100), true);
            TryPatch(GetType());
        }
        [HarmonyPostfix, HarmonyPatch(typeof(PlayerController), "Update")]
        private static void PatchContent()
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Managers.Datamanager.AddProp("Soul", 1000);
            }
            PlayerController player = Managers.Datamanager.GetPlayer();
            if (infiniteHealth.Value)
            {
                int max_hp = player.States.MaxLife;
                float set_hp = health.Value * max_hp / 100;
                player.States.Life = set_hp;
            }
            if (infiniteMana.Value)
            {
                int max_mp = player.States.MaxMP;
                float set_mp = mana.Value * max_mp / 100;
                player.States.MP = set_mp;
            }
            if (infiniteStamina.Value)
            {
                int max_st = player.States.MaxEnergy;
                float set_st = stamina.Value * max_st / 100;
                player.States.Energy = set_st;
            }
        }
    }
}
