using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class StatesRepo : IManageStates
    {

        public List<StateInfo> GetStates()
        {
            List<StateInfo> loadStateInfo = new List<StateInfo>();

            // Reads in information from Taxes.txt file, omitting the comments that start with #
            using (StreamReader sr = new StreamReader("Taxes.txt"))
            {
                
                bool foundHeader = false;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.Contains("#"))
                        continue;

                    if (!foundHeader)
                    {
                        foundHeader = true;
                        continue;
                    }

                    // Parse the information
                    string[] parts = line.Split('|');
                    StateInfo state = new StateInfo();
                    state.StateAbbreviation = parts[0];
                    state.StateName = parts[1];
                    state.TaxRate = decimal.Parse(parts[2]);

                    loadStateInfo.Add(state);
                    
                }
                return loadStateInfo;
                
            }
        }
    }
}
