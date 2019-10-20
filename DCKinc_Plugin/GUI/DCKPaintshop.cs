using DCKinc.customization;
using DCKinc.AAcustomization;
using DCKinc.WCcustomization;
using KSP.UI.Screens;
using System.Collections.Generic;
using UnityEngine;

namespace DCKinc
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    class DCK_Paintshop : MonoBehaviour
    {

        public static DCK_Paintshop Instance = null;
        private ApplicationLauncherButton toolbarButton = null;
        private bool showWindow = false;
        private Rect windowRect;

        void Awake()
        {
        }

        void Start()
        {
            Instance = this;
            windowRect = new Rect(Screen.width - 215, Screen.height - 300, 173, 75);  //default size and coordinates, change as suitable
            AddToolbarButton();
        }

        private void OnDestroy()
        {
            if (toolbarButton)
            {
                ApplicationLauncher.Instance.RemoveModApplication(toolbarButton);
                toolbarButton = null;
            }
        }

        void AddToolbarButton()
        {
            string textureDir = "DCKinc/Plugin/";

            if (toolbarButton == null)
            {
                Texture buttonTexture = GameDatabase.Instance.GetTexture(textureDir + "DCK_armor", false); //texture to use for the button
                toolbarButton = ApplicationLauncher.Instance.AddModApplication(ShowToolbarGUI, HideToolbarGUI, Dummy, Dummy, Dummy, Dummy, ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.VAB, buttonTexture);
            }
        }

        public void ShowToolbarGUI()
        {
            showWindow = true;
        }

        public void HideToolbarGUI()
        {
            showWindow = false;
        }

        void Dummy()
        { }

        void OnGUI()
        {
            if (showWindow)
            {
                windowRect = GUI.Window(this.GetInstanceID(), windowRect, DCKWindow, "DCK Painshop", HighLogic.Skin.window);   //change title as suitable
            }
        }

        void DCKWindow(int windowID)
        {
            if (GUI.Button(new Rect(10, 25, 75, 20), "DCK Prev", HighLogic.Skin.button))    //change rect here for button size, position and text
            {
                SendEventDCK(false);
            }

            if (GUI.Button(new Rect(90, 25, 75, 20), "DCK Next", HighLogic.Skin.button))       //change rect here for button size, position and text
            {
                SendEventDCK(true);
            }

            if (GUI.Button(new Rect(10, 50, 75, 20), "Next Tire", HighLogic.Skin.button))    //change rect here for button size, position and text
            {
                SendEventDCKWC(true);
            }

            if (GUI.Button(new Rect(90, 50, 75, 20), "Armor", HighLogic.Skin.button))       //change rect here for button size, position and text
            {
                SendEventDCKAA(true);
            }
/*
            if (GUI.Button(new Rect(10, 75, 75, 20), "Deploy", HighLogic.Skin.button))       //change rect here for button size, position and text
            {
                shieldToggle();
            }

            if (GUI.Button(new Rect(90, 75, 75, 20), "Shields", HighLogic.Skin.button))    //change rect here for button size, position and text
            {
                SendEventDCKShields(true);
            }
*/

            GUI.DragWindow();
        }
/*
        [KSPField(isPersistant = true, guiActive = true, guiName = "Shields")] public bool shieldsEnabled;

        public void shieldToggle()
        {
            if (!shieldsEnabled)
            {
                EnableShields();
            }
            else
            {
                DisableShields();
            }
        }
*/
        public void sanityCheck()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
            }
        }

/*
        public void EnableShields()
        {
            deployShields();
            shieldsEnabled = true;
        }

        public void DisableShields()
        {
            retractShields();
            shieldsEnabled = false;
        }


        public void deployShields()
        {
            Part root = EditorLogic.RootPart;
            if (!root)
                return;            // find all ModuleDeployableRadiator modules on all parts
            List<ModuleDeployableRadiator> shieldParts = new List<ModuleDeployableRadiator>(200);
            foreach (Part p in EditorLogic.fetch.ship.Parts)
            {
                shieldParts.AddRange(p.FindModulesImplementing<ModuleDeployableRadiator>());
            }
            foreach (ModuleDeployableRadiator shieldPart in shieldParts)
            {
                shieldPart.Extend();
            }
        }
        public void retractShields()
        {
            Part root = EditorLogic.RootPart;
            if (!root)
                return;            // find all ModuleDeployableRadiator modules on all parts
            List<ModuleDeployableRadiator> shieldParts = new List<ModuleDeployableRadiator>(200);
            foreach (Part p in EditorLogic.fetch.ship.Parts)
            {
                shieldParts.AddRange(p.FindModulesImplementing<ModuleDeployableRadiator>());
            }
            foreach (ModuleDeployableRadiator shieldPart in shieldParts)
            {
                shieldPart.Retract();
            }
        }
        
        void SendEventDCKShields(bool next)  //true: next texture, false: previous texture
        {
            Part root = EditorLogic.RootPart;
            if (!root)
                return;            // find all DCKtextureswitch2 modules on all parts
            List<DCKStextureswitch2> shieldParts = new List<DCKStextureswitch2>(200);
            foreach (Part p in EditorLogic.fetch.ship.Parts)
            {
                shieldParts.AddRange(p.FindModulesImplementing<DCKStextureswitch2>());
            }
            foreach (DCKStextureswitch2 shieldPart in shieldParts)
            {
                shieldPart.updateSymmetry = false;             //FIX symmetry problems because DCK also applies its own logic here
                                                            // send previous or next command
                if (next)
                    shieldPart.nextTextureEvent();
                else
                    shieldPart.previousTextureEvent();
            }
        }
*/

        void SendEventDCK(bool next)  //true: next texture, false: previous texture
        {
            Part root = EditorLogic.RootPart;
            if (!root)
                return;            // find all DCKtextureswitch2 modules on all parts
            List<DCKtextureswitch2> dckParts = new List<DCKtextureswitch2>(200);
            foreach (Part p in EditorLogic.fetch.ship.Parts)
            {
                dckParts.AddRange(p.FindModulesImplementing<DCKtextureswitch2>());
            }
            foreach (DCKtextureswitch2 dckPart in dckParts)
            {
                dckPart.updateSymmetry = false;             //FIX symmetry problems because DCK also applies its own logic here
                                                            // send previous or next command
                if (next)
                    dckPart.nextTextureEvent();
                else
                    dckPart.previousTextureEvent();
            }
        }


        void SendEventDCKWC(bool next)  //true: next texture, false: previous texture
        {
            Part root = EditorLogic.RootPart;
            if (!root)
                return;            // find all DCKWCtextureswitch2 modules on all parts
            List<DCKWCtextureswitch2> dckWCParts = new List<DCKWCtextureswitch2>(200);
            foreach (Part p in EditorLogic.fetch.ship.Parts)
            {
                dckWCParts.AddRange(p.FindModulesImplementing<DCKWCtextureswitch2>());
            }
            foreach (DCKWCtextureswitch2 dckWCPart in dckWCParts)
            {
                dckWCPart.updateSymmetry = false;             //FIX symmetry problems because DCK also applies its own logic here
                                                            // send previous or next command
                if (next)
                    dckWCPart.nextTextureEvent();
                else
                    dckWCPart.previousTextureEvent();
            }
        }


        void SendEventDCKAA(bool next)  //true: next texture, false: previous texture
        {
            Part root = EditorLogic.RootPart;
            if (!root)
                return;            // find all DCKAAtextureswitch2 modules on all parts
            List<DCKAAtextureswitch2> dckAAParts = new List<DCKAAtextureswitch2>(200);
            foreach (Part p in EditorLogic.fetch.ship.Parts)
            {
                dckAAParts.AddRange(p.FindModulesImplementing<DCKAAtextureswitch2>());
            }
            foreach (DCKAAtextureswitch2 dckAAPart in dckAAParts)
            {
                dckAAPart.updateSymmetry = false;             //FIX symmetry problems because DCK also applies its own logic here
                                                              // send previous or next command
                if (next)
                    dckAAPart.nextTextureEvent();
                else
                    dckAAPart.previousTextureEvent();
            }
        }
    }
}