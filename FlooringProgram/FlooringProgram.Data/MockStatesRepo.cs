using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class MockStatesRepo : IManageStates
    {
        
        // Hard-coded data for test mode.
        public List<StateInfo> GetStates()
        {
            StateInfo minnesota = new StateInfo();
            minnesota.StateAbbreviation = "MN";
            minnesota.StateName = "Minnesota";
            minnesota.TaxRate = 8;

            StateInfo iowa = new StateInfo();
            iowa.StateAbbreviation = "IA";
            iowa.StateName = "Iowa";
            iowa.TaxRate = 7;

            StateInfo illinois = new StateInfo();
            illinois.StateAbbreviation = "IL";
            illinois.StateName = "Illinois";
            illinois.TaxRate = (decimal)7.5;

            
            var StateList = new List<StateInfo>();
            StateList.Add(minnesota);
            StateList.Add(iowa);
            StateList.Add(illinois);
            
            return StateList;
        }
    }
}

