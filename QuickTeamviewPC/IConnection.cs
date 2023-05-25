using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTeamviewPC
{
    public interface IConnection
    {
        string ID_Ultra { get; set; }
        string Password_Ultra { get; set; }
        string ID_Teamview { get; set; }
        string Pass_Teamview { get; set; }
        string NamePC { get; set; }
        string RDP_IP { get; set; }
        string RDP_Username { get; set; }
        string RDP_Pass { get; set; }
        string StringSaveToFile();
    }
}
