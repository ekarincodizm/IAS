using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAS.Mockup
{
    // create lists 
    public class RegList
    {
        public Register Reg1 { get; set; }
        public Register Reg2 { get; set; }
        public Register Reg3 { get; set; }
        public Register Reg4 { get; set; }
        public Register Reg5 { get; set; }
        public Register Reg6 { get; set; }
        public Register Reg7 { get; set; }

        
        public List<RegList> GetList()
        {
            List<RegList> list = new List<RegList>();


            for (int i = 0; i < 5; i++)
            {
                RegList regLs = new RegList();


                for (int j = 1; j <= 7; j++)
                {
                    Register reg = new Register();
                    if (j == 1)
                    {
                        reg.date = j;
                        reg.registerCode = i.ToString();
                        regLs.Reg1 = reg;
                    }
                    if (j == 2)
                    {
                        reg.date = j;
                        reg.registerCode = i.ToString();
                        regLs.Reg2 = reg;
                    }
                    if (j == 3)
                    {
                        reg.date = j;
                        reg.registerCode = i.ToString();
                        regLs.Reg3 = reg;

                    }
                    if (j == 4)
                    {
                        reg.date = j;
                        reg.registerCode = i.ToString();
                        regLs.Reg4 = reg;
                    }
                    if (j == 5)
                    {
                        reg.date = j;
                        reg.registerCode = i.ToString();
                        regLs.Reg5 = reg;
                    }
                    if (j == 6)
                    {
                        reg.date = j;
                        reg.registerCode = i.ToString();
                        regLs.Reg6 = reg;
                    }
                    if (j == 7)
                    {
                        reg.date = j;
                        reg.registerCode = i.ToString();
                        regLs.Reg7 = reg;
                    }
                }

                list.Add(regLs);
            }
            return list;
        }

    }

    // create property Detail
    public partial class Register
    {
        public int date { get; set; }
        public string registerCode { get; set; }
        //public bool Test { get { return true; } set; }

    }
   

}