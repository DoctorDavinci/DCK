using System;
using UnityEngine;

namespace DCKinc
{
    [KSPAddon(KSPAddon.Startup.EditorAny, false)]
    class DCKAGSettings : MonoBehaviour
    {
        public void Awake()
        {
            GameEvents.onEditorPartEvent.Add(OnPartEvent);
        }

        public void OnDestroy()
        {
            GameEvents.onEditorPartEvent.Remove(OnPartEvent);
        }

        private void OnPartEvent(ConstructionEventType eventType, Part part)
        {
            try
            {
                switch (eventType)
                {
                    case ConstructionEventType.PartCreated:
                        handlePart(part);
                        break;
                    case ConstructionEventType.PartAttached:
                        onPartAttached(part);
                        foreach (Part counterpart in part.symmetryCounterparts)
                        {
                            onPartAttached(counterpart);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
            }
        }

        private static void onPartAttached(Part part)
        {
            handlePart(part);
            foreach (Part child in part.children)
            {
                onPartAttached(child);
            }
        }

        private static void handlePart(Part part)
        {
            foreach (ModuleDCKAG module in part.Modules.GetModules<ModuleDCKAG>())
            {
                handleDefaultGroupsForPart(part, module);
            }
        }

        private static void handleDefaultGroupsForPart(Part part, ModuleDCKAG defaultActionGroupModule)
        {
            foreach (PartModule module in part.Modules)
            {
                if (module.moduleName == defaultActionGroupModule.moduleSource)
                {
                    handleDefaultGroupsForPartModule(part, module, defaultActionGroupModule);
                }
            }
        }

        private static void handleDefaultGroupsForPartModule(
            Part part,
            PartModule module,
            ModuleDCKAG defaultActionGroupModule)
        {
            ModuleAnimateGeneric animationModule = module as ModuleAnimateGeneric;
            if (animationModule != null)
            {
                handleDefaultGroupsForAnimationModule(part, animationModule, defaultActionGroupModule);
                return;
            }

            foreach (BaseAction action in module.Actions)
            {
                if (action.guiName == defaultActionGroupModule.actionGuiName)
                {
                    action.actionGroup |= defaultActionGroupModule.defaultActionGroup;
                    action.defaultActionGroup |= defaultActionGroupModule.defaultActionGroup;
                }
            }
        }

        private static void handleDefaultGroupsForAnimationModule(
            Part part,
            ModuleAnimateGeneric animationModule,
            ModuleDCKAG defaultActionGroupModule)
        {
            if (animationModule.actionGUIName == defaultActionGroupModule.actionGuiName)
            {
                foreach (BaseAction action in animationModule.Actions)
                {
                    action.actionGroup |= defaultActionGroupModule.defaultActionGroup;
                    action.defaultActionGroup |= defaultActionGroupModule.defaultActionGroup;
                }
            }
        }
    }
}