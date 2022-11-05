using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModuleManager;
using KSP.Localization;
using UnityEngine;

namespace GoobaMod
{
    public class ModuleNightHibernation : PartModule
    {
        [KSPField(isPersistant = true)]
        private string directLOC = Localizer.Format("#autoLOC_235418");

        [KSPField(isPersistant = false, guiActive = true, guiActiveEditor = true, guiName = "Hibernate at Night")]
        public bool nightHib = false;

        [KSPEvent(active = true, guiActive = true, guiActiveEditor = false, guiActiveUnfocused = false, guiName = "Toggle Night Hiberation")]
        public void nightHibernate()
        {
            if (nightHib)
            {
                nightHib = false;
            }
            else
            {
                nightHib = true;
            }
        }

        

        public void Update()
        {
            Vessel ves = FlightGlobals.ActiveVessel;
            
            if (ves.rootPart.TryGetComponent<ModuleCommand>(out ModuleCommand probe) && nightHib)
            {
                foreach (Part p in ves.Parts)
                {
                    if (p.TryGetComponent(out ModuleDeployableSolarPanel solarPanel))
                    {
                        if (solarPanel.status == directLOC)
                        {
                            probe.hibernation = false;
                            return;
                        }
                        else
                        {
                            probe.hibernation = true;
                            return;
                        }
                    }
                }
            }
            
        }
    }
}
