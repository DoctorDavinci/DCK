namespace DCKinc
{
    public class ModuleDCKAG : PartModule
    {
        [KSPField]
        public string moduleSource;

        [KSPField]
        public string actionGuiName;

        [KSPField]
        public string activateGuiName;

        [KSPField]
        public string deactivateGuiName;

        [KSPField]
        public KSPActionGroup defaultActionGroup;

        public bool IsToggleMode
        {
            get
            {
                return !string.IsNullOrEmpty(actionGuiName);
            }
        }
    }
}
